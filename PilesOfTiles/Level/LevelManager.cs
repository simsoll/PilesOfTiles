using System;
using System.Linq;
using Caliburn.Micro;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using PilesOfTiles.Collision.Messages;
using PilesOfTiles.Input.Messages;
using PilesOfTiles.Level.Messages;
using Action = PilesOfTiles.Input.Messages.Action;

namespace PilesOfTiles.Level
{
    public class LevelManager : IHandle<BrickCollided>
    {
        private IEventAggregator _eventAggregator;
        private TimeSpan _moveDownThreshold;
        private TimeSpan _moveDownThresholdDelta;
        private TimeSpan _timeSinceDownMovement;
        private int _rowsClearedSinceDifficultyIncrease;
        private int _difficultyIncreaseThreshold;
        private int _difficultyLevel;

        public LevelManager(IEventAggregator eventAggregator, Vector2 position, int width, int height, int tileSize)
        {
            _eventAggregator = eventAggregator;
            _eventAggregator.Subscribe(this);

            InitializeLevel(position, width, height, tileSize, Color.Gray);
            _timeSinceDownMovement = TimeSpan.Zero;
            _moveDownThreshold = TimeSpan.FromMilliseconds(500);
            _moveDownThresholdDelta = TimeSpan.FromMilliseconds(50);
            _rowsClearedSinceDifficultyIncrease = 0;
            _difficultyIncreaseThreshold = 5;
            _difficultyLevel = 1;
        }

        public Level Level { get; private set; }

        public void InitializeLevel(Vector2 position, int height, int width, int tileSize, Color wallColor)
        {
            Level = new Level(position, height, width, tileSize, wallColor);
            _eventAggregator.PublishOnUIThread(new LevelCreated
            {
                Position = Level.Position,
                Tiles = Level.Tiles
            });
        }

        public void Update(GameTime gameTime)
        {
            _timeSinceDownMovement += gameTime.ElapsedGameTime;

            if (_moveDownThreshold < _timeSinceDownMovement)
            {
                _eventAggregator.PublishOnUIThread(new ActionRequested{ Action = Action.MoveDown});
                _timeSinceDownMovement = TimeSpan.Zero;
            }
        }

        public void Draw(SpriteBatch spriteBatch, Texture2D texture)
        {
            Level.Draw(spriteBatch, texture);
        }

        public void Handle(BrickCollided message)
        {
            Level.AddTiles(message.Tiles);
            CheckForFullRows();
        }

        public void CheckForFullRows()
        {
            var bottom = (int)Level.Tiles.Where(tile => tile.State == State.Removable).Select(tile => tile.Position.Y).Max();

            for (var i = bottom; 0 <= i; i--)
            {
                var tiles = Level.Tiles.Where(tile => tile.Position.Y == i && tile.State == State.Removable).ToList();
                var numberOfSolidTiles = Level.Tiles.Count(tile => tile.Position.Y == i && tile.State == State.Solid);

                if (tiles.Count() == Level.Width - numberOfSolidTiles)
                {
                    foreach (var tile in tiles)
                    {
                        Level.RemoveTile(tile);
                    }

                    MovesTilesDownAboveRow(i);
                    _eventAggregator.PublishOnUIThread(new RowCleared
                    {
                        Tiles = Level.Tiles
                    });
                    _rowsClearedSinceDifficultyIncrease++;
                    CheckForDifficultyIncrease();

                    CheckForFullRows();
                    return;
                }
            }
        }

        private void CheckForDifficultyIncrease()
        {
            if (_rowsClearedSinceDifficultyIncrease < _difficultyIncreaseThreshold) return;

            _difficultyLevel++;
            _rowsClearedSinceDifficultyIncrease = 0;
            _moveDownThreshold -= _moveDownThresholdDelta;
            _eventAggregator.PublishOnUIThread(new DifficultyLevelChanged
            {
                Value = _difficultyLevel
            });

            if (_moveDownThreshold == TimeSpan.Zero)
            {
                _eventAggregator.PublishOnUIThread(new GameCompleted());
            }
        }

        private void MovesTilesDownAboveRow(int i)
        {
            var resultList = Level.Tiles.Where(tile => tile.State == State.Solid).ToList();

            resultList.AddRange(Level.Tiles.Where(tile => tile.Position.Y > i && tile.State != State.Solid));

            resultList.AddRange(
                Level.Tiles.Where(tile => tile.Position.Y < i && tile.State != State.Solid)
                    .Select(tile => Tile.Create(tile.Position + new Vector2(0, 1), tile.Color, State.Removable)));

            Level.ResetTiles(resultList);
        }
    }
}
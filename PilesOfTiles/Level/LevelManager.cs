using System;
using System.Linq;
using System.Runtime.InteropServices;
using Caliburn.Micro;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using PilesOfTiles.Collision.Messages;
using PilesOfTiles.HighScore.Messages;
using PilesOfTiles.Input.Messages;
using PilesOfTiles.Level.Messages;
using PilesOfTiles.Manager;
using Action = PilesOfTiles.Input.Messages.Action;

namespace PilesOfTiles.Level
{
    public class LevelManager : IManager, IHandle<BrickCollided>, IHandle<GameEnded>
    {
        private IEventAggregator _eventAggregator;
        private TimeSpan _moveDownThreshold;
        private TimeSpan _moveDownThresholdDelta;
        private TimeSpan _timeSinceDownMovement;
        private int _rowsClearedSinceDifficultyIncrease;
        private int _difficultyIncreaseThreshold;
        private int _difficultyLevel;
        private Texture2D _tileTexture;
        
        private Vector2 _position;
        private int _width;
        private int _height;
        private int _tileSize;
        private Color _wallColor;

        public LevelManager(IEventAggregator eventAggregator, Vector2 position, int height, int width, Texture2D tileTexture, int tileSize)
        {
            _eventAggregator = eventAggregator;

            _tileTexture = tileTexture;
            _timeSinceDownMovement = TimeSpan.Zero;
            _moveDownThreshold = TimeSpan.FromMilliseconds(500);
            _moveDownThresholdDelta = TimeSpan.FromMilliseconds(50);
            _rowsClearedSinceDifficultyIncrease = 0;
            _difficultyIncreaseThreshold = 5;
            _difficultyLevel = 1;

            _position = position;
            _width = width;
            _height = height;
            _tileSize = tileSize;
            _wallColor = Color.Gray;

            InitializeLevel();
        }

        public Level Level { get; private set; }

        public void InitializeLevel()
        {
            Level = new Level(_position, _height, _width, _tileSize, _wallColor);
        }

        public void Load()
        {
            _eventAggregator.Subscribe(this);
            _eventAggregator.PublishOnUIThread(new LevelLoaded
            {
                Position = Level.Position,
                Tiles = Level.Tiles.Select(tile => Tile.Create(tile.Position, tile.Color, tile.State))
            });
        }

        public void Unload()
        {
            _eventAggregator.Unsubscribe(this);
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

        public void Draw(SpriteBatch spriteBatch)
        {
            Level.Draw(spriteBatch, _tileTexture);
        }

        public void Handle(BrickCollided message)
        {
            Level.AddTiles(message.Tiles.Select(tile => Tile.Create(tile.Position, tile.Color, tile.State)));
            _eventAggregator.PublishOnUIThread(new LevelUpdated
            {
                Tiles = Level.Tiles.Select(tile => Tile.Create(tile.Position, tile.Color, tile.State))
            });
            CheckForFullRows();
        }

        public void Handle(GameEnded message)
        {
            InitializeLevel();
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
                        Tiles = Level.Tiles.Select(tile => Tile.Create(tile.Position, tile.Color, tile.State)),
                        Row = i
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
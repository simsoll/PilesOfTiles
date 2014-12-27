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
        private TimeSpan _timeSinceDownMovement;

        public LevelManager(IEventAggregator eventAggregator, TimeSpan moveDownThreshold)
        {
            _eventAggregator = eventAggregator;
            _eventAggregator.Subscribe(this);

            _moveDownThreshold = moveDownThreshold;
            InitializeLevel(new Vector2(5, 5), 30, 20, 8, Color.Gray);
            _timeSinceDownMovement = TimeSpan.Zero;
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
            CheckFullRows();
        }

        public void CheckFullRows()
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
                    CheckFullRows();
                    return;
                }
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
using System;
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
            InitializeLevel(new Vector2(5, 5), 50, 35, 8, Color.Gray);
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
            Level.Tiles.AddRange(message.Tiles);
        }
    }
}
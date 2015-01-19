using System;
using Microsoft.Xna.Framework;
using PilesOfTiles.Randomizers;

namespace PilesOfTiles.Tiles
{
    public class ShakyTile : ITile
    {
        private ITile _tile;
        private IRandomizer _randomizer;

        private Vector2 _targetPosition;
        private Vector2 _sourcePosition;
        private TimeSpan _duration;
        private TimeSpan _lifeTime;

        public Vector2 Position { get; set; }
        public Color Color { get { return _tile.Color; } }
        public State State { get { return _tile.State; } }

        public ShakyTile(ITile tile, IRandomizer randomizer)
        {
            _tile = tile;
            _randomizer = randomizer;

            _targetPosition = tile.Position;
            _duration = TimeSpan.FromMilliseconds(400 + _randomizer.NextDouble() * 200 - 10);
            _lifeTime = TimeSpan.Zero;

            Position = _targetPosition;
        }

        public void Update(GameTime gameTime)
        {
            _lifeTime += gameTime.ElapsedGameTime;

            var isActive = _lifeTime < _duration;

            if (isActive)
            {
                Position = _targetPosition + new Vector2((float)_randomizer.NextDouble() - 0.5f, (float)_randomizer.NextDouble() - 0.5f);
            }
            else
            {
                Position = _targetPosition;
            }

            _tile.Update(gameTime);
        }
    }
}
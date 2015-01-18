using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using PilesOfTiles.Randomizers;

namespace PilesOfTiles.Tiles
{
    public class DynamicPositionedTile : ITile
    {
        private ITile _tile;
        private IRandomizer _randomizer;

        private Vector2 _targetPosition;
        private Vector2 _sourcePosition;
        private TimeSpan _duration;
        private TimeSpan _lifeTime;

        public DynamicPositionedTile(ITile tile, IRandomizer randomizer)
        {
            _tile = tile;
            _randomizer = randomizer;

            _targetPosition = tile.Position;
            _sourcePosition = new Vector2(_randomizer.Next(50) * 2 - 50, _randomizer.Next(50) * 2 - 50);
            _duration = TimeSpan.FromSeconds(_randomizer.NextDouble() * 2);
            _lifeTime = TimeSpan.Zero;

            Position = _sourcePosition;
        }

        public void Update(GameTime gameTime)
        {
            _lifeTime += gameTime.ElapsedGameTime;

            var cappedDuration = _lifeTime < _duration ? _lifeTime : _duration;
            var interpolator = cappedDuration.Ticks / (float) _duration.Ticks;

            Position = _sourcePosition * (1 - interpolator) + _targetPosition * interpolator;

            _tile.Update(gameTime);
        }

        public Vector2 Position { get; set; }
        public Color Color { get { return _tile.Color; } }
        public State State { get { return _tile.State; } }
    }
}
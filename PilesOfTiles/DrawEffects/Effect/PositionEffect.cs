using System;
using Microsoft.Xna.Framework;

namespace PilesOfTiles.DrawEffects.Effect
{
    public class PositionEffect
    {
        private Vector2 _targetPosition;

        private Random _random;
        private Vector2 _sourcePosition;
        private TimeSpan _duration;
        private TimeSpan _lifeTime;

        public PositionEffect(Vector2 targetPosition)
        {
            _targetPosition = targetPosition;

            _random = new Random();
            _sourcePosition = new Vector2(_random.Next(10), _random.Next(10));
            _duration = TimeSpan.FromSeconds(_random.NextDouble()*2);
            _lifeTime = TimeSpan.Zero;
        }

        public void Update(Vector2 targetPosition)
        {
            _targetPosition = targetPosition;
        }

        public Vector2 Position { get; private set; }

        public void Update(GameTime gameTime)
        {
            _lifeTime += gameTime.ElapsedGameTime;

            var cappedDuration = _lifeTime < _duration  ? _lifeTime : _duration;
            var interpolator = cappedDuration.Ticks/_duration.Ticks;

            Position = _sourcePosition * (1 - interpolator) + _targetPosition * interpolator;
        }
    }
}
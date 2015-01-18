using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace PilesOfTiles.Particles
{
    public class Particle
    {
        private Texture2D _texture;
        private Vector2 _position;
        private Vector2 _velocity;
        private float _angle;
        private float _angularVelocity;
        private Color _color;
        private float _size;
        private TimeSpan _timeToLive;

        private Vector2 _gravity;
        private TimeSpan _elapsedLifetime;

        public Particle(Texture2D texture, Vector2 position, Vector2 velocity,
            float angle, float angularVelocity, Color color, float size, TimeSpan timeToLive)
        {
            _texture = texture;
            _position = position;
            _velocity = velocity;
            _angle = angle;
            _angularVelocity = angularVelocity;
            _color = color;
            _size = size;
            _timeToLive = timeToLive;

            _gravity = new Vector2(0,1) * 30.81f;
            _elapsedLifetime = TimeSpan.Zero;
        }

        public bool IsDead()
        {
            return _timeToLive < _elapsedLifetime;
        }

        public void Update(GameTime gameTime)
        {
            _elapsedLifetime += gameTime.ElapsedGameTime;

            var elapsed = (float) gameTime.ElapsedGameTime.TotalSeconds;

            _velocity += _gravity*elapsed;
            _position += _velocity*elapsed;
            _angle += _angularVelocity*elapsed;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            var sourceRectangle = new Rectangle(0, 0, _texture.Width, _texture.Height);
            var origin = new Vector2(_texture.Width/2.0f, _texture.Height/2.0f);

            spriteBatch.Draw(_texture, _position, sourceRectangle, _color,
                _angle, origin, _size, SpriteEffects.None, 0f);
        }
    }
}

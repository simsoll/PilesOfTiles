using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace PilesOfTiles.Particle
{
    public class ParticleManager
    {
        private readonly Random _random;
        private readonly IList<Particle> _particles;
        private readonly IList<Texture2D> _textures;

        public Vector2 EmitterLocation { get; set; }

        public ParticleManager(IList<Texture2D> textures, Vector2 location)
        {
            _textures = textures;
            _particles = new List<Particle>();
            _random = new Random();

            EmitterLocation = location;
        }

        public void Update(GameTime gameTime)
        {
            var total = 1;

            for (var i = 0; i < total; i++)
            {
                _particles.Add(GenerateNewParticle());
            }

            for (var particle = 0; particle < _particles.Count; particle++)
            {
                _particles[particle].Update(gameTime);
                if (_particles[particle].IsDead())
                {
                    _particles.RemoveAt(particle);
                    particle--;
                }
            }
        }

        private Particle GenerateNewParticle()
        {
            var texture = _textures[_random.Next(_textures.Count)];
            var position = EmitterLocation;
            var velocity = new Vector2(
                                    40f * (float)(_random.NextDouble() * 2 - 1),
                                    40f * (float)(_random.NextDouble() * 2 - 1));
            var angle = 0;
            var angularVelocity = 10f * (float)(_random.NextDouble() * 2 - 1);
            var color = new Color(
                        (float)_random.NextDouble(),
                        (float)_random.NextDouble(),
                        (float)_random.NextDouble());
            var size = (float)_random.NextDouble();
            var timeToLive = TimeSpan.FromMilliseconds(2000 + _random.Next(4000));

            return new Particle(texture, position, velocity, angle, angularVelocity, color, size, timeToLive);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (var t in _particles)
            {
                t.Draw(spriteBatch);
            }
        }
    }
}
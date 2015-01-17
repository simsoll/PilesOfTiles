using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Caliburn.Micro;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using PilesOfTiles.Brick.Messages;
using PilesOfTiles.Collision.Messages;
using PilesOfTiles.Core;
using PilesOfTiles.Level.Messages;
using PilesOfTiles.Particle.Messages;
using IDrawable = PilesOfTiles.Core.IDrawable;

namespace PilesOfTiles.DrawEffect
{
    public class DrawEffectService : IController, IUpdatable, IDrawable, IHandle<LevelLoaded>, IHandle<LevelUpdated>, IHandle<RowCleared>, IHandle<BrickCreated>, IHandle<BrickMoved>, IHandle<ParticlesMoved>
    {
        private readonly IEventAggregator _eventAggregator;
        private readonly Texture2D _tileTexture;
        private readonly int _tileSize;
        private readonly Texture2D _textTexture;
        private readonly int _textSize;
        private Level.Level _level;
        private Brick.Brick _brick;
        private IEnumerable<Particle.Particle> _particles; 

        public DrawEffectService(IEventAggregator eventAggregator, Texture2D tileTexture, int tileSize, Texture2D textTexture, int textSize)
        {
            _eventAggregator = eventAggregator;
            _tileTexture = tileTexture;
            _tileSize = tileSize;
            _textTexture = textTexture;
            _textSize = textSize;
        }

        public void Handle(LevelLoaded message)
        {
            _level = message.Level;
        }

        public void Handle(LevelUpdated message)
        {
            _level = message.Level;
        }

        public void Handle(RowCleared message)
        {
            _level = message.Level;
        }

        public void Handle(BrickCreated message)
        {
            _brick = message.Brick;
        }

        public void Handle(BrickMoved message)
        {
            _brick = message.Brick;
        }

        public void Load()
        {
            _eventAggregator.Subscribe(this);
        }

        public void Unload()
        {
            _eventAggregator.Unsubscribe(this);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            _level.Draw(spriteBatch, _tileTexture);
            _brick.Draw(spriteBatch, _tileTexture, _tileSize);

            foreach (var particle in _particles)
            {
                particle.Draw(spriteBatch);
            }   
        }

        public void Update(GameTime gameTime)
        {
        }

        public void Handle(ParticlesMoved message)
        {
            _particles = message.Particles;
        }
    }
}

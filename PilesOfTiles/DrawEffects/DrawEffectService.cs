using System.Collections.Generic;
using Caliburn.Micro;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using PilesOfTiles.Bricks;
using PilesOfTiles.Bricks.Messages;
using PilesOfTiles.Collision.Messages;
using PilesOfTiles.Core;
using PilesOfTiles.DrawEffects.Effect;
using PilesOfTiles.Levels;
using PilesOfTiles.Levels.Messages;
using PilesOfTiles.Particles;
using PilesOfTiles.Particles.Messages;
using PilesOfTiles.Tiles;
using IDrawable = PilesOfTiles.Core.IDrawable;

namespace PilesOfTiles.DrawEffects
{
    public class DrawEffectService : IController, IUpdatable, IDrawable, IHandle<LevelLoaded>, IHandle<LevelUpdated>, IHandle<RowCleared>, IHandle<BrickCreated>, IHandle<BrickMoved>, IHandle<ParticlesMoved>
    {
        private readonly IEventAggregator _eventAggregator;
        private readonly Texture2D _tileTexture;
        private readonly int _tileSize;
        private readonly Texture2D _textTexture;
        private readonly int _textSize;
        private Level _level;
        private IDictionary<Tile,PositionEffect> _positionEffects; 
        private Brick _brick;
        private IEnumerable<Particle> _particles; 

        public DrawEffectService(IEventAggregator eventAggregator, Texture2D tileTexture, int tileSize, Texture2D textTexture, int textSize)
        {
            _eventAggregator = eventAggregator;
            _tileTexture = tileTexture;
            _tileSize = tileSize;
            _textTexture = textTexture;
            _textSize = textSize;

            _positionEffects = new Dictionary<Tile, PositionEffect>();
        }

        public void Handle(LevelLoaded message)
        {
            _level = message.Level;
            foreach (var tile in _level.Tiles)
            {
                _positionEffects.Add(tile, new PositionEffect(tile.Position()));
            }
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
            foreach (var tile in _level.Tiles)
            {
                //var position = _positionEffects[tile].Position;

                tile.Draw(spriteBatch, _tileTexture, _tileSize);
            }

            foreach (var tile in _brick.Tiles)
            {
                tile.Draw(spriteBatch, _tileTexture, _tileSize);
            }

            foreach (var particle in _particles)
            {
                particle.Draw(spriteBatch);
            }   
        }

        public void Update(GameTime gameTime)
        {
            foreach (var positionEffect in _positionEffects)
            {
                positionEffect.Value.Update(gameTime);
            }
        }

        public void Handle(ParticlesMoved message)
        {
            _particles = message.Particles;
        }
    }
}

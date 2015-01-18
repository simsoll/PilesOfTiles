using System.Collections.Generic;
using System.Linq;
using Caliburn.Micro;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using PilesOfTiles.Bricks;
using PilesOfTiles.Bricks.Messages;
using PilesOfTiles.Collision.Messages;
using PilesOfTiles.Core;
using PilesOfTiles.Levels;
using PilesOfTiles.Levels.Messages;
using PilesOfTiles.Particles;
using PilesOfTiles.Particles.Messages;
using PilesOfTiles.Randomizers;
using PilesOfTiles.Screens.Messages;
using PilesOfTiles.Tiles;
using IDrawable = PilesOfTiles.Core.IDrawable;

namespace PilesOfTiles.DrawEffects
{
    public class DrawEffectService : IController, IUpdatable, IDrawable, IHandle<LevelLoaded>, IHandle<LevelUpdated>, IHandle<RowCleared>, IHandle<BrickCreated>, IHandle<BrickMoved>, IHandle<ParticlesMoved>, IHandle<GameOver>
    {
        private readonly IEventAggregator _eventAggregator;
        private readonly IRandomizer _randomizer;
        private readonly Texture2D _tileTexture;
        private readonly int _tileSize;
        private readonly Texture2D _textTexture;
        private readonly int _textSize;
        private Brick _brick;
        private IEnumerable<ITile> _levelTiles; 
        private IEnumerable<Particle> _particles;

        public DrawEffectService(IEventAggregator eventAggregator, IRandomizer randomizer, Texture2D tileTexture,
            int tileSize, Texture2D textTexture, int textSize)
        {
            _eventAggregator = eventAggregator;
            _randomizer = randomizer;
            _tileTexture = tileTexture;
            _tileSize = tileSize;
            _textTexture = textTexture;
            _textSize = textSize;

            _levelTiles = new List<ITile>();
        }

        public void Handle(LevelLoaded message)
        {
            _levelTiles =
                message.Level.Tiles.Select(
                    tile => new DynamicPositionedTile(new Tile(tile.Position, tile.Color, tile.State), _randomizer)).ToList();
        }

        public void Handle(LevelUpdated message)
        {
            _levelTiles =
                message.Level.Tiles.Select(tile => new Tile(tile.Position, tile.Color, tile.State));
        }

        public void Handle(RowCleared message)
        {
            _levelTiles =
                message.Level.Tiles.Select(tile => new Tile(tile.Position, tile.Color, tile.State));
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
            foreach (var tile in _levelTiles)
            {
                spriteBatch.Draw(_tileTexture, tile.Position * _tileSize, tile.Color);
            }

            foreach (var tile in _brick.Tiles)
            {
                spriteBatch.Draw(_tileTexture, tile.Position * _tileSize, tile.Color);
            }

            foreach (var particle in _particles)
            {
                particle.Draw(spriteBatch);
            }   
        }

        public void Update(GameTime gameTime)
        {
            foreach (var tile in _levelTiles)
            {
                tile.Update(gameTime);
            }
        }

        public void Handle(ParticlesMoved message)
        {
            _particles = message.Particles;
        }

        public void Handle(GameOver message)
        {
            _particles = new List<Particle>();
        }
    }
}

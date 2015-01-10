using System;
using System.Collections.Generic;
using System.Linq;
using Caliburn.Micro;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using PilesOfTiles.Brick;
using PilesOfTiles.Brick.Messages;
using PilesOfTiles.Collision.Messages;
using PilesOfTiles.Level.Messages;
using PilesOfTiles.Manager;
using PilesOfTiles.View.Messages;

namespace PilesOfTiles.Particle
{
    public class ParticleManager : IManager, IHandle<LevelLoaded>, IHandle<LevelUpdated>, IHandle<RowCleared>, IHandle<BrickCreated>, IHandle<BrickMoving>
    {
        private readonly IEventAggregator _eventAggregator;
        private readonly IList<Texture2D> _textures;

        private readonly Random _random;
        private readonly IList<Particle> _particles;

        private IList<Tile> _levelTiles;
        private IList<Tile> _brickTiles;

        public ParticleManager(IEventAggregator eventAggregator, IList<Texture2D> textures)
        {
            _eventAggregator = eventAggregator;
            _textures = textures;
            _particles = new List<Particle>();
            _random = new Random();
        }

        public void Load()
        {
            _eventAggregator.Subscribe(this);
        }

        public void Unload()
        {
            _eventAggregator.Unsubscribe(this);
        }

        public void Handle(LevelLoaded message)
        {
            _levelTiles = message.Tiles.Select(tile => Tile.Create(tile.Position, tile.Color, tile.State)).ToList();
        }

        public void Handle(LevelUpdated message)
        {
            _levelTiles = message.Tiles.Select(tile => Tile.Create(tile.Position, tile.Color, tile.State)).ToList();
        }

        public void Handle(RowCleared message)
        {
            GenerateParticlesAsRowCleared(message.Row);
            _levelTiles = message.Tiles.Select(tile => Tile.Create(tile.Position, tile.Color, tile.State)).ToList();
        }

        public void Handle(BrickCreated message)
        {
            _brickTiles = message.Tiles.Select(x => Tile.Create(x.Position + message.Position, x.Color, State.Removable)).ToList();
        }

        public void Handle(BrickMoving message)
        {
            _brickTiles = message.Tiles.Select(x => Tile.Create(x.Position + message.Position, x.Color, State.Removable)).ToList();
            GenerateParticlesAsBrickMoved();
        }

        private void GenerateParticlesAsRowCleared(int row)
        {
            var tiles = _levelTiles.Where(tile => tile.Position.Y == row && tile.State == State.Removable);

            foreach (var tile in tiles)
            {
                GenerateParticlesAlong(tile.Position + Vector2.One*0.5f, tile.Position + Vector2.One*0.5f, new[] {tile.Color}, 5);
            }
        }

        private void GenerateParticlesAsBrickMoved()
        {
            foreach (var tile in _brickTiles)
            {
                CheckIfTileTouchesLevelTilesFromDirection(tile, Direction.Left);
                CheckIfTileTouchesLevelTilesFromDirection(tile, Direction.Right);
            }
        }

        private void CheckIfTileTouchesLevelTilesFromDirection(Tile tile, Direction direction)
        {
            var offset = TouchingOffsetFromDirection(direction);

            if (_levelTiles.Any(x => x.Position == tile.Position + offset))
            {
                var levelTile =
                    _levelTiles.FirstOrDefault(x => x.Position == tile.Position + offset);


                if (levelTile != null)
                {
                    var endPointPositions = ParticleEndPointPositionsFromDirection(tile.Position, direction);
                    GenerateParticlesAlong(endPointPositions.Item1, endPointPositions.Item2,
                        new[] {tile.Color, levelTile.Color}, _random.Next(5));
                }
            }
        }

        private void GenerateParticlesAlong(Vector2 startPosition, Vector2 endPosition, Color[] colors, int numberOfParticles)
        {
            var x = startPosition.X;

            for (var i = 0; i < numberOfParticles; i++)
            {
                var lowerBoundY = Math.Min(startPosition.Y, endPosition.Y);
                var upperBoundY = Math.Max(startPosition.Y, endPosition.Y);
                var y = _random.Next((int) lowerBoundY, (int) upperBoundY);

                _particles.Add(GenerateNewParticle(new Vector2(x, y), colors));
            }
        }

        private Tuple<Vector2, Vector2> ParticleEndPointPositionsFromDirection(Vector2 position, Direction direction)
        {
            switch (direction)
            {
                case Direction.Up:
                    return new Tuple<Vector2, Vector2>(position, position + Vector2.UnitX);
                case Direction.Down:
                    return new Tuple<Vector2, Vector2>(position + Vector2.UnitY, position + Vector2.One);
                case Direction.Left:
                    return new Tuple<Vector2, Vector2>(position, position + Vector2.UnitY);
                case Direction.Right:
                    return new Tuple<Vector2, Vector2>(position + Vector2.UnitX, position + Vector2.One);
            }

            return new Tuple<Vector2,Vector2>(Vector2.Zero, Vector2.Zero);
        }

        private Vector2 TouchingOffsetFromDirection(Direction direction)
        {
            switch (direction)
            {
                case Direction.Up:
                    return Vector2.UnitY*-1;
                case Direction.Down:
                    return Vector2.UnitY;
                case Direction.Left:
                    return Vector2.UnitX*-1;
                case Direction.Right:
                    return Vector2.UnitX;
            }

            return Vector2.Zero;
        }

        public void Update(GameTime gameTime)
        {
            //var total = 1;

            //for (var i = 0; i < total; i++)
            //{
            //    _particles.Add(GenerateNewParticle());
            //}

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

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (var t in _particles)
            {
                t.Draw(spriteBatch);
            }
        }

        private Particle GenerateNewParticle(Vector2 position, Color[] colors)
        {
            var texture = _textures[_random.Next(_textures.Count)];
            var velocity = new Vector2(
                                    40f * (float)(_random.NextDouble() * 2 - 1),
                                    40f * (float)(_random.NextDouble() * 2 - 1));
            var angle = 0;
            var angularVelocity = 10f * (float)(_random.NextDouble() * 2 - 1);
            var color = colors[_random.Next(colors.Length)];
            var size = (float)_random.NextDouble();
            var timeToLive = TimeSpan.FromMilliseconds(2000 + _random.Next(4000));

            return new Particle(texture, position*texture.Height, velocity, angle, angularVelocity, color, size, timeToLive);
        }
    }
}
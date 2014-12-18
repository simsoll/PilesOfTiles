using System;
using System.Collections.Generic;
using System.Linq;
using Caliburn.Micro;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using PilesOfTiles.Input.Messages;
using Vector2 = Microsoft.Xna.Framework.Vector2;

namespace PilesOfTiles.Brick
{
    public class BrickManager : IHandle<ActionRequested>
    {
        private readonly Random _random;
        private IEventAggregator _eventAggregator;

        public Brick Brick { get; private set; }
        public IEnumerable<BrickMap> BrickMaps { get; private set; } 

        public BrickManager(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;
            _eventAggregator.Subscribe(this);

            _random = new Random(); 
            InitializeBrickMaps();
            SpawnRandomBrickAt(new Vector2(5, 5));
        }

        public void Handle(ActionRequested message)
        {
            Brick.Update(message.Action);
        }


        public void Draw(SpriteBatch spriteBatch, Texture2D texture, int tileSize)
        {
            Brick.Draw(spriteBatch, texture, tileSize);
        }

        public void SpawnRandomBrickAt(Vector2 position)
        {
            var brickMap = BrickMaps.ElementAt(_random.Next(BrickMaps.Count()));
            Brick = new Brick(position, Direction.Up, brickMap);
        }

        private void InitializeBrickMaps()
        {
            BrickMaps = new List<BrickMap>
            {
                new BrickMap(new Dictionary<Direction, IEnumerable<Tile>>
                {
                    {
                        Direction.Up,
                        new List<Tile>
                        {
                            Tile.Create(new Vector2(0, 1), Color.Blue),
                            Tile.Create(new Vector2(1, 0), Color.Blue),
                            Tile.Create(new Vector2(1, 1), Color.Blue),
                            Tile.Create(new Vector2(2, 1), Color.Blue)
                        }
                    },
                    {
                        Direction.Right,
                        new List<Tile>
                        {
                            Tile.Create(new Vector2(1, 0), Color.Blue),
                            Tile.Create(new Vector2(1, 1), Color.Blue),
                            Tile.Create(new Vector2(2, 1), Color.Blue),
                            Tile.Create(new Vector2(1, 2), Color.Blue)
                        }
                    },
                    {
                        Direction.Down,
                        new List<Tile>
                        {
                            Tile.Create(new Vector2(0, 1), Color.Blue),
                            Tile.Create(new Vector2(1, 1), Color.Blue),
                            Tile.Create(new Vector2(2, 1), Color.Blue),
                            Tile.Create(new Vector2(1, 2), Color.Blue)
                        }
                    },
                    {
                        Direction.Left,
                        new List<Tile>
                        {
                            Tile.Create(new Vector2(1, 0), Color.Blue),
                            Tile.Create(new Vector2(0, 1), Color.Blue),
                            Tile.Create(new Vector2(1, 1), Color.Blue),
                            Tile.Create(new Vector2(1, 2), Color.Blue)
                        }
                    }

                })
            };
        }
    }
}
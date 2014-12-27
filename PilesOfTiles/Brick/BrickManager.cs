using System;
using System.Collections.Generic;
using System.Linq;
using Caliburn.Micro;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using PilesOfTiles.Brick.Messages;
using PilesOfTiles.Collision.Messages;
using PilesOfTiles.Input.Messages;
using Vector2 = Microsoft.Xna.Framework.Vector2;

namespace PilesOfTiles.Brick
{
    public class BrickManager : IHandle<ActionRequested>, IHandle<BrickCollided>
    {
        private Vector2 _spawnPosition;
        private readonly Random _random;
        private IEventAggregator _eventAggregator;

        public Brick Brick { get; private set; }
        public IEnumerable<BrickMap> BrickMaps { get; private set; } 

        public BrickManager(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;
            _eventAggregator.Subscribe(this);

            _spawnPosition = new Vector2(15, 5);
            _random = new Random(); 
            InitializeBrickMaps();
            SpawnRandomBrickAt(_spawnPosition);
        }

        public void Handle(ActionRequested message)
        {
            Brick.Update(message.Action);
            _eventAggregator.PublishOnUIThread(new BrickMoved
            {
                Action = message.Action,
                PointsAt = Brick.PointsAt,
                Position = Brick.Position,
                Tiles = Brick.BrickMap.GetTilesWhenPointingAt(Brick.PointsAt)
            });
        }

        public void Handle(BrickCollided message)
        {
            SpawnRandomBrickAt(_spawnPosition);
        }

        public void Draw(SpriteBatch spriteBatch, Texture2D texture, int tileSize)
        {
            Brick.Draw(spriteBatch, texture, tileSize);
        }

        public void SpawnRandomBrickAt(Vector2 position)
        {
            var brickMap = BrickMaps.ElementAt(_random.Next(BrickMaps.Count()));
            Brick = new Brick(position, Direction.Up, brickMap);
            _eventAggregator.PublishOnUIThread(new BrickCreated
            {
                PointsAt = Brick.PointsAt,
                Position = Brick.Position,
                Tiles = Brick.BrickMap.GetTilesWhenPointingAt(Brick.PointsAt)
            });
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
                            Tile.Create(new Vector2(0, 1), Color.MediumPurple, State.Removable),
                            Tile.Create(new Vector2(1, 0), Color.MediumPurple, State.Removable),
                            Tile.Create(new Vector2(1, 1), Color.MediumPurple, State.Removable),
                            Tile.Create(new Vector2(2, 1), Color.MediumPurple, State.Removable)
                        }
                    },
                    {
                        Direction.Right,
                        new List<Tile>
                        {
                            Tile.Create(new Vector2(1, 0), Color.MediumPurple, State.Removable),
                            Tile.Create(new Vector2(1, 1), Color.MediumPurple, State.Removable),
                            Tile.Create(new Vector2(2, 1), Color.MediumPurple, State.Removable),
                            Tile.Create(new Vector2(1, 2), Color.MediumPurple, State.Removable)
                        }
                    },
                    {
                        Direction.Down,
                        new List<Tile>
                        {
                            Tile.Create(new Vector2(0, 1), Color.MediumPurple, State.Removable),
                            Tile.Create(new Vector2(1, 1), Color.MediumPurple, State.Removable),
                            Tile.Create(new Vector2(2, 1), Color.MediumPurple, State.Removable),
                            Tile.Create(new Vector2(1, 2), Color.MediumPurple, State.Removable)
                        }
                    },
                    {
                        Direction.Left,
                        new List<Tile>
                        {
                            Tile.Create(new Vector2(1, 0), Color.MediumPurple, State.Removable),
                            Tile.Create(new Vector2(0, 1), Color.MediumPurple, State.Removable),
                            Tile.Create(new Vector2(1, 1), Color.MediumPurple, State.Removable),
                            Tile.Create(new Vector2(1, 2), Color.MediumPurple, State.Removable)
                        }
                    }

                }),
                new BrickMap(new Dictionary<Direction, IEnumerable<Tile>>
                {
                    {
                        Direction.Up,
                        new List<Tile>
                        {
                            Tile.Create(new Vector2(0, 0), Color.Yellow, State.Removable),
                            Tile.Create(new Vector2(1, 0), Color.Yellow, State.Removable),
                            Tile.Create(new Vector2(0, 1), Color.Yellow, State.Removable),
                            Tile.Create(new Vector2(1, 1), Color.Yellow, State.Removable)
                        }
                    },
                    {
                        Direction.Right,
                        new List<Tile>
                        {
                            Tile.Create(new Vector2(0, 0), Color.Yellow, State.Removable),
                            Tile.Create(new Vector2(1, 0), Color.Yellow, State.Removable),
                            Tile.Create(new Vector2(0, 1), Color.Yellow, State.Removable),
                            Tile.Create(new Vector2(1, 1), Color.Yellow, State.Removable)
                        }
                    },
                    {
                        Direction.Down,
                        new List<Tile>
                        {
                            Tile.Create(new Vector2(0, 0), Color.Yellow, State.Removable),
                            Tile.Create(new Vector2(1, 0), Color.Yellow, State.Removable),
                            Tile.Create(new Vector2(0, 1), Color.Yellow, State.Removable),
                            Tile.Create(new Vector2(1, 1), Color.Yellow, State.Removable)
                        }
                    },
                    {
                        Direction.Left,
                        new List<Tile>
                        {
                            Tile.Create(new Vector2(0, 0), Color.Yellow, State.Removable),
                            Tile.Create(new Vector2(1, 0), Color.Yellow, State.Removable),
                            Tile.Create(new Vector2(0, 1), Color.Yellow, State.Removable),
                            Tile.Create(new Vector2(1, 1), Color.Yellow, State.Removable)
                        }
                    }
                }),
                new BrickMap(new Dictionary<Direction, IEnumerable<Tile>>
                {
                    {
                        Direction.Up,
                        new List<Tile>
                        {
                            Tile.Create(new Vector2(-1, 0), Color.LightBlue, State.Removable),
                            Tile.Create(new Vector2(0, 0), Color.LightBlue, State.Removable),
                            Tile.Create(new Vector2(1, 0), Color.LightBlue, State.Removable),
                            Tile.Create(new Vector2(2, 0), Color.LightBlue, State.Removable)
                        }
                    },
                    {
                        Direction.Right,
                        new List<Tile>
                        {
                            Tile.Create(new Vector2(0, -1), Color.LightBlue, State.Removable),
                            Tile.Create(new Vector2(0, 0), Color.LightBlue, State.Removable),
                            Tile.Create(new Vector2(0, 1), Color.LightBlue, State.Removable),
                            Tile.Create(new Vector2(0, 2), Color.LightBlue, State.Removable)
                        }
                    },
                    {
                        Direction.Down,
                        new List<Tile>
                        {
                            Tile.Create(new Vector2(-1, 0), Color.LightBlue, State.Removable),
                            Tile.Create(new Vector2(0, 0), Color.LightBlue, State.Removable),
                            Tile.Create(new Vector2(1, 0), Color.LightBlue, State.Removable),
                            Tile.Create(new Vector2(2, 0), Color.LightBlue, State.Removable)
                        }
                    },
                    {
                        Direction.Left,
                        new List<Tile>
                        {
                            Tile.Create(new Vector2(0, -1), Color.LightBlue, State.Removable),
                            Tile.Create(new Vector2(0, 0), Color.LightBlue, State.Removable),
                            Tile.Create(new Vector2(0, 1), Color.LightBlue, State.Removable),
                            Tile.Create(new Vector2(0, 2), Color.LightBlue, State.Removable)
                        }
                    }
                }),
                new BrickMap(new Dictionary<Direction, IEnumerable<Tile>>
                {
                    {
                        Direction.Up,
                        new List<Tile>
                        {
                            Tile.Create(new Vector2(0, -1), Color.Orange, State.Removable),
                            Tile.Create(new Vector2(0, 0), Color.Orange, State.Removable),
                            Tile.Create(new Vector2(0, 1), Color.Orange, State.Removable),
                            Tile.Create(new Vector2(1, 1), Color.Orange, State.Removable)
                        }
                    },
                    {
                        Direction.Right,
                        new List<Tile>
                        {
                            Tile.Create(new Vector2(-1, 0), Color.Orange, State.Removable),
                            Tile.Create(new Vector2(0, 0), Color.Orange, State.Removable),
                            Tile.Create(new Vector2(1, 0), Color.Orange, State.Removable),
                            Tile.Create(new Vector2(-1, 1), Color.Orange, State.Removable)
                        }
                    },
                    {
                        Direction.Down,
                        new List<Tile>
                        {
                            Tile.Create(new Vector2(0, -1), Color.Orange, State.Removable),
                            Tile.Create(new Vector2(0, 0), Color.Orange, State.Removable),
                            Tile.Create(new Vector2(0, 1), Color.Orange, State.Removable),
                            Tile.Create(new Vector2(-1, -1), Color.Orange, State.Removable)
                        }
                    },
                    {
                        Direction.Left,
                        new List<Tile>
                        {
                            Tile.Create(new Vector2(-1, 0), Color.Orange, State.Removable),
                            Tile.Create(new Vector2(0, 0), Color.Orange, State.Removable),
                            Tile.Create(new Vector2(1, 0), Color.Orange, State.Removable),
                            Tile.Create(new Vector2(1, -1), Color.Orange, State.Removable)
                        }
                    }
                }),
                new BrickMap(new Dictionary<Direction, IEnumerable<Tile>>
                {
                    {
                        Direction.Up,
                        new List<Tile>
                        {
                            Tile.Create(new Vector2(1, -1), Color.Blue, State.Removable),
                            Tile.Create(new Vector2(1, 0), Color.Blue, State.Removable),
                            Tile.Create(new Vector2(1, 1), Color.Blue, State.Removable),
                            Tile.Create(new Vector2(0, 1), Color.Blue, State.Removable)
                        }
                    },
                    {
                        Direction.Right,
                        new List<Tile>
                        {
                            Tile.Create(new Vector2(0, 1), Color.Blue, State.Removable),
                            Tile.Create(new Vector2(1, 1), Color.Blue, State.Removable),
                            Tile.Create(new Vector2(2, 1), Color.Blue, State.Removable),
                            Tile.Create(new Vector2(0, 0), Color.Blue, State.Removable)
                        }
                    },
                    {
                        Direction.Down,
                        new List<Tile>
                        {
                            Tile.Create(new Vector2(1, -1), Color.Blue, State.Removable),
                            Tile.Create(new Vector2(1, 0), Color.Blue, State.Removable),
                            Tile.Create(new Vector2(1, 1), Color.Blue, State.Removable),
                            Tile.Create(new Vector2(2, -1), Color.Blue, State.Removable)
                        }
                    },
                    {
                        Direction.Left,
                        new List<Tile>
                        {
                            Tile.Create(new Vector2(0, 1), Color.Blue, State.Removable),
                            Tile.Create(new Vector2(1, 1), Color.Blue, State.Removable),
                            Tile.Create(new Vector2(2, 1), Color.Blue, State.Removable),
                            Tile.Create(new Vector2(2, 2), Color.Blue, State.Removable)
                        }
                    }
                }),
                new BrickMap(new Dictionary<Direction, IEnumerable<Tile>>
                {
                    {
                        Direction.Up,
                        new List<Tile>
                        {
                            Tile.Create(new Vector2(2, 0), Color.Green, State.Removable),
                            Tile.Create(new Vector2(1, 0), Color.Green, State.Removable),
                            Tile.Create(new Vector2(0, 1), Color.Green, State.Removable),
                            Tile.Create(new Vector2(1, 1), Color.Green, State.Removable)
                        }
                    },
                    {
                        Direction.Right,
                        new List<Tile>
                        {
                            Tile.Create(new Vector2(0, 0), Color.Green, State.Removable),
                            Tile.Create(new Vector2(1, 2), Color.Green, State.Removable),
                            Tile.Create(new Vector2(0, 1), Color.Green, State.Removable),
                            Tile.Create(new Vector2(1, 1), Color.Green, State.Removable)
                        }
                    },
                    {
                        Direction.Down,
                        new List<Tile>
                        {
                            Tile.Create(new Vector2(0, 0), Color.Green, State.Removable),
                            Tile.Create(new Vector2(1, 0), Color.Green, State.Removable),
                            Tile.Create(new Vector2(0, 1), Color.Green, State.Removable),
                            Tile.Create(new Vector2(-1, 1), Color.Green, State.Removable)
                        }
                    },
                    {
                        Direction.Left,
                        new List<Tile>
                        {
                            Tile.Create(new Vector2(0, 0), Color.Green, State.Removable),
                            Tile.Create(new Vector2(1, 0), Color.Green, State.Removable),
                            Tile.Create(new Vector2(0, -1), Color.Green, State.Removable),
                            Tile.Create(new Vector2(1, 1), Color.Green, State.Removable)
                        }
                    }
                }),
new BrickMap(new Dictionary<Direction, IEnumerable<Tile>>
                {
                    {
                        Direction.Up,
                        new List<Tile>
                        {
                            Tile.Create(new Vector2(0, 0), Color.Red, State.Removable),
                            Tile.Create(new Vector2(-1, 0), Color.Red, State.Removable),
                            Tile.Create(new Vector2(0, 1), Color.Red, State.Removable),
                            Tile.Create(new Vector2(1, 1), Color.Red, State.Removable)
                        }
                    },
                    {
                        Direction.Right,
                        new List<Tile>
                        {
                            Tile.Create(new Vector2(0, 0), Color.Red, State.Removable),
                            Tile.Create(new Vector2(1, 0), Color.Red, State.Removable),
                            Tile.Create(new Vector2(0, 1), Color.Red, State.Removable),
                            Tile.Create(new Vector2(1, -1), Color.Red, State.Removable)
                        }
                    },
                    {
                        Direction.Down,
                        new List<Tile>
                        {
                            Tile.Create(new Vector2(0, 0), Color.Red, State.Removable),
                            Tile.Create(new Vector2(1, 0), Color.Red, State.Removable),
                            Tile.Create(new Vector2(2, 1), Color.Red, State.Removable),
                            Tile.Create(new Vector2(1, 1), Color.Red, State.Removable)
                        }
                    },
                    {
                        Direction.Left,
                        new List<Tile>
                        {
                            Tile.Create(new Vector2(0, 2), Color.Red, State.Removable),
                            Tile.Create(new Vector2(1, 0), Color.Red, State.Removable),
                            Tile.Create(new Vector2(0, 1), Color.Red, State.Removable),
                            Tile.Create(new Vector2(1, 1), Color.Red, State.Removable)
                        }
                    }
                })
            };
        }
    }
}
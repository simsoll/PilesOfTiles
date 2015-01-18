using System;
using System.Collections.Generic;
using System.Linq;
using Caliburn.Micro;
using Microsoft.Xna.Framework;
using PilesOfTiles.Bricks.Messages;
using PilesOfTiles.Collision.Messages;
using PilesOfTiles.Core;
using PilesOfTiles.Input.Messages;
using PilesOfTiles.Screens.Messages;
using PilesOfTiles.Tiles;

namespace PilesOfTiles.Bricks
{
    public class BrickManager : IController, IHandle<ActionRequested>, IHandle<BrickCollided>, IHandle<GameStarted>
    {
        private Vector2 _spawnPosition;
        private readonly Random _random;
        private IEventAggregator _eventAggregator;

        public Brick Brick { get; private set; }
        public IEnumerable<BrickMap> BrickMaps { get; private set; }

        public BrickManager(IEventAggregator eventAggregator, Vector2 spawnPosition)
        {
            _eventAggregator = eventAggregator;

            _spawnPosition = spawnPosition;
            _random = new Random();
            InitializeBrickMaps();
        }

        public void Handle(ActionRequested message)
        {
            Brick.Update(message.Action);
            _eventAggregator.PublishOnUIThread(new BrickMoving
            {
                Brick = Brick,
                Action = message.Action
            });
        }

        public void Handle(BrickCollided message)
        {
            SpawnRandomBrickAt(_spawnPosition);
        }

        public void Handle(GameStarted message)
        {
            SpawnRandomBrickAt(_spawnPosition);
        }

        public void Load()
        {
            _eventAggregator.Subscribe(this);
        }

        public void Unload()
        {
            _eventAggregator.Unsubscribe(this);
        }

        public void SpawnRandomBrickAt(Vector2 position)
        {
            var brickMap = BrickMaps.ElementAt(_random.Next(BrickMaps.Count()));
            Brick = new Brick(position, Direction.Up, brickMap);
            _eventAggregator.PublishOnUIThread(new BrickCreated
            {
                Brick = Brick
            });
        }

        private void InitializeBrickMaps()
        {
            BrickMaps = new List<BrickMap>
            {
                new BrickMap(new Dictionary<Direction, IEnumerable<ITile>>
                {
                    {
                        Direction.Up,
                        new List<ITile>
                        {
                            new Tile(new Vector2(0, 1), Color.MediumPurple, State.Removable),
                            new Tile(new Vector2(1, 0), Color.MediumPurple, State.Removable),
                            new Tile(new Vector2(1, 1), Color.MediumPurple, State.Removable),
                            new Tile(new Vector2(2, 1), Color.MediumPurple, State.Removable)
                        }
                    },
                    {
                        Direction.Right,
                        new List<ITile>
                        {
                            new Tile(new Vector2(1, 0), Color.MediumPurple, State.Removable),
                            new Tile(new Vector2(1, 1), Color.MediumPurple, State.Removable),
                            new Tile(new Vector2(2, 1), Color.MediumPurple, State.Removable),
                            new Tile(new Vector2(1, 2), Color.MediumPurple, State.Removable)
                        }
                    },
                    {
                        Direction.Down,
                        new List<ITile>
                        {
                            new Tile(new Vector2(0, 1), Color.MediumPurple, State.Removable),
                            new Tile(new Vector2(1, 1), Color.MediumPurple, State.Removable),
                            new Tile(new Vector2(2, 1), Color.MediumPurple, State.Removable),
                            new Tile(new Vector2(1, 2), Color.MediumPurple, State.Removable)
                        }
                    },
                    {
                        Direction.Left,
                        new List<ITile>
                        {
                            new Tile(new Vector2(1, 0), Color.MediumPurple, State.Removable),
                            new Tile(new Vector2(0, 1), Color.MediumPurple, State.Removable),
                            new Tile(new Vector2(1, 1), Color.MediumPurple, State.Removable),
                            new Tile(new Vector2(1, 2), Color.MediumPurple, State.Removable)
                        }
                    }

                }),
                new BrickMap(new Dictionary<Direction, IEnumerable<ITile>>
                {
                    {
                        Direction.Up,
                        new List<ITile>
                        {
                            new Tile(new Vector2(0, 0), Color.Yellow, State.Removable),
                            new Tile(new Vector2(1, 0), Color.Yellow, State.Removable),
                            new Tile(new Vector2(0, 1), Color.Yellow, State.Removable),
                            new Tile(new Vector2(1, 1), Color.Yellow, State.Removable)
                        }
                    },
                    {
                        Direction.Right,
                        new List<ITile>
                        {
                            new Tile(new Vector2(0, 0), Color.Yellow, State.Removable),
                            new Tile(new Vector2(1, 0), Color.Yellow, State.Removable),
                            new Tile(new Vector2(0, 1), Color.Yellow, State.Removable),
                            new Tile(new Vector2(1, 1), Color.Yellow, State.Removable)
                        }
                    },
                    {
                        Direction.Down,
                        new List<ITile>
                        {
                            new Tile(new Vector2(0, 0), Color.Yellow, State.Removable),
                            new Tile(new Vector2(1, 0), Color.Yellow, State.Removable),
                            new Tile(new Vector2(0, 1), Color.Yellow, State.Removable),
                            new Tile(new Vector2(1, 1), Color.Yellow, State.Removable)
                        }
                    },
                    {
                        Direction.Left,
                        new List<ITile>
                        {
                            new Tile(new Vector2(0, 0), Color.Yellow, State.Removable),
                            new Tile(new Vector2(1, 0), Color.Yellow, State.Removable),
                            new Tile(new Vector2(0, 1), Color.Yellow, State.Removable),
                            new Tile(new Vector2(1, 1), Color.Yellow, State.Removable)
                        }
                    }
                }),
                new BrickMap(new Dictionary<Direction, IEnumerable<ITile>>
                {
                    {
                        Direction.Up,
                        new List<ITile>
                        {
                            new Tile(new Vector2(-1, 0), Color.CornflowerBlue, State.Removable),
                            new Tile(new Vector2(0, 0), Color.CornflowerBlue, State.Removable),
                            new Tile(new Vector2(1, 0), Color.CornflowerBlue, State.Removable),
                            new Tile(new Vector2(2, 0), Color.CornflowerBlue, State.Removable)
                        }
                    },
                    {
                        Direction.Right,
                        new List<ITile>
                        {
                            new Tile(new Vector2(0, -1), Color.CornflowerBlue, State.Removable),
                            new Tile(new Vector2(0, 0), Color.CornflowerBlue, State.Removable),
                            new Tile(new Vector2(0, 1), Color.CornflowerBlue, State.Removable),
                            new Tile(new Vector2(0, 2), Color.CornflowerBlue, State.Removable)
                        }
                    },
                    {
                        Direction.Down,
                        new List<ITile>
                        {
                            new Tile(new Vector2(-1, 0), Color.CornflowerBlue, State.Removable),
                            new Tile(new Vector2(0, 0), Color.CornflowerBlue, State.Removable),
                            new Tile(new Vector2(1, 0), Color.CornflowerBlue, State.Removable),
                            new Tile(new Vector2(2, 0), Color.CornflowerBlue, State.Removable)
                        }
                    },
                    {
                        Direction.Left,
                        new List<ITile>
                        {
                            new Tile(new Vector2(0, -1), Color.CornflowerBlue, State.Removable),
                            new Tile(new Vector2(0, 0), Color.CornflowerBlue, State.Removable),
                            new Tile(new Vector2(0, 1), Color.CornflowerBlue, State.Removable),
                            new Tile(new Vector2(0, 2), Color.CornflowerBlue, State.Removable)
                        }
                    }
                }),
                new BrickMap(new Dictionary<Direction, IEnumerable<ITile>>
                {
                    {
                        Direction.Up,
                        new List<ITile>
                        {
                            new Tile(new Vector2(0, -1), Color.Orange, State.Removable),
                            new Tile(new Vector2(0, 0), Color.Orange, State.Removable),
                            new Tile(new Vector2(0, 1), Color.Orange, State.Removable),
                            new Tile(new Vector2(1, 1), Color.Orange, State.Removable)
                        }
                    },
                    {
                        Direction.Right,
                        new List<ITile>
                        {
                            new Tile(new Vector2(-1, 0), Color.Orange, State.Removable),
                            new Tile(new Vector2(0, 0), Color.Orange, State.Removable),
                            new Tile(new Vector2(1, 0), Color.Orange, State.Removable),
                            new Tile(new Vector2(-1, 1), Color.Orange, State.Removable)
                        }
                    },
                    {
                        Direction.Down,
                        new List<ITile>
                        {
                            new Tile(new Vector2(0, -1), Color.Orange, State.Removable),
                            new Tile(new Vector2(0, 0), Color.Orange, State.Removable),
                            new Tile(new Vector2(0, 1), Color.Orange, State.Removable),
                            new Tile(new Vector2(-1, -1), Color.Orange, State.Removable)
                        }
                    },
                    {
                        Direction.Left,
                        new List<ITile>
                        {
                            new Tile(new Vector2(-1, 0), Color.Orange, State.Removable),
                            new Tile(new Vector2(0, 0), Color.Orange, State.Removable),
                            new Tile(new Vector2(1, 0), Color.Orange, State.Removable),
                            new Tile(new Vector2(1, -1), Color.Orange, State.Removable)
                        }
                    }
                }),
                new BrickMap(new Dictionary<Direction, IEnumerable<ITile>>
                {
                    {
                        Direction.Up,
                        new List<ITile>
                        {
                            new Tile(new Vector2(1, -1), Color.Blue, State.Removable),
                            new Tile(new Vector2(1, 0), Color.Blue, State.Removable),
                            new Tile(new Vector2(1, 1), Color.Blue, State.Removable),
                            new Tile(new Vector2(0, 1), Color.Blue, State.Removable)
                        }
                    },
                    {
                        Direction.Right,
                        new List<ITile>
                        {
                            new Tile(new Vector2(0, 1), Color.Blue, State.Removable),
                            new Tile(new Vector2(1, 1), Color.Blue, State.Removable),
                            new Tile(new Vector2(2, 1), Color.Blue, State.Removable),
                            new Tile(new Vector2(0, 0), Color.Blue, State.Removable)
                        }
                    },
                    {
                        Direction.Down,
                        new List<ITile>
                        {
                            new Tile(new Vector2(1, -1), Color.Blue, State.Removable),
                            new Tile(new Vector2(1, 0), Color.Blue, State.Removable),
                            new Tile(new Vector2(1, 1), Color.Blue, State.Removable),
                            new Tile(new Vector2(2, -1), Color.Blue, State.Removable)
                        }
                    },
                    {
                        Direction.Left,
                        new List<ITile>
                        {
                            new Tile(new Vector2(0, 1), Color.Blue, State.Removable),
                            new Tile(new Vector2(1, 1), Color.Blue, State.Removable),
                            new Tile(new Vector2(2, 1), Color.Blue, State.Removable),
                            new Tile(new Vector2(2, 2), Color.Blue, State.Removable)
                        }
                    }
                }),
                new BrickMap(new Dictionary<Direction, IEnumerable<ITile>>
                {
                    {
                        Direction.Up,
                        new List<ITile>
                        {
                            new Tile(new Vector2(2, 0), Color.Green, State.Removable),
                            new Tile(new Vector2(1, 0), Color.Green, State.Removable),
                            new Tile(new Vector2(0, 1), Color.Green, State.Removable),
                            new Tile(new Vector2(1, 1), Color.Green, State.Removable)
                        }
                    },
                    {
                        Direction.Right,
                        new List<ITile>
                        {
                            new Tile(new Vector2(0, 0), Color.Green, State.Removable),
                            new Tile(new Vector2(1, 2), Color.Green, State.Removable),
                            new Tile(new Vector2(0, 1), Color.Green, State.Removable),
                            new Tile(new Vector2(1, 1), Color.Green, State.Removable)
                        }
                    },
                    {
                        Direction.Down,
                        new List<ITile>
                        {
                            new Tile(new Vector2(0, 0), Color.Green, State.Removable),
                            new Tile(new Vector2(1, 0), Color.Green, State.Removable),
                            new Tile(new Vector2(0, 1), Color.Green, State.Removable),
                            new Tile(new Vector2(-1, 1), Color.Green, State.Removable)
                        }
                    },
                    {
                        Direction.Left,
                        new List<ITile>
                        {
                            new Tile(new Vector2(0, 0), Color.Green, State.Removable),
                            new Tile(new Vector2(1, 0), Color.Green, State.Removable),
                            new Tile(new Vector2(0, -1), Color.Green, State.Removable),
                            new Tile(new Vector2(1, 1), Color.Green, State.Removable)
                        }
                    }
                }),
                new BrickMap(new Dictionary<Direction, IEnumerable<ITile>>
                {
                    {
                        Direction.Up,
                        new List<ITile>
                        {
                            new Tile(new Vector2(0, 0), Color.Red, State.Removable),
                            new Tile(new Vector2(-1, 0), Color.Red, State.Removable),
                            new Tile(new Vector2(0, 1), Color.Red, State.Removable),
                            new Tile(new Vector2(1, 1), Color.Red, State.Removable)
                        }
                    },
                    {
                        Direction.Right,
                        new List<ITile>
                        {
                            new Tile(new Vector2(0, 0), Color.Red, State.Removable),
                            new Tile(new Vector2(1, 0), Color.Red, State.Removable),
                            new Tile(new Vector2(0, 1), Color.Red, State.Removable),
                            new Tile(new Vector2(1, -1), Color.Red, State.Removable)
                        }
                    },
                    {
                        Direction.Down,
                        new List<ITile>
                        {
                            new Tile(new Vector2(0, 0), Color.Red, State.Removable),
                            new Tile(new Vector2(1, 0), Color.Red, State.Removable),
                            new Tile(new Vector2(2, 1), Color.Red, State.Removable),
                            new Tile(new Vector2(1, 1), Color.Red, State.Removable)
                        }
                    },
                    {
                        Direction.Left,
                        new List<ITile>
                        {
                            new Tile(new Vector2(0, 2), Color.Red, State.Removable),
                            new Tile(new Vector2(1, 0), Color.Red, State.Removable),
                            new Tile(new Vector2(0, 1), Color.Red, State.Removable),
                            new Tile(new Vector2(1, 1), Color.Red, State.Removable)
                        }
                    }
                })
            };
        }
    }
}
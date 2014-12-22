using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Caliburn.Micro;
using Microsoft.Xna.Framework;
using PilesOfTiles.Brick.Messages;
using PilesOfTiles.Collision.Messages;
using PilesOfTiles.Input.Messages;
using PilesOfTiles.Level.Messages;
using Action = PilesOfTiles.Input.Messages.Action;

namespace PilesOfTiles.Collision
{
    public class CollisionManager : IHandle<LevelCreated>, IHandle<BrickCreated>, IHandle<BrickMoved>
    {
        private IEventAggregator _eventAggregator;
        private IEnumerable<Tile> _levelTiles;
        private IEnumerable<Tile> _brickTiles;

        public CollisionManager(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;
            _eventAggregator.Subscribe(this);
        }

        public void Handle(LevelCreated message)
        {
            _levelTiles = message.Tiles;
        }

        public void Handle(BrickCreated message)
        {
            _brickTiles = message.Tiles.Select(x => new Tile(x.Position + message.Position, x.Color));
            CheckForGameOver();
        }

        public void Handle(BrickMoved message)
        {
            _brickTiles = message.Tiles.Select(x => new Tile(x.Position + message.Position, x.Color));
            CheckBrickCollision(message.Action);
        }

        private void CheckForGameOver()
        {
            //TODO
        }

        private void CheckBrickCollision(Action action)
        {
            foreach (
                var tile in
                    _brickTiles.Where(
                        tile => _levelTiles.Any(x => x.Position.X == tile.Position.X && x.Position.Y == tile.Position.Y))
                )
            {
                switch (action)
                {
                    case Action.MoveDown:
                        _eventAggregator.PublishOnUIThread(new BrickCollided
                        {
                            Tiles =
                                _brickTiles.Select(x => Tile.Create(x.Position + new Vector2(0, -1), x.Color))
                                    .ToList()
                        });
                        return;
                    case Action.MoveLeft:
                        _eventAggregator.PublishOnUIThread(new ActionRequested
                        {
                            Action = Action.MoveRight
                        });
                        return;
                    case Action.MoveRight:
                        _eventAggregator.PublishOnUIThread(new ActionRequested
                        {
                            Action = Action.MoveLeft
                        });
                        return;
                    case Action.RotateClockWise:
                        _eventAggregator.PublishOnUIThread(new ActionRequested
                        {
                            Action = Action.RotateCounterClockWise
                        });
                        return;
                }
            }
        }
    }
}

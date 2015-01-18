using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Caliburn.Micro;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using PilesOfTiles.Bricks;
using PilesOfTiles.Bricks.Messages;
using PilesOfTiles.Collision.Messages;
using PilesOfTiles.Core;
using PilesOfTiles.Input.Messages;
using PilesOfTiles.Levels;
using PilesOfTiles.Levels.Messages;
using Action = PilesOfTiles.Input.Messages.Action;

namespace PilesOfTiles.Collision
{
    public class CollisionDetectionService : IController , IHandle<LevelLoaded>, IHandle<LevelUpdated>, IHandle<RowCleared>, IHandle<BrickCreated>, IHandle<BrickMoving>
    {
        private readonly IEventAggregator _eventAggregator;
        private Level _level;
        private Brick _brick;

        public CollisionDetectionService(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;
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
            CheckForGameOver();
        }

        public void Handle(BrickMoving message)
        {
            _brick = message.Brick;
            CheckBrickCollision(message.Action);
        }

        private void CheckForGameOver()
        {
            if (_brick.Tiles.Any(
                tile => _level.Tiles.Any(x => x.Position().X == tile.Position().X && x.Position().Y == tile.Position().Y)))
            {
                _eventAggregator.PublishOnUIThread(new GameOver());
            }
        }

        private void CheckBrickCollision(Action action)
        {
            foreach (
                var tile in
                    _brick.Tiles.Where(
                        tile => _level.Tiles.Any(x => x.Position().X == tile.Position().X && x.Position().Y == tile.Position().Y))
                )
            {
                switch (action)
                {
                    case Action.MoveDown:
                        _eventAggregator.PublishOnUIThread(new BrickCollided
                        {
                            Brick = _brick,
                            Correction = new Vector2(0, -1)
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

            //in case of no collision correction
            _eventAggregator.PublishOnUIThread(new BrickMoved
            {
                Brick = _brick
            });
        }

        public void Load()
        {
            _eventAggregator.Subscribe(this);
        }

        public void Unload()
        {
            _eventAggregator.Unsubscribe(this);
        }
    }
}

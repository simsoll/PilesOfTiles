﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Caliburn.Micro;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using PilesOfTiles.Brick.Messages;
using PilesOfTiles.Collision.Messages;
using PilesOfTiles.Input.Messages;
using PilesOfTiles.Level.Messages;
using PilesOfTiles.Manager;
using Action = PilesOfTiles.Input.Messages.Action;

namespace PilesOfTiles.Collision
{
    public class CollisionManager : IManager, IHandle<LevelLoaded>, IHandle<LevelUpdated>, IHandle<RowCleared>, IHandle<BrickCreated>, IHandle<BrickMoving>
    {
        private IEventAggregator _eventAggregator;
        private IList<Tile> _levelTiles;
        private IList<Tile> _brickTiles;

        public CollisionManager(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;
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
            _levelTiles = message.Tiles.Select(tile => Tile.Create(tile.Position, tile.Color, tile.State)).ToList();
        }

        public void Handle(BrickCreated message)
        {
            _brickTiles = message.Tiles.Select(x => Tile.Create(x.Position + message.Position, x.Color, State.Removable)).ToList();
            CheckForGameOver();
        }

        public void Handle(BrickMoving message)
        {
            _brickTiles = message.Tiles.Select(x => Tile.Create(x.Position + message.Position, x.Color, State.Removable)).ToList();
            CheckBrickCollision(message.Action);
        }

        private void CheckForGameOver()
        {
            if (_brickTiles.Any(
                tile => _levelTiles.Any(x => x.Position.X == tile.Position.X && x.Position.Y == tile.Position.Y)))
            {
                _eventAggregator.PublishOnUIThread(new GameOver());
            }
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
                            Tiles = _brickTiles.Select(
                                x => Tile.Create(x.Position + new Vector2(0, -1), x.Color, State.Removable))
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

        public void Load()
        {
            _eventAggregator.Subscribe(this);
        }

        public void Unload()
        {
            _eventAggregator.Unsubscribe(this);
        }

        public void Update(GameTime gameTime)
        {
        }

        public void Draw(SpriteBatch spriteBatch)
        {
        }
    }
}

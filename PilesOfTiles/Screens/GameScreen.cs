using System.Collections.Generic;
using Caliburn.Micro;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using PilesOfTiles.Core;
using PilesOfTiles.Input.Messages;
using PilesOfTiles.Screens.Messages;
using Action = PilesOfTiles.Input.Messages.Action;
using IDrawable = PilesOfTiles.Core.IDrawable;

namespace PilesOfTiles.Screens
{
    public class GameScreen : IScreen, IHandle<ActionRequested>
    {
        private readonly IEventAggregator _eventAggregator;
        private readonly IEnumerable<IController> _controllers;
        private readonly IEnumerable<IUpdatable> _updatables;
        private readonly IEnumerable<IDrawable> _drawables;

        public GameScreen(IEventAggregator eventAggregator, IEnumerable<IController> controllers,
            IEnumerable<IUpdatable> updatables, IEnumerable<IDrawable> drawables)
        {
            _eventAggregator = eventAggregator;
            _controllers = controllers;
            _updatables = updatables;
            _drawables = drawables;
        }

        public void Load()
        {
            _eventAggregator.Subscribe(this);
            foreach (var controller in _controllers)
            {
                controller.Load();
            }
        }

        public void Unload()
        {
            foreach (var controller in _controllers)
            {
                controller.Unload();
            }
            _eventAggregator.Unsubscribe(this);
        }

        public void Update(GameTime gameTime)
        {
            foreach (var updatable in _updatables)
            {
                updatable.Update(gameTime);
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (var drawable in _drawables)
            {
                drawable.Draw(spriteBatch);
            }
        }

        public void Handle(ActionRequested message)
        {
            if (message.Action == Action.Pause)
            {
                _eventAggregator.PublishOnUIThread(new GamePaused());
            }
        }
    }
}
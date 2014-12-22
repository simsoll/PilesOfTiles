using Caliburn.Micro;
using Microsoft.Xna.Framework.Input;
using PilesOfTiles.Core.Input.Keyboard.Messages;
using PilesOfTiles.Input.Messages;
using Action = PilesOfTiles.Input.Messages.Action;

namespace PilesOfTiles.Input
{
    public class InputManager : IHandle<KeyPressed>, IHandle<KeyHeld>
    {
        private IEventAggregator _eventAggregator;

        public InputManager(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;
            _eventAggregator.Subscribe(this);
        }

        public void Handle(KeyPressed message)
        {
            //TODO: get mapping from configuration
            switch (message.Key)
            {
                case Keys.Left:
                    _eventAggregator.PublishOnUIThread(new ActionRequested {Action = Action.MoveLeft});
                    break;
                case Keys.Right:
                    _eventAggregator.PublishOnUIThread(new ActionRequested {Action = Action.MoveRight});
                    break;
                case Keys.Down:
                    _eventAggregator.PublishOnUIThread(new ActionRequested {Action = Action.MoveDown});
                    break;
                case Keys.Space:
                    _eventAggregator.PublishOnUIThread(new ActionRequested {Action = Action.RotateClockWise});
                    break;
            }
        }


        public void Handle(KeyHeld message)
        {
            //TODO: get mapping from configuration
            switch (message.Key)
            {
                case Keys.Left:
                    _eventAggregator.PublishOnUIThread(new ActionRequested { Action = Action.MoveLeft });
                    break;
                case Keys.Right:
                    _eventAggregator.PublishOnUIThread(new ActionRequested { Action = Action.MoveRight });
                    break;
                case Keys.Down:
                    _eventAggregator.PublishOnUIThread(new ActionRequested { Action = Action.MoveDown });
                    break;
            }
        }
    }
}

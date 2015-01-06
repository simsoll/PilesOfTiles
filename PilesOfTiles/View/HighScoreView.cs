using Caliburn.Micro;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using PilesOfTiles.Core.Input.Keyboard.Messages;
using PilesOfTiles.View.Messages;

namespace PilesOfTiles.View
{
    public class HighScoreView : IView, IHandle<KeyPressed>
    {
        private IEventAggregator _eventAggregator;
        private Texture2D _textTexture;
        private int _textSize;
        private Color _textColor;


        public HighScoreView(IEventAggregator eventAggregator, Texture2D textTexture, int textSize, Color textColor)
        {
            _eventAggregator = eventAggregator;
            _textTexture = textTexture;
            _textSize = textSize;
            _textColor = textColor;
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

        public void Handle(KeyPressed message)
        {
            if (message.Key == Keys.Enter)
            {
                _eventAggregator.PublishOnUIThread(new ReturnToStartMenu());
            }
        }
    }
}
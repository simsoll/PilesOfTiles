using Caliburn.Micro;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using PilesOfTiles.Core.Input.Keyboard.Messages;
using PilesOfTiles.HighScore;
using PilesOfTiles.Screen.Messages;

namespace PilesOfTiles.Screen
{
    public class GamePausedScreen : IScreen, IHandle<KeyPressed>
    {
        private IEventAggregator _eventAggregator;
        private Texture2D _textTexture;
        private int _textSize;
        private Color _textColor;

        private string _titleText;
        private Vector2 _titleTextPosition;

        private PixelAlfabet _pixelAlfabet;

        public GamePausedScreen(IEventAggregator eventAggregator, Texture2D textTexture, int textSize, Color textColor)
        {
            _eventAggregator = eventAggregator;
            _textTexture = textTexture;
            _textSize = textSize;
            _textColor = textColor;

            _titleText = "Game Paused";
            _titleTextPosition = new Vector2(50, 50);

            _pixelAlfabet = new PixelAlfabet();
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
            _pixelAlfabet.DrawText(spriteBatch, _titleText, _textTexture, _titleTextPosition, _textSize,
                _textColor);
        }

        public void Handle(KeyPressed message)
        {
            _eventAggregator.PublishOnUIThread(new ResumeGame());
        }
    }
}
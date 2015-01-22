using Caliburn.Micro;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using PilesOfTiles.Core.Input.Keyboard.Messages;
using PilesOfTiles.HighScore;
using PilesOfTiles.Screens.Messages;
using PilesOfTiles.UserInterfaces;

namespace PilesOfTiles.Screens
{
    public class GamePausedScreen : IScreen, IHandle<KeyPressed>
    {
        private IEventAggregator _eventAggregator;
        private Texture2D _textTexture;
        private readonly Vector2 _centeredTextPosition;
        private int _textSize;
        private Color _textColor;

        private string _titleText;

        private PixelAlfabet _pixelAlfabet;

        public GamePausedScreen(IEventAggregator eventAggregator, Texture2D textTexture, Vector2 centeredTextPosition, int textSize, Color textColor)
        {
            _eventAggregator = eventAggregator;
            _textTexture = textTexture;
            _centeredTextPosition = centeredTextPosition;
            _textSize = textSize;
            _textColor = textColor;

            _titleText = "Game Paused";

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
            _pixelAlfabet.DrawTextCentered(spriteBatch, _titleText, _textTexture, _centeredTextPosition, _textSize,
                _textColor);
        }

        public void Handle(KeyPressed message)
        {
            _eventAggregator.PublishOnUIThread(new ResumeGame());
        }
    }
}
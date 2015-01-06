using Caliburn.Micro;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using PilesOfTiles.Core.Input.Keyboard.Messages;
using PilesOfTiles.HighScore;
using PilesOfTiles.View.Messages;

namespace PilesOfTiles.View
{
    public class GameEndedView : IView, IHandle<GameEnded>, IHandle<KeyPressed>
    {
        private IEventAggregator _eventAggregator;
        private Texture2D _textTexture;
        private int _textSize;
        private Color _textColor;

        private string _titleText;
        private Vector2 _titleTextPosition;

        private string _highScoreText;
        private Vector2 _highScoreTextPosition;

        private string _difficultyLevelText;
        private Vector2 _difficultyLevelPosition;

        private string _playerName;
        private Vector2 _playerNamePosition;

        private PixelAlfabet _pixelAlfabet;

        public GameEndedView(IEventAggregator eventAggregator, Texture2D textTexture, int textSize, Color textColor)
        {
            _eventAggregator = eventAggregator;
            _textTexture = textTexture;
            _textSize = textSize;
            _textColor = textColor;

            _titleTextPosition = new Vector2(50, 50);

            _highScoreText = "High score ";
            _highScoreTextPosition = new Vector2(50, 100);

            _difficultyLevelText = "Difficulty ";
            _difficultyLevelPosition = new Vector2(50, 150);

            _playerName = "";
            _playerNamePosition = new Vector2(50, 300);

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

            _pixelAlfabet.DrawText(spriteBatch, _highScoreText, _textTexture, _highScoreTextPosition, _textSize,
    _textColor);

            _pixelAlfabet.DrawText(spriteBatch, _difficultyLevelText, _textTexture, _difficultyLevelPosition, _textSize,
    _textColor);

            _pixelAlfabet.DrawText(spriteBatch, _playerName, _textTexture, _playerNamePosition, _textSize,
    _textColor);
        }

        public void Handle(GameEnded message)
        {
            _titleText = message.CauseBy;
            _highScoreText += message.Score;
            _difficultyLevelText += message.DifficultyLevel;

        }

        public void Handle(KeyPressed message)
        {
            if (message.Key == Keys.Enter)
            {
                _eventAggregator.PublishOnUIThread(new ShowHighScoreBoard());
                return;
            }

            if (_playerName.Length > 0 && message.Key == Keys.Back)
            {
                _playerName = _playerName.Substring(0, _playerName.Length - 1);
                return;
            }

            if (message.Key.ToString().Length == 1)
            {
                _playerName += message.Key.ToString();
            }
        }
    }
}
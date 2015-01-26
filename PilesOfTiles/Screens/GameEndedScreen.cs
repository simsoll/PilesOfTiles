using Caliburn.Micro;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using PilesOfTiles.Core.Input.Keyboard.Messages;
using PilesOfTiles.HighScore;
using PilesOfTiles.Screens.Messages;
using PilesOfTiles.UserInterfaces;

namespace PilesOfTiles.Screens
{
    public class GameEndedScreen : IScreen, IHandle<GameEnded>, IHandle<KeyPressed>
    {
        private IEventAggregator _eventAggregator;
        private readonly IHighScoreRepository _highScoreRepository;
        private Texture2D _textTexture;
        private int _textSize;
        private Color _textColor;

        private float _score;
        private int _difficultyLevel;

        private string _titleText;
        private Vector2 _titleTextPosition;

        private string _highScoreText;
        private Vector2 _highScoreTextPosition;

        private string _difficultyLevelText;
        private Vector2 _difficultyLevelPosition;

        private string _actionDescription;
        private Vector2 _actionDescriptionPosition;

        private string _playerName;
        private string _defaultPlayerName;
        private Vector2 _playerNamePosition;

        private PixelAlfabet _pixelAlfabet;

        public GameEndedScreen(IEventAggregator eventAggregator, IHighScoreRepository highScoreRepository, Texture2D textTexture, Vector2 centeredTextPosition, int textSize, Color textColor)
        {
            _eventAggregator = eventAggregator;
            _highScoreRepository = highScoreRepository;
            _textTexture = textTexture;
            _textSize = textSize;
            _textColor = textColor;

            _titleTextPosition = centeredTextPosition + new Vector2(0, -100);

            _highScoreTextPosition = centeredTextPosition + new Vector2(0, 0);

            _difficultyLevelPosition = centeredTextPosition + new Vector2(0, 50);

            _actionDescriptionPosition = centeredTextPosition + new Vector2(0, 100);

            _playerNamePosition = centeredTextPosition + new Vector2(0, 125);

            _pixelAlfabet = new PixelAlfabet();
            _defaultPlayerName = "Anonymous";

            ResetShownText();
        }

        private void ResetShownText()
        {
            _playerName = "";
            _highScoreText = "High score ";    
            _difficultyLevelText = "Difficulty ";
            _actionDescription = "Enter player name...";
        }

        public void Load()
        {
            _eventAggregator.Subscribe(this);
        }

        public void Unload()
        {
            _eventAggregator.Unsubscribe(this);
            ResetShownText();
        }

        public void Update(GameTime gameTime)
        {
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            _pixelAlfabet.DrawTextCentered(spriteBatch, _titleText, _textTexture, _titleTextPosition, _textSize,
                _textColor);

            _pixelAlfabet.DrawTextCentered(spriteBatch, _highScoreText, _textTexture, _highScoreTextPosition, _textSize,
    _textColor);

            _pixelAlfabet.DrawTextCentered(spriteBatch, _difficultyLevelText, _textTexture, _difficultyLevelPosition, _textSize,
    _textColor);

            _pixelAlfabet.DrawTextCentered(spriteBatch, _actionDescription, _textTexture, _actionDescriptionPosition, _textSize,
    _textColor);

            _pixelAlfabet.DrawTextCentered(spriteBatch, _playerName, _textTexture, _playerNamePosition, _textSize,
    _textColor);
        }

        public void Handle(GameEnded message)
        {
            _score = message.Score;
            _difficultyLevel = message.DifficultyLevel;

            _titleText = message.CauseBy;
            _highScoreText += message.Score;
            _difficultyLevelText += message.DifficultyLevel;

        }

        public void Handle(KeyPressed message)
        {
            if (message.Key == Keys.Enter)
            {
                SaveHighScore();
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

        private void SaveHighScore()
        {
            _highScoreRepository.Store(new HighScore.HighScore
            {
                PlayerName = _playerName == "" ? _defaultPlayerName: _playerName,
                Score = _score,
                DifficultyLevel = _difficultyLevel
            });
        }
    }
}
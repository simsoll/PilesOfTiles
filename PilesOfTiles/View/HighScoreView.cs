using System.Linq;
using Caliburn.Micro;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using PilesOfTiles.Core.Input.Keyboard.Messages;
using PilesOfTiles.HighScore;
using PilesOfTiles.View.Messages;

namespace PilesOfTiles.View
{
    public class HighScoreView : IView, IHandle<KeyPressed>
    {
        private IEventAggregator _eventAggregator;
        private Texture2D _textTexture;
        private int _textSize;
        private Color _textColor;

        private Vector2 _highScoreStartPosition;
        private Vector2 _highScoreStepPosition;

        private PixelAlfabet _pixelAlfabet;

        public HighScoreView(IEventAggregator eventAggregator, Texture2D textTexture, int textSize, Color textColor)
        {
            _eventAggregator = eventAggregator;
            _textTexture = textTexture;
            _textSize = textSize;
            _textColor = textColor;

            _pixelAlfabet = new PixelAlfabet();

            _highScoreStartPosition = new Vector2(50,50);
            _highScoreStepPosition = new Vector2(0, 20);
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
            var highScores = HighScoreRepository.Instance.GetAllHighScores()
                .OrderByDescending(x => x.Score)
                .ThenByDescending(x => x.DifficultyLevel);

            var basePosition = _highScoreStartPosition;

            foreach (var highScore in highScores)
            {
                var text = string.Format("Score {0} Difficulty {1} Player {2}", highScore.Score,
                    highScore.DifficultyLevel, highScore.PlayerName);

                _pixelAlfabet.DrawText(spriteBatch, text, _textTexture, basePosition, _textSize,
                    _textColor);

                basePosition += _highScoreStepPosition;
            }
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
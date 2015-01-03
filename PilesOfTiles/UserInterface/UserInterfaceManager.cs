using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Caliburn.Micro;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using PilesOfTiles.HighScore;
using PilesOfTiles.HighScore.Messages;
using PilesOfTiles.Level.Messages;

namespace PilesOfTiles.UserInterface
{
    public class UserInterfaceManager : IHandle<ScoreUpdated>, IHandle<DifficultyLevelChanged>
    {
        private IEventAggregator _eventAggregator;
        private readonly Vector2 _statisticsPosition;
        private readonly int _tileSize;
        private Texture2D _textTexture;
        private readonly int _textSize;
        private readonly Color _highScoreTextColor;
        private PixelAlfabet _pixelAlfabet;
        private float _score;
        private int _difficultyLevel;

        public UserInterfaceManager(IEventAggregator eventAggregator, Vector2 statisticsPosition, int tileSize, Texture2D textTexture, int textSize, Color highScoreTextColor)
        {
            _eventAggregator = eventAggregator;
            _eventAggregator.Subscribe(this);

            _statisticsPosition = statisticsPosition;
            _tileSize = tileSize;
            _textTexture = textTexture;
            _textSize = textSize;
            _highScoreTextColor = highScoreTextColor;
            _pixelAlfabet = new PixelAlfabet();
            _score = 0;
            _difficultyLevel = 1;
        }

        public void Handle(ScoreUpdated message)
        {
            _score = message.Score;
        }

        public void Handle(DifficultyLevelChanged message)
        {
            _difficultyLevel = message.Value;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            DrawStatistic(spriteBatch, _textTexture, "highscore", (int) _score, Vector2.Zero);
            DrawStatistic(spriteBatch, _textTexture, "difficulty", _difficultyLevel, new Vector2(0, _pixelAlfabet.Height));
        }

        public void DrawStatistic(SpriteBatch spriteBatch, Texture2D texture, string name, int value, Vector2 offset)
        {
            var basePosition = _statisticsPosition * _tileSize + offset;
            var baseValue = "0000000" + value;
            var baseText = baseValue.Substring(baseValue.Length - 7) + " " + name;
            var text = baseText.Substring(0);

            foreach (var number in text.ToArray())
            {
                var positionMap = _pixelAlfabet.GetVectorMap(number.ToString());
                foreach (var pixelPosition in positionMap)
                {
                    spriteBatch.Draw(texture, basePosition + pixelPosition*_textSize, _highScoreTextColor);
                }

                basePosition += new Vector2(_pixelAlfabet.Width, 0)*_textSize;
            }
        }
    }
}

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
using PilesOfTiles.Manager;
using PilesOfTiles.View.Messages;

namespace PilesOfTiles.UserInterface
{
    public class UserInterfaceManager : IManager, IHandle<GameStarted>, IHandle<ScoreUpdated>, IHandle<DifficultyLevelChanged>
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

        public UserInterfaceManager(IEventAggregator eventAggregator, Vector2 statisticsPosition, int tileSize,
            Texture2D textTexture, int textSize, Color highScoreTextColor)
        {
            _eventAggregator = eventAggregator;

            _statisticsPosition = statisticsPosition;
            _tileSize = tileSize;
            _textTexture = textTexture;
            _textSize = textSize;
            _highScoreTextColor = highScoreTextColor;
            _pixelAlfabet = new PixelAlfabet();
        }

        public void Handle(ScoreUpdated message)
        {
            _score = message.Score;
        }

        public void Handle(DifficultyLevelChanged message)
        {
            _difficultyLevel = message.Value;
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
            DrawStatistic(spriteBatch, _textTexture, "highscore", (int) _score, Vector2.Zero);
            DrawStatistic(spriteBatch, _textTexture, "difficulty", _difficultyLevel,
                new Vector2(0, _pixelAlfabet.Height));
        }

        public void DrawStatistic(SpriteBatch spriteBatch, Texture2D texture, string name, int value, Vector2 offset)
        {
            var position = _statisticsPosition*_tileSize + offset;
            var baseValue = "0000000" + value;
            var baseText = baseValue.Substring(baseValue.Length - 7) + " " + name;
            var text = baseText.Substring(0);

            _pixelAlfabet.DrawText(spriteBatch, text, _textTexture, position, _textSize, _highScoreTextColor);
        }

        public void Handle(GameStarted message)
        {
            _score = 0;
            _difficultyLevel = 1;
        }
    }
}

using Caliburn.Micro;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using PilesOfTiles.Core;
using PilesOfTiles.HighScore.Messages;
using PilesOfTiles.Levels.Messages;
using PilesOfTiles.Screens.Messages;
using IDrawable = PilesOfTiles.Core.IDrawable;

namespace PilesOfTiles.UserInterfaces
{
    public class UserInterfaceService : IController, IDrawable, IHandle<ScoreUpdated>, IHandle<DifficultyLevelChanged>
    {
        private IEventAggregator _eventAggregator;
        private readonly Vector2 _statisticsPosition;
        private Texture2D _textTexture;
        private readonly int _textSize;
        private readonly Color _highScoreTextColor;
        private PixelAlfabet _pixelAlfabet;
        private float _score;
        private int _difficultyLevel;

        public UserInterfaceService(IEventAggregator eventAggregator, Vector2 statisticsPosition,
            Texture2D textTexture, int textSize, Color highScoreTextColor)
        {
            _eventAggregator = eventAggregator;

            _statisticsPosition = statisticsPosition;
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

        public void Draw(SpriteBatch spriteBatch)
        {
            DrawText(spriteBatch, _textTexture, "highscore", Vector2.Zero);
            DrawText(spriteBatch, _textTexture, _score.ToString(), new Vector2(0, _pixelAlfabet.Height * 2));
            DrawText(spriteBatch, _textTexture, "difficulty", new Vector2(0, _pixelAlfabet.Height * 4));
            DrawText(spriteBatch, _textTexture, _difficultyLevel.ToString(), new Vector2(0, _pixelAlfabet.Height * 6));
        }

        public void DrawText(SpriteBatch spriteBatch, Texture2D texture, string text, Vector2 offset)
        {
            var position = _statisticsPosition*_textSize + offset -
                           new Vector2(_pixelAlfabet.TextPixelPlot(text).Width/2.0f, 0)*_textSize;
            _pixelAlfabet.DrawText(spriteBatch, text, _textTexture, position, _textSize, _highScoreTextColor);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Caliburn.Micro;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using PilesOfTiles.HighScore;
using PilesOfTiles.HighScore.Messages;

namespace PilesOfTiles.UserInterface
{
    public class UserInterfaceManager : IHandle<ScoreUpdated>
    {
        private IEventAggregator _eventAggregator;
        private readonly int _highScoreTileSize;
        private readonly Color _highScoreTextColor;
        private PixelAlfabet _pixelAlfabet;
        private float _score;

        public UserInterfaceManager(IEventAggregator eventAggregator, int highScoreTileSize, Color highScoreTextColor)
        {
            _eventAggregator = eventAggregator;
            _eventAggregator.Subscribe(this);

            _highScoreTileSize = highScoreTileSize;
            _highScoreTextColor = highScoreTextColor;
            _pixelAlfabet = new PixelAlfabet();
            _score = 0;
        }

        public void Handle(ScoreUpdated message)
        {
            _score = message.Score;
        }

        public void Draw(SpriteBatch spriteBatch, Texture2D texture, Vector2 position)
        {
            var basePosition = position;

            foreach (var number in _score.ToString().ToArray())
            {
                var positionMap = _pixelAlfabet.VectorMap[number.ToString()];
                foreach (var pixelPosition in positionMap)
                {
                    spriteBatch.Draw(texture, (basePosition + pixelPosition) * _highScoreTileSize, _highScoreTextColor);

                }

                basePosition += new Vector2(_pixelAlfabet.Width, 0);
            }
        }
    }
}

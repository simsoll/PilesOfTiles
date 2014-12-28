using System;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using Caliburn.Micro;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using PilesOfTiles.HighScore.Messages;
using PilesOfTiles.Level.Messages;

namespace PilesOfTiles.HighScore
{
    public class HighScoreManager : IHandle<RowCleared>, IHandle<DifficultyLevelChanged>
    {
        private IEventAggregator _eventAggregator;

        private float _score;
        private int _rowsCleared;
        private float _rowClearedScore;
        private float _comboMultiplierDelta;
        private float _difficultyLevelMultiplier;
        private readonly float _difficultyLevelMultiplierDelta;

        public HighScoreManager(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;
            _eventAggregator.Subscribe(this);
            
            _score = 0.0f;
            _rowsCleared = 0;
            _rowClearedScore = 10.0f;
            _comboMultiplierDelta = 0.5f;
            _difficultyLevelMultiplier = 0.0f;
            _difficultyLevelMultiplierDelta = 0.1f;
        }

        public void Handle(RowCleared message)
        {
            _rowsCleared++;
        }

        public void Handle(DifficultyLevelChanged message)
        {
            _difficultyLevelMultiplier += _difficultyLevelMultiplierDelta;
        }

        public void Update(GameTime gameTime)
        {
            if (_rowsCleared <= 0) return;

            var comboMultiplier = _comboMultiplierDelta*(_rowsCleared - 1);

            _score += _rowsCleared * _rowClearedScore * (1 + _difficultyLevelMultiplier + comboMultiplier);
            _eventAggregator.PublishOnUIThread(new ScoreUpdated
            {
                Score = _score
            });

            _rowsCleared = 0;
        }
    }
}

using System;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using Caliburn.Micro;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using PilesOfTiles.Collision.Messages;
using PilesOfTiles.Core;
using PilesOfTiles.HighScore.Messages;
using PilesOfTiles.Level.Messages;
using PilesOfTiles.Screen.Messages;
using GameEnded = PilesOfTiles.HighScore.Messages.GameEnded;
using GameOver = PilesOfTiles.Collision.Messages.GameOver;

namespace PilesOfTiles.HighScore
{
    public class HighScoreService : IController, IUpdatable, IHandle<GameStarted>, IHandle<BrickCollided>, IHandle<RowCleared>, IHandle<DifficultyLevelChanged>, IHandle<GameOver>, IHandle<GameCompleted>
    {
        private IEventAggregator _eventAggregator;

        private float _score;
        private int _rowsCleared;
        private int _difficultyLevel;
        private float _brickCollidedScore;
        private float _rowClearedScore;
        private float _comboMultiplierDelta;
        private float _difficultyLevelMultiplier;
        private readonly float _difficultyLevelMultiplierDelta;

        public HighScoreService(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;
            
            _brickCollidedScore = 1.0f;
            _rowClearedScore = 10.0f;
            _comboMultiplierDelta = 0.5f;
            _difficultyLevelMultiplier = 0.0f;
            _difficultyLevelMultiplierDelta = 0.1f;
        }

        private void InitializeScoreFields()
        {
            _score = 0.0f;
            _rowsCleared = 0;
            _difficultyLevel = 1;
        }

        public void Handle(BrickCollided message)
        {
            UpdateScoreWith(_brickCollidedScore * _difficultyLevel);
        }

        public void Handle(RowCleared message)
        {
            _rowsCleared++;
        }

        public void Handle(DifficultyLevelChanged message)
        {
            _difficultyLevel = message.Value;
            _difficultyLevelMultiplier += _difficultyLevelMultiplierDelta;
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
            if (_rowsCleared <= 0) return;

            var comboMultiplier = _comboMultiplierDelta*(_rowsCleared - 1);
            var points = _rowsCleared*_rowClearedScore*(1 + _difficultyLevelMultiplier + comboMultiplier);

            UpdateScoreWith(points);

            _rowsCleared = 0;
        }

        public void UpdateScoreWith(float points)
        {
            _score += points;
            _eventAggregator.PublishOnUIThread(new ScoreUpdated
            {
                Score = _score
            });
        }

        public void Draw(SpriteBatch spriteBatch)
        {
        }

        public void Handle(GameOver message)
        {
            PublishGameEndedEvent("Game Over");
        }

        public void Handle(GameCompleted message)
        {
            PublishGameEndedEvent("Game Completed");
        }

        private void PublishGameEndedEvent(string causedBy)
        {
            _eventAggregator.PublishOnUIThread(new GameEnded
            {
                CausedBy = causedBy,
                Score = _score,
                DifficultyLevel = _difficultyLevel
            });
        }

        public void Handle(GameStarted message)
        {
            InitializeScoreFields();
        }
    }
}

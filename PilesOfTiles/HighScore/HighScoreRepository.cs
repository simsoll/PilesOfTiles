using System;
using System.Collections.Generic;
using System.Deployment.Internal;
using System.Linq;
using System.Text;
using OpenTK.Graphics.OpenGL;

namespace PilesOfTiles.HighScore
{
    public class HighScoreRepository
    {
        private readonly IList<HighScore> _repository;

        private HighScoreRepository()
        {
            _repository = new List<HighScore>();
        }

        private static HighScoreRepository highScoreRepository;

        public static HighScoreRepository Instance
        {
            get { return highScoreRepository ?? (highScoreRepository = new HighScoreRepository()); }
        }

        public void StoreHighScore(HighScore highScore)
        {
            _repository.Add(highScore);
        }

        public IList<HighScore> GetAllHighScores()
        {
            return _repository;
        }

        public class HighScore
        {
            public string PlayerName { get; set; }
            public float Score { get; set; }
            public int DifficultyLevel { get; set; }
        }
    }


}

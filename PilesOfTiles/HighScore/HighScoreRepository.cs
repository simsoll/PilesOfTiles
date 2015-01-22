using System;
using System.Collections.Generic;
using System.Deployment.Internal;
using System.IO;
using System.Linq;
using System.Text;
using OpenTK.Graphics.OpenGL;

namespace PilesOfTiles.HighScore
{
    public class HighScoreRepository : IHighScoreRepository
    {
        private readonly string _highScoreFilePath;

        public HighScoreRepository(string highScoreFilePath)
        {
            _highScoreFilePath = highScoreFilePath;

            if (!File.Exists(_highScoreFilePath))
            {
                File.Create(_highScoreFilePath);
            }
        }

        public void Store(HighScore highScore)
        {
            File.AppendAllText(_highScoreFilePath, highScore + Environment.NewLine);
        }

        public IEnumerable<HighScore> GetAllHighScores()
        {
            return File.ReadLines(_highScoreFilePath)
                .Select(line => line.Split(';'))
                .Select(highScoreAttributes => new HighScore
                {
                    PlayerName = highScoreAttributes[0],
                    Score = Convert.ToSingle(highScoreAttributes[1]),
                    DifficultyLevel = Convert.ToInt32(highScoreAttributes[2])
                }).ToList();
        }
    }
}
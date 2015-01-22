using System.Collections.Generic;

namespace PilesOfTiles.HighScore
{
    public interface IHighScoreRepository
    {
        void Store(HighScore highScore);
        IEnumerable<HighScore> GetAllHighScores();
    }
}
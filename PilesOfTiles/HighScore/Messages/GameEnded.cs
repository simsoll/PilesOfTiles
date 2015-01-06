using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PilesOfTiles.HighScore.Messages
{
    public class GameEnded
    {
        public string CausedBy { get; set; }
        public float Score { get; set; }
        public int DifficultyLevel { get; set; }
    }
}

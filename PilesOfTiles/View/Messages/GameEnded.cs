using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PilesOfTiles.View.Messages
{
    public class GameEnded
    {
        public string CauseBy { get; set; }
        public float Score { get; set; }
        public int DifficultyLevel { get; set; }
    }
}

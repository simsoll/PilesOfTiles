using PilesOfTiles.Bricks;
using PilesOfTiles.Levels;

namespace PilesOfTiles.Collision.Messages
{
    public class BrickCorrected
    {
        public Brick Brick { get; set; }
        public Level Level { get; set; }
    }
}
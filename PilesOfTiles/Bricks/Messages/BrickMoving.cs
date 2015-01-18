using PilesOfTiles.Input.Messages;

namespace PilesOfTiles.Bricks.Messages
{
    public class BrickMoving
    {
        public Brick Brick { get; set; }
        public Action Action { get; set; }
    }
}

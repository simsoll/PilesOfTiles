using Action = PilesOfTiles.Input.Messages.Action;

namespace PilesOfTiles.Brick.Messages
{
    public class BrickMoving
    {
        public Brick Brick { get; set; }
        public Action Action { get; set; }
    }
}

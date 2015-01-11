using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Action = PilesOfTiles.Input.Messages.Action;

namespace PilesOfTiles.Brick.Messages
{
    public class BrickMoving
    {
        public Vector2 Position { get; set; }
        public Action Action { get; set; }
        public Direction PointsAt { get; set; }
        public IEnumerable<Tile> Tiles { get; set; }
    }
}

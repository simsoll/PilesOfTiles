using System.Collections.Generic;
using Microsoft.Xna.Framework;
using PilesOfTiles.Input.Messages;

namespace PilesOfTiles.Brick.Messages
{
    public class BrickCreated
    {
        public Vector2 Position { get; set; }
        public Direction PointsAt { get; set; }
        public IEnumerable<Tile> Tiles { get; set; }
    }
}
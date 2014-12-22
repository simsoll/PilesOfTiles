using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PilesOfTiles.Collision.Messages
{
    public class BrickCollided
    {
        public IEnumerable<Tile> Tiles { get; set; } 
    }
}

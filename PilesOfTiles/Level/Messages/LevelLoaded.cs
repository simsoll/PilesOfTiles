using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace PilesOfTiles.Level.Messages
{
    public class LevelLoaded
    {
        public Vector2 Position { get; set; }
        public IEnumerable<Tile> Tiles { get; set; }
    }
}

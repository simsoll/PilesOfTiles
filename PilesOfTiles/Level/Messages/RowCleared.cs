using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace PilesOfTiles.Level.Messages
{
    public class RowCleared
    {
        public IEnumerable<Tile> Tiles { get; set; }
    }
}
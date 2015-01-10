using System.Collections.Generic;

namespace PilesOfTiles.Level.Messages
{
    public class LevelUpdated
    {
        public IEnumerable<Tile> Tiles { get; set; }
    }
}
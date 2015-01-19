using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace PilesOfTiles.UserInterfaces
{
    public class PixelPlot
    {
        public int Width { get; set; }
        public int Height { get; set; }
        public IEnumerable<Vector2> Pixels { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace PilesOfTiles.Collision.Messages
{
    public class BrickCollided
    {
        public Brick.Brick Brick { get; set; }
        public Vector2 Correction { get; set; }
    }
}

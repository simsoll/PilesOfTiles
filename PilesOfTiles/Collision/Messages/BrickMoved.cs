using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using PilesOfTiles.Brick;

namespace PilesOfTiles.Collision.Messages
{
    public class BrickMoved
    {
        public Brick.Brick Brick { get; set; }
    }
}

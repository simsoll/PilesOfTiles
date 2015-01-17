using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace PilesOfTiles.Collision
{
    public interface ICollidableBox
    {
        Rectangle BoundedBox { get; set; }
    }
}

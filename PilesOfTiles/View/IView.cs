﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace PilesOfTiles.Screen
{
    public interface IView
    {
        void Load();
        void Unload();
        void Update(GameTime gameTime);
        void Draw(SpriteBatch spriteBatch);
    }
}

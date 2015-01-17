using Microsoft.Xna.Framework.Graphics;

namespace PilesOfTiles.Core
{
    public interface IDrawable
    {
        //int DrawOrder { get; set; }
        void Draw(SpriteBatch spriteBatch);
    }
}
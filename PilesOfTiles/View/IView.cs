using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace PilesOfTiles.View
{
    public interface IView
    {
        void Load();
        void Unload();
        void Update(GameTime gameTime);
        void Draw(SpriteBatch spriteBatch);
    }
}

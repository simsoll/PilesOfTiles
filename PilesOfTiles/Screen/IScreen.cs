using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace PilesOfTiles.Screen
{
    public interface IScreen
    {
        void Load();
        void Unload();
        void Update(GameTime gameTime);
        void Draw(SpriteBatch spriteBatch);
    }
}

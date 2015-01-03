using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace PilesOfTiles.Manager
{
    public interface IManager
    {
        void Load();
        void Unload();
        void Update(GameTime gameTime);
        void Draw(SpriteBatch spriteBatch);
    }
}
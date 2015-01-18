using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace PilesOfTiles.Tiles
{
    public interface ITile
    {
        void Draw(SpriteBatch spriteBatch, Texture2D texture, int tileSize);
        void Set(Vector2 position);

        Vector2 Position();
        Color Color();
        State State();
    }
}
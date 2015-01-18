using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace PilesOfTiles.Tiles
{
    public interface ITile
    {
        void Update(GameTime gameTime);

        Vector2 Position { get; set; }
        Color Color { get; }
        State State { get; }
    }
}
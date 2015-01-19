using Microsoft.Xna.Framework;

namespace PilesOfTiles.Tiles
{
    public interface ITile
    {
        Vector2 Position { get; set; }
        Color Color { get; }
        State State { get; }

        void Update(GameTime gameTime);
    }
}
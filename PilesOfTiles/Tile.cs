using System.Dynamic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace PilesOfTiles
{
    public class Tile
    {
        public Tile(Vector2 position, Color color)
        {
            Color = color;
            Position = position;
        }

        public Vector2 Position { get; private set; }
        public Color Color { get; private set; }

        public Tile Add(Vector2 position)
        {
            return new Tile(Position + position, Color);
        }

        public void Draw(SpriteBatch spriteBatch, Texture2D texture, int tileSize)
        {
            spriteBatch.Draw(
                    texture,
                    new Rectangle((int) Position.X * tileSize, (int) Position.Y * tileSize, tileSize, tileSize),
                    new Rectangle(0, 0, tileSize, tileSize),
                    Color);
        }

        public static Tile Create(Vector2 position, Color color)
        {
            return new Tile(position, color);
        }
    }
}
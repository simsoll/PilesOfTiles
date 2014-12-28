using System.Dynamic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace PilesOfTiles
{
    public class Tile
    {
        public Tile(Vector2 position, Color color, State state)
        {
            Color = color;
            Position = position;
            State = state;
        }

        public Vector2 Position { get; private set; }
        public Color Color { get; private set; }
        public State State { get; private set; }

        public Tile Add(Vector2 position)
        {
            return new Tile(Position + position, Color, State);
        }

        public void Draw(SpriteBatch spriteBatch, Texture2D texture, int tileSize)
        {
            spriteBatch.Draw(texture, Position*tileSize, Color);
        }

        public static Tile Create(Vector2 position, Color color, State state)
        {
            return new Tile(position, color, state);
        }
    }

    public enum State
    {
        Solid,
        Removable
    }
}
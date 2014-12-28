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
            var x = (int)position.X;
            var y = (int)position.Y;
            var correctedPosition = new Vector2(x, y);
            return new Tile(Position + correctedPosition, Color, State);
        }

        public void Draw(SpriteBatch spriteBatch, Texture2D texture, int tileSize)
        {
            spriteBatch.Draw(texture, Position*tileSize, Color);
        }

        public static Tile Create(Vector2 position, Color color, State state)
        {
            var x = (int) position.X;
            var y = (int) position.Y;
            var correctedPosition = new Vector2(x, y);
            return new Tile(correctedPosition, color, state);
        }
    }

    public enum State
    {
        Solid,
        Removable
    }
}
using System.Diagnostics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace PilesOfTiles.Tiles
{
    public class Tile : ITile
    {
        private Vector2 _position;

        public Vector2 Position
        {
            get { return _position; }
            set { _position = VectorWithWholeIndices(value); }
        }

        public Color Color { get; private set; }
        public State State { get; private set; }


        public Tile(Vector2 position, Color color, State state)
        {
            Position = position;
            Color = color;
            State = state;
        }

        public void Update(GameTime gameTime)
        {
        }

        private Vector2 VectorWithWholeIndices(Vector2 vector)
        {
            return new Vector2((int)vector.X, (int)vector.Y);
        }
    }

    public enum State
    {
        Solid,
        Removable
    }
}
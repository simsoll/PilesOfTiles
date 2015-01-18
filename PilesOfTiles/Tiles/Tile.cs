using System.Diagnostics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace PilesOfTiles.Tiles
{
    public class Tile : ITile
    {
        private Vector2 _position;
        private readonly Color _color;
        private readonly State _state;


        public Tile(Vector2 position, Color color, State state)
        {
            _position = CorrectToWholeIndices(position);
            _color = color;
            _state = state;
        }

        public void Set(Vector2 position)
        {
            _position = CorrectToWholeIndices(position);
        }

        public void Draw(SpriteBatch spriteBatch, Texture2D texture, int tileSize)
        {
            spriteBatch.Draw(texture, _position * tileSize, _color);
        }

        public Vector2 Position()
        {
            return _position;
        }

        public Color Color()
        {
            return _color;
        }

        public State State()
        {
            return _state;
        }

        private Vector2 CorrectToWholeIndices(Vector2 vector)
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
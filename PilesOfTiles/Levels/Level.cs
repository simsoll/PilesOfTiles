using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using PilesOfTiles.Tiles;

namespace PilesOfTiles.Levels
{
    public class Level
    {
        public Level(Vector2 position, int height, int width, Color wallColor)
        {
            Position = position;
            Height = height;
            Width = width;
            WallColor = wallColor;
            Tiles = AddWalls().ToList();
        }

        public Vector2 Position { get; private set; }
        public int Height { get; private set; }
        public int Width { get; private set; }
        public Color WallColor { get; private set; }
        public List<ITile> Tiles { get; private set; }

        public void AddTiles(IEnumerable<ITile> tiles)
        {
            Tiles.AddRange(tiles);
        }

        public void RemoveTile(ITile tile)
        {
            Tiles.Remove(tile);
        }

        public void ResetTiles(IEnumerable<ITile> tiles)
        {
            Tiles = tiles.ToList();
        }

        private IEnumerable<ITile> AddWalls()
        {
            //add bottom
            for (var x = 0; x < Width - 1; x++)
            {
                yield return new Tile(new Vector2(x, Height) + Position, WallColor, State.Solid);
            }

            //add sides
            for (var y = 0; y <= Height; y++)
            {
                yield return new Tile(new Vector2(0, y) + Position, WallColor, State.Solid);
                yield return new Tile(new Vector2(Width - 1, y) + Position, WallColor, State.Solid);
            }
        }
    }
}

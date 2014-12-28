﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace PilesOfTiles.Level
{
    public class Level
    {
        public Level(Vector2 position, int height, int width, int tileSize, Color wallColor)
        {
            Position = position;
            Height = height;
            Width = width;
            TileSize = tileSize;
            WallColor = wallColor;
            Tiles = AddWalls().ToList();
        }

        public Vector2 Position { get; private set; }
        public int Height { get; private set; }
        public int Width { get; private set; }
        public int TileSize { get; private set; }
        public Color WallColor { get; private set; }
        public List<Tile> Tiles { get; private set; }


        public void AddTiles(IEnumerable<Tile> tiles)
        {
            Tiles.AddRange(tiles);
        }

        public void RemoveTile(Tile tile)
        {
            Tiles.Remove(tile);
        }

        public void ResetTiles(IEnumerable<Tile> tiles)
        {
            Tiles = tiles.ToList();
        }

        private IEnumerable<Tile> AddWalls()
        {
            //add bottom
            for (var x = 0; x < Width - 1; x++)
            {
                yield return Tile.Create(new Vector2(x, Height) + Position, WallColor, State.Solid);
            }

            //add sides
            for (var y = 0; y <= Height; y++)
            {
                yield return Tile.Create(new Vector2(0, y) + Position, WallColor, State.Solid);
                yield return Tile.Create(new Vector2(Width - 1, y) + Position, WallColor, State.Solid);
            }
        }

        public void Draw(SpriteBatch spriteBatch, Texture2D texture)
        {
            foreach (var tile in Tiles)
            {
                tile.Draw(spriteBatch, texture, TileSize);
            }
        }
    }
}

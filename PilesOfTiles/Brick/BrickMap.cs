using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;

namespace PilesOfTiles.Brick
{
    public class BrickMap
    {
        public IDictionary<Direction, IEnumerable<Tile>> DirectionToTilesMapper { get; private set; }

        public BrickMap(IDictionary<Direction, IEnumerable<Tile>> directionToTilesMapper)
        {
            DirectionToTilesMapper = directionToTilesMapper;
        }

        public IEnumerable<Tile> GetTilesWhenPointingAt(Direction direction, Vector2 position)
        {
            if (!DirectionToTilesMapper.ContainsKey(direction))
            {
                throw new ArgumentException(string.Format("Direction {0} is not defined for this brick!", direction));
            }

            var tiles = DirectionToTilesMapper[direction]
                    .Select(tile => Tile.Create(tile.Position, tile.Color, tile.State))
                    .ToList();

            foreach (var tile in tiles)
            {
                tile.Add(position);
            }

            return tiles;
        }
    }
}
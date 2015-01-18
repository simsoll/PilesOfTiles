using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using PilesOfTiles.Tiles;

namespace PilesOfTiles.Bricks
{
    public class BrickMap
    {
        public IDictionary<Direction, IEnumerable<ITile>> DirectionToTilesMapper { get; private set; }

        public BrickMap(IDictionary<Direction, IEnumerable<ITile>> directionToTilesMapper)
        {
            DirectionToTilesMapper = directionToTilesMapper;
        }

        public IEnumerable<ITile> GetTilesWhenPointingAt(Direction direction, Vector2 position)
        {
            if (!DirectionToTilesMapper.ContainsKey(direction))
            {
                throw new ArgumentException(string.Format("Direction {0} is not defined for this brick!", direction));
            }

            var tiles = DirectionToTilesMapper[direction]
                    .Select(tile => new Tile(tile.Position, tile.Color, tile.State))
                    .ToList();

            foreach (var tile in tiles)
            {
                tile.Position += position;
            }

            return tiles;
        }
    }
}
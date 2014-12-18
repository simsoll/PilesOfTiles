using System;
using System.Collections.Generic;

namespace PilesOfTiles.Brick
{
    public class BrickMap
    {
        public IDictionary<Direction, IEnumerable<Tile>> DirectionToTilesMapper { get; private set; }

        public BrickMap(IDictionary<Direction, IEnumerable<Tile>> directionToTilesMapper)
        {
            DirectionToTilesMapper = directionToTilesMapper;
        }

        public IEnumerable<Tile> GetTilesWhenPointingAt(Direction direction)
        {
            if (!DirectionToTilesMapper.ContainsKey(direction))
            {
                throw new ArgumentException(string.Format("Direction {0} is not defined for this brick!", direction));
            }
            return DirectionToTilesMapper[direction];
        }
    }
}
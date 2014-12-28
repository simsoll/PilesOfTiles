using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace PilesOfTiles.HighScore
{
    public class PixelAlfabet
    {
        private readonly IDictionary<string, IEnumerable<Vector2>> _vectorMapDictionary;

        public int Width { get { return 7; } }
        public int Height { get { return 9; } }

        public PixelAlfabet()
        {
            _vectorMapDictionary = InitializePixelMap();
        }

        public IEnumerable<Vector2> GetVectorMap(string key)
        {
            return _vectorMapDictionary[key.ToUpper()];
        }

        private IDictionary<string, IEnumerable<Vector2>> InitializePixelMap()
        {
            return new Dictionary<string, IEnumerable<Vector2>>
            {
                {
                    " ",
                    new List<Vector2>()
                },
                {
                    "A",
                    new List<Vector2>
                    {
                        new Vector2(1, 3),
                        new Vector2(1, 4),
                        new Vector2(1, 5),
                        new Vector2(1, 6),
                        new Vector2(1, 7),
                        new Vector2(2, 2),
                        new Vector2(2, 5),
                        new Vector2(3, 1),
                        new Vector2(3, 5),
                        new Vector2(4, 2),
                        new Vector2(4, 5),
                        new Vector2(5, 3),
                        new Vector2(5, 4),
                        new Vector2(5, 5),
                        new Vector2(5, 6),
                        new Vector2(5, 7)
                    }
                },
                {
                    "B",
                    new List<Vector2>
                    {
                        new Vector2(1,1),
                        new Vector2(1,2),
                        new Vector2(1,3),
                        new Vector2(1,4),
                        new Vector2(1,5),
                        new Vector2(1,6),
                        new Vector2(1,7),
                        new Vector2(2,1),
                        new Vector2(2,4),
                        new Vector2(2,7),
                        new Vector2(3,1),
                        new Vector2(3,4),
                        new Vector2(3,7),
                        new Vector2(4,1),
                        new Vector2(4,4),
                        new Vector2(4,7),
                        new Vector2(5,2),
                        new Vector2(5,3),
                        new Vector2(5,5),
                        new Vector2(5,6)
                    }
                },
                {
                    "C",
                    new List<Vector2>
                    {
                        new Vector2(1,2),
                        new Vector2(1,3),
                        new Vector2(1,4),
                        new Vector2(1,5),
                        new Vector2(1,6),
                        new Vector2(2,1),
                        new Vector2(2,7),
                        new Vector2(3,1),
                        new Vector2(3,7),
                        new Vector2(4,1),
                        new Vector2(4,7),
                        new Vector2(5,2),
                        new Vector2(5,6)                        
                    }
                },
                {
                    "D",
                    new List<Vector2>
                    {
                        new Vector2(1,1),
                        new Vector2(1,2),
                        new Vector2(1,3),
                        new Vector2(1,4),
                        new Vector2(1,5),
                        new Vector2(1,6),
                        new Vector2(1,7),
                        new Vector2(2,1),
                        new Vector2(2,7),
                        new Vector2(3,1),
                        new Vector2(3,7),
                        new Vector2(4,1),
                        new Vector2(4,7),
                        new Vector2(5,2),
                        new Vector2(5,3),
                        new Vector2(5,4),
                        new Vector2(5,5),
                        new Vector2(5,6)
                    }
                },
                {
                    "E",
                    new List<Vector2>
                    {
                        new Vector2(1,1),
                        new Vector2(1,2),
                        new Vector2(1,3),
                        new Vector2(1,4),
                        new Vector2(1,5),
                        new Vector2(1,6),
                        new Vector2(1,7),
                        new Vector2(2,1),
                        new Vector2(2,4),
                        new Vector2(2,7),
                        new Vector2(3,1),
                        new Vector2(3,4),
                        new Vector2(3,7),
                        new Vector2(4,1),
                        new Vector2(4,4),
                        new Vector2(4,7),
                        new Vector2(5,1),
                        new Vector2(5,7)
                    }
                },
                {
                    "F",
                    new List<Vector2>
                    {
                        new Vector2(1,1),
                        new Vector2(1,2),
                        new Vector2(1,3),
                        new Vector2(1,4),
                        new Vector2(1,5),
                        new Vector2(1,6),
                        new Vector2(1,7),
                        new Vector2(2,1),
                        new Vector2(2,4),
                        new Vector2(3,1),
                        new Vector2(3,4),
                        new Vector2(4,1),
                        new Vector2(4,4),
                        new Vector2(5,1),
                    }
                },
                {
                    "G",
                    new List<Vector2>
                    {
                        new Vector2(1,2),
                        new Vector2(1,3),
                        new Vector2(1,4),
                        new Vector2(1,5),
                        new Vector2(1,6),
                        new Vector2(2,1),
                        new Vector2(2,7),
                        new Vector2(3,1),
                        new Vector2(3,7),
                        new Vector2(4,1),
                        new Vector2(4,4),
                        new Vector2(4,7),
                        new Vector2(5,2),
                        new Vector2(5,4),                        
                        new Vector2(5,5),                       
                        new Vector2(5,6)                        
                    }
                },
                {
                    "H",
                    new List<Vector2>
                    {
                        new Vector2(1,1),
                        new Vector2(1,2),
                        new Vector2(1,3),
                        new Vector2(1,4),
                        new Vector2(1,5),
                        new Vector2(1,6),
                        new Vector2(1,7),
                        new Vector2(2,4),
                        new Vector2(3,4),
                        new Vector2(4,4),
                        new Vector2(5,1),
                        new Vector2(5,2),
                        new Vector2(5,3),
                        new Vector2(5,4),
                        new Vector2(5,5),
                        new Vector2(5,6),
                        new Vector2(5,7)
                    }
                },
                {
                    "I",
                    new List<Vector2>
                    {
                        new Vector2(1,1),
                        new Vector2(1,7),
                        new Vector2(2,1),
                        new Vector2(2,7),
                        new Vector2(3,1),
                        new Vector2(3,2),
                        new Vector2(3,3),
                        new Vector2(3,4),
                        new Vector2(3,5),
                        new Vector2(3,6),
                        new Vector2(3,7),
                        new Vector2(4,1),
                        new Vector2(4,7),
                        new Vector2(5,1),
                        new Vector2(5,7)
                    }
                },
                {
                    "J",
                    new List<Vector2>
                    {
                        new Vector2(1,5),
                        new Vector2(1,6),
                        new Vector2(2,1),
                        new Vector2(2,7),
                        new Vector2(3,1),
                        new Vector2(3,7),
                        new Vector2(4,1),
                        new Vector2(4,7),
                        new Vector2(5,1),
                        new Vector2(5,2),
                        new Vector2(5,3),
                        new Vector2(5,4),
                        new Vector2(5,5),
                        new Vector2(5,6)
                    }
                },
                {
                   "K",
                   new List<Vector2>
                   {
                        new Vector2(1,1),
                        new Vector2(1,2),
                        new Vector2(1,3),
                        new Vector2(1,4),
                        new Vector2(1,5),
                        new Vector2(1,6),
                        new Vector2(1,7),
                        new Vector2(2,4),
                        new Vector2(3,3),
                        new Vector2(3,5),
                        new Vector2(4,2),
                        new Vector2(4,6),
                        new Vector2(5,1),
                        new Vector2(5,7),
                   }
                },
                {
                    "L",
                    new List<Vector2>
                    {
                        new Vector2(1,1),
                        new Vector2(1,2),
                        new Vector2(1,3),
                        new Vector2(1,4),
                        new Vector2(1,5),
                        new Vector2(1,6),
                        new Vector2(1,7),
                        new Vector2(2,7),
                        new Vector2(3,7),
                        new Vector2(4,7),
                        new Vector2(5,7),
                    }
                },
                {
                    "M",
                    new List<Vector2>
                    {
                        new Vector2(1,1),
                        new Vector2(1,2),
                        new Vector2(1,3),
                        new Vector2(1,4),
                        new Vector2(1,5),
                        new Vector2(1,6),
                        new Vector2(1,7),
                        new Vector2(2,2),
                        new Vector2(3,3),
                        new Vector2(4,2),
                        new Vector2(5,1),
                        new Vector2(5,2),
                        new Vector2(5,3),
                        new Vector2(5,4),
                        new Vector2(5,5),
                        new Vector2(5,6),
                        new Vector2(5,7)
                    }
                },
                {
                    "N",
                    new List<Vector2>
                    {
                        new Vector2(1,1),
                        new Vector2(1,2),
                        new Vector2(1,3),
                        new Vector2(1,4),
                        new Vector2(1,5),
                        new Vector2(1,6),
                        new Vector2(1,7),
                        new Vector2(2,3),
                        new Vector2(3,4),
                        new Vector2(4,5),
                        new Vector2(5,1),
                        new Vector2(5,2),
                        new Vector2(5,3),
                        new Vector2(5,4),
                        new Vector2(5,5),
                        new Vector2(5,6),
                        new Vector2(5,7)
                    }
                },
                {
                    "O",
                    new List<Vector2>
                    {
                        new Vector2(1,2),
                        new Vector2(1,3),
                        new Vector2(1,4),
                        new Vector2(1,5),
                        new Vector2(1,6),
                        new Vector2(2,1),
                        new Vector2(2,7),
                        new Vector2(3,1),
                        new Vector2(3,7),
                        new Vector2(4,1),
                        new Vector2(4,7),
                        new Vector2(5,2),
                        new Vector2(5,3),
                        new Vector2(5,4),
                        new Vector2(5,5),
                        new Vector2(5,6)                        
                    }
                },
                {
                    "P",
                    new List<Vector2>
                    {
                        new Vector2(1,1),
                        new Vector2(1,2),
                        new Vector2(1,3),
                        new Vector2(1,4),
                        new Vector2(1,5),
                        new Vector2(1,6),
                        new Vector2(1,7),
                        new Vector2(2,1),
                        new Vector2(2,4),
                        new Vector2(3,1),
                        new Vector2(3,4),
                        new Vector2(4,1),
                        new Vector2(4,4),
                        new Vector2(5,2),
                        new Vector2(5,3),
                    }
                },
                {
                    "Q",
                    new List<Vector2>
                    {
                        new Vector2(1,2),
                        new Vector2(1,3),
                        new Vector2(1,4),
                        new Vector2(1,5),
                        new Vector2(1,6),
                        new Vector2(2,1),
                        new Vector2(2,7),
                        new Vector2(3,1),
                        new Vector2(3,5),
                        new Vector2(3,7),
                        new Vector2(4,1),
                        new Vector2(4,6),
                        new Vector2(4,7),
                        new Vector2(5,2),
                        new Vector2(5,3),
                        new Vector2(5,4),
                        new Vector2(5,5),
                        new Vector2(5,6)                        
                    }
                },
                {
                    "R",
                    new List<Vector2>
                    {
                        new Vector2(1,1),
                        new Vector2(1,2),
                        new Vector2(1,3),
                        new Vector2(1,4),
                        new Vector2(1,5),
                        new Vector2(1,6),
                        new Vector2(1,7),
                        new Vector2(2,1),
                        new Vector2(2,4),
                        new Vector2(3,1),
                        new Vector2(3,4),
                        new Vector2(4,1),
                        new Vector2(4,4),
                        new Vector2(5,2),
                        new Vector2(5,3),
                        new Vector2(5,5),
                        new Vector2(5,6),
                        new Vector2(5,7),
                    }
                },
                {
                    "S",
                    new List<Vector2>
                    {
                        new Vector2(1,2),
                        new Vector2(1,3),
                        new Vector2(1,6),
                        new Vector2(2,1),
                        new Vector2(2,4),
                        new Vector2(2,7),
                        new Vector2(3,1),
                        new Vector2(3,4),
                        new Vector2(3,7),
                        new Vector2(4,1),
                        new Vector2(4,4),
                        new Vector2(4,7),
                        new Vector2(5,2),
                        new Vector2(5,5),
                        new Vector2(5,6)
                    }
                },
                {
                    "T",
                    new List<Vector2>
                    {
                        new Vector2(1,1),
                        new Vector2(2,1),
                        new Vector2(3,1),
                        new Vector2(3,2),
                        new Vector2(3,3),
                        new Vector2(3,4),
                        new Vector2(3,5),
                        new Vector2(3,6),
                        new Vector2(3,7),
                        new Vector2(4,1),
                        new Vector2(5,1),
                    }
                },
                {
                    "U",
                    new List<Vector2>
                    {
                        new Vector2(1,1),
                        new Vector2(1,2),
                        new Vector2(1,3),
                        new Vector2(1,4),
                        new Vector2(1,5),
                        new Vector2(1,6),
                        new Vector2(2,7),
                        new Vector2(3,7),
                        new Vector2(4,7),
                        new Vector2(5,1),
                        new Vector2(5,2),
                        new Vector2(5,3),
                        new Vector2(5,4),
                        new Vector2(5,5),
                        new Vector2(5,6)                        
                    }
                },
                {
                    "V",
                    new List<Vector2>
                    {
                        new Vector2(1,1),
                        new Vector2(1,2),
                        new Vector2(1,3),
                        new Vector2(1,4),
                        new Vector2(1,5),
                        new Vector2(1,6),
                        new Vector2(2,6),
                        new Vector2(3,7),
                        new Vector2(4,6),
                        new Vector2(5,1),
                        new Vector2(5,2),
                        new Vector2(5,3),
                        new Vector2(5,4),
                        new Vector2(5,5),
                        new Vector2(5,6)                        
                    }
                },
                {
                    "W",
                    new List<Vector2>
                    {
                        new Vector2(1,1),
                        new Vector2(1,2),
                        new Vector2(1,3),
                        new Vector2(1,4),
                        new Vector2(1,5),
                        new Vector2(1,6),
                        new Vector2(2,7),
                        new Vector2(3,5),
                        new Vector2(3,6),
                        new Vector2(4,7),
                        new Vector2(5,1),
                        new Vector2(5,2),
                        new Vector2(5,3),
                        new Vector2(5,4),
                        new Vector2(5,5),
                        new Vector2(5,6)                        
                    }
                },
                {
                    "X",
                    new List<Vector2>
                    {
                        new Vector2(1,1),
                        new Vector2(1,2),
                        new Vector2(1,6),
                        new Vector2(2,3),
                        new Vector2(2,5),
                        new Vector2(3,4),
                        new Vector2(4,3),
                        new Vector2(4,5),
                        new Vector2(5,1),
                        new Vector2(5,2),
                        new Vector2(5,6),                        
                        new Vector2(5,7),                       
                    }
                },
                {
                    "Y",
                    new List<Vector2>
                    {
                        new Vector2(1,1),
                        new Vector2(1,2),
                        new Vector2(1,3),
                        new Vector2(2,4),
                        new Vector2(3,5),
                        new Vector2(3,6),
                        new Vector2(3,7),
                        new Vector2(4,4),
                        new Vector2(5,1),
                        new Vector2(5,2),
                        new Vector2(5,3),
                    }
                },
                {
                    "Z",
                    new List<Vector2>
                    {
                        new Vector2(1,1),
                        new Vector2(1,6),
                        new Vector2(1,7),
                        new Vector2(2,1),
                        new Vector2(2,5),
                        new Vector2(2,7),
                        new Vector2(3,1),
                        new Vector2(3,4),
                        new Vector2(3,7),
                        new Vector2(4,1),
                        new Vector2(4,3),
                        new Vector2(4,7),
                        new Vector2(5,1),
                        new Vector2(5,2),
                        new Vector2(5,7),                       
                    }
                },
                {
                    "1",
                    new List<Vector2>
                    {
                        new Vector2(1,1),
                        new Vector2(1,7),
                        new Vector2(2,1),
                        new Vector2(2,7),
                        new Vector2(3,1),
                        new Vector2(3,2),
                        new Vector2(3,3),
                        new Vector2(3,4),
                        new Vector2(3,5),
                        new Vector2(3,6),
                        new Vector2(3,7),
                        new Vector2(4,7),
                        new Vector2(5,7)
                    }
                },
                {
                    "2",
                    new List<Vector2>
                    {
                        new Vector2(1,2),
                        new Vector2(1,7),
                        new Vector2(2,1),
                        new Vector2(2,6),
                        new Vector2(2,7),
                        new Vector2(3,1),
                        new Vector2(3,5),
                        new Vector2(3,7),
                        new Vector2(4,1),
                        new Vector2(4,4),
                        new Vector2(4,7),
                        new Vector2(5,2),
                        new Vector2(5,3),
                        new Vector2(5,7),                       
                    }
                },
                {
                    "3",
                    new List<Vector2>
                    {
                        new Vector2(1,2),
                        new Vector2(1,6),
                        new Vector2(2,1),
                        new Vector2(2,7),
                        new Vector2(3,1),
                        new Vector2(3,4),
                        new Vector2(3,7),
                        new Vector2(4,1),
                        new Vector2(4,4),
                        new Vector2(4,7),
                        new Vector2(5,2),
                        new Vector2(5,3),
                        new Vector2(5,5),
                        new Vector2(5,6)
                    }
                },
                {
                    "4",
                    new List<Vector2>
                    {
                        new Vector2(1,4),
                        new Vector2(1,5),
                        new Vector2(2,3),
                        new Vector2(2,5),
                        new Vector2(3,2),
                        new Vector2(3,5),
                        new Vector2(4,1),
                        new Vector2(4,2),
                        new Vector2(4,3),
                        new Vector2(4,4),
                        new Vector2(4,5),
                        new Vector2(4,6),
                        new Vector2(4,7),
                        new Vector2(5,5),
                    }
                },
                {
                    "5",
                    new List<Vector2>
                    {
                        new Vector2(1,1),
                        new Vector2(1,2),
                        new Vector2(1,3),
                        new Vector2(1,6),
                        new Vector2(2,1),
                        new Vector2(2,3),
                        new Vector2(2,7),
                        new Vector2(3,1),
                        new Vector2(3,3),
                        new Vector2(3,7),
                        new Vector2(4,1),
                        new Vector2(4,3),
                        new Vector2(4,7),
                        new Vector2(5,1),
                        new Vector2(5,4),
                        new Vector2(5,5),
                        new Vector2(5,6)
                    }
                },
                {
                    "6",
                    new List<Vector2>
                    {
                        new Vector2(1,2),
                        new Vector2(1,3),
                        new Vector2(1,4),
                        new Vector2(1,5),
                        new Vector2(1,6),
                        new Vector2(2,1),
                        new Vector2(2,4),
                        new Vector2(2,7),
                        new Vector2(3,1),
                        new Vector2(3,4),
                        new Vector2(3,7),
                        new Vector2(4,1),
                        new Vector2(4,4),
                        new Vector2(4,7),
                        new Vector2(5,2),
                        new Vector2(5,5),
                        new Vector2(5,6)
                    }
                },
                {
                    "7",
                    new List<Vector2>
                    {
                        new Vector2(1,1),
                        new Vector2(2,1),
                        new Vector2(3,1),
                        new Vector2(3,5),
                        new Vector2(3,6),
                        new Vector2(3,7),
                        new Vector2(4,1),
                        new Vector2(4,4),
                        new Vector2(5,1),
                        new Vector2(5,2),
                        new Vector2(5,3),
                    }
                },
                {
                    "8",
                    new List<Vector2>
                    {
                        new Vector2(1,2),
                        new Vector2(1,3),
                        new Vector2(1,5),
                        new Vector2(1,6),
                        new Vector2(2,1),
                        new Vector2(2,4),
                        new Vector2(2,7),
                        new Vector2(3,1),
                        new Vector2(3,4),
                        new Vector2(3,7),
                        new Vector2(4,1),
                        new Vector2(4,4),
                        new Vector2(4,7),
                        new Vector2(5,2),
                        new Vector2(5,3),
                        new Vector2(5,5),
                        new Vector2(5,6)
                    }
                },
                {
                    "9",
                    new List<Vector2>
                    {
                        new Vector2(1,2),
                        new Vector2(1,3),
                        new Vector2(1,6),
                        new Vector2(2,1),
                        new Vector2(2,4),
                        new Vector2(2,7),
                        new Vector2(3,1),
                        new Vector2(3,4),
                        new Vector2(3,7),
                        new Vector2(4,1),
                        new Vector2(4,4),
                        new Vector2(4,7),
                        new Vector2(5,2),
                        new Vector2(5,3),
                        new Vector2(5,4),
                        new Vector2(5,5),
                        new Vector2(5,6)
                    }
                },
                {
                    "0",
                    new List<Vector2>
                    {
                        new Vector2(1,2),
                        new Vector2(1,3),
                        new Vector2(1,4),
                        new Vector2(1,5),
                        new Vector2(1,6),
                        new Vector2(2,1),
                        new Vector2(2,7),
                        new Vector2(3,1),
                        new Vector2(3,7),
                        new Vector2(4,1),
                        new Vector2(4,7),
                        new Vector2(5,2),
                        new Vector2(5,3),
                        new Vector2(5,4),
                        new Vector2(5,5),
                        new Vector2(5,6)                        
                    }
                },
            };
        }
    }
}
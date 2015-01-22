using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace PilesOfTiles.UserInterfaces
{
    public class PixelAlfabet
    {
        private readonly IDictionary<string, PixelPlot> _vectorMapDictionary;

        public int Width
        {
            get { return 7; }
        }

        public int Height
        {
            get { return 9; }
        }

        public PixelAlfabet()
        {
            _vectorMapDictionary = InitializePixelMap();
        }

        public PixelPlot TextPixelPlot(string text)
        {
            var width = 0;
            var height = 0;
            var pixels = new List<Vector2>();

            foreach (
                var pixelPlot in
                    text.Select(letter => _vectorMapDictionary[letter.ToString(CultureInfo.InvariantCulture).ToUpperInvariant()]))
            {
                width += pixelPlot.Width;
                height = Math.Max(height, pixelPlot.Height);
                pixels.AddRange(pixelPlot.Pixels);
            }

            return new PixelPlot
            {
                Width = width,
                Height = height,
                Pixels = pixels
            };
        }

        public void DrawText(SpriteBatch spriteBatch, string text, Texture2D texture, Vector2 position, int size,
            Color color)
        {
            var basePosition = position;

            foreach (var number in text.ToArray())
            {
                var positionMap = GetPixelPlot(number.ToString(CultureInfo.InvariantCulture));
                foreach (var pixelPosition in positionMap.Pixels)
                {
                    spriteBatch.Draw(texture, basePosition + pixelPosition*size, color);
                }

                basePosition += new Vector2(Width, 0)*size;
            }
        }

        public void DrawTextCentered(SpriteBatch spriteBatch, string text, Texture2D texture, Vector2 position, int size,
            Color color)
        {
            var textPixelPlot = TextPixelPlot(text);
            var offSet = new Vector2(textPixelPlot.Width/2.0f, textPixelPlot.Height/2.0f);
            DrawText(spriteBatch, text, texture, (position - offSet)*size, size, color);
        }

        private PixelPlot GetPixelPlot(string key)
        {
            return _vectorMapDictionary[key.ToUpper()];
        }

        private IDictionary<string, PixelPlot> InitializePixelMap()
        {
            return new Dictionary<string, PixelPlot>
            {
                {
                    " ",
                    new PixelPlot
                    {
                        Width = 3,
                        Height = 9,
                        Pixels = new List<Vector2>()
                    }
                },
                {
                    "-",
                    new PixelPlot
                    {
                        Width = 5,
                        Height = 9,
                        Pixels = new List<Vector2>
                        {
                            new Vector2(1, 4),
                            new Vector2(2, 4),
                            new Vector2(3, 4)
                        }
                    }
                },
                {
                    ".",
                    new PixelPlot
                    {
                        Width = 3,
                        Height = 9,
                        Pixels = new List<Vector2>
                        {
                            new Vector2(1, 7)
                        }
                    }
                },
                {
                    ",",
                    new PixelPlot
                    {
                        Width = 4,
                        Height = 9,
                        Pixels = new List<Vector2>
                        {
                            new Vector2(1, 8),
                            new Vector2(2, 7)
                        }
                    }
                },
                {
                    "!",
                    new PixelPlot
                    {
                        Width = 3,
                        Height = 9,
                        Pixels = new List<Vector2>
                        {
                            new Vector2(1, 1),
                            new Vector2(1, 2),
                            new Vector2(1, 3),
                            new Vector2(1, 4),
                            new Vector2(1, 5),
                            new Vector2(1, 7),
                        }
                    }

                },
                {
                    "?",
                    new PixelPlot
                    {
                        Width = 7,
                        Height = 9,
                        Pixels = new List<Vector2>
                        {
                            new Vector2(1, 2),
                            new Vector2(2, 1),
                            new Vector2(3, 1),
                            new Vector2(3, 5),
                            new Vector2(3, 7),
                            new Vector2(4, 1),
                            new Vector2(4, 4),
                            new Vector2(5, 2),
                            new Vector2(5, 3),
                        }
                    }

                },
                {
                    "A",
                    new PixelPlot
                    {
                        Width = 7,
                        Height = 9,
                        Pixels = new List<Vector2>
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

                    }
                },
                {
                    "B",
                    new PixelPlot
                    {
                        Width = 7,
                        Height = 9,
                        Pixels = new List<Vector2>
                        {
                            new Vector2(1, 1),
                            new Vector2(1, 2),
                            new Vector2(1, 3),
                            new Vector2(1, 4),
                            new Vector2(1, 5),
                            new Vector2(1, 6),
                            new Vector2(1, 7),
                            new Vector2(2, 1),
                            new Vector2(2, 4),
                            new Vector2(2, 7),
                            new Vector2(3, 1),
                            new Vector2(3, 4),
                            new Vector2(3, 7),
                            new Vector2(4, 1),
                            new Vector2(4, 4),
                            new Vector2(4, 7),
                            new Vector2(5, 2),
                            new Vector2(5, 3),
                            new Vector2(5, 5),
                            new Vector2(5, 6)
                        }
                    }
                },
                {
                    "C",
                    new PixelPlot
                    {
                        Width = 7,
                        Height = 9,
                        Pixels = new List<Vector2>
                        {
                            new Vector2(1, 2),
                            new Vector2(1, 3),
                            new Vector2(1, 4),
                            new Vector2(1, 5),
                            new Vector2(1, 6),
                            new Vector2(2, 1),
                            new Vector2(2, 7),
                            new Vector2(3, 1),
                            new Vector2(3, 7),
                            new Vector2(4, 1),
                            new Vector2(4, 7),
                            new Vector2(5, 2),
                            new Vector2(5, 6)
                        }
                    }
                },
                {
                    "D",
                    new PixelPlot
                    {
                        Width = 7,
                        Height = 9,
                        Pixels = new List<Vector2>
                        {
                            new Vector2(1, 1),
                            new Vector2(1, 2),
                            new Vector2(1, 3),
                            new Vector2(1, 4),
                            new Vector2(1, 5),
                            new Vector2(1, 6),
                            new Vector2(1, 7),
                            new Vector2(2, 1),
                            new Vector2(2, 7),
                            new Vector2(3, 1),
                            new Vector2(3, 7),
                            new Vector2(4, 1),
                            new Vector2(4, 7),
                            new Vector2(5, 2),
                            new Vector2(5, 3),
                            new Vector2(5, 4),
                            new Vector2(5, 5),
                            new Vector2(5, 6)
                        }
                    }
                },
                {
                    "E",
                    new PixelPlot
                    {
                        Width = 7,
                        Height = 9,
                        Pixels = new List<Vector2>
                        {
                            new Vector2(1, 1),
                            new Vector2(1, 2),
                            new Vector2(1, 3),
                            new Vector2(1, 4),
                            new Vector2(1, 5),
                            new Vector2(1, 6),
                            new Vector2(1, 7),
                            new Vector2(2, 1),
                            new Vector2(2, 4),
                            new Vector2(2, 7),
                            new Vector2(3, 1),
                            new Vector2(3, 4),
                            new Vector2(3, 7),
                            new Vector2(4, 1),
                            new Vector2(4, 4),
                            new Vector2(4, 7),
                            new Vector2(5, 1),
                            new Vector2(5, 7)
                        }
                    }
                },
                {
                    "F",
                    new PixelPlot
                    {
                        Width = 7,
                        Height = 9,
                        Pixels = new List<Vector2>
                        {
                            new Vector2(1, 1),
                            new Vector2(1, 2),
                            new Vector2(1, 3),
                            new Vector2(1, 4),
                            new Vector2(1, 5),
                            new Vector2(1, 6),
                            new Vector2(1, 7),
                            new Vector2(2, 1),
                            new Vector2(2, 4),
                            new Vector2(3, 1),
                            new Vector2(3, 4),
                            new Vector2(4, 1),
                            new Vector2(4, 4),
                            new Vector2(5, 1),
                        }
                    }
                },
                {
                    "G",
                    new PixelPlot
                    {
                        Width = 7,
                        Height = 9,
                        Pixels = new List<Vector2>
                        {
                            new Vector2(1, 2),
                            new Vector2(1, 3),
                            new Vector2(1, 4),
                            new Vector2(1, 5),
                            new Vector2(1, 6),
                            new Vector2(2, 1),
                            new Vector2(2, 7),
                            new Vector2(3, 1),
                            new Vector2(3, 7),
                            new Vector2(4, 1),
                            new Vector2(4, 4),
                            new Vector2(4, 7),
                            new Vector2(5, 2),
                            new Vector2(5, 4),
                            new Vector2(5, 5),
                            new Vector2(5, 6)
                        }
                    }
                },
                {
                    "H",
                    new PixelPlot
                    {
                        Width = 7,
                        Height = 9,
                        Pixels = new List<Vector2>
                        {
                            new Vector2(1, 1),
                            new Vector2(1, 2),
                            new Vector2(1, 3),
                            new Vector2(1, 4),
                            new Vector2(1, 5),
                            new Vector2(1, 6),
                            new Vector2(1, 7),
                            new Vector2(2, 4),
                            new Vector2(3, 4),
                            new Vector2(4, 4),
                            new Vector2(5, 1),
                            new Vector2(5, 2),
                            new Vector2(5, 3),
                            new Vector2(5, 4),
                            new Vector2(5, 5),
                            new Vector2(5, 6),
                            new Vector2(5, 7)
                        }
                    }
                },
                {
                    "I",
                    new PixelPlot
                    {
                        Width = 7,
                        Height = 9,
                        Pixels = new List<Vector2>
                        {
                            new Vector2(1, 1),
                            new Vector2(1, 7),
                            new Vector2(2, 1),
                            new Vector2(2, 7),
                            new Vector2(3, 1),
                            new Vector2(3, 2),
                            new Vector2(3, 3),
                            new Vector2(3, 4),
                            new Vector2(3, 5),
                            new Vector2(3, 6),
                            new Vector2(3, 7),
                            new Vector2(4, 1),
                            new Vector2(4, 7),
                            new Vector2(5, 1),
                            new Vector2(5, 7)
                        }
                    }
                },
                {
                    "J",
                    new PixelPlot
                    {
                        Width = 7,
                        Height = 9,
                        Pixels = new List<Vector2>
                        {
                            new Vector2(1, 5),
                            new Vector2(1, 6),
                            new Vector2(2, 1),
                            new Vector2(2, 7),
                            new Vector2(3, 1),
                            new Vector2(3, 7),
                            new Vector2(4, 1),
                            new Vector2(4, 7),
                            new Vector2(5, 1),
                            new Vector2(5, 2),
                            new Vector2(5, 3),
                            new Vector2(5, 4),
                            new Vector2(5, 5),
                            new Vector2(5, 6)
                        }
                    }
                },
                {
                    "K",
                    new PixelPlot
                    {
                        Width = 7,
                        Height = 9,
                        Pixels = new List<Vector2>
                        {
                            new Vector2(1, 1),
                            new Vector2(1, 2),
                            new Vector2(1, 3),
                            new Vector2(1, 4),
                            new Vector2(1, 5),
                            new Vector2(1, 6),
                            new Vector2(1, 7),
                            new Vector2(2, 4),
                            new Vector2(3, 3),
                            new Vector2(3, 5),
                            new Vector2(4, 2),
                            new Vector2(4, 6),
                            new Vector2(5, 1),
                            new Vector2(5, 7),
                        }
                    }
                },
                {
                    "L",
                    new PixelPlot
                    {
                        Width = 7,
                        Height = 9,
                        Pixels = new List<Vector2>
                        {
                            new Vector2(1, 1),
                            new Vector2(1, 2),
                            new Vector2(1, 3),
                            new Vector2(1, 4),
                            new Vector2(1, 5),
                            new Vector2(1, 6),
                            new Vector2(1, 7),
                            new Vector2(2, 7),
                            new Vector2(3, 7),
                            new Vector2(4, 7),
                            new Vector2(5, 7),
                        }
                    }
                },
                {
                    "M",
                    new PixelPlot
                    {
                        Width = 7,
                        Height = 9,
                        Pixels = new List<Vector2>
                        {
                            new Vector2(1, 1),
                            new Vector2(1, 2),
                            new Vector2(1, 3),
                            new Vector2(1, 4),
                            new Vector2(1, 5),
                            new Vector2(1, 6),
                            new Vector2(1, 7),
                            new Vector2(2, 2),
                            new Vector2(3, 3),
                            new Vector2(4, 2),
                            new Vector2(5, 1),
                            new Vector2(5, 2),
                            new Vector2(5, 3),
                            new Vector2(5, 4),
                            new Vector2(5, 5),
                            new Vector2(5, 6),
                            new Vector2(5, 7)
                        }
                    }
                },
                {
                    "N",
                    new PixelPlot
                    {
                        Width = 7,
                        Height = 9,
                        Pixels = new List<Vector2>
                        {
                            new Vector2(1, 1),
                            new Vector2(1, 2),
                            new Vector2(1, 3),
                            new Vector2(1, 4),
                            new Vector2(1, 5),
                            new Vector2(1, 6),
                            new Vector2(1, 7),
                            new Vector2(2, 3),
                            new Vector2(3, 4),
                            new Vector2(4, 5),
                            new Vector2(5, 1),
                            new Vector2(5, 2),
                            new Vector2(5, 3),
                            new Vector2(5, 4),
                            new Vector2(5, 5),
                            new Vector2(5, 6),
                            new Vector2(5, 7)
                        }
                    }
                },
                {
                    "O",
                    new PixelPlot
                    {
                        Width = 7,
                        Height = 9,
                        Pixels = new List<Vector2>
                        {
                            new Vector2(1, 2),
                            new Vector2(1, 3),
                            new Vector2(1, 4),
                            new Vector2(1, 5),
                            new Vector2(1, 6),
                            new Vector2(2, 1),
                            new Vector2(2, 7),
                            new Vector2(3, 1),
                            new Vector2(3, 7),
                            new Vector2(4, 1),
                            new Vector2(4, 7),
                            new Vector2(5, 2),
                            new Vector2(5, 3),
                            new Vector2(5, 4),
                            new Vector2(5, 5),
                            new Vector2(5, 6)
                        }
                    }
                },
                {
                    "P",
                    new PixelPlot
                    {
                        Width = 7,
                        Height = 9,
                        Pixels = new List<Vector2>
                        {
                            new Vector2(1, 1),
                            new Vector2(1, 2),
                            new Vector2(1, 3),
                            new Vector2(1, 4),
                            new Vector2(1, 5),
                            new Vector2(1, 6),
                            new Vector2(1, 7),
                            new Vector2(2, 1),
                            new Vector2(2, 4),
                            new Vector2(3, 1),
                            new Vector2(3, 4),
                            new Vector2(4, 1),
                            new Vector2(4, 4),
                            new Vector2(5, 2),
                            new Vector2(5, 3),
                        }
                    }
                },
                {
                    "Q",
                    new PixelPlot
                    {
                        Width = 7,
                        Height = 9,
                        Pixels = new List<Vector2>
                        {
                            new Vector2(1, 2),
                            new Vector2(1, 3),
                            new Vector2(1, 4),
                            new Vector2(1, 5),
                            new Vector2(1, 6),
                            new Vector2(2, 1),
                            new Vector2(2, 7),
                            new Vector2(3, 1),
                            new Vector2(3, 5),
                            new Vector2(3, 7),
                            new Vector2(4, 1),
                            new Vector2(4, 6),
                            new Vector2(4, 7),
                            new Vector2(5, 2),
                            new Vector2(5, 3),
                            new Vector2(5, 4),
                            new Vector2(5, 5),
                            new Vector2(5, 6)
                        }
                    }
                },
                {
                    "R",
                    new PixelPlot
                    {
                        Width = 7,
                        Height = 9,
                        Pixels = new List<Vector2>
                        {
                            new Vector2(1, 1),
                            new Vector2(1, 2),
                            new Vector2(1, 3),
                            new Vector2(1, 4),
                            new Vector2(1, 5),
                            new Vector2(1, 6),
                            new Vector2(1, 7),
                            new Vector2(2, 1),
                            new Vector2(2, 4),
                            new Vector2(3, 1),
                            new Vector2(3, 4),
                            new Vector2(4, 1),
                            new Vector2(4, 4),
                            new Vector2(5, 2),
                            new Vector2(5, 3),
                            new Vector2(5, 5),
                            new Vector2(5, 6),
                            new Vector2(5, 7),
                        }
                    }
                },
                {
                    "S",
                    new PixelPlot
                    {
                        Width = 7,
                        Height = 9,
                        Pixels = new List<Vector2>
                        {
                            new Vector2(1, 2),
                            new Vector2(1, 3),
                            new Vector2(1, 6),
                            new Vector2(2, 1),
                            new Vector2(2, 4),
                            new Vector2(2, 7),
                            new Vector2(3, 1),
                            new Vector2(3, 4),
                            new Vector2(3, 7),
                            new Vector2(4, 1),
                            new Vector2(4, 4),
                            new Vector2(4, 7),
                            new Vector2(5, 2),
                            new Vector2(5, 5),
                            new Vector2(5, 6)
                        }
                    }
                },
                {
                    "T",
                    new PixelPlot
                    {
                        Width = 7,
                        Height = 9,
                        Pixels = new List<Vector2>
                        {
                            new Vector2(1, 1),
                            new Vector2(2, 1),
                            new Vector2(3, 1),
                            new Vector2(3, 2),
                            new Vector2(3, 3),
                            new Vector2(3, 4),
                            new Vector2(3, 5),
                            new Vector2(3, 6),
                            new Vector2(3, 7),
                            new Vector2(4, 1),
                            new Vector2(5, 1),
                        }
                    }
                },
                {
                    "U",
                    new PixelPlot
                    {
                        Width = 7,
                        Height = 9,
                        Pixels = new List<Vector2>
                        {
                            new Vector2(1, 1),
                            new Vector2(1, 2),
                            new Vector2(1, 3),
                            new Vector2(1, 4),
                            new Vector2(1, 5),
                            new Vector2(1, 6),
                            new Vector2(2, 7),
                            new Vector2(3, 7),
                            new Vector2(4, 7),
                            new Vector2(5, 1),
                            new Vector2(5, 2),
                            new Vector2(5, 3),
                            new Vector2(5, 4),
                            new Vector2(5, 5),
                            new Vector2(5, 6)
                        }
                    }
                },
                {
                    "V",
                    new PixelPlot
                    {
                        Width = 7,
                        Height = 9,
                        Pixels = new List<Vector2>
                        {
                            new Vector2(1, 1),
                            new Vector2(1, 2),
                            new Vector2(1, 3),
                            new Vector2(1, 4),
                            new Vector2(1, 5),
                            new Vector2(1, 6),
                            new Vector2(2, 6),
                            new Vector2(3, 7),
                            new Vector2(4, 6),
                            new Vector2(5, 1),
                            new Vector2(5, 2),
                            new Vector2(5, 3),
                            new Vector2(5, 4),
                            new Vector2(5, 5),
                            new Vector2(5, 6)
                        }
                    }
                },
                {
                    "W",
                    new PixelPlot
                    {
                        Width = 7,
                        Height = 9,
                        Pixels = new List<Vector2>
                        {
                            new Vector2(1, 1),
                            new Vector2(1, 2),
                            new Vector2(1, 3),
                            new Vector2(1, 4),
                            new Vector2(1, 5),
                            new Vector2(1, 6),
                            new Vector2(2, 7),
                            new Vector2(3, 5),
                            new Vector2(3, 6),
                            new Vector2(4, 7),
                            new Vector2(5, 1),
                            new Vector2(5, 2),
                            new Vector2(5, 3),
                            new Vector2(5, 4),
                            new Vector2(5, 5),
                            new Vector2(5, 6)
                        }
                    }
                },
                {
                    "X",
                    new PixelPlot
                    {
                        Width = 7,
                        Height = 9,
                        Pixels = new List<Vector2>
                        {
                            new Vector2(1, 1),
                            new Vector2(1, 2),
                            new Vector2(1, 6),
                            new Vector2(2, 3),
                            new Vector2(2, 5),
                            new Vector2(3, 4),
                            new Vector2(4, 3),
                            new Vector2(4, 5),
                            new Vector2(5, 1),
                            new Vector2(5, 2),
                            new Vector2(5, 6),
                            new Vector2(5, 7),
                        }
                    }
                },
                {
                    "Y",
                    new PixelPlot
                    {
                        Width = 7,
                        Height = 9,
                        Pixels = new List<Vector2>
                        {
                            new Vector2(1, 1),
                            new Vector2(1, 2),
                            new Vector2(1, 3),
                            new Vector2(2, 4),
                            new Vector2(3, 5),
                            new Vector2(3, 6),
                            new Vector2(3, 7),
                            new Vector2(4, 4),
                            new Vector2(5, 1),
                            new Vector2(5, 2),
                            new Vector2(5, 3),
                        }
                    }
                },
                {
                    "Z",
                    new PixelPlot
                    {
                        Width = 7,
                        Height = 9,
                        Pixels = new List<Vector2>
                        {
                            new Vector2(1, 1),
                            new Vector2(1, 6),
                            new Vector2(1, 7),
                            new Vector2(2, 1),
                            new Vector2(2, 5),
                            new Vector2(2, 7),
                            new Vector2(3, 1),
                            new Vector2(3, 4),
                            new Vector2(3, 7),
                            new Vector2(4, 1),
                            new Vector2(4, 3),
                            new Vector2(4, 7),
                            new Vector2(5, 1),
                            new Vector2(5, 2),
                            new Vector2(5, 7),
                        }
                    }
                },
                {
                    "1",
                    new PixelPlot
                    {
                        Width = 7,
                        Height = 9,
                        Pixels = new List<Vector2>
                        {
                            new Vector2(1, 1),
                            new Vector2(1, 7),
                            new Vector2(2, 1),
                            new Vector2(2, 7),
                            new Vector2(3, 1),
                            new Vector2(3, 2),
                            new Vector2(3, 3),
                            new Vector2(3, 4),
                            new Vector2(3, 5),
                            new Vector2(3, 6),
                            new Vector2(3, 7),
                            new Vector2(4, 7),
                            new Vector2(5, 7)
                        }
                    }
                },
                {
                    "2",
                    new PixelPlot
                    {
                        Width = 7,
                        Height = 9,
                        Pixels = new List<Vector2>
                        {
                            new Vector2(1, 2),
                            new Vector2(1, 7),
                            new Vector2(2, 1),
                            new Vector2(2, 6),
                            new Vector2(2, 7),
                            new Vector2(3, 1),
                            new Vector2(3, 5),
                            new Vector2(3, 7),
                            new Vector2(4, 1),
                            new Vector2(4, 4),
                            new Vector2(4, 7),
                            new Vector2(5, 2),
                            new Vector2(5, 3),
                            new Vector2(5, 7),
                        }
                    }
                },
                {
                    "3",
                    new PixelPlot
                    {
                        Width = 7,
                        Height = 9,
                        Pixels = new List<Vector2>
                        {
                            new Vector2(1, 2),
                            new Vector2(1, 6),
                            new Vector2(2, 1),
                            new Vector2(2, 7),
                            new Vector2(3, 1),
                            new Vector2(3, 4),
                            new Vector2(3, 7),
                            new Vector2(4, 1),
                            new Vector2(4, 4),
                            new Vector2(4, 7),
                            new Vector2(5, 2),
                            new Vector2(5, 3),
                            new Vector2(5, 5),
                            new Vector2(5, 6)
                        }
                    }
                },
                {
                    "4",
                    new PixelPlot
                    {
                        Width = 7,
                        Height = 9,
                        Pixels = new List<Vector2>
                        {
                            new Vector2(1, 4),
                            new Vector2(1, 5),
                            new Vector2(2, 3),
                            new Vector2(2, 5),
                            new Vector2(3, 2),
                            new Vector2(3, 5),
                            new Vector2(4, 1),
                            new Vector2(4, 2),
                            new Vector2(4, 3),
                            new Vector2(4, 4),
                            new Vector2(4, 5),
                            new Vector2(4, 6),
                            new Vector2(4, 7),
                            new Vector2(5, 5),
                        }
                    }
                },
                {
                    "5",
                    new PixelPlot
                    {
                        Width = 7,
                        Height = 9,
                        Pixels = new List<Vector2>
                        {
                            new Vector2(1, 1),
                            new Vector2(1, 2),
                            new Vector2(1, 3),
                            new Vector2(1, 6),
                            new Vector2(2, 1),
                            new Vector2(2, 3),
                            new Vector2(2, 7),
                            new Vector2(3, 1),
                            new Vector2(3, 3),
                            new Vector2(3, 7),
                            new Vector2(4, 1),
                            new Vector2(4, 3),
                            new Vector2(4, 7),
                            new Vector2(5, 1),
                            new Vector2(5, 4),
                            new Vector2(5, 5),
                            new Vector2(5, 6)
                        }
                    }
                },
                {
                    "6",
                    new PixelPlot
                    {
                        Width = 7,
                        Height = 9,
                        Pixels = new List<Vector2>
                        {
                            new Vector2(1, 2),
                            new Vector2(1, 3),
                            new Vector2(1, 4),
                            new Vector2(1, 5),
                            new Vector2(1, 6),
                            new Vector2(2, 1),
                            new Vector2(2, 4),
                            new Vector2(2, 7),
                            new Vector2(3, 1),
                            new Vector2(3, 4),
                            new Vector2(3, 7),
                            new Vector2(4, 1),
                            new Vector2(4, 4),
                            new Vector2(4, 7),
                            new Vector2(5, 2),
                            new Vector2(5, 5),
                            new Vector2(5, 6)
                        }
                    }
                },
                {
                    "7",
                    new PixelPlot
                    {
                        Width = 7,
                        Height = 9,
                        Pixels = new List<Vector2>
                        {
                            new Vector2(1, 1),
                            new Vector2(2, 1),
                            new Vector2(3, 1),
                            new Vector2(3, 5),
                            new Vector2(3, 6),
                            new Vector2(3, 7),
                            new Vector2(4, 1),
                            new Vector2(4, 4),
                            new Vector2(5, 1),
                            new Vector2(5, 2),
                            new Vector2(5, 3),
                        }
                    }
                },
                {
                    "8",
                    new PixelPlot
                    {
                        Width = 7,
                        Height = 9,
                        Pixels = new List<Vector2>
                        {
                            new Vector2(1, 2),
                            new Vector2(1, 3),
                            new Vector2(1, 5),
                            new Vector2(1, 6),
                            new Vector2(2, 1),
                            new Vector2(2, 4),
                            new Vector2(2, 7),
                            new Vector2(3, 1),
                            new Vector2(3, 4),
                            new Vector2(3, 7),
                            new Vector2(4, 1),
                            new Vector2(4, 4),
                            new Vector2(4, 7),
                            new Vector2(5, 2),
                            new Vector2(5, 3),
                            new Vector2(5, 5),
                            new Vector2(5, 6)
                        }
                    }
                },
                {
                    "9",
                    new PixelPlot
                    {
                        Width = 7,
                        Height = 9,
                        Pixels = new List<Vector2>
                        {
                            new Vector2(1, 2),
                            new Vector2(1, 3),
                            new Vector2(1, 6),
                            new Vector2(2, 1),
                            new Vector2(2, 4),
                            new Vector2(2, 7),
                            new Vector2(3, 1),
                            new Vector2(3, 4),
                            new Vector2(3, 7),
                            new Vector2(4, 1),
                            new Vector2(4, 4),
                            new Vector2(4, 7),
                            new Vector2(5, 2),
                            new Vector2(5, 3),
                            new Vector2(5, 4),
                            new Vector2(5, 5),
                            new Vector2(5, 6)
                        }
                    }
                },
                {
                    "0",
                    new PixelPlot
                    {
                        Width = 7,
                        Height = 9,
                        Pixels = new List<Vector2>
                        {
                            new Vector2(1, 2),
                            new Vector2(1, 3),
                            new Vector2(1, 4),
                            new Vector2(1, 5),
                            new Vector2(1, 6),
                            new Vector2(2, 1),
                            new Vector2(2, 7),
                            new Vector2(3, 1),
                            new Vector2(3, 7),
                            new Vector2(4, 1),
                            new Vector2(4, 7),
                            new Vector2(5, 2),
                            new Vector2(5, 3),
                            new Vector2(5, 4),
                            new Vector2(5, 5),
                            new Vector2(5, 6)
                        }
                    }
                },
            };
        }
    }
}
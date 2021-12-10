//-----------------------------------------------------------------------
// <copyright file="HeightMap.cs" company="Andy Young">
//     Copyright (c) Andy Young. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace AdventOfCode2021.Models
{
    /// <summary>
    /// Represents a height map.
    /// </summary>
    internal class HeightMap
    {
        private static readonly (int, int)[] Directions = new[] { (-1, 0), (1, 0), (0, -1), (0, 1) };

        private readonly int width;
        private readonly int height;

        /// <summary>
        /// Initializes a new instance of the <see cref="HeightMap"/> class.
        /// </summary>
        /// <param name="inputLines">The lines read from the input file.</param>
        public HeightMap(string[] inputLines)
        {
            this.Entries = inputLines.Select(l => l.Select(c => new HeightMapEntry(c)).ToArray()).ToArray();
            this.width = this.Entries.Length;
            this.height = this.Entries[0].Length;
        }

        /// <summary>
        /// Gets the height map entries.
        /// </summary>
        public HeightMapEntry[][] Entries { get; private set; }

        /// <summary>
        /// Gets the coordinates of all the low points.
        /// </summary>
        /// <returns>The coordinates of all the low points.</returns>
        public List<(int X, int Y)> GetCoordinatesOfLowPoints()
        {
            List<(int, int)> lowPoints = new();

            for (int i = 0; i < this.width; i++)
            {
                for (int j = 0; j < this.height; j++)
                {
                    if (this.Entries[i][j].IsLowPoint != null)
                    {
                        // Already known, skip.
                        continue;
                    }

                    bool isLowPoint = this.IsLowPoint(i, j);
                    this.Entries[i][j].IsLowPoint = isLowPoint;

                    if (isLowPoint)
                    {
                        lowPoints.Add((i, j));
                    }
                }
            }

            return lowPoints;
        }

        /// <summary>
        /// Gets the size of the basin for the low point at the given coordinates.
        /// </summary>
        /// <param name="i">The index along the first dimension.</param>
        /// <param name="j">The index along the second dimension.</param>
        /// <returns>The size of the basin.</returns>
        public int GetBasinSize(int i, int j)
        {
            if (this.Entries[i][j].IsLowPoint != true)
            {
                throw new InvalidOperationException("This is not a low point.");
            }

            Stack<(int, int)> stack = new();
            HashSet<(int, int)> basin = new();
            stack.Push((i, j));
            basin.Add((i, j));

            while (stack.TryPop(out (int X, int Y) coords))
            {
                foreach ((int X, int Y) direction in Directions)
                {
                    int x = coords.X + direction.X;
                    int y = coords.Y + direction.Y;

                    if (x < 0 || x >= this.width)
                    {
                        continue;
                    }

                    if (y < 0 || y >= this.height)
                    {
                        continue;
                    }

                    if (this.Entries[x][y].Height != 9 && !basin.Contains((x, y)))
                    {
                        stack.Push((x, y));
                        basin.Add((x, y));
                    }
                }
            }

            return basin.Count;
        }

        /// <summary>
        /// Determines if the height map entry is a low point.
        /// </summary>
        /// <param name="i">The index along the first dimension.</param>
        /// <param name="j">The index along the second dimension.</param>
        /// <returns><c>true</c> if the height map entry is a low point, <c>false</c> otherwise.</returns>
        private bool IsLowPoint(int i, int j)
        {
            foreach ((int X, int Y) direction in Directions)
            {
                int x = i + direction.X;
                int y = j + direction.Y;

                if (x < 0 || x >= this.width)
                {
                    continue;
                }

                if (y < 0 || y >= this.height)
                {
                    continue;
                }

                if (this.Entries[x][y].IsLowPoint == true
                    || this.Entries[i][j].Height >= this.Entries[x][y].Height)
                {
                    return false;
                }
            }

            return true;
        }
    }
}

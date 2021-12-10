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
        /// Gets the sum of the risk levels of the low points.
        /// </summary>
        /// <returns>The sum of the risk levels of the low points.</returns>
        public int GetSumOfLowPointRiskLevels()
        {
            int sum = 0;
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
                        sum += this.Entries[i][j].Height + 1;
                    }
                }
            }

            return sum;
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

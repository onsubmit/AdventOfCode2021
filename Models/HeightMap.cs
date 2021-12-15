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
        private readonly int width;
        private readonly int height;

        private readonly Coordinate start;
        private readonly Coordinate end;

        /// <summary>
        /// Initializes a new instance of the <see cref="HeightMap"/> class.
        /// </summary>
        /// <param name="inputLines">The lines read from the input file.</param>
        public HeightMap(string[] inputLines)
        {
            this.Entries = inputLines.Select(l => l.Select(c => new HeightMapEntry(c)).ToArray()).ToArray();
            this.width = this.Entries.Length;
            this.height = this.Entries[0].Length;
            this.start = new(0, 0);
            this.end = new(this.width - 1, this.height - 1);
        }

        /// <summary>
        /// Gets the height map entries.
        /// </summary>
        public HeightMapEntry[][] Entries { get; private set; }

        /// <summary>
        /// Gets the coordinates of all the low points.
        /// </summary>
        /// <returns>The coordinates of all the low points.</returns>
        public List<Coordinate> GetCoordinatesOfLowPoints()
        {
            List<Coordinate> lowPoints = new();

            for (int i = 0; i < this.width; i++)
            {
                for (int j = 0; j < this.height; j++)
                {
                    if (this.Entries[i][j].IsLowPoint != null)
                    {
                        // Already known, skip.
                        continue;
                    }

                    Coordinate coordinate = new(i, j);
                    bool isLowPoint = this.IsLowPoint(coordinate);
                    this.Entries[i][j].IsLowPoint = isLowPoint;

                    if (isLowPoint)
                    {
                        lowPoints.Add(coordinate);
                    }
                }
            }

            return lowPoints;
        }

        /// <summary>
        /// Gets the size of the basin for the low point at the given coordinates.
        /// </summary>
        /// <param name="coordinate">The coordinate.</param>
        /// <returns>The size of the basin.</returns>
        public int GetBasinSize(Coordinate coordinate)
        {
            if (this.Entries[coordinate.X][coordinate.Y].IsLowPoint != true)
            {
                throw new InvalidOperationException("This is not a low point.");
            }

            Stack<Coordinate> stack = new();
            HashSet<Coordinate> basin = new();
            stack.Push(coordinate);
            basin.Add(coordinate);

            while (stack.TryPop(out Coordinate coords))
            {
                foreach (Coordinate direction in Directions.Cardinal)
                {
                    Coordinate neighbor = coords + direction;
                    if (!neighbor.IsInRange(this.start, this.end))
                    {
                        continue;
                    }

                    if (this.Entries[neighbor.X][neighbor.Y].Height != 9 && !basin.Contains(neighbor))
                    {
                        stack.Push(neighbor);
                        basin.Add(neighbor);
                    }
                }
            }

            return basin.Count;
        }

        /// <summary>
        /// Determines if the height map entry is a low point.
        /// </summary>
        /// <param name="coordinate">The coordinate.</param>
        /// <returns><c>true</c> if the height map entry is a low point, <c>false</c> otherwise.</returns>
        private bool IsLowPoint(Coordinate coordinate)
        {
            foreach (Coordinate direction in Directions.Cardinal)
            {
                Coordinate neighbor = coordinate + direction;
                if (!neighbor.IsInRange(this.start, this.end))
                {
                    continue;
                }

                if (this.Entries[neighbor.X][neighbor.Y].IsLowPoint == true
                    || this.Entries[coordinate.X][coordinate.Y].Height >= this.Entries[neighbor.X][neighbor.Y].Height)
                {
                    return false;
                }
            }

            return true;
        }
    }
}

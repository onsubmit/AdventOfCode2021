//-----------------------------------------------------------------------
// <copyright file="VentDiagram.cs" company="Andy Young">
//     Copyright (c) Andy Young. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace AdventOfCode2021.Models
{
    /// <summary>
    /// Represents a vent diagram.
    /// </summary>
    internal class VentDiagram
    {
        /// <summary>
        /// The positions in the diagram.
        /// </summary>
        private readonly uint[,] positions = new uint[0, 0];

        /// <summary>
        /// Initializes a new instance of the <see cref="VentDiagram"/> class.
        /// </summary>
        /// <param name="width">The width of the diagram.</param>
        /// <param name="height">The height of the diagram.</param>
        public VentDiagram(int width, int height)
        {
            this.positions = new uint[width + 1, height + 1];
        }

        /// <summary>
        /// Gets the value of the position at the given coordinate.
        /// </summary>
        /// <param name="coordinate">The coordinate.</param>
        /// <returns>The value of the position at the given coordinate.</returns>
        public uint this[Coordinate coordinate] => this.positions[coordinate.X, coordinate.Y];

        /// <summary>
        /// Gets the value of the position at the given coordinates.
        /// </summary>
        /// <param name="x">The x coordinate.</param>
        /// <param name="y">The y coordinate.</param>
        /// <returns>The value of the position at the given coordinates.</returns>
        public uint this[int x, int y] => this.positions[x, y];

        /// <summary>
        /// Records a vent reading.
        /// </summary>
        /// <param name="reading">The vent reading.</param>
        public void RecordReading(VentReading reading)
        {
            if (reading.Start.X == reading.End.X)
            {
                // Vertical reading
                int minY = Math.Min(reading.Start.Y, reading.End.Y);
                int maxY = Math.Max(reading.Start.Y, reading.End.Y);

                for (int y = minY; y <= maxY; y++)
                {
                    this.positions[reading.Start.X, y]++;
                }
            }
            else if (reading.Start.Y == reading.End.Y)
            {
                // Horizontal reading
                int minX = Math.Min(reading.Start.X, reading.End.X);
                int maxX = Math.Max(reading.Start.X, reading.End.X);

                for (int x = minX; x <= maxX; x++)
                {
                    this.positions[x, reading.Start.Y]++;
                }
            }
            else
            {
                // Ignore readings that are not horizontal or vertical.
            }
        }
    }
}

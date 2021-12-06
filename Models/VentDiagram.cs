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
            bool isHorizontalReading = reading.Start.X == reading.End.X;
            bool isVerticalReading = reading.Start.Y == reading.End.Y;
            bool isDiagonalReading = Math.Abs(reading.End.X - reading.Start.X) == Math.Abs(reading.End.Y - reading.Start.Y);

            if (!isHorizontalReading && !isVerticalReading && !isDiagonalReading)
            {
                throw new InvalidOperationException("Only horizontal, vertical, and diagonal readings are supported.");
            }

            int x = reading.Start.X;
            int y = reading.Start.Y;

            int xDelta = isHorizontalReading ? 0 : reading.Start.X < reading.End.X ? 1 : -1;
            int yDelta = isVerticalReading ? 0 : reading.Start.Y < reading.End.Y ? 1 : -1;

            int stopX = reading.End.X + xDelta;
            int stopY = reading.End.Y + yDelta;

            while (x != stopX || y != stopY)
            {
                this.positions[x, y]++;
                x += xDelta;
                y += yDelta;
            }
        }
    }
}

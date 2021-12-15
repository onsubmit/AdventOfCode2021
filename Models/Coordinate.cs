//-----------------------------------------------------------------------
// <copyright file="Coordinate.cs" company="Andy Young">
//     Copyright (c) Andy Young. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace AdventOfCode2021.Models
{
    /// <summary>
    /// A coordinate.
    /// </summary>
    public record struct Coordinate
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Coordinate"/> class.
        /// </summary>
        /// <param name="x">The x value.</param>
        /// <param name="y">The y value.</param>
        public Coordinate(int x, int y)
        {
            this.X = x;
            this.Y = y;
        }

        /// <summary>
        /// Gets or sets the x value.
        /// </summary>
        public int X { get; set; }

        /// <summary>
        /// Gets or sets the y value.
        /// </summary>
        public int Y { get; set; }

        /// <summary>
        /// Determines if the coordinate is within the given range.
        /// </summary>
        /// <param name="min">The minimum value.</param>
        /// <param name="max">The maximum value.</param>
        /// <returns><c>true</c> if the coordinate is within the given range, <c>false</c> otherwise.</returns>
        public bool IsInRange(Coordinate min, Coordinate max)
        {
            if (this.X < min.X || this.X > max.X)
            {
                return false;
            }

            if (this.Y < min.Y || this.Y > max.Y)
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// Adds two coordinates together.
        /// </summary>
        /// <param name="a">First coordinate.</param>
        /// <param name="b">Second coordinate.</param>
        /// <returns>The sum of the two coordinates.</returns>
        public static Coordinate operator +(Coordinate a, Coordinate b)
        {
            return new() { X = a.X + b.X, Y = a.Y + b.Y };
        }

        /// <summary>
        /// Returns a string representation of the coordinate.
        /// </summary>
        /// <returns>A string representation of the coordinate.</returns>
        public override string ToString()
        {
            return $"({this.X}, {this.Y})";
        }
    }
}

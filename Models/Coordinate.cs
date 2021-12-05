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
        /// Gets or sets the x value.
        /// </summary>
        public int X { get; set; }

        /// <summary>
        /// Gets or sets the y value.
        /// </summary>
        public int Y { get; set; }
    }
}

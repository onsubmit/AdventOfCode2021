//-----------------------------------------------------------------------
// <copyright file="VentReading.cs" company="Andy Young">
//     Copyright (c) Andy Young. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace AdventOfCode2021.Models
{
    /// <summary>
    /// A vent reading.
    /// </summary>
    public readonly record struct VentReading
    {
        /// <summary>
        /// Gets the start coordinate.
        /// </summary>
        public Coordinate Start { get; init; }

        /// <summary>
        /// Gets the end coordinate.
        /// </summary>
        public Coordinate End { get; init; }
    }
}

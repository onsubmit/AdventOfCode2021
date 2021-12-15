//-----------------------------------------------------------------------
// <copyright file="Directions.cs" company="Andy Young">
//     Copyright (c) Andy Young. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace AdventOfCode2021.Models
{
    /// <summary>
    /// Represents a set of directions.
    /// </summary>
    internal static class Directions
    {
        /// <summary>
        /// The cardinal directions: N, S, E, W.
        /// </summary>
        public static readonly Coordinate[] Cardinal = new Coordinate[] { new(-1, 0), new(1, 0), new(0, -1), new(0, 1) };

        /// <summary>
        /// The cardinal and ordinal directions: N, S, E, W, NE, SE, SW, NW.
        /// </summary>
        public static readonly Coordinate[] CardinalAndOrdinal = new Coordinate[] { new(-1, -1), new(-1, 0), new(-1, 1), new(0, -1), new(0, 1), new(1, -1), new(1, 0), new(1, 1) };
    }
}

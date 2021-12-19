//-----------------------------------------------------------------------
// <copyright file="SnailfishNumberSearchDirection.cs" company="Andy Young">
//     Copyright (c) Andy Young. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace AdventOfCode2021.Models
{
    /// <summary>
    /// The search direction while traversing the <see cref="SnailfishNumber"/> graph.
    /// </summary>
    [Flags]
    internal enum SnailfishNumberSearchDirection
    {
        /// <summary>
        /// Favoring left.
        /// </summary>
        Left = 0x1,

        /// <summary>
        /// Favoring right.
        /// </summary>
        Right = 0x2,

        /// <summary>
        /// Going up the graph.
        /// </summary>
        Up = 0x4,

        /// <summary>
        /// Going down the graph.
        /// </summary>
        Down = 0x8,
    }
}

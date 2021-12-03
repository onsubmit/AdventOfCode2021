// <copyright file="Vector.cs" company="Andy Young">
// Copyright (c) Andy Young. All rights reserved.
// </copyright>

namespace AdventOfCode2021.Models
{
    /// <summary>
    /// Vector representation.
    /// </summary>
    internal class Vector
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Vector"/> class.
        /// </summary>
        /// <param name="direction">The direction.</param>
        /// <param name="distance">The distance.</param>
        public Vector(Direction direction, int distance)
        {
            this.Direction = direction;
            this.Distance = distance;
        }

        /// <summary>
        /// Gets the zero vector.
        /// </summary>
        public static Vector Zero { get; } = new Vector(0, 0);

        /// <summary>
        /// Gets the direction.
        /// </summary>
        public Direction Direction { get; }

        /// <summary>
        /// Gets the distance.
        /// </summary>
        public int Distance { get; }
    }
}

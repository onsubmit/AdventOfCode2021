//-----------------------------------------------------------------------
// <copyright file="Submarine.cs" company="Andy Young">
//     Copyright (c) Andy Young. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace AdventOfCode2021.Models
{
    /// <summary>
    /// Represents a submarine.
    /// </summary>
    internal class Submarine
    {
        /// <summary>
        /// Gets the horizontal position.
        /// </summary>
        public int HorizontalPosition { get; private set; }

        /// <summary>
        /// Gets the depth.
        /// </summary>
        public int Depth { get; private set; }

        /// <summary>
        /// Gets the aim.
        /// </summary>
        public int Aim { get; private set; }

        /// <summary>
        /// Moves the submarines according to the provided vector.
        /// </summary>
        /// <param name="vector">The vector describing the movement direction and distance.</param>
        public void Move(Vector vector)
        {
            switch (vector.Direction)
            {
                case Direction.Up:
                    this.Aim -= vector.Distance;
                    break;

                case Direction.Down:
                    this.Aim += vector.Distance;
                    break;

                case Direction.Forward:
                    this.HorizontalPosition += vector.Distance;
                    this.Depth += this.Aim * vector.Distance;
                    break;
            }
        }
    }
}

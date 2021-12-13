//-----------------------------------------------------------------------
// <copyright file="CavePath.cs" company="Andy Young">
//     Copyright (c) Andy Young. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace AdventOfCode2021.Models
{
    /// <summary>
    /// Represents a path of caves.
    /// </summary>
    internal class CavePath
    {
        private Cave? popularSmallCave = null;

        /// <summary>
        /// Gets the path's caves.
        /// </summary>
        public Stack<Cave> Caves { get; private set; } = new();

        /// <summary>
        /// Pushes a new save onto the path.
        /// </summary>
        /// <param name="cave">The cave to push.</param>
        public void Push(Cave cave)
        {
            this.Caves.Push(cave);
        }

        /// <summary>
        /// Pops a cave off the path.
        /// </summary>
        /// <returns>The popped cave.</returns>
        public Cave Pop()
        {
            Cave popped = this.Caves.Pop();
            if (this.popularSmallCave == popped)
            {
                this.popularSmallCave = null;
            }

            return popped;
        }

        /// <summary>
        /// Determines if the small cave can be added to the path.
        /// </summary>
        /// <param name="cave">The small cave.</param>
        /// <returns><c>true</c> if the cave can be added to the path, <c>false</c> otherwise.</returns>
        /// <exception cref="InvalidOperationException">Thrown if the cave is not small.</exception>
        public bool CanAddSmallCave(Cave cave)
        {
            if (!cave.IsSmall)
            {
                throw new InvalidOperationException("Cave must be small.");
            }

            if (!this.Caves.Contains(cave))
            {
                return true;
            }

            // Part 2 solution
            if (this.popularSmallCave == null)
            {
                this.popularSmallCave = cave;
                return true;
            }

            if (this.popularSmallCave == cave && this.Caves.Count(c => c == cave) == 1)
            {
                // We can add this small cave twice.
                return true;
            }

            // We can only add this small cave once.
            return false;
        }
    }
}

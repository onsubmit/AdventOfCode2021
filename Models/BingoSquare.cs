//-----------------------------------------------------------------------
// <copyright file="BingoSquare.cs" company="Andy Young">
//     Copyright (c) Andy Young. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace AdventOfCode2021.Models
{
    /// <summary>
    /// Represents a single square on a Bingo board.
    /// </summary>
    internal class BingoSquare
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BingoSquare"/> class.
        /// </summary>
        /// <param name="value">The square's value.</param>
        public BingoSquare(int value)
        {
            this.Value = value;
        }

        /// <summary>
        /// Gets the square value.
        /// </summary>
        public int Value { get; private set; }

        /// <summary>
        /// Gets a value indicating whether the square is marked.
        /// </summary>
        public bool IsMarked { get; private set; }

        /// <summary>
        /// Marks the square.
        /// </summary>
        public void Mark()
        {
            this.IsMarked = true;
        }

        /// <summary>
        /// Returns a string representation of the square.
        /// </summary>
        /// <returns>A string representation of the square.</returns>
        public override string ToString() => $"{this.Value}{(this.IsMarked ? " (X)" : string.Empty)}";
    }
}

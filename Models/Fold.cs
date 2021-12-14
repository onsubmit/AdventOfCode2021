//-----------------------------------------------------------------------
// <copyright file="Fold.cs" company="Andy Young">
//     Copyright (c) Andy Young. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace AdventOfCode2021.Models
{
    /// <summary>
    /// Represents a fold.
    /// </summary>
    internal class Fold
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Fold"/> class.
        /// </summary>
        /// <param name="dimension">The dimension of the fold.</param>
        /// <param name="position">The position of the fold.</param>
        public Fold(Dimension dimension, int position)
        {
            this.Dimension = dimension;
            this.Position = position;
        }

        /// <summary>
        /// Gets the fold dimension.
        /// </summary>
        public Dimension Dimension { get; private set; }

        /// <summary>
        /// Gets the fold position.
        /// </summary>
        public int Position { get; private set; }
    }
}

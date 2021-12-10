//-----------------------------------------------------------------------
// <copyright file="HeightMapEntry.cs" company="Andy Young">
//     Copyright (c) Andy Young. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace AdventOfCode2021.Models
{
    /// <summary>
    /// Represents an entry in the height map.
    /// </summary>
    internal class HeightMapEntry
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="HeightMapEntry"/> class.
        /// </summary>
        /// <param name="height">The height.</param>
        public HeightMapEntry(char height)
        {
            this.Height = (int)char.GetNumericValue(height);
        }

        /// <summary>
        /// Gets the entry's height.
        /// </summary>
        public int Height { get; private set; }

        /// <summary>
        /// Gets or sets a value indicating whether the entry corresponds to a low point. Null implies unknown.
        /// </summary>
        public bool? IsLowPoint { get; set; }
    }
}

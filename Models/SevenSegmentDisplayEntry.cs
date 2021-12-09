//-----------------------------------------------------------------------
// <copyright file="SevenSegmentDisplayEntry.cs" company="Andy Young">
//     Copyright (c) Andy Young. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace AdventOfCode2021.Models
{
    /// <summary>
    /// Represents a 7-segment display entry.
    /// </summary>
    internal record struct SevenSegmentDisplayEntry
    {
        /// <summary>
        /// Gets or sets the signal patterns.
        /// </summary>
        public List<string> SignalPatterns { get; set; }

        /// <summary>
        /// Gets or sets the output value.
        /// </summary>
        public List<string> OutputValue { get; set; }

        /// <summary>
        /// Generates a string-representation of a <see cref="SevenSegmentDisplayEntry"/>.
        /// </summary>
        /// <returns>A string-representation of a <see cref="SevenSegmentDisplayEntry"/>.</returns>
        public override string ToString()
        {
            return $"{string.Join(" ", this.SignalPatterns)} | {string.Join(" ", this.OutputValue)}";
        }
    }
}

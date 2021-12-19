//-----------------------------------------------------------------------
// <copyright file="SnailfishNumberParser.cs" company="Andy Young">
//     Copyright (c) Andy Young. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace AdventOfCode2021.Models
{
    /// <summary>
    /// A parser for Snailfish numbers.
    /// </summary>
    internal static class SnailfishNumberParser
    {
        /// <summary>
        /// Parses a single number.
        /// </summary>
        /// <param name="raw">The raw input.</param>
        /// <returns>The new snailfish number.</returns>
        public static SnailfishNumber ParseSingleNumber(string raw)
        {
            if (raw.StartsWith('[') && raw.EndsWith(']'))
            {
                return new SnailfishNumber(raw);
            }

            return new SnailfishRegularNumber(raw);
        }
    }
}

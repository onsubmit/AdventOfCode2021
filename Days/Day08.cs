//-----------------------------------------------------------------------
// <copyright file="Day08.cs" company="Andy Young">
//     Copyright (c) Andy Young. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace AdventOfCode2021.Days
{
    using AdventOfCode2021.Models;

    /// <summary>
    /// Calculates the solution for the particular day.
    /// </summary>
    internal class Day08 : IDay
    {
        /// <summary>
        /// Calculates the solution for the particular day.
        /// </summary>
        /// <returns>The solution.</returns>
        public string GetSolution()
        {
            using StreamReader sr = new("input\\Day08.txt");
            string? line = null;

            int sum = 0;
            while ((line = sr.ReadLine()) != null)
            {
                SevenSegmentDisplayEntry entry = GetSevenSegmentDisplayEntry(line);
                sum += entry.GetDecodedOutputValue();
            }

            return sum.ToString();
        }

        /// <summary>
        /// Gets a <see cref="SevenSegmentDisplayEntry"/> from an input line.
        /// </summary>
        /// <param name="line">The input line.</param>
        /// <returns>The <see cref="SevenSegmentDisplayEntry"/>.</returns>
        /// <exception cref="InvalidOperationException">Thrown if input is bad.</exception>
        private static SevenSegmentDisplayEntry GetSevenSegmentDisplayEntry(string line)
        {
            string[] split = line.Split('|');
            if (split.Length != 2)
            {
                throw new InvalidOperationException("Input is bad");
            }

            return new()
            {
                SignalPatterns = new(split[0].Trim().Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(s => string.Concat(s.OrderBy(c => c)))),
                OutputValue = new(split[1].Trim().Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(s => string.Concat(s.OrderBy(c => c)))),
            };
        }
    }
}

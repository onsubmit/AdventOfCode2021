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
            int[] numSegments = new int[10] { 6, 2, 5, 5, 4, 5, 6, 3, 7, 6 };
            int[] uniqueSegmentAmounts = new int[4] { 2, 3, 4, 7 };

            using StreamReader sr = new("input\\Day08.txt");
            string? line = null;

            int numUniqueNumbers = 0;
            while ((line = sr.ReadLine()) != null)
            {
                SevenSegmentDisplayEntry entry = GetSevenSegmentDisplayEntry(line);
                numUniqueNumbers += entry.OutputValue.Count(v => uniqueSegmentAmounts.Contains(v.Length));
            }

            return numUniqueNumbers.ToString();
        }

        private static SevenSegmentDisplayEntry GetSevenSegmentDisplayEntry(string line)
        {
            string[] split = line.Split('|');
            if (split.Length != 2)
            {
                throw new InvalidOperationException("Bad input");
            }

            return new()
            {
                SignalPatterns = new(split[0].Trim().Split(' ', StringSplitOptions.RemoveEmptyEntries)),
                OutputValue = new(split[1].Trim().Split(' ', StringSplitOptions.RemoveEmptyEntries)),
            };
        }
    }
}

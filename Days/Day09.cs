//-----------------------------------------------------------------------
// <copyright file="Day09.cs" company="Andy Young">
//     Copyright (c) Andy Young. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace AdventOfCode2021.Days
{
    using AdventOfCode2021.Models;

    /// <summary>
    /// Calculates the solution for the particular day.
    /// </summary>
    internal class Day09 : IDay
    {
        /// <summary>
        /// Calculates the solution for the particular day.
        /// </summary>
        /// <returns>The solution.</returns>
        public string GetSolution()
        {
            string[] lines = File.ReadAllLines("input\\Day09.txt");
            if (lines.Length == 0)
            {
                throw new InvalidOperationException("Input is bad.");
            }

            HeightMap heightMap = new(lines);
            int sum = heightMap.GetSumOfLowPointRiskLevels();

            return sum.ToString();
        }
    }
}

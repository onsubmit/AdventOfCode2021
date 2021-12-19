//-----------------------------------------------------------------------
// <copyright file="Day18.cs" company="Andy Young">
//     Copyright (c) Andy Young. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace AdventOfCode2021.Days
{
    using AdventOfCode2021.Models;

    /// <summary>
    /// Calculates the solution for the particular day.
    /// </summary>
    internal class Day18 : IDay
    {
        /// <summary>
        /// Calculates the solution for the particular day.
        /// </summary>
        /// <returns>The solution.</returns>
        public string GetSolution()
        {
            string[] lines = File.ReadAllLines("input\\Day18.txt");

            int maxMagnitude = int.MinValue;
            for (int i = 0; i < lines.Length; i++)
            {
                for (int j = 0; j < lines.Length; j++)
                {
                    if (i == j)
                    {
                        continue;
                    }

                    SnailfishNumber sum = new SnailfishNumber(lines[i]) + new SnailfishNumber(lines[j]);
                    maxMagnitude = Math.Max(maxMagnitude, sum.Magnitude);
                }
            }

            return maxMagnitude.ToString();
        }
    }
}

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
            using StreamReader sr = new("input\\Day18.txt");
            string? line = null;

            List<SnailfishNumber> numbers = new();
            while ((line = sr.ReadLine()) != null)
            {
                numbers.Add(new(line));
            }

            SnailfishNumber sum = numbers[0];
            for (int i = 1; i < numbers.Count; i++)
            {
                sum += numbers[i];
            }

            return sum.Magnitude.ToString();
        }
    }
}

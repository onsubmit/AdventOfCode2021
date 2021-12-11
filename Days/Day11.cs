//-----------------------------------------------------------------------
// <copyright file="Day11.cs" company="Andy Young">
//     Copyright (c) Andy Young. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace AdventOfCode2021.Days
{
    using AdventOfCode2021.Models;

    /// <summary>
    /// Calculates the solution for the particular day.
    /// </summary>
    internal class Day11 : IDay
    {
        /// <summary>
        /// Calculates the solution for the particular day.
        /// </summary>
        /// <returns>The solution.</returns>
        public string GetSolution()
        {
            string[] lines = File.ReadAllLines("input\\Day11.txt");
            Octopi octopi = new(lines);

            for (int i = 1; i <= 1000; i++)
            {
                octopi.Step();
                if (octopi.AreAllOctopiFlashing)
                {
                    return i.ToString();
                }
            }

            throw new InvalidOperationException("All octopi never flashed simultanesouly.");
        }
    }
}

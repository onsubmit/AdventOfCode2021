//-----------------------------------------------------------------------
// <copyright file="Day12.cs" company="Andy Young">
//     Copyright (c) Andy Young. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace AdventOfCode2021.Days
{
    using AdventOfCode2021.Models;

    /// <summary>
    /// Calculates the solution for the particular day.
    /// </summary>
    internal class Day12 : IDay
    {
        /// <summary>
        /// Calculates the solution for the particular day.
        /// </summary>
        /// <returns>The solution.</returns>
        public string GetSolution()
        {
            using StreamReader sr = new("input\\Day12.txt");
            string? line = null;

            CaveGraph caves = new();
            while ((line = sr.ReadLine()) != null)
            {
                caves.AddConnectedCaves(line);
            }

            List<List<Cave>> paths = caves.GetPaths();
            int solution = paths.Count(l => l.Any(c => c.IsSmall));
            return solution.ToString();
        }
    }
}

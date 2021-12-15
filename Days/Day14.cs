//-----------------------------------------------------------------------
// <copyright file="Day14.cs" company="Andy Young">
//     Copyright (c) Andy Young. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace AdventOfCode2021.Days
{
    using AdventOfCode2021.Models;

    /// <summary>
    /// Calculates the solution for the particular day.
    /// </summary>
    internal class Day14 : IDay
    {
        /// <summary>
        /// Calculates the solution for the particular day.
        /// </summary>
        /// <returns>The solution.</returns>
        public string GetSolution()
        {
            using StreamReader sr = new("input\\Day14.txt");
            string? line = sr.ReadLine();
            if (line == null)
            {
                throw new InvalidOperationException("Input is bad.");
            }

            Polymer polymer = new(line);

            while ((line = sr.ReadLine()) != null)
            {
                if (string.IsNullOrWhiteSpace(line))
                {
                    // Blank line between template and rules.
                    continue;
                }

                string[] split = line.Split("->");
                string pair = split[0].Trim();
                char element = split[1].Trim()[0];
                polymer.AddPairInsertionRule(pair, element);
            }

            for (int i = 0; i < 10; i++)
            {
                polymer.RunInsertionRules();
            }

            int mostCommonElementCount = polymer.ElementCounts.MaxBy(kvp => kvp.Value).Value;
            int leastCommonElementCount = polymer.ElementCounts.MinBy(kvp => kvp.Value).Value;
            int solution = mostCommonElementCount - leastCommonElementCount;

            return solution.ToString();
        }
    }
}

//-----------------------------------------------------------------------
// <copyright file="Day07.cs" company="Andy Young">
//     Copyright (c) Andy Young. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace AdventOfCode2021.Days
{
    /// <summary>
    /// Calculates the solution for the particular day.
    /// </summary>
    internal class Day07 : IDay
    {
        /// <summary>
        /// Calculates the solution for the particular day.
        /// </summary>
        /// <returns>The solution.</returns>
        public string GetSolution()
        {
            using StreamReader sr = new("input\\Day07.txt");
            string? line = sr.ReadLine();

            if (line == null)
            {
                throw new InvalidOperationException("Input is bad");
            }

            int[] crabPositions = line.Split(",").Select(s => int.Parse(s)).ToArray();
            int maxPosition = crabPositions.Max();

            int[] moveDistanceCosts = Enumerable.Range(0, maxPosition + 1).Select(n => n * (n + 1) / 2).ToArray();
            long sum = crabPositions.Sum(position => moveDistanceCosts[position]);

            long minFuelCost = long.MaxValue;
            for (int horizontalPosition = 0; horizontalPosition <= maxPosition; horizontalPosition++)
            {
                long fuelAdjustment = crabPositions.Sum(position => moveDistanceCosts[Math.Abs(horizontalPosition - position)] - moveDistanceCosts[position]);
                minFuelCost = Math.Min(minFuelCost, sum + fuelAdjustment);
            }

            return minFuelCost.ToString();
        }
    }
}

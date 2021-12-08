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

            // Sorting the positions makes the fuel cost calculations a bit easier below.
            int[] crabPositions = line.Split(",").Select(s => int.Parse(s)).OrderBy(x => x).ToArray();

            long sum = crabPositions.Sum();
            int maxPosition = crabPositions[^1];

            // The fuel needed to meet at x = 0 is the sum of the crab's initial positions.
            long minFuelCost = sum;
            for (int horizontalPosition = 1; horizontalPosition <= maxPosition; horizontalPosition++)
            {
                long runningFuelCost = sum;
                for (int i = 0; i < crabPositions.Length; i++)
                {
                    if (crabPositions[i] < horizontalPosition)
                    {
                        // These crabs now need to move to the right, away from 0.
                        // Example 1:
                        // How much more fuel would be required if a crab starting at 5
                        // ended at 12 instead of 0? 12 - 5 - 5 = 2 units of fuel.
                        //
                        // Example 2:
                        // How much more fuel would be required if a crab starting at 7
                        // ended at 9 instead of 0? 9 - 7 - 7 = -5 units of fuel.
                        runningFuelCost += horizontalPosition - (2 * crabPositions[i]);
                    }
                    else
                    {
                        // These crabs still have to move left toward 0, but not as far.
                        // Example:
                        // How much less fuel would be required if a crab starting at 12
                        // ended at 4 instead of 0? 4 units, of course, since they only have to
                        // move to 4 instead of 0.
                        runningFuelCost -= horizontalPosition * (crabPositions.Length - i);
                        break;
                    }
                }

                minFuelCost = Math.Min(minFuelCost, runningFuelCost);
            }

            return minFuelCost.ToString();
        }
    }
}

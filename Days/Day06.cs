//-----------------------------------------------------------------------
// <copyright file="Day06.cs" company="Andy Young">
//     Copyright (c) Andy Young. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace AdventOfCode2021.Days
{
    /// <summary>
    /// Calculates the solution for the particular day.
    /// </summary>
    internal class Day06 : IDay
    {
        /// <summary>
        /// Calculates the solution for the particular day.
        /// </summary>
        /// <returns>The solution.</returns>
        public string GetSolution()
        {
            const int NumDays = 80;
            const int NumDaysToSpawn = 6;
            const int NumExtraDaysForNewFishToSpawn = 2;

            using StreamReader sr = new("input\\Day06.txt");
            string? line = sr.ReadLine();

            if (line == null)
            {
                throw new InvalidOperationException("Input is bad");
            }

            List<uint> fish = line.Split(",").Select(s => uint.Parse(s)).ToList();
            for (int day = 0; day < NumDays; day++)
            {
                int numFishAtDayStart = fish.Count;
                for (int i = 0; i < numFishAtDayStart; i++)
                {
                    if (fish[i] == 0)
                    {
                        fish[i] = NumDaysToSpawn;
                        fish.Add(NumDaysToSpawn + NumExtraDaysForNewFishToSpawn);
                    }
                    else
                    {
                        fish[i]--;
                    }
                }
            }

            return fish.Count.ToString();
        }
    }
}

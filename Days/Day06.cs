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
            const int NumDays = 256;
            const int NumDaysToSpawn = 6;
            const int NumExtraDaysForNewFishToSpawn = 2;
            const int NumDaysToSpawnForNewFish = NumDaysToSpawn + NumExtraDaysForNewFishToSpawn;

            using StreamReader sr = new("input\\Day06.txt");
            string? line = sr.ReadLine();

            if (line == null)
            {
                throw new InvalidOperationException("Input is bad");
            }

            List<uint> fish = line.Split(",").Select(s => uint.Parse(s)).ToList();
            long[] fishCounts = new long[NumDaysToSpawn + NumExtraDaysForNewFishToSpawn + 1];
            foreach (long f in fish)
            {
                fishCounts[f]++;
            }

            for (int day = 0; day < NumDays; day++)
            {
                long numFishThatWillSpawn = fishCounts[0];

                for (int i = 0; i < fishCounts.Length - 1; i++)
                {
                    fishCounts[i] = fishCounts[i + 1];
                }

                fishCounts[NumDaysToSpawn] += numFishThatWillSpawn;
                fishCounts[NumDaysToSpawnForNewFish] = numFishThatWillSpawn;
            }

            return fishCounts.Sum().ToString();
        }
    }
}

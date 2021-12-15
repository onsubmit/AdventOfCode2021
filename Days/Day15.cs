//-----------------------------------------------------------------------
// <copyright file="Day15.cs" company="Andy Young">
//     Copyright (c) Andy Young. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace AdventOfCode2021.Days
{
    using AdventOfCode2021.Models;

    /// <summary>
    /// Calculates the solution for the particular day.
    /// </summary>
    [Skip("Part 2 takes about 8 minutes")]
    internal class Day15 : IDay
    {
        private static readonly Dictionary<Coordinate, int> TotalRisks = new();
        private static readonly Dictionary<Coordinate, Coordinate> Previous = new();
        private static readonly MinPriorityQueue<Coordinate, int> Queue = new();
        private static readonly Dictionary<Coordinate, int> Risks = new();

        private static int width;
        private static int height;

        private static Coordinate start;
        private static Coordinate end;

        /// <summary>
        /// Calculates the solution for the particular day.
        /// </summary>
        /// <returns>The solution.</returns>
        public string GetSolution()
        {
            BuildRisksFromInput();
            BuildTotalRisks();
            BuildQueue();
            ProcessQueue();

            int totalRisk = 0;
            for (Coordinate target = end; Previous.ContainsKey(target); target = Previous[target])
            {
                totalRisk += Risks[target];
            }

            return totalRisk.ToString();
        }

        /// <summary>
        /// Processes the queue.
        /// </summary>
        private static void ProcessQueue()
        {
            while (Queue.TryDequeue(out Coordinate coordinate, out int _) && coordinate != end)
            {
                foreach (Coordinate direction in Directions.Cardinal)
                {
                    Coordinate neighbor = coordinate + direction;

                    if (!neighbor.IsInRange(start, end))
                    {
                        continue;
                    }

                    int risk = TotalRisks[coordinate] + Risks[neighbor];
                    if (risk < TotalRisks[neighbor])
                    {
                        TotalRisks[neighbor] = risk;
                        Previous[neighbor] = coordinate;
                        Queue.SetPriority(neighbor, risk);
                    }
                }
            }
        }

        /// <summary>
        /// Builds the total risks.
        /// </summary>
        private static void BuildTotalRisks()
        {
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    Coordinate coordinate = new(x, y);
                    TotalRisks[coordinate] = coordinate == start ? 0 : int.MaxValue;
                }
            }
        }

        private static void BuildQueue()
        {
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    Coordinate coordinate = new(x, y);
                    Queue.Enqueue(coordinate, TotalRisks[coordinate]);
                }
            }
        }

        /// <summary>
        /// Builds the risks from the input file.
        /// </summary>
        private static void BuildRisksFromInput()
        {
            const int LargerBy = 5;

            string[] lines = File.ReadAllLines("input\\Day15.txt");
            width = lines.Length;
            height = lines[0].Length;

            for (int i = 0; i < LargerBy; i++)
            {
                for (int j = 0; j < LargerBy; j++)
                {
                    for (int x = 0; x < width; x++)
                    {
                        for (int y = 0; y < height; y++)
                        {
                            int value = (int)char.GetNumericValue(lines[x][y]) + i + j;
                            value = value > 9 ? value % 9 : value;

                            Coordinate coordinate = new(x + (i * width), y + (j * height));
                            Risks[coordinate] = value;
                        }
                    }
                }
            }

            width *= LargerBy;
            height *= LargerBy;

            start = new(0, 0);
            end = new(width - 1, height - 1);
        }
    }
}

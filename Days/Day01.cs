//-----------------------------------------------------------------------
// <copyright file="Day01.cs" company="Andy Young">
//     Copyright (c) Andy Young. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace AdventOfCode2021.Days
{
    /// <summary>
    /// Calculates the solution for the particular day.
    /// </summary>
    internal class Day01 : IDay
    {
        /// <summary>
        /// Gets the number of depth increases.
        /// </summary>
        /// <returns>The number of depth increases.</returns>
        public string GetSolution()
        {
            int increases = 0;

            using StreamReader sr = new StreamReader("input\\Day01.txt");
            string? previousLine = sr.ReadLine();
            string? currentLine = null;
            while ((currentLine = sr.ReadLine()) != null)
            {
                if (int.TryParse(previousLine, out int previousDepth)
                    && int.TryParse(currentLine, out int currentDepth)
                    && currentDepth > previousDepth)
                {
                    increases++;
                }

                previousLine = currentLine;
            }

            return increases.ToString();
        }
    }
}

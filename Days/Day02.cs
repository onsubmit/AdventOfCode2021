//-----------------------------------------------------------------------
// <copyright file="Day02.cs" company="Andy Young">
//     Copyright (c) Andy Young. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace AdventOfCode2021.Days
{
    using AdventOfCode2021.Models;

    /// <summary>
    /// Calculates the solution for the particular day.
    /// </summary>
    internal class Day02 : IDay
    {
        /// <summary>
        /// Calculates the solution for the particular day.
        /// </summary>
        /// <returns>The solution.</returns>
        public string GetSolution()
        {
            Submarine sub = new();

            using StreamReader sr = new("input\\Day02.txt");
            string? line = null;

            while ((line = sr.ReadLine()) != null)
            {
                Vector vector = GetVectorFromLine(line);

                sub.Move(vector);
            }

            int solution = sub.HorizontalPosition * sub.Depth;
            return solution.ToString();
        }

        /// <summary>
        /// Gets the vector corresponding to the given input line.
        /// </summary>
        /// <param name="line">The input line.</param>
        /// <returns>The vector.</returns>
        private static Vector GetVectorFromLine(string line)
        {
            if (line == null)
            {
                throw new ArgumentNullException(nameof(line));
            }

            string[] parts = line.Split(' ');
            if (parts.Length != 2)
            {
                throw new ArgumentException($"Invalid format: ${line}", nameof(line));
            }

            if (!Enum.TryParse(parts[0], true, out Direction direction))
            {
                throw new InvalidOperationException($"Cannot parse direction: ${parts[0]}");
            }

            if (!int.TryParse(parts[1], out int distance))
            {
                throw new InvalidOperationException($"Cannot parse distance: ${parts[1]}");
            }

            return new Vector(direction, distance);
        }
    }
}

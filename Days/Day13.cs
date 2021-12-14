//-----------------------------------------------------------------------
// <copyright file="Day13.cs" company="Andy Young">
//     Copyright (c) Andy Young. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace AdventOfCode2021.Days
{
    using System.Text.RegularExpressions;
    using AdventOfCode2021.Models;

    /// <summary>
    /// Calculates the solution for the particular day.
    /// </summary>
    internal class Day13 : IDay
    {
        /// <summary>
        /// Calculates the solution for the particular day.
        /// </summary>
        /// <returns>The solution.</returns>
        public string GetSolution()
        {
            using StreamReader sr = new("input\\Day13.txt");
            string? line = null;

            Regex foldRegex = new("^fold along (?<DIMENSION>[x|y])=(?<POSITION>\\d+)$");

            List<Coordinate> coordinates = new();
            List<Fold> folds = new();
            while ((line = sr.ReadLine()) != null)
            {
                if (string.IsNullOrWhiteSpace(line))
                {
                    // Blank line between coordinates and folds.
                    continue;
                }

                Match match = foldRegex.Match(line);
                if (match.Success)
                {
                    if (!Enum.TryParse(match.Groups["DIMENSION"].Value, true /* ignoreCase */, out Dimension dimension)
                        || !int.TryParse(match.Groups["POSITION"].Value, out int position))
                    {
                        throw new InvalidOperationException("Input is bad");
                    }

                    folds.Add(new Fold(dimension, position));
                }
                else
                {
                    string[] coords = line.Split(',');
                    if (coords.Length != 2)
                    {
                        throw new InvalidOperationException("Input is bad");
                    }

                    coordinates.Add(new() { X = int.Parse(coords[0]), Y = int.Parse(coords[1]) });
                }
            }

            Paper paper = new(coordinates);
            int solution = paper.Fold(folds[0]).VisibleDots;
            return solution.ToString();
        }
    }
}

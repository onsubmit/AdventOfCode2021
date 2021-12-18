//-----------------------------------------------------------------------
// <copyright file="Day17.cs" company="Andy Young">
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
    internal class Day17 : IDay
    {
        /// <summary>
        /// Calculates the solution for the particular day.
        /// </summary>
        /// <returns>The solution.</returns>
        public string GetSolution()
        {
            using StreamReader sr = new("input\\Day17.txt");
            string? line = sr.ReadLine();
            if (string.IsNullOrWhiteSpace(line))
            {
                throw new InvalidOperationException("Input is bad.");
            }

            // target area: x=20..30, y=-10..-5
            Regex regex = new(@"target area: x=(?<XMIN>-?\d+)..(?<XMAX>-?\d+), y=(?<YMIN>-?\d+)..(?<YMAX>-?\d+)");
            Match match = regex.Match(line);
            if (!match.Success)
            {
                throw new InvalidOperationException("Input is bad.");
            }

            Coordinate min = new(int.Parse(match.Groups["XMIN"].Value), int.Parse(match.Groups["YMIN"].Value));
            Coordinate max = new(int.Parse(match.Groups["XMAX"].Value), int.Parse(match.Groups["YMAX"].Value));

            // This is arbitrary but anything more than this never applies for this problem.
            const int MaxY = 130;

            int maxY = int.MinValue;
            List<Coordinate> velocities = new();

            // Note: these bounds wouldn't work if the target had negative x-values.
            for (int x = 1; x <= max.X; x++)
            {
                for (int y = min.Y; y <= MaxY; y++)
                {
                    Coordinate initialVelocity = new(x, y);
                    if (TryLaunch(initialVelocity, min, max, out Coordinate finalPosition, out int tempMaxY))
                    {
                        velocities.Add(initialVelocity);
                        maxY = Math.Max(maxY, tempMaxY);
                    }
                    else if (finalPosition.Y > max.Y)
                    {
                        // We didn't come down far enough.
                        // No need to try anymore y-values.
                        break;
                    }
                }
            }

            string solution = $"Part 1: {maxY}. Part 2: {velocities.Count}";
            return solution;
        }

        /// <summary>
        /// Tries to launch at the given initial velocity.
        /// </summary>
        /// <param name="initialVelocity">The initial velocity.</param>
        /// <param name="targetMin">The minimum coordinates of the target.</param>
        /// <param name="targetMax">The maximum coordinates of the target.</param>
        /// <param name="finalPosition">The final position of the trajectory.</param>
        /// <param name="maxY">The maximum y-value obtained by the trajectory.</param>
        /// <returns><c>true</c> if the target was reached, <c>false</c> otherwise.</returns>
        private static bool TryLaunch(Coordinate initialVelocity, Coordinate targetMin, Coordinate targetMax, out Coordinate finalPosition, out int maxY)
        {
            Coordinate position = new(0, 0);
            Coordinate velocity = initialVelocity;

            maxY = int.MinValue;
            while (true)
            {
                maxY = Math.Max(maxY, position.Y);
                finalPosition = position;

                if (position.IsInRange(targetMin, targetMax))
                {
                    // Direct hit!
                    return true;
                }

                if (velocity.X == 0)
                {
                    // No more horizontal velocity.
                    if (position.X < targetMin.X || position.X > targetMax.X)
                    {
                        // We're to the left or right of the target and will never reach it.
                        return false;
                    }

                    if (position.Y < targetMax.Y && velocity.Y <= 0)
                    {
                        // We're below the target and still falling. We'll never come back up to it.
                        return false;
                    }
                }

                // The probe's x position increases by its x velocity.
                position.X += velocity.X;

                // The probe's y position increases by its y velocity.
                position.Y += velocity.Y;

                // Due to drag, the probe's x velocity changes by 1 toward the value 0.
                if (velocity.X < 0)
                {
                    velocity.X++;
                }
                else if (velocity.X > 0)
                {
                    velocity.X--;
                }

                // Due to gravity, the probe's y velocity decreases by 1.
                velocity.Y--;
            }
        }
    }
}

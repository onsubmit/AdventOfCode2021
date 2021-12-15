//-----------------------------------------------------------------------
// <copyright file="Day05.cs" company="Andy Young">
//     Copyright (c) Andy Young. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace AdventOfCode2021.Days
{
    using AdventOfCode2021.Models;

    /// <summary>
    /// Calculates the solution for the particular day.
    /// </summary>
    internal class Day05 : IDay
    {
        /// <summary>
        /// Calculates the solution for the particular day.
        /// </summary>
        /// <returns>The solution.</returns>
        public string GetSolution()
        {
            int solution = 0;

            using StreamReader sr = new("input\\Day05.txt");
            string? line = null;

            Coordinate minCoordinates = new(int.MaxValue, int.MaxValue);
            Coordinate maxCoordinates = new(int.MinValue, int.MinValue);

            List<VentReading> readings = new();
            while ((line = sr.ReadLine()) != null)
            {
                VentReading reading = GetVentReadingFromLine(line);
                readings.Add(reading);

                foreach (Coordinate coordinate in new[] { reading.Start, reading.End })
                {
                    minCoordinates.X = Math.Min(minCoordinates.X, coordinate.X);
                    minCoordinates.Y = Math.Min(minCoordinates.Y, coordinate.Y);

                    maxCoordinates.X = Math.Max(maxCoordinates.X, coordinate.X);
                    maxCoordinates.Y = Math.Max(maxCoordinates.Y, coordinate.Y);
                }
            }

            VentDiagram ventDiagram = new(maxCoordinates.X, maxCoordinates.Y);
            foreach (VentReading reading in readings)
            {
                ventDiagram.RecordReading(reading);
            }

            for (int x = 0; x < maxCoordinates.X; x++)
            {
                for (int y = 0; y < maxCoordinates.Y; y++)
                {
                    if (ventDiagram[x, y] >= 2)
                    {
                        solution++;
                    }
                }
            }

            return solution.ToString();
        }

        /// <summary>
        /// Gets the vent reading from the input line.
        /// </summary>
        /// <param name="line">The line from the input.</param>
        /// <returns>The vent reading.</returns>
        /// <exception cref="InvalidOperationException">Thrown if input is bad.</exception>
        private static VentReading GetVentReadingFromLine(string line)
        {
            string[] pointStrings = line.Split("->");
            if (pointStrings.Length != 2)
            {
                throw new InvalidOperationException($"Line did not contain two points: {line}");
            }

            Coordinate[] coordinates = pointStrings.Select(point =>
            {
                string[] pointParts = point.Split(',');
                if (pointParts.Length != 2)
                {
                    throw new InvalidOperationException($"Point did not contain two coordinates: {point}");
                }

                return new Coordinate()
                {
                    X = int.Parse(pointParts[0].Trim()),
                    Y = int.Parse(pointParts[1].Trim()),
                };
            }).ToArray();

            return new VentReading() { Start = coordinates[0], End = coordinates[1] };
        }
    }
}

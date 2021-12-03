//-----------------------------------------------------------------------
// <copyright file="Day03.cs" company="Andy Young">
//     Copyright (c) Andy Young. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace AdventOfCode2021.Days
{
    /// <summary>
    /// Calculates the solution for the particular day.
    /// </summary>
    internal class Day03 : IDay
    {
        /// <summary>
        /// Calculates the solution for the particular day.
        /// </summary>
        /// <returns>The solution.</returns>
        public string GetSolution()
        {
            int[] commonBits = GetMostCommonBits();
            int gammaRate = GetDecimalFromBits(commonBits);
            int epsilonRate = GetDecimalFromBits(commonBits.Select(b => b == 0 ? 1 : 0));
            int powerConsumption = gammaRate * epsilonRate;

            return powerConsumption.ToString();
        }

        /// <summary>
        /// Gets the most common bits from the input.
        /// </summary>
        /// <returns>Array of the most common bits.</returns>
        /// <exception cref="InvalidOperationException">Thrown when input is bad.</exception>
        private static int[] GetMostCommonBits()
        {
            using StreamReader sr = new("input\\Day03.txt");
            string? line = sr.ReadLine();

            if (line == null)
            {
                throw new InvalidOperationException("Bad input");
            }

            int[] bits = new int[line.Length];

            do
            {
                for (int i = 0; i < line.Length; i++)
                {
                    bits[i] += line[i] switch
                    {
                        '0' => -1, // Subtract 1 for 0
                        '1' => 1, // Add 1 for 1
                        _ => throw new InvalidOperationException("Input line must be binary"),
                    };
                }
            }
            while ((line = sr.ReadLine()) != null);

            for (int i = 0; i < bits.Length; i++)
            {
                // If bit is positive, it's the most common one.
                bits[i] = bits[i] switch
                {
                    > 0 => 1,
                    < 0 => 0,
                    _ => throw new InvalidOperationException($"Bit {i} was equally common. Undefined behavior."),
                };
            }

            return bits;
        }

        /// <summary>
        /// Converts a bit array into a decimal string.
        /// </summary>
        /// <param name="bits">The bit array.</param>
        /// <returns>Decimal string.</returns>
        private static int GetDecimalFromBits(IEnumerable<int> bits)
        {
            return Convert.ToInt32(string.Join(string.Empty, bits), 2);
        }
    }
}

//-----------------------------------------------------------------------
// <copyright file="Day03.cs" company="Andy Young">
//     Copyright (c) Andy Young. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace AdventOfCode2021.Days
{
    using AdventOfCode2021.Models;

    /// <summary>
    /// Calculates the solution for the particular day.
    /// </summary>
    internal class Day03 : IDay
    {
        private static readonly List<int[]> AllBits = GetAllBits();

        /// <summary>
        /// Calculates the solution for the particular day.
        /// </summary>
        /// <returns>The solution.</returns>
        public string GetSolution()
        {
            int oxygenGeneratorRating = GetOxygenGeneratorRating();
            int carbonDioxideScrubberRating = GetCO2ScrubberRating();
            int lifeSupportRating = oxygenGeneratorRating * carbonDioxideScrubberRating;

            return lifeSupportRating.ToString();
        }

        /// <summary>
        /// Gets all the bits from the input.
        /// </summary>
        /// <returns>All the bits.</returns>
        private static List<int[]> GetAllBits()
        {
            List<int[]> allBits = new();
            using StreamReader sr = new("input\\Day03.txt");

            string? line;
            while ((line = sr.ReadLine()) != null)
            {
                int[] bits = line.Select(c => c == '1' ? 1 : 0).ToArray();
                allBits.Add(bits);
            }

            return allBits;
        }

        /// <summary>
        /// Gets the oxygen generator rating.
        /// </summary>
        /// <returns>The oxygen generator rating.</returns>
        private static int GetOxygenGeneratorRating() => GetRating(CommonBitStrategy.Most);

        /// <summary>
        /// Gets the C02 scrubber rating.
        /// </summary>
        /// <returns>The C02 scrubber rating.</returns>
        private static int GetCO2ScrubberRating() => GetRating(CommonBitStrategy.Least);

        /// <summary>
        /// Gets a rating.
        /// </summary>
        /// <param name="strategy">The strategy to use to determine the common bits.</param>
        /// <returns>The rating.</returns>
        /// <exception cref="InvalidOperationException">Thrown if input is bad.</exception>
        private static int GetRating(CommonBitStrategy strategy)
        {
            List<int[]> filteredBits = new(AllBits);
            int[] commonBits = GetMostCommonBits(filteredBits, strategy);

            for (int i = 0; i < commonBits.Length; i++)
            {
                filteredBits = filteredBits.Where(b => b[i] == commonBits[i]).ToList();

                if (filteredBits.Count == 1)
                {
                    return GetDecimalFromBits(filteredBits[0]);
                }

                commonBits = GetMostCommonBits(filteredBits, strategy);
            }

            throw new InvalidOperationException("Could not determine rating");
        }

        /// <summary>
        /// Gets the list of most common bit for the given bits.
        /// </summary>
        /// <param name="bits">The bits to find the most common bit.</param>
        /// <param name="strategy">The strategy to use to determine the common bits.</param>
        /// <returns>The list of common bits.</returns>
        private static int[] GetMostCommonBits(List<int[]> bits, CommonBitStrategy strategy)
        {
            if (bits.Count == 0)
            {
                return Array.Empty<int>();
            }

            int[] commonBits = new int[bits[0].Length];
            for (int i = 0; i < bits.Count; i++)
            {
                for (int j = 0; j < bits[i].Length; j++)
                {
                    // Increment for ones.
                    // Decrement for zeros.
                    commonBits[j] += bits[i][j] switch
                    {
                        0 => -1,
                        1 => 1,
                        _ => throw new InvalidOperationException("Input line must be binary"),
                    };
                }
            }

            // Determine most/least common bits.
            for (int i = 0; i < commonBits.Length; i++)
            {
                commonBits[i] = commonBits[i] switch
                {
                    < 0 => strategy == CommonBitStrategy.Most ? 0 : 1,
                    _ => strategy == CommonBitStrategy.Most ? 1 : 0,
                };
            }

            return commonBits;
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

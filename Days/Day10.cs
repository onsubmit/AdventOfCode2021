//-----------------------------------------------------------------------
// <copyright file="Day10.cs" company="Andy Young">
//     Copyright (c) Andy Young. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace AdventOfCode2021.Days
{
    /// <summary>
    /// Calculates the solution for the particular day.
    /// </summary>
    internal class Day10 : IDay
    {
        /// <summary>
        /// The pairs of matching characters.
        /// </summary>
        private static readonly Dictionary<char, char> Pairs = new()
        {
            { '(', ')' },
            { '[', ']' },
            { '{', '}' },
            { '<', '>' },
        };

        /// <summary>
        /// The scores for each closing character.
        /// </summary>
        private static readonly Dictionary<char, int> Scores = new()
        {
            { ')', 1 },
            { ']', 2 },
            { '}', 3 },
            { '>', 4 },
        };

        /// <summary>
        /// Calculates the solution for the particular day.
        /// </summary>
        /// <returns>The solution.</returns>
        public string GetSolution()
        {
            using StreamReader sr = new("input\\Day10.txt");
            List<long> sums = new();
            string? line = null;

            while ((line = sr.ReadLine()) != null)
            {
                if (!TryParseLine(line, out Stack<char> stack))
                {
                    continue;
                }

                if (stack.Count > 0)
                {
                    // The line is not corrupt, but it is incomplete.
                    long sum = 0;

                    while (stack.TryPop(out char c))
                    {
                        sum *= 5;
                        sum += Scores[Pairs[c]];
                    }

                    sums.Add(sum);
                }
            }

            if (sums.Count % 2 != 1)
            {
                throw new InvalidOperationException("There is supposed to be an odd number of scores.");
            }

            long solution = sums.OrderBy(s => s).ElementAt(sums.Count / 2);
            return solution.ToString();
        }

        /// <summary>
        /// Determines if the line is corrupt.
        /// </summary>
        /// <param name="line">The line from the input file.</param>
        /// <param name="stack">The remaining unclosed characters.</param>
        /// <returns><c>true</c> if the line is corrupt, <c>false</c> otherwise.</returns>
        private static bool TryParseLine(string line, out Stack<char> stack)
        {
            stack = new();

            foreach (char c in line)
            {
                if (Pairs.ContainsKey(c))
                {
                    // Opening character found.
                    stack.Push(c);
                }
                else if (c == Pairs[stack.Peek()])
                {
                    // Closing character find that corresponds to the most recent opening character.
                    stack.Pop();
                }
                else
                {
                    // Closing character find that does not correspond to the most recent opening character.
                    // Line is corrupt.
                    return false;
                }
            }

            return true;
        }
    }
}

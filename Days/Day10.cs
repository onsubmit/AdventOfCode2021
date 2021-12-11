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
        /// Calculates the solution for the particular day.
        /// </summary>
        /// <returns>The solution.</returns>
        public string GetSolution()
        {
            Dictionary<char, char> pairs = new()
            {
                { '(', ')' },
                { '[', ']' },
                { '{', '}' },
                { '<', '>' },
            };

            Dictionary<char, int> scores = new()
            {
                { ')', 3 },
                { ']', 57 },
                { '}', 1197 },
                { '>', 25137 },
            };

            using StreamReader sr = new("input\\Day10.txt");
            string? line = null;

            int sum = 0;
            Stack<char> stack = new();
            while ((line = sr.ReadLine()) != null)
            {
                stack.Clear();
                foreach (char c in line)
                {
                    if (pairs.ContainsKey(c))
                    {
                        stack.Push(c);
                    }
                    else if (c == pairs[stack.Peek()])
                    {
                        stack.Pop();
                    }
                    else
                    {
                        sum += scores[c];
                        break;
                    }
                }
            }

            return sum.ToString();
        }
    }
}

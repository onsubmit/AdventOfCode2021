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
            int windowSize = 3;

            using StreamReader sr = new("input\\Day01.txt");

            // TODO: Write a general solution that works for any window size.
            if (windowSize == 1)
            {
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

            List<int> window = GetWindow(sr, (2 * windowSize) - 1);

            int index = 0;
            int previousSum = 0;
            bool previousSumKnown = false;
            while (window.Count == (2 * windowSize) - 1)
            {
                int currentSum = 0;
                for (int i = index; i < index + windowSize; i++)
                {
                    if (!previousSumKnown)
                    {
                        previousSum += window[i];
                    }

                    currentSum += window[i + 1];
                }

                if (currentSum > previousSum)
                {
                    increases++;
                }

                index++;
                previousSum = currentSum;
                previousSumKnown = true;

                if (index + windowSize >= window.Count)
                {
                    window = window.GetRange(windowSize, window.Count - windowSize);
                    window.AddRange(GetWindow(sr, windowSize));
                    index = -1;
                }
            }

            return increases.ToString();
        }

        /// <summary>
        /// Gets the measurement window of size <paramref name="windowSize"/>.
        /// </summary>
        /// <param name="sr">The <see cref="StreamReader"/> for the depth input file.</param>
        /// <param name="windowSize">The size of the window.</param>
        /// <returns>A list of depth of readings. Can return incomplete windows.</returns>
        private static List<int> GetWindow(StreamReader sr, int windowSize)
        {
            List<int> window = new();
            for (int i = 0; i < windowSize; i++)
            {
                string? line = sr.ReadLine();

                if (line == null || !int.TryParse(line, out int depth))
                {
                    return window;
                }

                window.Add(depth);
            }

            return window;
        }
    }
}

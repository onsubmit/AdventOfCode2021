//-----------------------------------------------------------------------
// <copyright file="SevenSegmentDisplayEntry.cs" company="Andy Young">
//     Copyright (c) Andy Young. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace AdventOfCode2021.Models
{
    /// <summary>
    /// Represents a 7-segment display entry.
    /// </summary>
    internal record struct SevenSegmentDisplayEntry
    {
        /// <summary>
        /// Gets or sets the signal patterns.
        /// </summary>
        public List<string> SignalPatterns { get; set; }

        /// <summary>
        /// Gets or sets the output value.
        /// </summary>
        public List<string> OutputValue { get; set; }

        /// <summary>
        /// Gets the decoded output value.
        /// </summary>
        /// <returns>The decoded output value.</returns>
        public int GetDecodedOutputValue()
        {
            Dictionary<string, int> patternToDigitMap = this.GetPatternToDigitMap();
            string outputValue = string.Join(string.Empty, this.OutputValue.Select(v => patternToDigitMap[v]));
            return int.Parse(outputValue);
        }

        /// <summary>
        /// Gets the mapping from pattern to actual digit.
        /// </summary>
        /// <returns>The mapping from pattern to actual digit.</returns>
        private Dictionary<string, int> GetPatternToDigitMap()
        {
            Dictionary<string, int> patternToDigitMap = new();
            List<string> remainingPatterns = new(this.SignalPatterns);

            // 1 is the only pattern with 2 segments.
            string one = remainingPatterns.Where(v => v.Length == 2).Single();
            remainingPatterns.Remove(one);
            patternToDigitMap.Add(one, 1);

            // 7 is the only pattern with 3 segments.
            string seven = remainingPatterns.Where(v => v.Length == 3).Single();
            remainingPatterns.Remove(seven);
            patternToDigitMap.Add(seven, 7);

            // 4 is the only pattern with 4 segments.
            string four = remainingPatterns.Where(v => v.Length == 4).Single();
            remainingPatterns.Remove(four);
            patternToDigitMap.Add(four, 4);

            // 8 is the only pattern with 7 segments.
            string eight = remainingPatterns.Where(v => v.Length == 7).Single();
            remainingPatterns.Remove(eight);
            patternToDigitMap.Add(eight, 8);

            // 3 is the only pattern with 5 segments that has the same segments as 1.
            string three = remainingPatterns.Where(v => v.Length == 5 && one.All(c => v.Contains(c))).Single();
            remainingPatterns.Remove(three);
            patternToDigitMap.Add(three, 3);

            // 9 is the only pattern with 6 segments that has the same segments as 3.
            string nine = remainingPatterns.Where(v => v.Length == 6 && three.All(c => v.Contains(c))).Single();
            remainingPatterns.Remove(nine);
            patternToDigitMap.Add(nine, 9);

            // 0 is the only remaining pattern with 6 segments that has the same segments as 1.
            string zero = remainingPatterns.Where(v => v.Length == 6 && one.All(c => v.Contains(c))).Single();
            remainingPatterns.Remove(zero);
            patternToDigitMap.Add(zero, 0);

            // 6 is the only remaining pattern with 6 segments.
            string six = remainingPatterns.Where(v => v.Length == 6).Single();
            remainingPatterns.Remove(six);
            patternToDigitMap.Add(six, 6);

            // 5 is the only remaining pattern with 5 segments where 6 has the same segments as it.
            string five = remainingPatterns.Where(v => v.Length == 5 && v.All(c => six.Contains(c))).Single();
            remainingPatterns.Remove(five);
            patternToDigitMap.Add(five, 5);

            // 2 is the only remaining pattern with 5 segments.
            string two = remainingPatterns.Where(v => v.Length == 5).Single();
            remainingPatterns.Remove(two);
            patternToDigitMap.Add(two, 2);

            return patternToDigitMap;
        }
    }
}

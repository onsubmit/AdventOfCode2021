//-----------------------------------------------------------------------
// <copyright file="Polymer.cs" company="Andy Young">
//     Copyright (c) Andy Young. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace AdventOfCode2021.Models
{
    /// <summary>
    /// Represents a polymer.
    /// </summary>
    internal class Polymer
    {
        private readonly Dictionary<string, char> pairInsertionRules = new();
        private readonly Dictionary<string, long> elementPairCounts = new();

        /// <summary>
        /// Initializes a new instance of the <see cref="Polymer"/> class.
        /// </summary>
        /// <param name="template">The template from the input file.</param>
        /// <exception cref="ArgumentException">Thrown if the input is bad.</exception>
        public Polymer(string template)
        {
            if (string.IsNullOrWhiteSpace(template))
            {
                throw new ArgumentException("The template must be non-empty", nameof(template));
            }

            if (template.Any(c => !char.IsLetter(c)))
            {
                throw new ArgumentException("The template must consist entirely of letters.", nameof(template));
            }

            for (int i = 0; i < template.Length - 1; i++)
            {
                string pair = $"{template[i]}{template[i + 1]}";
                this.IncrementPairCount(pair);
                this.IncrementElementCount(template[i]);
            }

            // Don't forget the last element.
            this.IncrementElementCount(template[^1]);
        }

        /// <summary>
        /// Gets the counts of all the individual elements.
        /// </summary>
        public Dictionary<char, long> ElementCounts { get; private set; } = new();

        /// <summary>
        /// Adds an element pair insertion rule.
        /// </summary>
        /// <param name="pair">The pair between which the element will be inserted.</param>
        /// <param name="element">The element to insert.</param>
        /// <exception cref="ArgumentException">Thrown if input is bad.</exception>
        public void AddPairInsertionRule(string pair, char element)
        {
            ValidatePair(pair);

            if (!char.IsLetter(element))
            {
                throw new ArgumentException("Element must be a letter", nameof(element));
            }

            this.pairInsertionRules.Add(pair, element);
        }

        /// <summary>
        /// Runs all the insertion rules once.
        /// </summary>
        public void RunInsertionRules()
        {
            Dictionary<string, (long, List<string>)> replacements = new();
            foreach (KeyValuePair<string, char> kvp in this.pairInsertionRules)
            {
                string pair = kvp.Key;
                char element = kvp.Value;

                if (!this.elementPairCounts.ContainsKey(pair))
                {
                    continue;
                }

                long numToReplace = this.elementPairCounts[pair];
                List<string> newPairs = new() { $"{pair[0]}{element}", $"{element}{pair[1]}" };

                replacements.Add(pair, (numToReplace, newPairs));
            }

            foreach (KeyValuePair<string, (long NumToReplace, List<string> NewPairs)> kvp in replacements)
            {
                string pairToReplace = kvp.Key;
                List<string> newPairs = kvp.Value.NewPairs;

                long numToReplace = kvp.Value.NumToReplace;
                this.RemovePair(pairToReplace, numToReplace);

                this.IncrementPairCount(newPairs[0], numToReplace);
                this.IncrementPairCount(newPairs[1], numToReplace);

                this.IncrementElementCount(newPairs[0][0], numToReplace);
                this.IncrementElementCount(newPairs[0][1], numToReplace);

                // Don't double-count the common element
                this.IncrementElementCount(newPairs[1][1], numToReplace);
            }
        }

        /// <summary>
        /// Validates the element pair is valid.
        /// </summary>
        /// <param name="pair">The element pair.</param>
        /// <exception cref="ArgumentException">Thrown if invalid.</exception>
        private static void ValidatePair(string pair)
        {
            if (string.IsNullOrWhiteSpace(pair))
            {
                throw new ArgumentException("The pair must be non-empty", nameof(pair));
            }

            if (pair.Length != 2)
            {
                throw new ArgumentException("The pair must be of length 2.", nameof(pair));
            }

            if (pair.Any(c => !char.IsLetter(c)))
            {
                throw new ArgumentException("The pair must consist entirely of letters.", nameof(pair));
            }
        }

        /// <summary>
        /// Increments the count of the given element.
        /// </summary>
        /// <param name="element">The element whose count to increment.</param>
        /// <param name="amount">The amount by which to increment.</param>
        private void IncrementElementCount(char element, long amount = 1)
        {
            if (!this.ElementCounts.ContainsKey(element))
            {
                this.ElementCounts[element] = 0;
            }

            this.ElementCounts[element] += amount;
        }

        /// <summary>
        /// Increments the count of the given element pair.
        /// </summary>
        /// <param name="pair">The element pair whose count to increment.</param>
        /// <param name="amount">The amount by which to increment.</param>
        private void IncrementPairCount(string pair, long amount = 1)
        {
            ValidatePair(pair);

            if (!this.elementPairCounts.ContainsKey(pair))
            {
                this.elementPairCounts[pair] = 0;
            }

            this.elementPairCounts[pair] += amount;
        }

        /// <summary>
        /// Removes an element pair from the polymer.
        /// </summary>
        /// <param name="pair">The pair to remove.</param>
        /// <param name="copies">The number of copies to remove.</param>
        private void RemovePair(string pair, long copies = 1)
        {
            ValidatePair(pair);

            if (!this.elementPairCounts.ContainsKey(pair))
            {
                return;
            }

            if (this.elementPairCounts[pair] < copies)
            {
                throw new InvalidOperationException($"Can't remove {copies} copies of {pair}. Only found {this.elementPairCounts[pair]}");
            }

            // Remove the pairs.
            this.elementPairCounts[pair] -= copies;

            if (this.elementPairCounts[pair] == 0)
            {
                this.elementPairCounts.Remove(pair);
            }

            // Update the element counts.
            foreach (char element in pair)
            {
                this.ElementCounts[element] -= copies;
            }
        }
    }
}

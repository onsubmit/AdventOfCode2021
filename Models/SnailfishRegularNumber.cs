//-----------------------------------------------------------------------
// <copyright file="SnailfishRegularNumber.cs" company="Andy Young">
//     Copyright (c) Andy Young. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace AdventOfCode2021.Models
{
    /// <summary>
    /// Represents a regular snailfish number.
    /// </summary>
    internal class SnailfishRegularNumber : SnailfishNumber
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SnailfishRegularNumber"/> class.
        /// </summary>
        /// <param name="raw">The raw string from the input file.</param>
        public SnailfishRegularNumber(string raw)
            : base(raw)
        {
            this.Value = int.Parse(raw);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SnailfishRegularNumber"/> class.
        /// </summary>
        /// <param name="value">The value of the number.</param>
        public SnailfishRegularNumber(int value = 0)
            : base(value.ToString())
        {
            this.Value = value;
        }

        /// <summary>
        /// Gets the number's value.
        /// </summary>
        public int Value { get; private set; }

        /// <summary>
        /// Gets the number's magnitude.
        /// </summary>
        public override int Magnitude => this.Value;

        /// <summary>
        /// Increments the number's value.
        /// </summary>
        /// <param name="amount">The amount by which to increment the value.</param>
        public void Increment(int amount)
        {
            this.Value += amount;
        }

        /// <summary>
        /// Returns a string representation of the number.
        /// </summary>
        /// <returns>A string representation of the number.</returns>
        public override string ToString()
        {
            return this.Value.ToString();
        }
    }
}

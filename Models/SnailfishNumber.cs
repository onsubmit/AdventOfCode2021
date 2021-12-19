//-----------------------------------------------------------------------
// <copyright file="SnailfishNumber.cs" company="Andy Young">
//     Copyright (c) Andy Young. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace AdventOfCode2021.Models
{
    /// <summary>
    /// Represents a snailfish number.
    /// </summary>
    internal class SnailfishNumber
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SnailfishNumber"/> class.
        /// </summary>
        public SnailfishNumber()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SnailfishNumber"/> class.
        /// </summary>
        /// <param name="raw">The raw string from the input file.</param>
        public SnailfishNumber(string raw)
        {
            this.Raw = raw;

            if (!raw.Contains(','))
            {
                return;
            }

            string inner = raw.StartsWith('[') && raw.EndsWith(']') ? raw[1..^1] : raw;

            Stack<char> stack = new();
            for (int i = 0; i < inner.Length; i++)
            {
                char c = inner[i];
                if (c == '[')
                {
                    stack.Push(c);
                }
                else if (c == ']')
                {
                    stack.Pop();
                }
                else if (c == ',' && stack.Count == 0)
                {
                    // Found the separator between the two numbers.
                    string left = inner[0..i];
                    string right = inner[(i + 1)..];

                    this.Left = SnailfishNumberParser.ParseSingleNumber(left);
                    this.Left.Parent = this;

                    this.Right = SnailfishNumberParser.ParseSingleNumber(right);
                    this.Right.Parent = this;
                    return;
                }
            }

            throw new InvalidOperationException("Input is bad");
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SnailfishNumber"/> class.
        /// </summary>
        /// <param name="left">The left number.</param>
        /// <param name="right">The right number.</param>
        public SnailfishNumber(SnailfishNumber left, SnailfishNumber right)
        {
            this.Raw = $"[{left.Raw},{right.Raw}]";

            this.Parent = null;

            this.Left = left;
            this.Right = right;

            left.Parent = this;
            right.Parent = this;
        }

        /// <summary>
        /// Gets the raw number from input.
        /// </summary>
        public string? Raw { get; private set; }

        /// <summary>
        /// Gets or sets the parent number.
        /// </summary>
        public SnailfishNumber? Parent { get; set; }

        /// <summary>
        /// Gets or sets the left number.
        /// </summary>
        public SnailfishNumber? Left { get; set; }

        /// <summary>
        /// Gets or sets the right number.
        /// </summary>
        public SnailfishNumber? Right { get; set; }

        /// <summary>
        /// Gets the number's magnitude.
        /// </summary>
        public virtual int Magnitude
        {
            get
            {
                if (this.Left == null || this.Right == null)
                {
                    throw new InvalidOperationException();
                }

                return (3 * this.Left.Magnitude) + (2 * this.Right.Magnitude);
            }
        }

        /// <summary>
        /// Gets a value indicating whether the number can be exploded.
        /// </summary>
        public bool CanExplode => this.Left != null && this.Right != null;

        /// <summary>
        /// Adds two numbers together.
        /// </summary>
        /// <param name="a">The first number.</param>
        /// <param name="b">The second number.</param>
        /// <returns>The reduced sum.</returns>
        public static SnailfishNumber operator +(SnailfishNumber a, SnailfishNumber b)
        {
            SnailfishNumber num = new(a, b);
            num.Reduce();
            return num;
        }

        /// <summary>
        /// Reduces the number.
        /// </summary>
        public void Reduce()
        {
            SnailfishNumberReducer.Reduce(this);
        }

        /// <summary>
        /// Returns a string representation of the number.
        /// </summary>
        /// <returns>A string representation of the number.</returns>
        public override string ToString()
        {
            string left = this.Left is SnailfishRegularNumber ? this.Left.ToString() : $"[{this.Left}]";
            string right = this.Right is SnailfishRegularNumber ? this.Right.ToString() : $"[{this.Right}]";
            return $"{left},{right}";
        }
    }
}

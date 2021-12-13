//-----------------------------------------------------------------------
// <copyright file="Cave.cs" company="Andy Young">
//     Copyright (c) Andy Young. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace AdventOfCode2021.Models
{
    /// <summary>
    /// Represents a cave.
    /// </summary>
    internal class Cave
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Cave"/> class.
        /// </summary>
        /// <param name="name">The cave's name.</param>
        /// <exception cref="ArgumentException">Thrown if the name is not provided.</exception>
        public Cave(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException($"{nameof(name)} must be provided.");
            }

            if (!name.All(c => char.IsLetter(c)))
            {
                throw new ArgumentException($"{name} must be consist entirely of letters.");
            }

            this.Name = name;
        }

        /// <summary>
        /// Gets the cave's name.
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// Gets the caves that are connected to this cave.
        /// </summary>
        public List<Cave> ConnectedCaves { get; private set; } = new();

        /// <summary>
        /// Gets a value indicating whether the cave is big.
        /// </summary>
        public bool IsBig => this.Name[0] >= 'A' && this.Name[0] <= 'Z';

        /// <summary>
        /// Gets a value indicating whether the cave is small.
        /// </summary>
        public bool IsSmall => !this.IsBig;

        /// <summary>
        /// Gets a value indicating whether the cave is the starting cave.
        /// </summary>
        public bool IsStart => this.Name == "start";

        /// <summary>
        /// Gets a value indicating whether the cave is the ending cave.
        /// </summary>
        public bool IsEnd => this.Name == "end";

        /// <summary>
        /// Adds the provided cave as a connected cave.
        /// </summary>
        /// <param name="cave">The connected cave.</param>
        public void AddConnectedCave(Cave cave)
        {
            this.ConnectedCaves.Add(cave);
        }
    }
}

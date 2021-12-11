//-----------------------------------------------------------------------
// <copyright file="Octopi.cs" company="Andy Young">
//     Copyright (c) Andy Young. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace AdventOfCode2021.Models
{
    /// <summary>
    /// Represents a group of octopi.
    /// </summary>
    internal class Octopi
    {
        private static readonly (int, int)[] Directions = new[] { (-1, -1), (-1, 0), (-1, 1), (0, -1), (0, 1), (1, -1), (1, 0), (1, 1) };

        private readonly Octopus[,] octopi;
        private readonly int width;
        private readonly int height;
        private readonly int total;

        /// <summary>
        /// Initializes a new instance of the <see cref="Octopi"/> class.
        /// </summary>
        /// <param name="lines">The lines from the input file.</param>
        public Octopi(string[] lines)
        {
            this.width = lines.Length;
            this.height = lines[0].Length;
            this.total = this.width * this.height;

            this.octopi = new Octopus[this.width, this.height];
            for (int x = 0; x < this.width; x++)
            {
                for (int y = 0; y < this.height; y++)
                {
                    this.octopi[x, y] = new Octopus(lines[x][y]);
                }
            }
        }

        /// <summary>
        /// Gets a value indicating whether all octopi are currently flashing.
        /// </summary>
        public bool AreAllOctopiFlashing { get; private set; }

        /// <summary>
        /// Performs a step over all the octopi.
        /// </summary>
        public void Step()
        {
            List<Octopus> newlyFlashing = new();

            for (int x = 0; x < this.width; x++)
            {
                for (int y = 0; y < this.height; y++)
                {
                    this.StepAt(x, y, ref newlyFlashing);
                }
            }

            // Dim the flashing octopi at the end of each step.
            newlyFlashing.ForEach(o => o.Dim());

            this.AreAllOctopiFlashing = newlyFlashing.Count == this.total;
        }

        /// <summary>
        /// Performs a step at a single octopus.
        /// </summary>
        /// <param name="x">The first coordinate.</param>
        /// <param name="y">The second coordinate.</param>
        /// <param name="newlyFlashing">The list of newly flashing octopi.</param>
        private void StepAt(int x, int y, ref List<Octopus> newlyFlashing)
        {
            Octopus octopus = this.octopi[x, y];
            if (!octopus.IsFlashing)
            {
                octopus.Step();
                if (octopus.IsFlashing)
                {
                    newlyFlashing.Add(octopus);
                    this.SpreadFlashingAt(x, y, ref newlyFlashing);
                }
            }
        }

        /// <summary>
        /// Spreads the flashing from the octopus at (x, y) to adjacent octopi.
        /// </summary>
        /// <param name="i">The first coordinate.</param>
        /// <param name="j">The second coordinate.</param>
        /// <param name="newlyFlashing">The list of newly flashing octopi.</param>
        private void SpreadFlashingAt(int i, int j, ref List<Octopus> newlyFlashing)
        {
            foreach ((int X, int Y) direction in Directions)
            {
                int x = i + direction.X;
                int y = j + direction.Y;

                if (x < 0 || x >= this.width)
                {
                    continue;
                }

                if (y < 0 || y >= this.height)
                {
                    continue;
                }

                this.StepAt(x, y, ref newlyFlashing);
            }
        }
    }
}

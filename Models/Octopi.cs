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
        private readonly Octopus[,] octopi;
        private readonly int width;
        private readonly int height;
        private readonly int total;
        private readonly Coordinate start;
        private readonly Coordinate end;

        /// <summary>
        /// Initializes a new instance of the <see cref="Octopi"/> class.
        /// </summary>
        /// <param name="lines">The lines from the input file.</param>
        public Octopi(string[] lines)
        {
            this.width = lines.Length;
            this.height = lines[0].Length;
            this.total = this.width * this.height;
            this.start = new(0, 0);
            this.end = new(this.width - 1, this.height - 1);

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
                    this.StepAt(new(x, y), ref newlyFlashing);
                }
            }

            // Dim the flashing octopi at the end of each step.
            newlyFlashing.ForEach(o => o.Dim());

            this.AreAllOctopiFlashing = newlyFlashing.Count == this.total;
        }

        /// <summary>
        /// Performs a step at a single octopus.
        /// </summary>
        /// <param name="coordinate">The coordinate.</param>
        /// <param name="newlyFlashing">The list of newly flashing octopi.</param>
        private void StepAt(Coordinate coordinate, ref List<Octopus> newlyFlashing)
        {
            Octopus octopus = this.octopi[coordinate.X, coordinate.Y];
            if (!octopus.IsFlashing)
            {
                octopus.Step();
                if (octopus.IsFlashing)
                {
                    newlyFlashing.Add(octopus);
                    this.SpreadFlashingAt(coordinate, ref newlyFlashing);
                }
            }
        }

        /// <summary>
        /// Spreads the flashing from the octopus at (x, y) to adjacent octopi.
        /// </summary>
        /// <param name="coordinate">The coordinate.</param>
        /// <param name="newlyFlashing">The list of newly flashing octopi.</param>
        private void SpreadFlashingAt(Coordinate coordinate, ref List<Octopus> newlyFlashing)
        {
            foreach (Coordinate direction in Directions.CardinalAndOrdinal)
            {
                Coordinate neighbor = coordinate + direction;
                if (!neighbor.IsInRange(this.start, this.end))
                {
                    continue;
                }

                this.StepAt(neighbor, ref newlyFlashing);
            }
        }
    }
}

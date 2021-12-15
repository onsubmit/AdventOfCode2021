//-----------------------------------------------------------------------
// <copyright file="Paper.cs" company="Andy Young">
//     Copyright (c) Andy Young. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace AdventOfCode2021.Models
{
    using System.Text;

    /// <summary>
    /// Represents a piece of transparent paper.
    /// </summary>
    internal class Paper
    {
        private readonly bool[,] grid;
        private readonly int width;
        private readonly int height;

        /// <summary>
        /// Initializes a new instance of the <see cref="Paper"/> class.
        /// </summary>
        /// <param name="width">The paper width.</param>
        /// <param name="height">The paper height.</param>
        public Paper(int width, int height)
        {
            this.grid = new bool[width, height];
            this.width = width;
            this.height = height;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Paper"/> class.
        /// </summary>
        /// <param name="coordinates">The coordinates containing the dots.</param>
        /// <exception cref="InvalidOperationException">Thrown if input is bad.</exception>
        public Paper(List<Coordinate> coordinates)
        {
            Coordinate min = new(int.MaxValue, int.MaxValue);
            Coordinate max = new(int.MinValue, int.MinValue);

            foreach (Coordinate coordinate in coordinates)
            {
                min.X = Math.Min(min.X, coordinate.X);
                min.Y = Math.Min(min.Y, coordinate.Y);

                max.X = Math.Max(max.X, coordinate.X);
                max.Y = Math.Max(max.Y, coordinate.Y);
            }

            if (min.X != 0 || min.Y != 0)
            {
                throw new InvalidOperationException("The minimum coordinate is not the origin.");
            }

            this.width = max.X + 1;
            this.height = max.Y + 1;

            this.grid = new bool[this.width, this.height];
            foreach (Coordinate coordinate in coordinates)
            {
                this.grid[coordinate.X, coordinate.Y] = true;
            }
        }

        /// <summary>
        /// Folds the paper.
        /// </summary>
        /// <param name="fold">The fold to perform.</param>
        /// <returns>A new folded paper.</returns>
        /// <exception cref="InvalidOperationException">Thrown if the fold operation is invalid.</exception>
        public Paper Fold(Fold fold)
        {
            if (fold.Dimension == Dimension.Y)
            {
                Paper foldedPaper = new(this.width, fold.Position);

                for (int x = 0; x < this.width; x++)
                {
                    for (int y = (2 * fold.Position) - (this.height - 1); y < fold.Position; y++)
                    {
                        if (this.grid[x, y] || this.grid[x, (2 * fold.Position) - y])
                        {
                            foldedPaper.grid[x, y] = true;
                        }
                    }
                }

                return foldedPaper;
            }
            else if (fold.Dimension == Dimension.X)
            {
                Paper foldedPaper = new(fold.Position, this.height);

                for (int y = 0; y < this.height; y++)
                {
                    for (int x = (2 * fold.Position) - (this.width - 1); x < fold.Position; x++)
                    {
                        if (this.grid[x, y] || this.grid[(2 * fold.Position) - x, y])
                        {
                            foldedPaper.grid[x, y] = true;
                        }
                    }
                }

                return foldedPaper;
            }

            throw new InvalidOperationException("Invalid fold dimension");
        }

        /// <summary>
        /// Returns a string representation of the paper.
        /// </summary>
        /// <returns>A string representation of the paper.</returns>
        public override string ToString()
        {
            StringBuilder sb = new();
            for (int y = 0; y < this.height; y++)
            {
                StringBuilder line = new();
                for (int x = 0; x < this.width; x++)
                {
                    line.Append(this.grid[x, y] ? "#" : " ");
                }

                sb.AppendLine(line.ToString());
            }

            return sb.ToString();
        }
    }
}

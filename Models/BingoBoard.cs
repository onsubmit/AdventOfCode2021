//-----------------------------------------------------------------------
// <copyright file="BingoBoard.cs" company="Andy Young">
//     Copyright (c) Andy Young. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace AdventOfCode2021.Models
{
    /// <summary>
    /// Represents a Bingo board.
    /// </summary>
    internal class BingoBoard
    {
        private readonly BingoSquare[,] board;
        private bool? isWonCache;

        /// <summary>
        /// Initializes a new instance of the <see cref="BingoBoard"/> class.
        /// </summary>
        /// <param name="values">Board values.</param>
        public BingoBoard(List<List<int>> values)
        {
            int width = values.Count;
            int height = values[0].Count;

            this.board = new BingoSquare[width, height];
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    this.board[x, y] = new BingoSquare(values[x][y]);
                }
            }
        }

        /// <summary>
        /// Marks the value on board if found.
        /// </summary>
        /// <param name="value">The value to mark.</param>
        /// <returns><c>true</c> if the value was found on the board, <c>false</c> otherwise.</returns>
        public bool Mark(int value)
        {
            for (int x = 0; x < this.board.GetLength(0); x++)
            {
                for (int y = 0; y < this.board.GetLength(1); y++)
                {
                    if (this.board[x, y].Value == value)
                    {
                        this.board[x, y].Mark();

                        // Assume boards have unique values.
                        return true;
                    }
                }
            }

            return false;
        }

        /// <summary>
        /// Marks the value on a losing board if found.
        /// </summary>
        /// <param name="value">The value to mark.</param>
        /// <returns><c>true</c> if the value was found on the board, <c>false</c> if the board is already won or the value was not found.</returns>
        public bool MarkIfLost(int value)
        {
            if (this.isWonCache == true)
            {
                return false;
            }

            return this.Mark(value);
        }

        /// <summary>
        /// Determines if the board is won and caches the result.
        /// </summary>
        /// <returns><c>true</c> if the board is won, <c>false</c> otherwise.</returns>
        public bool DetermineIfWonAndCacheResult()
        {
            if (this.isWonCache == true)
            {
                return true;
            }

            // Check rows.
            for (int x = 0; x < this.board.GetLength(0); x++)
            {
                bool areAllMarked = true;
                for (int y = 0; y < this.board.GetLength(1); y++)
                {
                    if (!this.board[x, y].IsMarked)
                    {
                        areAllMarked = false;
                        break;
                    }
                }

                if (areAllMarked)
                {
                    this.isWonCache = true;
                    return true;
                }
            }

            // Check columns.
            for (int y = 0; y < this.board.GetLength(1); y++)
            {
                bool areAllMarked = true;
                for (int x = 0; x < this.board.GetLength(0); x++)
                {
                    if (!this.board[x, y].IsMarked)
                    {
                        areAllMarked = false;
                        break;
                    }
                }

                if (areAllMarked)
                {
                    this.isWonCache = true;
                    return true;
                }
            }

            this.isWonCache = false;
            return false;
        }

        /// <summary>
        /// Gets the sum of the board's unmarked squares.
        /// </summary>
        /// <returns>The sum of the board's unmarked squares.</returns>
        public int GetSumOfUnmarkedSquares()
        {
            int sum = 0;
            for (int x = 0; x < this.board.GetLength(0); x++)
            {
                for (int y = 0; y < this.board.GetLength(1); y++)
                {
                    if (!this.board[x, y].IsMarked)
                    {
                        sum += this.board[x, y].Value;
                    }
                }
            }

            return sum;
        }
    }
}

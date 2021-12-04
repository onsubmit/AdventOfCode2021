//-----------------------------------------------------------------------
// <copyright file="BingoGame.cs" company="Andy Young">
//     Copyright (c) Andy Young. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace AdventOfCode2021.Models
{
    /// <summary>
    /// Represents a Bingo game.
    /// </summary>
    internal class BingoGame
    {
        private readonly List<BingoBoard> boards;
        private readonly Stack<int> numbersToDraw;

        /// <summary>
        /// Initializes a new instance of the <see cref="BingoGame"/> class.
        /// </summary>
        /// <param name="boards">The boards.</param>
        /// <param name="numbersToDraw">The numbers to draw.</param>
        public BingoGame(List<BingoBoard> boards, int[] numbersToDraw)
        {
            this.boards = boards;
            this.numbersToDraw = new Stack<int>(numbersToDraw.Reverse());
        }

        /// <summary>
        /// Tries to draw a number.
        /// </summary>
        /// <param name="number">The drawn number.</param>
        /// <returns><c>True</c> if a number was successfully drawn, <c>false</c> otherwise.</returns>
        public bool TryDrawNumber(out int number) => this.numbersToDraw.TryPop(out number);

        /// <summary>
        /// Marks the number on each losing board and gets the last board to win, if found.
        /// </summary>
        /// <param name="drawnNumber">The drawn number.</param>
        /// <param name="lastWinningBoard">The last winning board.</param>
        /// <returns><c>true</c> if a newly won board was found, <c>false</c> otherwise.</returns>
        public bool TryMarkLosingBoardsAndGetLastWinningBoard(int drawnNumber, out BingoBoard? lastWinningBoard)
        {
            lastWinningBoard = null;

            foreach (BingoBoard board in this.boards)
            {
                if (board.MarkIfLost(drawnNumber))
                {
                    if (board.DetermineIfWonAndCacheResult())
                    {
                        lastWinningBoard = board;
                    }
                }
            }

            return lastWinningBoard != null;
        }
    }
}

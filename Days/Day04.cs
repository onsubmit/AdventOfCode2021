//-----------------------------------------------------------------------
// <copyright file="Day04.cs" company="Andy Young">
//     Copyright (c) Andy Young. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace AdventOfCode2021.Days
{
    using AdventOfCode2021.Models;

    /// <summary>
    /// Calculates the solution for the particular day.
    /// </summary>
    internal class Day04 : IDay
    {
        /// <summary>
        /// Calculates the solution for the particular day.
        /// </summary>
        /// <returns>The solution.</returns>
        public string GetSolution()
        {
            using StreamReader sr = new("input\\Day04.txt");
            string? line = sr.ReadLine();

            if (line == null)
            {
                throw new InvalidOperationException("Input is bad");
            }

            int[] drawnNumbers = line.Split(",").Select(s => int.Parse(s)).ToArray();
            List<BingoBoard> boards = GetBoards(sr);

            BingoGame game = new(boards, drawnNumbers);

            while (game.TryDrawNumber(out int number))
            {
                if (game.TryGetWinningBoard(number, out BingoBoard? winningBoard))
                {
                    int sumOfUnmarkedSquares = winningBoard?.GetSumOfUnmarkedSquares() ?? 0;
                    int solution = sumOfUnmarkedSquares * number;
                    return solution.ToString();
                }
            }

            throw new InvalidOperationException("No winning board was found");
        }

        /// <summary>
        /// Gets the boards from the input file.
        /// </summary>
        /// <param name="sr">The input file <see cref="StreamReader"/> object.</param>
        /// <returns>The list of boards from the input file.</returns>
        private static List<BingoBoard> GetBoards(StreamReader sr)
        {
            List<BingoBoard> boards = new();
            List<List<int>> boardLines = new();

            string? line = null;
            while ((line = sr.ReadLine()) != null)
            {
                if (string.IsNullOrWhiteSpace(line))
                {
                    if (boardLines.Any())
                    {
                        boards.Add(new BingoBoard(boardLines));
                        boardLines.Clear();
                    }

                    continue;
                }

                List<int> lineNumbers = line.Split(" ", StringSplitOptions.RemoveEmptyEntries).Select(s => int.Parse(s.Trim())).ToList();
                boardLines.Add(lineNumbers);
            }

            // Don't forget the last board.
            boards.Add(new BingoBoard(boardLines));
            return boards;
        }
    }
}

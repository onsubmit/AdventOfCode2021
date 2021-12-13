//-----------------------------------------------------------------------
// <copyright file="CaveGraph.cs" company="Andy Young">
//     Copyright (c) Andy Young. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace AdventOfCode2021.Models
{
    /// <summary>
    /// Represents the graph of caves.
    /// </summary>
    internal class CaveGraph
    {
        private readonly Dictionary<string, Cave> caves = new();

        /// <summary>
        /// Gets the start cave.
        /// </summary>
        public Cave StartCave => this.caves["start"];

        /// <summary>
        /// Gets the end cave.
        /// </summary>
        public Cave EndCave => this.caves["end"];

        /// <summary>
        /// Adds a set of connected caves to the graph.
        /// </summary>
        /// <param name="line">The input line representing the connected caves.</param>
        /// <exception cref="InvalidOperationException">Thrown if input is bad.</exception>
        public void AddConnectedCaves(string line)
        {
            if (string.IsNullOrWhiteSpace(line))
            {
                throw new InvalidOperationException("Input is bad");
            }

            string[] split = line.Split("-");
            if (split.Length != 2)
            {
                throw new InvalidOperationException("Input is bad");
            }

            string fromName = split[0];
            string toName = split[1];
            if (!this.caves.TryGetValue(fromName, out Cave? from))
            {
                from = new(fromName);
                this.caves[fromName] = from;
            }

            if (!this.caves.TryGetValue(toName, out Cave? to))
            {
                to = new(toName);
                this.caves[toName] = to;
            }

            from.AddConnectedCave(to);
            to.AddConnectedCave(from);
        }

        /// <summary>
        /// Gets all the paths from the current cave to the end cave.
        /// </summary>
        /// <returns>The list of all the paths.</returns>
        public List<CavePath> GetPaths()
        {
            List<CavePath> paths = new();
            CavePath path = new();
            path.Push(this.StartCave);

            this.GetPaths(this.StartCave, ref paths, ref path);

            return paths;
        }

        /// <summary>
        /// Gets all the paths from the current cave to the end cave.
        /// </summary>
        /// <param name="currentCave">The current cave.</param>
        /// <param name="paths">The list of known paths.</param>
        /// <param name="currentPath">The current processing path.</param>
        private void GetPaths(Cave currentCave, ref List<CavePath> paths, ref CavePath currentPath)
        {
            foreach (Cave connectedCave in currentCave.ConnectedCaves)
            {
                if (connectedCave == this.StartCave)
                {
                    continue;
                }

                if (connectedCave == this.EndCave)
                {
                    CavePath path = new();
                    path.Push(connectedCave);

                    foreach (Cave cave in currentPath.Caves)
                    {
                        path.Push(cave);
                    }

                    paths.Add(path);
                }
                else if (connectedCave.IsBig || currentPath.CanAddSmallCave(connectedCave))
                {
                    currentPath.Push(connectedCave);

                    this.GetPaths(connectedCave, ref paths, ref currentPath);
                    currentPath.Pop();
                }
            }
        }
    }
}

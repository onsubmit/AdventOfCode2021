//-----------------------------------------------------------------------
// <copyright file="Packet.cs" company="Andy Young">
//     Copyright (c) Andy Young. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace AdventOfCode2021.Models
{
    /// <summary>
    /// Represents a packet.
    /// </summary>
    internal abstract class Packet
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Packet"/> class.
        /// </summary>
        /// <param name="version">The packet version.</param>
        public Packet(int version)
        {
            this.Version = version;
        }

        /// <summary>
        /// Gets the packet version.
        /// </summary>
        public int Version { get; private set; }
    }
}

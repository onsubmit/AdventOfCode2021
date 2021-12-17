//-----------------------------------------------------------------------
// <copyright file="LiteralPacket.cs" company="Andy Young">
//     Copyright (c) Andy Young. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace AdventOfCode2021.Models
{
    /// <summary>
    /// Represents a literal packet.
    /// </summary>
    internal class LiteralPacket : Packet
    {
        /// <summary>
        /// Literal packets all have a type of "4".
        /// </summary>
        public const int Type = 4;

        /// <summary>
        /// Initializes a new instance of the <see cref="LiteralPacket"/> class.
        /// </summary>
        /// <param name="version">The packet version.</param>
        /// <param name="value">The packet value.</param>
        public LiteralPacket(int version, long value)
            : base(version)
        {
            this.Value = value;
        }

        /// <summary>
        /// Gets the packet value.
        /// </summary>
        public long Value { get; private set; }
    }
}

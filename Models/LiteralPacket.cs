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
        /// Initializes a new instance of the <see cref="LiteralPacket"/> class.
        /// </summary>
        /// <param name="version">The packet version.</param>
        /// <param name="value">The packet value.</param>
        public LiteralPacket(int version, long value)
            : base(PacketType.Literal, version)
        {
            this.Value = value;
        }

        /// <summary>
        /// Returns a string representation of the literal packet.
        /// </summary>
        /// <returns>A string representation of the literal packet.</returns>
        public override string ToString()
        {
            return this.Value.ToString();
        }
    }
}

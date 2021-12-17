//-----------------------------------------------------------------------
// <copyright file="OperatorPacket.cs" company="Andy Young">
//     Copyright (c) Andy Young. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace AdventOfCode2021.Models
{
    /// <summary>
    /// Represents an operator packet.
    /// </summary>
    internal class OperatorPacket : Packet
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="OperatorPacket"/> class.
        /// </summary>
        /// <param name="version">The packet version.</param>
        public OperatorPacket(int version)
            : base(version)
        {
        }
    }
}

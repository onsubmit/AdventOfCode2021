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
        /// <param name="type">The packet type.</param>
        /// <param name="version">The packet version.</param>
        public Packet(PacketType type, int version)
        {
            this.Type = type;
            this.Version = version;
        }

        /// <summary>
        /// Gets the packet type.
        /// </summary>
        public PacketType Type { get; private set; }

        /// <summary>
        /// Gets the packet version.
        /// </summary>
        public int Version { get; private set; }

        /// <summary>
        /// Gets or sets the packet value.
        /// </summary>
        public virtual long Value { get; protected set; }
    }
}

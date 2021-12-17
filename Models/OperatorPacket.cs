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
        /// <param name="type">The packet type.</param>
        /// <param name="version">The packet version.</param>
        public OperatorPacket(PacketType type, int version)
            : base(type, version)
        {
            if (type == PacketType.Literal)
            {
                throw new ArgumentException($"Use LiteralPacket.");
            }
        }

        /// <summary>
        /// Gets the packet's subpackets.
        /// </summary>
        public List<Packet> SubPackets { get; private set; } = new();

        /// <summary>
        /// Gets the value of the operator packet.
        /// </summary>
        public override long Value
        {
            get
            {
                switch (this.Type)
                {
                    case PacketType.Sum:
                        return this.SubPackets.Sum(p => p.Value);
                    case PacketType.Product:
                        return this.SubPackets.Aggregate(1L, (acc, p) => acc * p.Value);
                    case PacketType.Minimum:
                        Packet minimum = this.SubPackets.MinBy(p => p.Value)
                            ?? throw new InvalidOperationException("Could not find packet with minimum value.");
                        return minimum.Value;
                    case PacketType.Maximum:
                        Packet maximum = this.SubPackets.MaxBy(p => p.Value)
                            ?? throw new InvalidOperationException("Could not find packet with maximum value.");
                        return maximum.Value;
                    case PacketType.GreaterThan:
                        if (this.SubPackets.Count != 2)
                        {
                            throw new InvalidOperationException($"GreaterThan packets must only have two sub-packets. Found {this.SubPackets.Count}");
                        }

                        return this.SubPackets[0].Value > this.SubPackets[1].Value ? 1 : 0;
                    case PacketType.LessThan:
                        if (this.SubPackets.Count != 2)
                        {
                            throw new InvalidOperationException($"LessThan packets must only have two sub-packets. Found {this.SubPackets.Count}");
                        }

                        return this.SubPackets[0].Value < this.SubPackets[1].Value ? 1 : 0;
                    case PacketType.EqualTo:
                        if (this.SubPackets.Count != 2)
                        {
                            throw new InvalidOperationException($"EqualTo packets must only have two sub-packets. Found {this.SubPackets.Count}");
                        }

                        return this.SubPackets[0].Value == this.SubPackets[1].Value ? 1 : 0;
                    default:
                        throw new InvalidOperationException("Invalid packet type.");
                }
            }
        }

        /// <summary>
        /// Returns a string representation of the operator packet.
        /// </summary>
        /// <returns>A string representation of the operator packet.</returns>
        public override string ToString()
        {
            return $"Type: {this.Type}. Value: {this.Value}";
        }
    }
}

//-----------------------------------------------------------------------
// <copyright file="PacketType.cs" company="Andy Young">
//     Copyright (c) Andy Young. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace AdventOfCode2021.Models
{
    /// <summary>
    /// Packet types.
    /// </summary>
    internal enum PacketType
    {
        /// <summary>
        /// Value is the sum of the values of their sub-packets.
        /// If they only have a single sub-packet, their value is the value of the sub-packet.
        /// </summary>
        Sum = 0,

        /// <summary>
        /// Value is the result of multiplying together the values of their sub-packets.
        /// If they only have a single sub-packet, their value is the value of the sub-packet.
        /// </summary>
        Product = 1,

        /// <summary>
        /// Value is the minimum of the values of their sub-packets.
        /// </summary>
        Minimum = 2,

        /// <summary>
        /// Value is the maximum of the values of their sub-packets.
        /// </summary>
        Maximum = 3,

        /// <summary>
        /// Value is a literal value.
        /// </summary>
        Literal = 4,

        /// <summary>
        /// Value is 1 if the value of the first sub-packet is greater than the value of the second sub-packet; otherwise, their value is 0.
        /// These packets always have exactly two sub-packets.
        /// </summary>
        GreaterThan = 5,

        /// <summary>
        /// Value is 1 if the value of the first sub-packet is less than the value of the second sub-packet; otherwise, their value is 0.
        /// These packets always have exactly two sub-packets.
        /// </summary>
        LessThan = 6,

        /// <summary>
        /// Value is 1 if the value of the first sub-packet is equal to the value of the second sub-packet; otherwise, their value is 0.
        /// These packets always have exactly two sub-packets.
        /// </summary>
        EqualTo = 7,
    }
}

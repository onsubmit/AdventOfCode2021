//-----------------------------------------------------------------------
// <copyright file="Day16.cs" company="Andy Young">
//     Copyright (c) Andy Young. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace AdventOfCode2021.Days
{
    using System.Text;
    using AdventOfCode2021.Models;

    /// <summary>
    /// Calculates the solution for the particular day.
    /// </summary>
    internal class Day16 : IDay
    {
        private static int index = 0;
        private static string binaryDigits = string.Empty;

        /// <summary>
        /// Calculates the solution for the particular day.
        /// </summary>
        /// <returns>The solution.</returns>
        public string GetSolution()
        {
            binaryDigits = GetBinaryStringFromInput();

            Packet packet = DecodePackets();
            return packet.Value.ToString();
        }

        /// <summary>
        /// Gets the binary string from the input file.
        /// </summary>
        /// <returns>The binary string from the input file.</returns>
        /// <exception cref="InvalidOperationException">Thrown if input is bad.</exception>
        private static string GetBinaryStringFromInput()
        {
            using StreamReader sr = new("input\\Day16.txt");
            string? line = sr.ReadLine();
            if (string.IsNullOrWhiteSpace(line))
            {
                throw new InvalidOperationException("Input is bad.");
            }

            StringBuilder sb = new();
            foreach (char hex in line)
            {
                int base10 = Convert.ToInt32(hex.ToString(), 16);
                string base2 = Convert.ToString(base10, 2).PadLeft(4, '0');
                sb.Append(base2);
            }

            return sb.ToString();
        }

        /// <summary>
        /// Parses the packets.
        /// </summary>
        /// <returns>The decoded packets.</returns>
        /// <exception cref="InvalidOperationException">Thrown if input is bad.</exception>
        private static Packet DecodePackets()
        {
            // The first three bits are the packet version.
            int packetVersion = Convert.ToInt32(binaryDigits[index..(index += 3)], 2);

            // The next three bits are the packet type ID.
            PacketType packetType = (PacketType)Convert.ToInt32(binaryDigits[index..(index += 3)], 2);
            if (packetType < PacketType.Sum || packetType > PacketType.EqualTo)
            {
                throw new InvalidCastException($"Invalid packet type: {packetType})");
            }

            if (packetType == PacketType.Literal)
            {
                long valueBase10 = ParseLiteralPacketValue();
                LiteralPacket packet = new(packetVersion, valueBase10);
                return packet;
            }

            OperatorPacket operatorPacket = new(packetType, packetVersion);

            // The next bit is the length type ID.
            int lengthType = Convert.ToInt32(binaryDigits[index++].ToString(), 2);
            switch (lengthType)
            {
                case 0:
                    // The next 15 bits are a number that represents the total length in bits of the sub-packets contained by this packet.
                    int subPacketLength = Convert.ToInt32(binaryDigits[index..(index += 15)], 2);
                    int endIndex = index + subPacketLength;

                    while (index < endIndex)
                    {
                        operatorPacket.SubPackets.Add(DecodePackets());
                    }

                    break;
                case 1:
                    // The next 11 bits are a number that represents the number of sub-packets immediately contained by this packet.
                    int numSubPackets = Convert.ToInt32(binaryDigits[index..(index += 11)], 2);
                    for (int i = 0; i < numSubPackets; i++)
                    {
                        operatorPacket.SubPackets.Add(DecodePackets());
                    }

                    break;
                default:
                    throw new InvalidOperationException("Length type must be 0 or 1.");
            }

            return operatorPacket;
        }

        /// <summary>
        /// Parses the value of a literal packet.
        /// </summary>
        /// <returns>The value of a literal packet.</returns>
        /// <exception cref="InvalidOperationException">Thrown if input is bad.</exception>
        private static long ParseLiteralPacketValue()
        {
            StringBuilder sb = new();
            while (index < binaryDigits.Length)
            {
                char prefix = binaryDigits[index++];
                string valueBase2 = binaryDigits[index..(index += 4)];
                sb.Append(valueBase2);

                if (prefix == '0')
                {
                    long valueBase10 = Convert.ToInt64(sb.ToString(), 2);
                    return valueBase10;
                }
            }

            throw new InvalidOperationException("Literal packet did not terminate properly.");
        }
    }
}

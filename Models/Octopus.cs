//-----------------------------------------------------------------------
// <copyright file="Octopus.cs" company="Andy Young">
//     Copyright (c) Andy Young. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace AdventOfCode2021.Models
{
    /// <summary>
    /// Represents an octopus.
    /// </summary>
    internal class Octopus
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Octopus"/> class.
        /// </summary>
        /// <param name="energyLevel">The energy level of the octopus.</param>
        public Octopus(char energyLevel)
        {
            this.EnergyLevel = (int)char.GetNumericValue(energyLevel);
        }

        /// <summary>
        /// Gets the energy level.
        /// </summary>
        public int EnergyLevel { get; private set; }

        /// <summary>
        /// Gets a value indicating whether the octopus is flashing.
        /// </summary>
        public bool IsFlashing { get; private set; }

        /// <summary>
        /// Performs a step.
        /// </summary>
        public void Step()
        {
            this.EnergyLevel = (this.EnergyLevel + 1) % 10;
            this.IsFlashing = this.EnergyLevel == 0;
        }

        /// <summary>
        /// Dims a flashing octopus.
        /// </summary>
        public void Dim()
        {
            this.IsFlashing = false;
        }
    }
}

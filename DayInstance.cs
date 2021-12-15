//-----------------------------------------------------------------------
// <copyright file="DayInstance.cs" company="Andy Young">
//     Copyright (c) Andy Young. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace AdventOfCode2021
{
    using AdventOfCode2021.Days;

    /// <summary>
    /// Represents a day.
    /// </summary>
    internal class DayInstance
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DayInstance"/> class.
        /// </summary>
        /// <param name="day">The day.</param>
        public DayInstance(IDay day)
        {
            this.Day = day;
        }

        /// <summary>
        /// Gets the day.
        /// </summary>
        public IDay Day { get; private set; }

        /// <summary>
        /// Gets or sets the reason why the day is skipped.
        /// </summary>
        public string? SkippedReason { get; set; }
    }
}

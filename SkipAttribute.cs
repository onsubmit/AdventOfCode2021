//-----------------------------------------------------------------------
// <copyright file="SkipAttribute.cs" company="Andy Young">
//     Copyright (c) Andy Young. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace AdventOfCode2021
{
    /// <summary>
    /// Skips any day with this attribute.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    internal class SkipAttribute : Attribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SkipAttribute"/> class.
        /// </summary>
        /// <param name="reason">The reason why the day was skipped.</param>
        public SkipAttribute(string reason)
        {
            this.Reason = reason;
        }

        /// <summary>
        /// Gets the reason why the day was skipped.
        /// </summary>
        public string Reason { get; private set; }
    }
}

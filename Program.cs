// <copyright file="Program.cs" company="Andy Young">
// Copyright (c) Andy Young. All rights reserved.
// </copyright>

namespace AdventOfCode2021
{
    using System;
    using System.Diagnostics;
    using AdventOfCode2021.Days;

    /// <summary>
    /// Program class.
    /// </summary>
    public class Program
    {
        private static readonly IDay[] Days = GetDays();

        /// <summary>
        /// Main entry point.
        /// </summary>
        public static void Main()
        {
            int totalLength = Days.Length.ToString().Length;
            for (int i = 0; i < Days.Length; i++)
            {
                Stopwatch stopWatch = Stopwatch.StartNew();
                string solution = Days[i].GetSolution();
                stopWatch.Stop();

                string dayNumber = (i + 1).ToString().PadLeft(totalLength, '0');
                double elapsed = Math.Round(stopWatch.Elapsed.TotalMilliseconds);

                Console.WriteLine($"Day {dayNumber}: {solution} in {elapsed}ms");
            }
        }

        /// <summary>
        /// Gets an array containing an instance of each day.
        /// </summary>
        /// <returns>An array containing an instance of each day.</returns>
        /// <exception cref="InvalidCastException">Thrown if instance creation of any day fails.</exception>
        private static IDay[] GetDays()
        {
            Type dayInterfaceType = typeof(IDay);
            return AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(assembly => assembly.GetTypes())
                .Where(type => type != dayInterfaceType && dayInterfaceType.IsAssignableFrom(type))
                .Select(type => Activator.CreateInstance(type) as IDay ?? throw new InvalidCastException()).ToArray();
        }
    }
}
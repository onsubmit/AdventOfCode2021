//-----------------------------------------------------------------------
// <copyright file="Program.cs" company="Andy Young">
//     Copyright (c) Andy Young. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

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
        private static readonly List<DayInstance> Days = GetDays();

        /// <summary>
        /// Main entry point.
        /// </summary>
        public static void Main()
        {
            int totalLength = Days.Count.ToString().Length;
            for (int i = 0; i < Days.Count; i++)
            {
                string dayNumber = (i + 1).ToString().PadLeft(totalLength, '0');

                if (!string.IsNullOrWhiteSpace(Days[i].SkippedReason))
                {
                    Console.WriteLine($"Day {dayNumber}: Skipped due to <{Days[i].SkippedReason}>");
                    continue;
                }

                Stopwatch stopWatch = Stopwatch.StartNew();
                string solution = Days[i].Day.GetSolution();
                stopWatch.Stop();

                double elapsed = Math.Round(stopWatch.Elapsed.TotalMilliseconds);
                Console.WriteLine($"Day {dayNumber}: {solution} in {elapsed}ms");
            }
        }

        /// <summary>
        /// Gets an array containing an instance of each day.
        /// </summary>
        /// <returns>An array containing an instance of each day.</returns>
        /// <exception cref="InvalidCastException">Thrown if instance creation of any day fails.</exception>
        private static List<DayInstance> GetDays()
        {
            Type dayInterfaceType = typeof(IDay);
            List<DayInstance> days = new();

            IEnumerable<Type> types = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(assembly => assembly.GetTypes())
                .Where(type => type != dayInterfaceType && dayInterfaceType.IsAssignableFrom(type));

            foreach (Type type in types)
            {
                DayInstance day = new(Activator.CreateInstance(type) as IDay ?? throw new InvalidCastException());

                IEnumerable<SkipAttribute> attributes = type.GetCustomAttributes(typeof(SkipAttribute), false).Cast<SkipAttribute>();
                day.SkippedReason = attributes.SingleOrDefault()?.Reason;

                days.Add(day);
            }

            return days;
        }
    }
}
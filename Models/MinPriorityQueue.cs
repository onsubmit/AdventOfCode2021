//-----------------------------------------------------------------------
// <copyright file="MinPriorityQueue.cs" company="Andy Young">
//     Copyright (c) Andy Young. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace AdventOfCode2021.Models
{
    /// <summary>
    /// Represents a minimum priority queue since <see cref="PriorityQueue{TElement, TPriority}"/> does not have a method to change a priority.
    /// </summary>
    /// <typeparam name="TElement">Specifies the type of elements in the queue.</typeparam>
    /// <typeparam name="TPriority">Specifies the type of priority associated with enqueued elements.</typeparam>
    internal class MinPriorityQueue<TElement, TPriority>
        where TElement : notnull
    {
        private readonly Dictionary<TElement, TPriority> queue = new();

        /// <summary>
        /// Determines if the queue has any elements.
        /// </summary>
        /// <returns><c>true</c> if the queue has any elements, <c>false</c> otherwise.</returns>
        public bool Any()
        {
            return this.queue.Any();
        }

        /// <summary>
        /// Adds the specified element with associated priority to the  queue.
        /// </summary>
        /// <param name="element">The element to add.</param>
        /// <param name="priority">The element's priority.</param>
        public void Enqueue(TElement element, TPriority priority)
        {
            this.queue.Add(element, priority);
        }

        /// <summary>
        /// Removes and returns the minimal element from the queue.
        /// </summary>
        /// <returns>The minimal element of the queue.</returns>
        public TElement Dequeue()
        {
            TElement element = this.queue.MinBy(kvp => kvp.Value).Key;
            this.queue.Remove(element);

            return element;
        }

        /// <summary>
        /// Removes the minimal element from the queue and copies it and its associated priority to the element and priority arguments.
        /// </summary>
        /// <param name="element">When this method returns, contains the removed element.</param>
        /// <param name="priority">When this method returns, contains the priority associated with the removed element.</param>
        /// <returns><c>true</c> if the element is successfully removed; false if the queue is empty.</returns>
        public bool TryDequeue(out TElement? element, out TPriority? priority)
        {
            element = default;
            priority = default;

            if (!this.queue.Any())
            {
                return false;
            }

            element = this.queue.MinBy(kvp => kvp.Value).Key;
            this.queue.Remove(element);

            return true;
        }

        /// <summary>
        /// Sets the priority of the given element in the queue.
        /// </summary>
        /// <param name="element">The element whose priority to set.</param>
        /// <param name="priority">The new priority.</param>
        public void SetPriority(TElement element, TPriority priority)
        {
            this.queue[element] = priority;
        }
    }
}

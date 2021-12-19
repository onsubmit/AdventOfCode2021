//-----------------------------------------------------------------------
// <copyright file="SnailfishNumberReducer.cs" company="Andy Young">
//     Copyright (c) Andy Young. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace AdventOfCode2021.Models
{
    /// <summary>
    /// Reduces snailfish numbers.
    /// </summary>
    internal static class SnailfishNumberReducer
    {
        /// <summary>
        /// Reduces a snailfish number.
        /// </summary>
        /// <param name="number">The snailfish number to reduce.</param>
        public static void Reduce(SnailfishNumber number)
        {
            while (TryExplode(number) || TrySplit(number))
            {
                // Continue reducing.
            }
        }

        /// <summary>
        /// Explodes a pair in the number if it finds one in need of exploding.
        /// </summary>
        /// <param name="number">The number to search for a pair in need of exploding.</param>
        /// <returns><c>true</c> if a pair was exploded, <c>false</c> otherwise.</returns>
        /// <exception cref="InvalidOperationException">Thrown if I've royally messed something up.</exception>
        private static bool TryExplode(SnailfishNumber number)
        {
            if (TryFindPairToExplode(number, 0, out SnailfishNumber? explodeable))
            {
                if (explodeable == null)
                {
                    throw new InvalidOperationException("We found a number to explode but didn't return it.");
                }

                if (TryFindNearestRegularNumber(explodeable, SnailfishNumberSearchDirection.Left | SnailfishNumberSearchDirection.Up, out SnailfishRegularNumber? left))
                {
                    if (explodeable?.Left is not SnailfishRegularNumber explodeableLeft)
                    {
                        throw new InvalidOperationException("Explodeable pairs must consist of regular numbers.");
                    }

                    left?.Increment(explodeableLeft.Value);
                }

                if (TryFindNearestRegularNumber(explodeable, SnailfishNumberSearchDirection.Right | SnailfishNumberSearchDirection.Up, out SnailfishRegularNumber? right))
                {
                    if (explodeable?.Right is not SnailfishRegularNumber explodeableRight)
                    {
                        throw new InvalidOperationException("Explodeable pairs must consist of regular numbers.");
                    }

                    right?.Increment(explodeableRight.Value);
                }

                if (explodeable.Parent == null)
                {
                    throw new InvalidOperationException("An explodeable number must have a parent.");
                }

                SnailfishRegularNumber replacement = new();
                if (explodeable.Parent.Left == explodeable)
                {
                    explodeable.Parent.Left = replacement;
                }

                if (explodeable.Parent.Right == explodeable)
                {
                    explodeable.Parent.Right = replacement;
                }

                replacement.Parent = explodeable.Parent;
                return true;
            }

            return false;
        }

        /// <summary>
        /// Splits a regular number if it finds one in need of splitting.
        /// </summary>
        /// <param name="number">The number to search for a regular number in need of splitting.</param>
        /// <returns><c>true</c> if a regular number was split, <c>false</c> otherwise.</returns>
        /// <exception cref="InvalidOperationException">Thrown if I've royally messed something up.</exception>
        private static bool TrySplit(SnailfishNumber number)
        {
            if (TryFindNumberToSplit(number, out SnailfishRegularNumber? splittable))
            {
                if (splittable == null)
                {
                    throw new InvalidOperationException("We found a number to split but didn't return it.");
                }

                SnailfishNumber replacement = new();
                replacement.Left = new SnailfishRegularNumber(splittable.Value / 2);
                replacement.Left.Parent = replacement;

                replacement.Right = new SnailfishRegularNumber((splittable.Value + 1) / 2);
                replacement.Right.Parent = replacement;

                if (splittable.Parent == null)
                {
                    throw new InvalidOperationException("A splittable number must have a parent.");
                }

                if (splittable.Parent.Left == splittable)
                {
                    splittable.Parent.Left = replacement;
                }

                if (splittable.Parent.Right == splittable)
                {
                    splittable.Parent.Right = replacement;
                }

                replacement.Parent = splittable.Parent;
                return true;
            }

            return false;
        }

        /// <summary>
        /// Tries to find a pair to explode.
        /// </summary>
        /// <param name="number">The number to search for a pair in need of exploding.</param>
        /// <param name="nestedLevel">The current nested level of the search.</param>
        /// <param name="explodeable">The explodable pair if found.</param>
        /// <returns><c>true</c> if an explodable pair was found, <c>false</c> otherwise.</returns>
        private static bool TryFindPairToExplode(SnailfishNumber number, int nestedLevel, out SnailfishNumber? explodeable)
        {
            explodeable = null;
            if (!number.CanExplode)
            {
                return false;
            }

            if (nestedLevel >= 4 && number.Left is SnailfishRegularNumber && number.Right is SnailfishRegularNumber)
            {
                explodeable = number;
                return true;
            }

            if (number.Left != null && TryFindPairToExplode(number.Left, nestedLevel + 1, out explodeable))
            {
                return true;
            }

            if (number.Right != null && TryFindPairToExplode(number.Right, nestedLevel + 1, out explodeable))
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// Tries to find a regular number to split.
        /// </summary>
        /// <param name="number">The number to search for a regular number in need of splitting.</param>
        /// <param name="splittable">The splittable regular number if found.</param>
        /// <returns><c>true</c> if a regular number was found, <c>false</c> otherwise.</returns>
        private static bool TryFindNumberToSplit(SnailfishNumber number, out SnailfishRegularNumber? splittable)
        {
            splittable = null;

            if (number is SnailfishRegularNumber regularNumber && regularNumber.Value >= 10)
            {
                splittable = regularNumber;
                return true;
            }

            if (number.Left != null && TryFindNumberToSplit(number.Left, out splittable))
            {
                return true;
            }

            if (number.Right != null && TryFindNumberToSplit(number.Right, out splittable))
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// Tries to find the nearest regular number in the given direction.
        /// </summary>
        /// <param name="number">The current number to search.</param>
        /// <param name="direction">The direction of the search.</param>
        /// <param name="regularNumber">The regular number if found.</param>
        /// <returns><c>true</c> if a regular number was found, <c>false</c> otherwise.</returns>
        private static bool TryFindNearestRegularNumber(SnailfishNumber number, SnailfishNumberSearchDirection direction, out SnailfishRegularNumber? regularNumber)
        {
            regularNumber = null;

            if (direction.HasFlag(SnailfishNumberSearchDirection.Up))
            {
                if (number.Parent == null)
                {
                    // We've gone up to the root, go back down.
                    direction &= ~SnailfishNumberSearchDirection.Up;
                    direction |= SnailfishNumberSearchDirection.Down;

                    return TryFindNearestRegularNumber(number, direction, out regularNumber);
                }

                SnailfishNumber? sibling = direction.HasFlag(SnailfishNumberSearchDirection.Left) ? number.Parent.Left : number.Parent.Right;
                if (sibling == null)
                {
                    return false;
                }

                if (number != sibling)
                {
                    if (sibling is SnailfishRegularNumber regularParentNode)
                    {
                        regularNumber = regularParentNode;
                        return true;
                    }

                    direction &= ~SnailfishNumberSearchDirection.Up;
                    direction |= SnailfishNumberSearchDirection.Down;

                    return TryFindNearestRegularNumber(sibling, direction, out regularNumber);
                }
                else
                {
                    if (number.Parent.Parent == null)
                    {
                        // We are nearing the root but along a path that we can't go back down.
                        return false;
                    }

                    return TryFindNearestRegularNumber(number.Parent, direction, out regularNumber);
                }
            }
            else if (direction.HasFlag(SnailfishNumberSearchDirection.Down))
            {
                IEnumerable<SnailfishNumber?> siblings = new[] { number.Left, number.Right };
                if (direction.HasFlag(SnailfishNumberSearchDirection.Left))
                {
                    siblings = siblings.Reverse();
                }

                foreach (SnailfishNumber? sibling in siblings)
                {
                    if (sibling == null)
                    {
                        continue;
                    }

                    if (sibling is SnailfishRegularNumber regularSibling)
                    {
                        regularNumber = regularSibling;
                        return true;
                    }
                    else if (TryFindNearestRegularNumber(sibling, direction, out regularNumber))
                    {
                        return true;
                    }
                }
            }

            return false;
        }
    }
}

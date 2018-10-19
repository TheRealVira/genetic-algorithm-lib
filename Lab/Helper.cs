using System;
using System.Collections.Generic;
using System.Linq;

namespace Lab
{
    /// <summary>
    ///     A helper extension class.
    /// </summary>
    public static class Helper
    {
        /// <summary>
        ///     Gets the top n predators out of a generation.
        /// </summary>
        /// <typeparam name="T">Type of the boxed value.</typeparam>
        /// <param name="curGeneration">The current generation.</param>
        /// <param name="n">Population size of predators of a generation.</param>
        /// <returns></returns>
        public static List<AdvancedObject<T>> GetTopN<T>(this IEnumerable<AdvancedObject<T>> curGeneration, int n)
        {
            var advancedObjects = curGeneration as AdvancedObject<T>[] ?? curGeneration.ToArray();
            return advancedObjects.OrderBy(x => x.Score).ToList().GetRange(0, Math.Min(n, advancedObjects.Length));
        }
    }
}
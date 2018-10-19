using System;
using System.Collections.Generic;

namespace Lab
{
    /// <summary>
    ///     A generic factory which would allow developers to generate a generic list of <see cref="AdvancedObject{T}" />s.
    /// </summary>
    public static class AdvancedObjectFactory
    {
        /// <summary>
        ///     Generates n random <see cref="AdvancedObject{T}" />s.
        /// </summary>
        /// <typeparam name="T">The type of <see cref="AdvancedObject{T}" /> to be generated.</typeparam>
        /// <typeparam name="TValue">The type of the value of which the generated <see cref="AdvancedObject{T}" /> should hold.</typeparam>
        /// <param name="rand">The randomness.</param>
        /// <param name="n">The size of generated <see cref="AdvancedObject{T}" />s.</param>
        /// <param name="arguments">The arguments of which those <see cref="AdvancedObject{T}" />s should get initialized with.</param>
        /// <returns>
        ///     Returns a list (of size n) elements of <see cref="AdvancedObject{T}" />s containing
        ///     <typeparamref name="TValue" />.
        /// </returns>
        public static IEnumerable<T> GenerateAdvancedObjects<T, TValue>(Random rand, int n, params object[] arguments)
            where T : AdvancedObject<TValue>
        {
            var myArgs = new object[arguments.Length + 1];
            myArgs[0] = rand;
            Array.Copy(arguments, 0, myArgs, 1, arguments.Length);

            for (var i = 0; i < n; i++)
                yield return (T) Activator.CreateInstance(typeof(T), myArgs);
        }
    }
}
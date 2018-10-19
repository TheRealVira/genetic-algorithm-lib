using System;

namespace Lab
{
    /// <summary>
    ///     A generic factory generating approaches for specific <see cref="AdvancedObject{T}" />s.
    /// </summary>
    public static class ApproachFactory
    {
        /// <summary>
        ///     Generates an approach based on its' type and value.
        /// </summary>
        /// <typeparam name="T">
        ///     This should be of type Approach where <typeparamref name="TValue" /> would contain the type of its'
        ///     boxed value.
        /// </typeparam>
        /// <typeparam name="TValue">The type of the boxed value of which approach should be generated.</typeparam>
        /// <returns>
        ///     Returns an approach which would could be used to step forward step(s) for a specific generation of an
        ///     <see cref="AdvancedObject{T}" />.
        /// </returns>
        public static Approach<TValue> GenApproach<T, TValue>() where T : Approach<TValue>
        {
            return (T) Activator.CreateInstance(typeof(T));
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;

namespace Lab
{
    /// <summary>
    ///     An approach is a simplistic representation of options to iterate through evolutionary steps.
    /// </summary>
    /// <typeparam name="T">The type of which the wanted <see cref="AdvancedObject{T}" /> would </typeparam>
    public abstract class Approach<T>
    {
        /// <summary>
        ///     Does one generational step.
        /// </summary>
        /// <param name="predators">The current predator population.</param>
        /// <param name="rand">The randomness.</param>
        /// <param name="mutationRate">The mutation rate one individual might have a mutational gene.</param>
        /// <returns>Returns a new population based on its' given predators and parameters including their past predators.</returns>
        public virtual IEnumerable<AdvancedObject<T>> DoStep(IEnumerable<AdvancedObject<T>> predators, Random rand,
            double mutationRate)
        {
            var newChildren = new List<AdvancedObject<T>>();
            var advancedObjects = predators as AdvancedObject<T>[] ?? predators.ToArray();

            foreach (var predator in advancedObjects)
            foreach (var predator2 in advancedObjects)
            {
                if (predator2.Equals(predator)) continue;

                var newChild = predator.Breed(predator2);

                if (rand.NextDouble() <= mutationRate) newChild.Mutate(rand);

                if (newChildren.Any(x => x.MObject.Equals(newChild.MObject))) continue;

                newChildren.Add(newChild);
            }

            newChildren.AddRange(advancedObjects);
            return newChildren;
        }

        /// <summary>
        ///     Does n generational steps.
        /// </summary>
        /// <param name="predators">The current predator population.</param>
        /// <param name="rand">The randomness.</param>
        /// <param name="mutationRate">The mutation rate one individual might have a mutational gene.</param>
        /// <param name="goalObject">The most optimal object/individual.</param>
        /// <param name="n">Steps a population would take</param>
        /// <returns>
        ///     Returns a new population based on its' given predators and parameters including their past predators (of the
        ///     last generation).
        /// </returns>
        public virtual List<AdvancedObject<T>> DoSteps(IEnumerable<AdvancedObject<T>> predators, Random rand,
            double mutationRate, T goalObject, int n)
        {
            var newGen = new List<AdvancedObject<T>>();
            var advancedObjects = predators as AdvancedObject<T>[] ?? predators.ToArray();
            newGen.AddRange(advancedObjects);

            for (var i = 0; i < n; i++)
            {
                var currentNewStep = DoStep(newGen.GetTopN(advancedObjects.Count()), rand, mutationRate);
                newGen.Clear();
                newGen.AddRange(currentNewStep);
                newGen.ForEach(x => x.Score = x.CalculateScore(goalObject));
            }

            return newGen;
        }
    }
}
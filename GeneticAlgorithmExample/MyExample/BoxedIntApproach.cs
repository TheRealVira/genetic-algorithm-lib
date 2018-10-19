using System;
using System.Collections.Generic;
using System.Linq;
using Lab;

namespace GeneticAlgorithmExample.MyExample
{
    /// <inheritdoc />
    /// <summary>
    /// </summary>
    /// <seealso cref="!:Lab.Approach{System.Int32}" />
    internal class BoxedIntApproach : Approach<int>
    {
        public override IEnumerable<AdvancedObject<int>> DoStep(IEnumerable<AdvancedObject<int>> predators, Random rand,
            double mutationRate)
        {
            var toRet = base.DoStep(predators, rand, mutationRate);
            var advancedObjects = toRet as AdvancedObject<int>[] ?? toRet.ToArray();
            var currentBest = advancedObjects.GetTopN(1)[0];

            Console.WriteLine($"Top:\t{currentBest.MObject}\t{currentBest.Score}");

            return advancedObjects;
        }
    }
}
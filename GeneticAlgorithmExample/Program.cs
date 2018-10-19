using System;
using System.Collections.Generic;
using System.Linq;
using GeneticAlgorithmExample.MyExample;
using Lab;
using Lab.Example;

namespace GeneticAlgorithmExample
{
    internal class Program
    {
        /// <summary>
        ///     Count of how many generations are going to get generated.
        /// </summary>
        private const int GENERATIONS = 30;

        /// <summary>
        ///     Has to be divisible by GAP.
        /// </summary>
        private const int POPULATION_COUNT = 100;

        /// <summary>
        ///     Percentage of predators inside one generation.
        /// </summary>
        private const double GAP = 5;

        /// <summary>
        ///     Count of predators inside one generation.
        /// </summary>
        private const int PREDATORS = (int) (POPULATION_COUNT / GAP);

        /// <summary>
        ///     The mutationrate one new child would have some new genes.
        /// </summary>
        private const double MUTATIONRATE = .15;

        /// <summary>
        ///     The random generator.
        /// </summary>
        private static Random _rand;

        /// <summary>
        ///     The goal string our generations would try to achieve.
        /// </summary>
        private static object _goalObject;

        private static void Main(string[] args)
        {
            _rand = new Random(DateTime.Now.Millisecond);

            //var result = stringApproach();
            var result = StepByStepApproach();
            //var result = intApproach();

            Console.WriteLine("===========================");
            Console.WriteLine("Output:");
            Console.WriteLine("--");

            Console.WriteLine("Top 3 (the closer to 0 the better):\n---\nScore\tText");
            result.GetTopN(3).ForEach(Console.WriteLine);
            Console.ReadKey(false);
        }

        private static IEnumerable<AdvancedObject<string>> StepByStepApproach()
        {
            Console.WriteLine("Please feel free to enter my goal text:\n`");
            _goalObject = Console.ReadLine();

            var generation = AdvancedObjectFactory
                .GenerateAdvancedObjects<BoxedAdvancedString, string>(_rand, POPULATION_COUNT,
                    ((string) _goalObject).Length).Cast<AdvancedObject<string>>().ToList();

            var stringApproach = ApproachFactory.GenApproach<BoxedStringApproach, string>();

            for (var i = 0; i < GENERATIONS - 1; i++)
            {
                generation = stringApproach.DoSteps(generation.GetTopN(PREDATORS), _rand, MUTATIONRATE,
                    (string) _goalObject,
                    GENERATIONS);

                generation.ForEach(x => x.CalculateScore((string) _goalObject));

                var currentBest = generation.GetTopN(1)[0];

                Console.WriteLine($"Top@Generation[{i}]:\t{currentBest.MObject}");
            }

            return stringApproach.DoSteps(generation.GetTopN(PREDATORS), _rand, MUTATIONRATE, (string) _goalObject,
                GENERATIONS);
        }

        private static List<AdvancedObject<string>> stringApproach()
        {
            Console.WriteLine("Please feel free to enter my goal text:\n`");
            _goalObject = Console.ReadLine();

            var generation = AdvancedObjectFactory
                .GenerateAdvancedObjects<BoxedAdvancedString, string>(_rand, POPULATION_COUNT,
                    ((string) _goalObject).Length).ToList();

            var stringApproach = new BoxedStringApproach();

            return stringApproach.DoSteps(generation.GetTopN(PREDATORS), _rand, MUTATIONRATE, (string) _goalObject,
                GENERATIONS);
        }

        private static List<AdvancedObject<int>> intApproach()
        {
            Console.WriteLine("Please feel free to enter my number:\n`");
            _goalObject = int.Parse(Console.ReadLine() ?? throw new InvalidOperationException());

            var generation = AdvancedObjectFactory.GenerateAdvancedObjects<BoxedInteger, int>(_rand, POPULATION_COUNT)
                .ToList();

            var intApproach = new BoxedIntApproach();

            return intApproach.DoSteps(generation.GetTopN(PREDATORS), _rand, MUTATIONRATE, (int) _goalObject,
                GENERATIONS);
        }
    }
}
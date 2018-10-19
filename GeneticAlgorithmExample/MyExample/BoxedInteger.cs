using System;
using System.Linq;
using Lab;

namespace GeneticAlgorithmExample.MyExample
{
    /// <inheritdoc />
    /// <summary>
    /// </summary>
    /// <seealso cref="!:Lab.AdvancedObject{System.Int32}" />
    internal class BoxedInteger : AdvancedObject<int>
    {
        public BoxedInteger(Random rand) : base(rand)
        {
        }

        public BoxedInteger(int mObject) : base(mObject)
        {
        }

        public BoxedInteger(int mObject, int goalObject) : base(mObject, goalObject)
        {
        }

        public override double CalculateScore(int goalObject)
        {
            var temp = (goalObject - MObject) / Math.Sqrt(Math.Pow(goalObject, 2) + Math.Pow(MObject, 2));
            if (temp < 0) return temp * -1;

            return 1 - temp;
        }

        public override void Mutate(Random rand)
        {
            var tempData = BitConverter.GetBytes(MObject);
            tempData[rand.Next(0, tempData.Length)] = (byte) rand.Next(byte.MinValue, byte.MaxValue);
            MObject = BitConverter.ToInt32(tempData, 0);
        }

        public override AdvancedObject<int> Breed(AdvancedObject<int> parent2)
        {
            var getGenFromParent1 = true;
            var tempData = BitConverter.GetBytes(MObject);
            var tempData2 = BitConverter.GetBytes(parent2.MObject);

            return new BoxedInteger(BitConverter.ToInt32(tempData.Zip(tempData2, (x, y) =>
            {
                if (getGenFromParent1)
                {
                    getGenFromParent1 = !getGenFromParent1;
                    return x;
                }

                getGenFromParent1 = !getGenFromParent1;
                return y;
            }).ToArray(), 0));
        }

        protected override AdvancedObject<int> GenAdvancedObject(Random rand, object param = null)
        {
            return new BoxedInteger(rand.Next(int.MinValue, int.MaxValue));
        }
    }
}
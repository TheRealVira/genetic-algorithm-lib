using System;
using System.Linq;
using System.Text;

namespace Lab.Example
{
    /// <inheritdoc />
    /// <summary>
    ///     An example implementation of an advanced object.
    /// </summary>
    /// <seealso cref="!:Lab.AdvancedObject{System.String}" />
    public class BoxedAdvancedString : AdvancedObject<string>
    {
        public static int PossibleSelectableCharsMin = 32;
        public static int PossibleSelectableCharsMax = 126;

        public BoxedAdvancedString(Random rand, int stringLength) : base(rand, stringLength)
        {
        }

        public BoxedAdvancedString(string mObject) : base(mObject)
        {
        }

        public BoxedAdvancedString(string mObject, string goalObject) : base(mObject, goalObject)
        {
        }

        public override double CalculateScore(string goalObject)
        {
            if (goalObject.Length != MObject.Length) return 1;

            return (double) goalObject.ToCharArray()
                       .Zip(MObject.ToCharArray(), (c1, c2) => new {c1, c2})
                       .Count(m => m.c1 != m.c2) / MObject.Length;
        }

        public override AdvancedObject<string> Breed(AdvancedObject<string> parent2)
        {
            var getGenFromParent1 = false;
            return new BoxedAdvancedString(new string(MObject.Zip(parent2.MObject, (x, y) =>
            {
                if (getGenFromParent1)
                {
                    getGenFromParent1 = !getGenFromParent1;
                    return x;
                }

                getGenFromParent1 = !getGenFromParent1;
                return y;
            }).ToArray()));
        }

        public override void Mutate(Random rand)
        {
            MObject = new StringBuilder(MObject)
            {
                [rand.Next(MObject.Length)] =
                    (char) rand.Next(PossibleSelectableCharsMin, PossibleSelectableCharsMax)
            }.ToString();
        }

        protected override AdvancedObject<string> GenAdvancedObject(Random rand, object param = null)
        {
            if (param == null || param.GetType() != typeof(int)) return null;

            var toRet = new char[(int) param];
            for (var i = 0; i < (int) param; i++)
                toRet[i] = (char) rand.Next(PossibleSelectableCharsMin, PossibleSelectableCharsMax);

            return new BoxedAdvancedString(new string(toRet));
        }
    }
}
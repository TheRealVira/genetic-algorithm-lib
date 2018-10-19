using System;

namespace Lab
{
    /// <summary>
    ///     An abstract projection of one individual (a boxed object).
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class AdvancedObject<T>
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="AdvancedObject{T}" /> class.
        /// </summary>
        /// <param name="rand">The randomness.</param>
        /// <param name="param">A list of optional parameters.</param>
        protected AdvancedObject(Random rand, object param = null)
        {
            MObject = GenAdvancedObject(rand, param).MObject;
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="AdvancedObject{T}" /> class.
        /// </summary>
        /// <param name="mObject">The individuals object which should get boxed inside an <see cref="AdvancedObject{T}" />.</param>
        protected AdvancedObject(T mObject)
        {
            MObject = mObject;
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="AdvancedObject{T}" /> class.
        /// </summary>
        /// <param name="mObject">The individuals object which should get boxed inside an <see cref="AdvancedObject{T}" />.</param>
        /// <param name="goalObject">The most optimal object/individual.</param>
        protected AdvancedObject(T mObject, T goalObject)
        {
            MObject = mObject;
            Score = CalculateScore(goalObject);
        }

        /// <summary>
        ///     Gets the my boxed object.
        /// </summary>
        /// <value>
        ///     This object contains my current value and is used to determine my score.
        /// </value>
        public T MObject { get; protected set; }

        /// <summary>
        ///     A Score of 0 would be considered as the best possible situation.
        /// </summary>
        public double Score { get; set; }

        /// <summary>
        ///     Calculates the score.
        /// </summary>
        /// <param name="goalObject">The best achievable object.</param>
        /// <returns>Returns a number between 0 and 1; the closer to 0 the better.</returns>
        public abstract double CalculateScore(T goalObject);

        /// <summary>
        ///     Mutates this advanced object.
        /// </summary>
        /// <param name="rand">The randomness.</param>
        public abstract void Mutate(Random rand);

        /// <summary>
        ///     Breeds the specified parent2 with the current advanced object.
        /// </summary>
        /// <param name="parent2">An other parent this object is going to breed with.</param>
        /// <returns>Returns a newly breed advanced object.</returns>
        public abstract AdvancedObject<T> Breed(AdvancedObject<T> parent2);

        public override string ToString()
        {
            return $"{Score:##.000}\t{MObject.ToString()}";
        }

        /// <summary>
        ///     Generates one randomized advanced object.
        /// </summary>
        /// <param name="rand">The randomness.</param>
        /// <param name="param">An unspecified parameter.</param>
        /// <returns>Returns the newly created random object.</returns>
        protected abstract AdvancedObject<T> GenAdvancedObject(Random rand, object param = null);
    }
}
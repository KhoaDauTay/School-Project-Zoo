using System;
using Reproducers;

namespace Animals
{
    /// <summary>
    /// The class that represents an ostrich.
    /// </summary>
    [Serializable]
    public sealed class Ostrich : Bird
    {
        /// <summary>
        /// Initializes a new instance of the Ostrich class.
        /// </summary>
        /// <param name="name">The name of the ostrich.</param>
        /// <param name="age">The age of the ostrich.</param>
        /// <param name="weight">The weigh of the ostrich (in pounds).</param>
        /// <param name="gender">The gender of the ostrich.</param>
        public Ostrich(string name, int age, double weight, Gender gender)
            : base(name, age, weight, gender)
        {
            this.MoveBehavior = MoveBehaviorFactory.CreateMoveBehavior(MoveBehaviorType.Pace);
            this.BabyWeightPercentage = 30.0;
        }
    }
}
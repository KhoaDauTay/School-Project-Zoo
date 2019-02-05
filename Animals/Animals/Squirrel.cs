using System;
using Reproducers;

namespace Animals
{
    /// <summary>
    /// The class representing a squirrel.
    /// </summary>
    [Serializable]
    public class Squirrel : Mammal
    {
        /// <summary>
        /// Initializes a new instance of the Squirrel class.
        /// </summary>
        /// <param name="name">The name of the squirrel.</param>
        /// <param name="age">The age of the squirrel.</param>
        /// <param name="weight">The weight of the squirrel (in pounds).</param>
        /// <param name="gender">The gender of the squirrel.</param>
        public Squirrel(string name, int age, double weight, Gender gender)
            : base(name, age, weight, gender)
        {
            this.BabyWeightPercentage = 17.0;
            this.MoveBehavior = new ClimbBehavior();
        }
    }
}
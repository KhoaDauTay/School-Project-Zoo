using System;
using Reproducers;

namespace Animals
{
    /// <summary>
    /// The class representing a kangaroo.
    /// </summary>
    [Serializable]
    public class Kangaroo : Mammal
    {
        /// <summary>
        /// Initializes a new instance of the Kangaroo class.
        /// </summary>
        /// <param name="name">The name of the kangaroo.</param>
        /// <param name="age">The age of the kangaroo.</param>
        /// <param name="weight">The weight of the kangaroo (in pounds).</param>
        /// <param name="gender">The gender of the kangaroo.</param>
        public Kangaroo(string name, int age, double weight, Gender gender)
            : base(name, age, weight, gender)
        {
            this.BabyWeightPercentage = 13.0;
        }
    }
}
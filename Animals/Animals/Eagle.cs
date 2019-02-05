using System;
using Reproducers;

namespace Animals
{
    /// <summary>
    /// The class that represents an eagle.
    /// </summary>
    [Serializable]
    public class Eagle : Bird
    {
        /// <summary>
        /// Initializes a new instance of the Eagle class.
        /// </summary>
        /// <param name="name">The name of the eagle.</param>
        /// <param name="age">The age of the eagle.</param>
        /// <param name="weight">The weight of the eagle.</param>
        /// <param name="gender">The gender of the eagle.</param>
        public Eagle(string name, int age, double weight, Gender gender)
            : base(name, age, weight, gender)
        {
            this.BabyWeightPercentage = 35.0;
        }
    }
}
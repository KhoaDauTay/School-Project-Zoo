using System;
using Reproducers;

namespace Animals
{
    /// <summary>
    /// The class that represents a shark.
    /// </summary>
    [Serializable]
    public class Shark : Fish
    {
        /// <summary>
        /// Initializes a new instance of the Shark class.
        /// </summary>
        /// <param name="name">The name of the shark.</param>
        /// <param name="age">The age of the shark.</param>
        /// <param name="weight">The weight of the shark.</param>
        /// <param name="gender">The gender of the shark.</param>
        public Shark(string name, int age, double weight, Gender gender)
            : base(name, age, weight, gender)
        {
            this.BabyWeightPercentage = 2.0;
        }

        /// <summary>
        /// The size of the shark when it appears in the cage.
        /// </summary>
        public override double DisplaySize
        {
            get
            {
                return this.Age == 0 ? 1 : 1.5;
            }
        }
    }
}
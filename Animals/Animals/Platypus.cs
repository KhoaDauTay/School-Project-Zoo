using System;
using Reproducers;

namespace Animals
{
    /// <summary>
    /// The class which is used to represent a platypus.
    /// </summary>
    [Serializable]
    public sealed class Platypus : Mammal, IHatchable
    {
        /// <summary>
        /// Initializes a new instance of the Platypus class.
        /// </summary>
        /// <param name="name">The name of the platypus.</param>
        /// <param name="age">The age of the platypus.</param>
        /// <param name="weight">The weight of the platypus (in pounds).</param>
        /// <param name="gender">The gender of the platypus.</param>
        public Platypus(string name, int age, double weight, Gender gender)
            : base(name, age, weight, gender)
        {
            this.MoveBehavior = MoveBehaviorFactory.CreateMoveBehavior(MoveBehaviorType.Swim);
            this.BabyWeightPercentage = 12.0;
            this.ReproduceBehavior = new LayEggBehavior();
            this.EatBehavior = new ShowAffectionBehavior();        
        }

        /// <summary>
        /// The size of the platypus when it is displayed in the cage.
        /// </summary>
        public override double DisplaySize
        {
            get
            {
               return this.Age == 0 ? 0.5 : 1.1;
            }
        }

        /// <summary>
        /// Hatches the platypus.
        /// </summary>
        public void Hatch()
        {
            // the platypus hatches from an egg.
        }
    }
}
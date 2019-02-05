using System;
using Reproducers;

namespace Animals
{
    /// <summary>
    /// The class which is used to represent a bird.
    /// </summary>
    [Serializable]
    public abstract class Bird : Animal, IHatchable
    {
        /// <summary>
        /// Initializes a new instance of the Bird class.
        /// </summary>
        /// <param name="name">The name of the bird.</param>
        /// <param name="age">The age of the bird.</param>
        /// <param name="weight">The weight of the bird (in pounds).</param>
        /// <param name="gender">The gender of the bird.</param>
        public Bird(string name, int age, double weight, Gender gender)
            : base(name, age, weight, gender)
        {
            this.MoveBehavior = MoveBehaviorFactory.CreateMoveBehavior(MoveBehaviorType.Fly);
            this.ReproduceBehavior = new LayEggBehavior();
            this.EatBehavior = new ConsumeBehavior();
        }

        /// <summary>
        /// Hatches from its egg.
        /// </summary>
        public void Hatch()
        {
            // Break out of egg.
        }
    }
}
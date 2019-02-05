using System;
using Reproducers;

namespace Animals
{
    /// <summary>
    /// The class that represents a fish.
    /// </summary>
    [Serializable]
    public abstract class Fish : Animal
    {
        /// <summary>
        /// Initializes a new instance of the Fish class.
        /// </summary>
        /// <param name="name">The name of the fish.</param>
        /// <param name="age">The age of the fish.</param>
        /// <param name="weight">The weight of the fish.</param>
        /// <param name="gender">The gender of the fish.</param>
        public Fish(string name, int age, double weight, Gender gender)
            : base(name, age, weight, gender)
        {
            this.MoveBehavior = MoveBehaviorFactory.CreateMoveBehavior(MoveBehaviorType.Swim);
            this.ReproduceBehavior = new LayEggBehavior();
            this.EatBehavior = new ConsumeBehavior();
        }
    }
}
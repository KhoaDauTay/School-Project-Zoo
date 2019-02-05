using System;
using Foods;
using Reproducers;

namespace Animals
{
    /// <summary>
    /// The class which is used to represent a mammal.
    /// </summary>
    [Serializable]
    public abstract class Mammal : Animal
    {
        /// <summary>
        /// Initializes a new instance of the Mammal class.
        /// </summary>
        /// <param name="name">The name of the mammal.</param>
        /// <param name="age">The age of the mammal.</param>
        /// <param name="weight">The weight of the mammal (in pounds).</param>
        /// <param name="gender">The gender of the mammal.</param>
        public Mammal(string name, int age, double weight, Gender gender)
            : base(name, age, weight, gender)
        {
            this.MoveBehavior = MoveBehaviorFactory.CreateMoveBehavior(MoveBehaviorType.Pace);
            this.ReproduceBehavior = new GiveBirthBehavior();
            this.EatBehavior = new ConsumeBehavior();
        }

        /// <summary>
        /// Feeds a baby eater.
        /// </summary>
        /// <param name="newborn">The eater to feed.</param>
        private void FeedNewborn(IEater newborn)
        {
            // Determine milk weight.
            double milkWeight = this.Weight * 0.005;

            // Generate milk.
            Food milk = new Food(milkWeight);

            // Feed baby.
            newborn.Eat(milk);

            // Reduce parent's weight.
            this.Weight -= milkWeight;
        }
    }
}
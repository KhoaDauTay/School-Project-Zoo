using System;
using Foods;
using Reproducers;

namespace Animals
{
    /// <summary>
    /// The class that represents a reproducing behavior of giving birth.
    /// </summary>
    [Serializable]
    public class GiveBirthBehavior : IReproduceBehavior
    {
        /// <summary>
        /// The random used to randomly set a gender to male or female.
        /// </summary>
        private static Random random = new Random(DateTime.Now.Millisecond);

        /// <summary>
        /// Has an animal reproduce.
        /// </summary>
        /// <param name="mother">The pregnant mother animal.</param>
        /// <returns>The baby animal.</returns>
        public IReproducer Reproduce(Animal mother)
        {
            // Define and initialize a result variable.
            IReproducer baby = null;

            // Set the baby animal weight based upon its type.
            double weight = mother.Weight * (mother.BabyWeightPercentage / 100);

            // Create a baby animal of the same type as the mother.
            Gender gender = GiveBirthBehavior.random.Next(0, 2) == 0 ? Gender.Female : Gender.Male;
            baby = (IReproducer)Activator.CreateInstance(mother.GetType(), "Baby", 0, weight, gender);

            // Reduce the mother's weight by 1.5 times the baby's weight.
            mother.Weight -= weight * 1.5;

            if (mother is Mammal)
            {
                this.FeedNewborn(mother as Mammal, baby as Mammal);
            }

            // Return result.
            return baby;
        }

        /// <summary>
        /// Feeds the newborn baby.
        /// </summary>
        /// <param name="mother">The newborn's mother.</param>
        /// <param name="baby">The newborn baby animal.</param>
        private void FeedNewborn(Mammal mother, Mammal baby)
        {
            // Determine milk weight.
            double milkWeight = mother.Weight * 0.005;

            // Generate milk.
            Food milk = new Food(milkWeight);

            // Feed baby.
            baby.Eat(milk);

            // Reduce parent's weight.
            mother.Weight -= milkWeight;
        }
    }
}
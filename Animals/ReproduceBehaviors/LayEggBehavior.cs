using System;
using Reproducers;

namespace Animals
{
    /// <summary>
    /// The class that represents a reproducing behavior of laying an egg.
    /// </summary>
    [Serializable]
    public class LayEggBehavior : IReproduceBehavior
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
            IReproducer result = this.LayEgg(mother);

            if (result is IHatchable)
            {
                this.HatchEgg(result as IHatchable);
            }
            else
            {
                // TODO: Handle the situation where the baby isn't hatchable
            }

            return result;
        }

        /// <summary>
        /// Hatches a baby out of its egg.
        /// </summary>
        /// <param name="egg">The egg to hatch.</param>
        private void HatchEgg(IHatchable egg)
        {
            egg.Hatch();
        }

        /// <summary>
        /// Has an animal lay an egg.
        /// </summary>
        /// <param name="mother">The pregnant mother animal.</param>
        /// <returns>The baby animal.</returns>
        private IReproducer LayEgg(Animal mother)
        {
            // Define and initialize a result variable.
            IReproducer result = null;

            // Set the baby animal weight based upon its type.
            double weight = mother.Weight * (mother.BabyWeightPercentage / 100);

            // Create a baby animal of the same type as the mother.
            Gender gender = LayEggBehavior.random.Next(0, 2) == 0 ? Gender.Female : Gender.Male;
            result = (IReproducer)Activator.CreateInstance(mother.GetType(), "Baby", 0, weight, gender);

            // Reduce the mother's weight by 1.5 times the baby's weight.
            mother.Weight -= weight * 1.5;

            // Return result.
            return result;
        }
    }
}
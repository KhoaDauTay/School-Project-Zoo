using System;
using Foods;

namespace Animals
{
    /// <summary>
    /// The class that represents an eating behavior of burying a bone and then eating it.
    /// </summary>
    [Serializable]
    public class BuryAndEatBoneBehavior : IEatBehavior
    {
        /// <summary>
        /// Eats the specified food.
        /// </summary>
        /// <param name="eater">The eater that will consume the food.</param>
        /// <param name="food">The food to eat.</param>
        public void Eat(IEater eater, Food food)
        {
            this.BuryBone(food);

            this.DigUpAndEatBone();

            // Increase the animal's weight by the weight of the food eaten.
            eater.Weight += food.Weight;

            this.Bark();
        }

        /// <summary>
        /// Makes a barking noise.
        /// </summary>
        private void Bark()
        {
            // Bark in excitement.
        }

        /// <summary>
        /// Has the animal bury food.
        /// </summary>
        /// <param name="bone">The bone to be buried.</param>
        private void BuryBone(Food bone)
        {
            // Bury bone.
        }

        /// <summary>
        /// Digs up an existing bone and eats it.
        /// </summary>
        private void DigUpAndEatBone()
        {
            // Dig up bone.

            // Eat bone.
        }
    }
}
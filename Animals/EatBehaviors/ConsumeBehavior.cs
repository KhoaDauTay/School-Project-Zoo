using System;
using Foods;

namespace Animals
{
    /// <summary>
    /// The class that represents an eating behavior of consuming food.
    /// </summary>
    [Serializable]
    public class ConsumeBehavior : IEatBehavior
    {
        /// <summary>
        /// Has an animal eat food.
        /// </summary>
        /// <param name="eater">The eater who will consume the food.</param>
        /// <param name="food">The food that will be ate.</param>
        public void Eat(IEater eater, Food food)
        {
            eater.Weight += food.Weight;
        }
    }
}
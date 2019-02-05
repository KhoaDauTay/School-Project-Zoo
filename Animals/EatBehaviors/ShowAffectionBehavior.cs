using System;
using Foods;

namespace Animals
{
    /// <summary>
    /// The class that represents an eating behavior of showing affection.
    /// </summary>
    [Serializable]
    public class ShowAffectionBehavior : IEatBehavior
    {
        /// <summary>
        /// Animal consumes the food.
        /// </summary>
        /// <param name="eater">The eater who will consume the food.</param>
        /// <param name="food">The food that will be ate.</param>
        public void Eat(IEater eater, Food food)
        {
            eater.Weight += food.Weight;

            this.ShowAffection();
        }  
        
        /// <summary>
        /// Shows affection after eating.
        /// </summary>
        public void ShowAffection()
        {
            // Shows affection after eating.
        }  
    }
}
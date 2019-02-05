using Foods;

namespace Animals
{
    /// <summary>
    /// The interface that defines a contract for all eating behaviors.
    /// </summary>
    public interface IEatBehavior
    {
        /// <summary>
        /// Has an animal eat food.
        /// </summary>
        /// <param name="eater">The eater that will consume the food.</param>
        /// <param name="food">The food to eat.</param>
        void Eat(IEater eater, Food food);
    }
}
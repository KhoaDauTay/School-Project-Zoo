using System;
using Utilities;

namespace Animals
{
    /// <summary>
    /// The class that represents a flying behavior.
    /// </summary>
    [Serializable]
    public class FlyBehavior : IMoveBehavior
    {
        /// <summary>
        /// Makes an animal fly.
        /// </summary>
        /// <param name="animal">The animal to move.</param>
        public void Move(Animal animal)
        {
            MoveHelper.MoveHorizontally(animal, animal.MoveDistance);

            // If the animal is currently moving down.
            if (animal.YDirection == VerticalDirection.Down)
            {
                // Move down by 10 and switch directions.
                animal.YPosition += 10;
                animal.YDirection = VerticalDirection.Up;
            }
            else
            {
                // Move up by 10 and switch directions.
                animal.YPosition -= 10;
                animal.YDirection = VerticalDirection.Down;
            }
        }
    }
}
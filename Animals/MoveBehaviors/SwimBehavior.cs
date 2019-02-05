using System;

namespace Animals
{
    /// <summary>
    /// The class that represents a swimming behavior.
    /// </summary>
    [Serializable]
    public class SwimBehavior : IMoveBehavior
    {
        /// <summary>
        /// Makes an animal swim.
        /// </summary>
        /// <param name="animal">The animal to move.</param>
        public void Move(Animal animal)
        {
            MoveHelper.MoveHorizontally(animal, animal.MoveDistance);

            MoveHelper.MoveVertically(animal, animal.MoveDistance / 2);
        }
    }
}
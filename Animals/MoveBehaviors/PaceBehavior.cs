using System;

namespace Animals
{
    /// <summary>
    /// The class that represents a pacing behavior.
    /// </summary>
    [Serializable]
    public class PaceBehavior : IMoveBehavior
    {
        /// <summary>
        /// Makes an animal pace.
        /// </summary>
        /// <param name="animal">The animal to move.</param>
        public void Move(Animal animal)
        {
            MoveHelper.MoveHorizontally(animal, animal.MoveDistance);
        }
    }
}
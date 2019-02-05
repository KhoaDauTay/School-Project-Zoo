using System;

namespace Animals
{
    /// <summary>
    /// The class that represents the standing still. 
    /// </summary>
    [Serializable]
    public class NoMoveBehavior : IMoveBehavior
    {
        /// <summary>
        /// Moves or doesn't move the animal.
        /// </summary>
        /// <param name="animal">The animal to stand still.</param>
        public void Move(Animal animal)
        {
            // Standing still.
        }
    }
}

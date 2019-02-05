namespace Animals
{
    /// <summary>
    /// The interface that defines a contract for all movement behaviors.
    /// </summary>
    public interface IMoveBehavior
    {
        /// <summary>
        /// Moves an animal.
        /// </summary>
        /// <param name="animal">The animal to move.</param>
        void Move(Animal animal);
    }
}
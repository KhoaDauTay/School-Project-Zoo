using Reproducers;

namespace Animals
{
    /// <summary>
    /// The interface that defines a contract for all reproducing behaviors.
    /// </summary>
    public interface IReproduceBehavior
    {
        /// <summary>
        /// Has an animal reproduce.
        /// </summary>
        /// <param name="mother">The pregnant mother animal.</param>
        /// <returns>The baby animal.</returns>
        IReproducer Reproduce(Animal mother);
    }
}
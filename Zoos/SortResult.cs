using System;

namespace Zoos
{
    /// <summary>
    /// The class that represents a result from sorting a list.
    /// </summary>
    [Serializable]
    public class SortResult
    {
        /// <summary>
        /// Gets or sets the number of swaps.
        /// </summary>
        public int SwapCount { get; set; }

        /// <summary>
        /// Gets or sets the number of comparisons.
        /// </summary>
        public int CompareCount { get; set; }

        /// <summary>
        /// Gets or sets the number of elapsed milliseconds.
        /// </summary>
        public double ElapsedMilliseconds { get; set; }
    }
}
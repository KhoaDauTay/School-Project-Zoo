using System;

namespace BoothItems
{
    /// <summary>
    /// The class representing an item.
    /// </summary>
    [Serializable]
    public class Item
    {
        /// <summary>
        /// The weight of the item.
        /// </summary>
        private double weight;

        /// <summary>
        /// Initializes a new instance of the Item class.
        /// </summary>
        /// <param name="weight">The weight of the item.</param>
        public Item(double weight)
        {
            this.weight = weight;
        }

        /// <summary>
        /// Gets the weight of the item.
        /// </summary>
        public double Weight
        {
            get
            {
                return this.weight;
            }
        }
    }
}
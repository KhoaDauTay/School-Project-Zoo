using System;

namespace BoothItems
{
    /// <summary>
    /// The class that represents a map of a zoo.
    /// </summary>
    [Serializable]
    public class Map : Item
    {
        /// <summary>
        /// The date the map was issued.
        /// </summary>
        private DateTime dateIssued;

        /// <summary>
        /// Initializes a new instance of the Map class.
        /// </summary>
        /// <param name="weight">The weight of the map.</param>
        /// <param name="dateIssued">The date the map was issued.</param>
        public Map(double weight, DateTime dateIssued)
            : base(weight)
        {
            this.dateIssued = dateIssued;
        }

        /// <summary>
        /// Gets the date the map was issued.
        /// </summary>
        public DateTime DateIssued
        {
            get
            {
                return this.dateIssued;
            }
        }
    }
}
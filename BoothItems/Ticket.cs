using System;

namespace BoothItems
{
    /// <summary>
    /// The class that represents a ticket.
    /// </summary>
    [Serializable]
    public class Ticket : SoldItem
    {
        /// <summary>
        /// A value indicating whether or not the ticket has been redeemed.
        /// </summary>
        private bool isRedeemed;

        /// <summary>
        /// The serial number of the ticket.
        /// </summary>
        private int serialNumber;

        /// <summary>
        /// Initializes a new instance of the Ticket class.
        /// </summary>
        /// <param name="price">The price of the ticket.</param>
        /// <param name="serialNumber">The serial number of the ticket.</param>
        /// <param name="weight">The weight of the ticket.</param>
        public Ticket(decimal price, int serialNumber, double weight)
            : base(price, weight)
        {
            this.serialNumber = serialNumber;
        }

        /// <summary>
        /// Gets a value indicating whether or not the ticket has been redeemed.
        /// </summary>
        public bool IsRedeemed
        {
            get
            {
                return this.isRedeemed;
            }
        }

        /// <summary>
        /// Gets the serial number of the ticket.
        /// </summary>
        public decimal SerialNumber
        {
            get
            {
                return this.serialNumber;
            }
        }

        /// <summary>
        /// Mark the ticket as having been used.
        /// </summary>
        public void Redeem()
        {
            this.isRedeemed = true;
        }
    }
}
using System;
using System.Collections.Generic;
using BoothItems;
using MoneyCollectors;

namespace People
{
    /// <summary>
    /// The class representing a money collecting booth.
    /// </summary>
    [Serializable]
    public class MoneyCollectingBooth : Booth
    {
        /// <summary>
        /// The starting money balance of the booth.
        /// </summary>
        public static readonly decimal InitialMoneyBalance = 100.00m;

        /// <summary>
        /// The booth's money box.
        /// </summary>
        private IMoneyCollector moneyBox;

        /// <summary>
        /// The price of a ticket.
        /// </summary>
        private decimal ticketPrice;

        /// <summary>
        /// The price of a water bottle.
        /// </summary>
        private decimal waterBottlePrice;

        /// <summary>
        /// The booth's stack of tickets.
        /// </summary>
        private Stack<Ticket> ticketStack;

        /// <summary>
        /// Initializes a new instance of the MoneyCollectingBooth class.
        /// </summary>
        /// <param name="attendant">The booth's attendant.</param>
        /// <param name="ticketPrice">The booth's ticket price.</param>
        /// <param name="waterBottlePrice">The booth's water bottle price.</param>
        /// <param name="moneyBox">The booth's money box.</param>
        public MoneyCollectingBooth(Employee attendant, decimal ticketPrice, decimal waterBottlePrice, IMoneyCollector moneyBox)
            : base(attendant)
        {
            this.ticketPrice = ticketPrice;
            this.waterBottlePrice = waterBottlePrice;
            this.moneyBox = moneyBox;
            this.ticketStack = new Stack<Ticket>();

            // Set the intial money balance.
            this.AddMoney(MoneyCollectingBooth.InitialMoneyBalance);

            for (int i = 0; i < 5; i++)
            {
                this.ticketStack.Push(new Ticket(this.TicketPrice, i + 1, 0.01));
            }

            for (int i = 0; i < 5; i++)
            {
                this.Items.Add(new WaterBottle(this.WaterBottlePrice, i + 1, 1));
            }
        }

        /// <summary>
        /// Gets the balance of the booth's money box.
        /// </summary>
        public decimal MoneyBalance
        {
            get
            {
                return this.moneyBox.MoneyBalance;
            }
        }

        /// <summary>
        /// Gets the price of a ticket.
        /// </summary>
        public decimal TicketPrice
        {
            get
            {
                return this.ticketPrice;
            }
        }

        /// <summary>
        /// Gets the price of a water bottle.
        /// </summary>
        public decimal WaterBottlePrice
        {
            get
            {
                return this.waterBottlePrice;
            }
        }

        /// <summary>
        /// Adds money to the booth's money box.
        /// </summary>
        /// <param name="amount">The amount to add.</param>
        public void AddMoney(decimal amount)
        {
            this.moneyBox.AddMoney(amount);
        }

        /// <summary>
        /// Removes money from the booth's money box.
        /// </summary>
        /// <param name="amount">The amount to remove from the money box.</param>
        /// <returns>The amount removed from the money box.</returns>
        public decimal RemoveMoney(decimal amount)
        {
            return this.moneyBox.RemoveMoney(amount);
        }

        /// <summary>
        /// Sells a ticket.
        /// </summary>
        /// <param name="payment">The payment for the ticket.</param>
        /// <returns>The sold ticket.</returns>
        public Ticket SellTicket(decimal payment)
        {
            Ticket result = null;

            try
            {
                if (payment >= this.TicketPrice)
                {
                    result = this.ticketStack.Pop();
                }

                if (result != null)
                {
                    this.AddMoney(payment);
                }
            }
            catch (MissingItemException ex)
            {
                throw new NullReferenceException("Ticket not found.", ex);
            }

            return result;
        }

        /// <summary>
        /// Sells a water bottle.
        /// </summary>
        /// <param name="payment">The payment for the water bottle.</param>
        /// <returns>The sold water bottle.</returns>
        public WaterBottle SellWaterBottle(decimal payment)
        {
            WaterBottle result = null;

            try
            {
                if (payment >= this.WaterBottlePrice)
                {
                    result = this.Attendant.FindItem(this.Items, typeof(WaterBottle)) as WaterBottle;
                }

                if (result != null)
                {
                    this.AddMoney(payment);
                }
            }
            catch (MissingItemException ex)
            {
                throw new NullReferenceException("Water bottle not found.", ex);
            }

            return result;
        }
    }
}
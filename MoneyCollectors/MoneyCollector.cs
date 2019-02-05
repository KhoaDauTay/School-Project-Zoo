using System;

namespace MoneyCollectors
{
    /// <summary>
    /// The class that represents a collector of money.
    /// </summary>
    [Serializable]
    public abstract class MoneyCollector : IMoneyCollector
    {
        /// <summary>
        /// The collector's current money balance.
        /// </summary>
        private decimal moneyBalance;

        /// <summary>
        /// Gets or sets the collector's current money balance.
        /// </summary>
        public decimal MoneyBalance
        {
            get
            {
                return this.moneyBalance;
            }

            set
            {
                this.moneyBalance = value;

                if (this.OnBalanceChange != null)
                {
                    this.OnBalanceChange();
                }
            }
        }

        /// <summary>
        /// Gets or sets the action of the on balance change.
        /// </summary>
        public Action OnBalanceChange { get; set; }

        /// <summary>
        /// Adds money to the money collector.
        /// </summary>
        /// <param name="amount">The amount of money to add.</param>
        public void AddMoney(decimal amount)
        {
            this.MoneyBalance += amount;
        }

        /// <summary>
        /// Removes a specified amount of money from the money collector.
        /// </summary>
        /// <param name="amount">The amount of money to remove from the money collector.</param>
        /// <returns>The amount of money that was removed from the money collector.</returns>
        public virtual decimal RemoveMoney(decimal amount)
        {
            decimal amountRemoved;

            // If there is enough money in the wallet...
            if (this.MoneyBalance >= amount)
            {
                // Return the requested amount.
                amountRemoved = amount;
            }
            else
            {
                // Return all remaining money.
                amountRemoved = this.MoneyBalance;
            }

            // Subtract the amount removed from the wallet.
            this.MoneyBalance -= amountRemoved;

            return amountRemoved;
        }
    }
}
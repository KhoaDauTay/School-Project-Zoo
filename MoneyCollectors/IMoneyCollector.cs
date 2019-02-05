using System;

namespace MoneyCollectors
{
    /// <summary>
    /// The interface that defines a contract for all money collectors.
    /// </summary>
    public interface IMoneyCollector
    {
        /// <summary>
        /// Gets the money collector's money balance.
        /// </summary>
        decimal MoneyBalance { get; }

        /// <summary>
        /// Gets or sets the action on balance change.
        /// </summary>
        Action OnBalanceChange { get; set; }

        /// <summary>
        /// Adds money to the collector's balance.
        /// </summary>
        /// <param name="amount">The amount to add.</param>
        void AddMoney(decimal amount);

        /// <summary>
        /// Removes money from the collector's balance.
        /// </summary>
        /// <param name="amount">The amount to remove.</param>
        /// <returns>The amount removed.</returns>
        decimal RemoveMoney(decimal amount);
    }
}
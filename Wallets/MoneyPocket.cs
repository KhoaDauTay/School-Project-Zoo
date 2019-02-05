using System;
using MoneyCollectors;

namespace Wallets
{
    /// <summary>
    /// The class that represents a pocket for collecting money.
    /// </summary>
    [Serializable]
    public class MoneyPocket : MoneyCollector
    {
        /// <summary>
        /// Removes money from the money pocket.
        /// </summary>
        /// <param name="amount">The amount of money to remove.</param>
        /// <returns>The amount removed.</returns>
        public override decimal RemoveMoney(decimal amount)
        {
            decimal result;
            this.Unfold();
            result = base.RemoveMoney(amount);
            this.Fold();
            return result;
        }

        /// <summary>
        /// Folds the money pocket closed.
        /// </summary>
        private void Fold()
        {
        }

        /// <summary>
        /// Unfolds the money pocket open.
        /// </summary>
        private void Unfold()
        {
        }
    }
}
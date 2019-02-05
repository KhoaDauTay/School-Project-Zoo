using System;
using MoneyCollectors;

namespace MoneyBoxes
{
    /// <summary>
    /// The class that represents a box for collecting money.
    /// </summary>
    [Serializable]
    public class MoneyBox : MoneyCollector
    {
        /// <summary>
        /// Removes money from the money box.
        /// </summary>
        /// <param name="amount">The amount of money to remove.</param>
        /// <returns>The amount of money removed.</returns>
        public override decimal RemoveMoney(decimal amount)
        {
            decimal result;
            this.Unlock();
            result = base.RemoveMoney(amount);
            this.Lock();
            return result;
        }

        /// <summary>
        /// Closes and locks the money box.
        /// </summary>
        private void Lock()
        {
        }

        /// <summary>
        /// Unlocks and opens the money box.
        /// </summary>
        private void Unlock()
        {
        }
    }
}
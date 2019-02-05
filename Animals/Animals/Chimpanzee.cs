using System;
using MoneyCollectors;
using Reproducers;

namespace Animals
{
    /// <summary>
    /// The class that represents a chimpanzee.
    /// </summary>
    [Serializable]
    public class Chimpanzee : Mammal, IMoneyCollector
    {
        /// <summary>
        /// Initializes a new instance of the Chimpanzee class.
        /// </summary>
        /// <param name="name">The name of the chimpanzee.</param>
        /// <param name="age">The age of the chimpanzee.</param>
        /// <param name="weight">The weight of the chimpanzee (in pounds).</param>
        /// <param name="gender">The gender of the chimpanzee.</param>
        public Chimpanzee(string name, int age, double weight, Gender gender)
            : base(name, age, weight, gender)
        {
            this.BabyWeightPercentage = 10.0;
        }

        /// <summary>
        /// Gets the chimpanzee money balance.
        /// </summary>
        public decimal MoneyBalance
        {
            get
            {
                return 0m;
            }
        }

        /// <summary>
        /// Gets or sets the action on the balance change.
        /// </summary>
        public Action OnBalanceChange { get; set; }

        /// <summary>
        /// Adds money to the chimpanzee's money balance.
        /// </summary>
        /// <param name="amount">The amount to add.</param>
        public void AddMoney(decimal amount)
        {
            // Buy some bananas.
        }

        /// <summary>
        /// Removes money from the chimpanzee's money balance.
        /// </summary>
        /// <param name="amount">The amount to remove.</param>
        /// <returns>The amount removed.</returns>
        public decimal RemoveMoney(decimal amount)
        {
            return 0m;
        }
    }
}
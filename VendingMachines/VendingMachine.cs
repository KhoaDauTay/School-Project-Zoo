using System;
using Foods;
using MoneyCollectors;

namespace VendingMachines
{
    /// <summary>
    /// The class which is used to represent a vending machine.
    /// </summary>
    [Serializable]
    public class VendingMachine
    {
        /// <summary>
        /// The maximums food stock level of the vending machine (in pounds).
        /// </summary>
        private readonly double maxFoodStock = 250.0;

        /// <summary>
        /// The price of food (per pound).
        /// </summary>
        private decimal foodPricePerPound;

        /// <summary>
        /// The amount of food currently in stock (in pounds).
        /// </summary>
        private double foodStock;

        /// <summary>
        /// The vending machine's money box.
        /// </summary>
        private IMoneyCollector moneyBox;

        /// <summary>
        /// Initializes a new instance of the VendingMachine class.
        /// </summary>
        /// <param name="foodPrice">The price of food (per pound).</param>
        /// <param name="moneyBox">The vending machine's money box.</param>
        public VendingMachine(decimal foodPrice, IMoneyCollector moneyBox)
        {
            this.foodPricePerPound = foodPrice;
            this.moneyBox = moneyBox;

            // Fill vending machine with an initial load of food.
            this.foodStock = this.maxFoodStock;
        }

        /// <summary>
        /// Gets the money balance of the vending machine's money box.
        /// </summary>
        public decimal MoneyBalance
        {
            get
            {
                return this.moneyBox.MoneyBalance;
            }
        }

        /// <summary>
        /// Adds money to the vending machine's money box.
        /// </summary>
        /// <param name="amount">The amount of money to add.</param>
        public void AddMoney(decimal amount)
        {
            this.moneyBox.AddMoney(amount);
        }

        /// <summary>
        /// Removes money from the vending machine's money box.
        /// </summary>
        /// <param name="amount">The amount of money to remove.</param>
        /// <returns>The amount of money removed.</returns>
        public decimal RemoveMoney(decimal amount)
        {
            return this.moneyBox.RemoveMoney(amount);
        }

        /// <summary>
        /// Buys food from the vending machine.
        /// </summary>
        /// <param name="payment">The payment for the food.</param>
        /// <returns>The purchased food.</returns>
        public Food BuyFood(decimal payment)
        {
            // Add money to the vending machine.
            this.AddMoney(payment);

            // Determine the weight of the food to return based upon the amount paid.
            double weight = (double)(payment / this.foodPricePerPound);

            // Reduce stock level.
            this.foodStock -= weight;

            // Create and return food.
            return new Food(weight);
        }

        /// <summary>
        /// Determines the price of food for an animal of a specified weight.
        /// </summary>
        /// <param name="animalWeight">The weight of the animal for which to determine food price.</param>
        /// <returns>The price for the amount of food required to sufficiently feed an animal of the specified weight.</returns>
        public decimal DetermineFoodPrice(double animalWeight)
        {
            // Determine the amount of food required.
            double foodWeight = animalWeight * 0.02;

            // Determine food price.
            decimal foodPrice = (decimal)foodWeight * this.foodPricePerPound;

            // Round food price.
            foodPrice = Math.Round(foodPrice, 2);

            return foodPrice;
        }
    }
}
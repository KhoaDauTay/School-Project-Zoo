using System;
using System.Collections.Generic;
using BoothItems;
using Foods;
using Reproducers;

namespace People
{
    /// <summary>
    /// The class which is used to represent an employee.
    /// </summary>
    [Serializable]
    public class Employee : IEater
    {
        /// <summary>
        /// The number of baby animals the employee has delivered.
        /// </summary>
        private int animalDeliveryCount;

        /// <summary>
        /// The name of the employee.
        /// </summary>
        private string name;

        /// <summary>
        /// The employee's identification number.
        /// </summary>
        private int number;

        /// <summary>
        /// Initializes a new instance of the Employee class.
        /// </summary>
        /// <param name="name">The name of the employee.</param>
        /// <param name="number">The employee's identification number.</param>
        public Employee(string name, int number)
        {
            this.name = name;
            this.number = number;
        }

        /// <summary>
        /// Gets or sets the weight of the employee.
        /// </summary>
        public double Weight { get; set; }

        /// <summary>
        /// Aids the specified reproducer in delivering its baby.
        /// </summary>
        /// <param name="reproducer">The reproducer that is to give birth.</param>
        /// <returns>The resulting baby reproducer.</returns>
        public IReproducer DeliverAnimal(IReproducer reproducer)
        {
            // Sterilize birthing area.
            this.SterilizeBirthingArea();

            // Give birth.
            IReproducer baby = reproducer.Reproduce();

            // Wash up birthing area.
            this.WashUpBirthingArea();

            // Increase counter.
            this.animalDeliveryCount++;

            return baby;
        }

        /// <summary>
        /// Eats the specified food.
        /// </summary>
        /// <param name="food">The food to eat.</param>
        public void Eat(Food food)
        {
            // Eat the food.
        }

        /// <summary>
        /// Finds the first matching item from the booth's item list.
        /// </summary>
        /// <param name="items">The list of items to search.</param>
        /// <param name="type">The type of item required.</param>
        /// <returns>The found ticket.</returns>
        public Item FindItem(List<Item> items, Type type)
        {
            Item result = null;

            if (items.Count > 0)
            {
                foreach (Item i in items)
                {
                    if (i.GetType() == type)
                    {
                        result = i;
                        items.Remove(result);
                        break;
                    }
                }
            }

            if (result == null)
            {
                throw new MissingItemException(string.Format("An item of type {0} does not exist.", type.Name));
            }

            return result;
        }

        /// <summary>
        /// Sterilizes the birthing area in preparation for delivering a baby.
        /// </summary>
        private void SterilizeBirthingArea()
        {
            // Sterilize birthing area.
        }

        /// <summary>
        /// Washes up the birthing area after having delivered a baby.
        /// </summary>
        private void WashUpBirthingArea()
        {
            // Wash up birthing area.
        }
    }
}
using System;
using Reproducers;

namespace Animals
{
    /// <summary>
    /// The class that represents a factory for creating animals.
    /// </summary>
    public static class AnimalFactory
    {
        /// <summary>
        /// Creates an animal of the specified type, with the specified values.
        /// </summary>
        /// <param name="type">The type of animal to create.</param>
        /// <param name="name">The animal's name.</param>
        /// <param name="age">The animal's age.</param>
        /// <param name="weight">The animal's weight (in pounds).</param>
        /// <param name="gender">The animal's gender.</param>
        /// <returns>The created animal.</returns>
        public static Animal CreateAnimal(AnimalType type, string name, int age, double weight, Gender gender)
        {
            Animal result = null;

            switch (type)
            {
                case AnimalType.Chimpanzee:
                    result = new Chimpanzee(name, age, weight, gender);
                    break;
                case AnimalType.Dingo:
                    result = new Dingo(name, age, weight, gender);
                    break;
                case AnimalType.Eagle:
                    result = new Eagle(name, age, weight, gender);
                    break;
                case AnimalType.Hummingbird:
                    result = new Hummingbird(name, age, weight, gender);
                    break;
                case AnimalType.Kangaroo:
                    result = new Kangaroo(name, age, weight, gender);
                    break;
                case AnimalType.Ostrich:
                    result = new Ostrich(name, age, weight, gender);
                    break;
                case AnimalType.Platypus:
                    result = new Platypus(name, age, weight, gender);
                    break;
                case AnimalType.Shark:
                    result = new Shark(name, age, weight, gender);
                    break;
                case AnimalType.Squirrel:
                    result = new Squirrel(name, age, weight, gender);
                    break;
                default:
                    throw new Exception("Unsupported animal type.");
            }

            return result;
        }
    }
}
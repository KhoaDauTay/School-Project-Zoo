using System;
using System.Collections.Generic;
using System.Linq;
using Animals;
using People;
using Reproducers;

namespace Zoos
{
    /// <summary>
    /// The class which represents zoo extensions.
    /// </summary>
    public static class ZooExtensions
    {
        /// <summary>
        /// Finds the animal in the zoo.
        /// </summary>
        /// <param name="z">The current zoo.</param>
        /// <param name="match">The matching predicate.</param>
        /// <returns>Returns the found animal.</returns>
        public static Animal FindAnimal(this Zoo z, Predicate<Animal> match)
        {
            return z.Animals.ToList().Find(match);
        }

        /// <summary>
        /// Finds a guest based on name.
        /// </summary>
        /// <param name="z">The current zoo.</param>
        /// <param name="match">The matching item to find.</param>
        /// <returns>The first matching guest.</returns>
        public static Guest FindGuest(this Zoo z, Predicate<Guest> match)
        {
            return z.Guests.ToList().Find(match);
        }

        /// <summary>
        /// The youngest guests in the zoo.
        /// </summary>
        /// <param name="z">The current zoo.</param>
        /// <returns>The list of guests.</returns>
        public static IEnumerable<object> GetYoungGuests(this Zoo z)
        {
            return from g in z.Guests where g.Age <= 10 select new { g.Name, g.Age };
        }

        /// <summary>
        /// The female dingoes.
        /// </summary>
        /// <param name="z">The current zoo.</param>
        /// <returns>The list of animals.</returns>
        public static IEnumerable<object> GetFemaleDingoes(this Zoo z)
        {
            return from a in z.Animals where a.GetType() == typeof(Dingo) && a.Gender == Gender.Female select new { a.Name, a.Age, a.Weight, a.Gender };
        }

        /// <summary>
        /// The heavy animals.
        /// </summary>
        /// <param name="z">The current zoo.</param>
        /// <returns>The list of animals.</returns>
        public static IEnumerable<object> GetHeavyAnimals(this Zoo z)
        {
            return from a in z.Animals where a.Weight >= 200 select new { Type = a.GetType().Name, a.Name, a.Age, a.Weight };
        }

        /// <summary>
        /// The guests sorted by age.
        /// </summary>
        /// <param name="z">The current zoo.</param>
        /// <returns>The list of guests.</returns>
        public static IEnumerable<object> GetGuestsByAge(this Zoo z)
        {
            return from g in z.Guests where g.Age >= 0 orderby g.Name ascending select new { g.Name, g.Age, g.Gender };
        }

        /// <summary>
        /// The flying animals.
        /// </summary>
        /// <param name="z">The current zoo.</param>
        /// <returns>The list of animals.</returns>
        public static IEnumerable<object> GetFlyingAnimals(this Zoo z)
        {
            return from a in z.Animals where a.MoveBehavior is FlyBehavior select new { Type = a.GetType().Name, a.Name };
        }

        /// <summary>
        /// The adopted animals.
        /// </summary>
        /// <param name="z">The current zoo.</param>
        /// <returns>The list of animals.</returns>
        public static IEnumerable<object> GetAdoptedAnimals(this Zoo z)
        {
            return from g in z.Guests where g.AdoptedAnimal != null select new { g.Name, AnimalName = g.AdoptedAnimal.Name, Type = g.AdoptedAnimal.GetType().Name };
        }

        /// <summary>
        /// The total balance by wallet color.
        /// </summary>
        /// <param name="z">The current zoo.</param>
        /// <returns>The list of objects.</returns>
        public static IEnumerable<object> GetTotalBalanceByWalletColor(this Zoo z)
        {
            return from g in z.Guests
                   group g by g.Wallet.Color
                   into gWallet
                   orderby gWallet.Key
                   select new { GroupKey = gWallet.Key.ToString(), Total = gWallet.Sum(g => g.Wallet.MoneyBalance) };
        }

        /// <summary>
        /// The average weight by animal type.
        /// </summary>
        /// <param name="z">The current zoo.</param>
        /// <returns>The list of objects.</returns>
        public static IEnumerable<object> GetAverageWeightByAnimalType(this Zoo z)
        {
            return from a in z.Animals
                   group a by a.GetType().Name
                   into aAnimal
                   orderby aAnimal.Key
                   select new { GroupKey = aAnimal.Key, AverageWeight = aAnimal.Average(a => a.Weight) };
        }
    }
}

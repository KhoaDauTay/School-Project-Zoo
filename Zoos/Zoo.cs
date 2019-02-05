using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using Animals;
using BirthingRooms;
using BoothItems;
using MoneyBoxes;
using People;
using Reproducers;
using VendingMachines;

namespace Zoos
{
    /// <summary>
    /// The class which is used to represent a zoo.
    /// </summary>
    [Serializable]
    public class Zoo
    {
        /// <summary>
        /// A list of all animals currently residing within the zoo.
        /// </summary>
        private List<Animal> animals;

        /// <summary>
        /// The zoo's vending machine which allows guests to buy snacks for animals.
        /// </summary>
        private VendingMachine animalSnackMachine;

        /// <summary>
        /// The zoo's room for birthing animals.
        /// </summary>
        private BirthingRoom b168;

        /// <summary>
        /// The zoo's collection of cages.
        /// </summary>
        private Dictionary<Type, Cage> cages = new Dictionary<Type, Cage>();

        /// <summary>
        /// The maximum number of guests the zoo can accommodate at a given time.
        /// </summary>
        private int capacity;

        /// <summary>
        /// A list of all guests currently visiting the zoo.
        /// </summary>
        private List<Guest> guests;

        /// <summary>
        /// The zoo's information booth.
        /// </summary>
        private GivingBooth informationBooth;

        /// <summary>
        /// The zoo's ladies' restroom.
        /// </summary>
        private Restroom ladiesRoom;

        /// <summary>
        /// The zoo's men's restroom.
        /// </summary>
        private Restroom mensRoom;

        /// <summary>
        /// The name of the zoo.
        /// </summary>
        private string name;

        /// <summary>
        /// The delegate of the temperature change.
        /// </summary>
        [NonSerialized]
        private Action<double, double> onBirthingRoomTemperatureChange;

        /// <summary>
        /// On adding the animal.
        /// </summary>
        [NonSerialized]
        private Action<Animal> onAddAnimal;

        /// <summary>
        /// On adding the guest.
        /// </summary>
        [NonSerialized]
        private Action<Guest> onAddGuest;

        /// <summary>
        /// On removing the animal.
        /// </summary>
        [NonSerialized]
        private Action<Animal> onRemoveAnimal;

        /// <summary>
        /// On removing the guest.
        /// </summary>
        [NonSerialized]
        private Action<Guest> onRemoveGuest;

        /// <summary>
        /// The zoo's ticket booth.
        /// </summary>
        private MoneyCollectingBooth ticketBooth;

        /// <summary>
        /// Initializes a new instance of the Zoo class.
        /// </summary>
        /// <param name="name">The name of the zoo.</param>
        /// <param name="capacity">The maximum number of guests the zoo can accommodate at a given time.</param>
        /// <param name="restroomCapacity">The capacity of the zoo's restrooms.</param>
        /// <param name="animalFoodPrice">The price of a pound of food from the zoo's animal snack machine.</param>
        /// <param name="ticketPrice">The price of an admission ticket to the zoo.</param>
        /// <param name="waterBottlePrice">The price of a water bottle.</param>
        /// <param name="attendant">The zoo's ticket booth attendant.</param>
        /// <param name="vet">The zoo's birthing room vet.</param>
        public Zoo(string name, int capacity, int restroomCapacity, decimal animalFoodPrice, decimal ticketPrice, decimal waterBottlePrice, Employee attendant, Employee vet)
        {
            this.animals = new List<Animal>();
            this.animalSnackMachine = new VendingMachine(animalFoodPrice, new MoneyBox());
            this.b168 = new BirthingRoom(vet);
            this.b168.OnTemperatureChange = (previousTemp, currentTemp) =>
            {
                this.OnBirthingRoomTemperatureChange(previousTemp, currentTemp);
            };
            this.capacity = capacity;
            this.guests = new List<Guest>();
            this.informationBooth = new GivingBooth(attendant);
            this.ladiesRoom = new Restroom(restroomCapacity, Gender.Female);
            this.mensRoom = new Restroom(restroomCapacity, Gender.Male);
            this.name = name;
            this.ticketBooth = new MoneyCollectingBooth(attendant, ticketPrice, waterBottlePrice, new MoneyBox());

            foreach (AnimalType at in Enum.GetValues(typeof(AnimalType)))
            {
                this.cages.Add(Animal.ConvertAnimalTypeToType(at), new Cage(400, 800));
            }

            this.AddAnimal(new Chimpanzee("Bobo", 10, 128.2, Gender.Male));
            this.AddAnimal(new Chimpanzee("Bubbles", 3, 103.8, Gender.Female));
            this.AddAnimal(new Dingo("Spot", 5, 41.3, Gender.Male));
            this.AddAnimal(new Dingo("Maggie", 6, 37.2, Gender.Female));
            this.AddAnimal(new Dingo("Toby", 0, 15.0, Gender.Male));
            this.AddAnimal(new Eagle("Ari", 12, 10.1, Gender.Female));
            this.AddAnimal(new Hummingbird("Buzz", 2, 0.02, Gender.Male));
            this.AddAnimal(new Hummingbird("Bitsy", 1, 0.03, Gender.Female));
            this.AddAnimal(new Kangaroo("Kanga", 8, 72.0, Gender.Female));
            this.AddAnimal(new Kangaroo("Roo", 0, 23.9, Gender.Male));
            this.AddAnimal(new Kangaroo("Jake", 9, 153.5, Gender.Male));
            this.AddAnimal(new Ostrich("Stretch", 26, 231.7, Gender.Male));
            this.AddAnimal(new Ostrich("Speedy", 30, 213.0, Gender.Female));
            this.AddAnimal(new Platypus("Patti", 13, 4.4, Gender.Female));
            this.AddAnimal(new Platypus("Bill", 11, 4.9, Gender.Male));
            this.AddAnimal(new Platypus("Ted", 0, 1.1, Gender.Male));
            this.AddAnimal(new Shark("Bruce", 19, 810.6, Gender.Female));
            this.AddAnimal(new Shark("Anchor", 17, 458.0, Gender.Male));
            this.AddAnimal(new Shark("Chum", 14, 377.3, Gender.Male));
            this.AddAnimal(new Squirrel("Chip", 4, 1.0, Gender.Male));
            this.AddAnimal(new Squirrel("Dale", 4, 0.9, Gender.Male));
        }

        /// <summary>
        /// Gets a list of the zoo's animals.
        /// </summary>
        public IEnumerable<Animal> Animals
        {
            get
            {
                return this.animals;
            }
        }

        /// <summary>
        /// Gets the zoo's animal snack machine.
        /// </summary>
        public VendingMachine AnimalSnackMachine
        {
            get
            {
                return this.animalSnackMachine;
            }
        }

        /// <summary>
        /// Gets or sets the temperature of the zoo's birthing room.
        /// </summary>
        public double BirthingRoomTemperature
        {
            get
            {
                return this.b168.Temperature;
            }

            set
            {
                this.b168.Temperature = value;
            }
        }

        /// <summary>
        /// Gets or sets adding the animal.
        /// </summary>
        public Action<Animal> OnAddAnimal
        {
            get
            {
                return this.onAddAnimal;
            }

            set
            {
                this.onAddAnimal = value;
            }
        }

        /// <summary>
        /// Gets or sets adding the guest.
        /// </summary>
        public Action<Guest> OnAddGuest
        {
            get
            {
                return this.onAddGuest;
            }

            set
            {
                this.onAddGuest = value;
            }
        }

        /// <summary>
        /// Gets or sets the removing of the animal.
        /// </summary>
        public Action<Animal> OnRemoveAnimal
        {
            get
            {
                return this.onRemoveAnimal;
            }

            set
            {
                this.onRemoveAnimal = value;
            }
        }

        /// <summary>
        /// Gets or sets the removing of the guest.
        /// </summary>
        public Action<Guest> OnRemoveGuest
        {
            get
            {
                return this.onRemoveGuest;
            }

            set
            {
                this.onRemoveGuest = value;
            }
        }

        /// <summary>
        /// Gets a list of the zoo's guests.
        /// </summary>
        public IEnumerable<Guest> Guests
        {
            get
            {
                return this.guests;
            }
        }

        /// <summary>
        /// Gets or sets an action when the birthing room temperature changes.
        /// </summary>
        public Action<double, double> OnBirthingRoomTemperatureChange
        {
            get
            {
                return this.onBirthingRoomTemperatureChange;
            }

            set
            {
                this.onBirthingRoomTemperatureChange = value;
            }
        }

        /// <summary>
        /// Gets the total weight of all animals in the zoo.
        /// </summary>
        public double TotalAnimalWeight
        {
            get
            {
                double totalWeight = 0;

                // Loop through the zoo's list of animals.
                foreach (Animal a in this.animals)
                {
                    // Add the current animal's weight to the total.
                    totalWeight += a.Weight;
                }

                return totalWeight;
            }
        }

        /// <summary>
        /// Loads the zoo from a file.
        /// </summary>
        /// <param name="fileName">The name of the file.</param>
        /// <returns>The zoo from the file.</returns>
        public static Zoo LoadFromFile(string fileName)
        {
            Zoo result = null;

            // Create a binary formatter
            BinaryFormatter formatter = new BinaryFormatter();

            // Open and read a file using the passed-in file name
            // Use a using statement to automatically clean up object references
            // and close the file handle when the deserialization process is complete
            using (Stream stream = File.OpenRead(fileName))
            {
                // Deserialize (load) the file as a zoo
                result = formatter.Deserialize(stream) as Zoo;
            }

            return result;
        }

        /// <summary>
        /// Creates a new zoo.
        /// </summary>
        /// <returns>A newly created zoo.</returns>
        public static Zoo NewZoo()
        {
            // Create an instance of the Zoo class.
            Zoo comoZoo = new Zoo("Como Zoo", 1000, 4, 0.75m, 15.00m, 3.00m, new Employee("Sam", 42), new Employee("Flora", 98));

            return comoZoo;
        }

        /// <summary>
        /// Adds an animal to the zoo.
        /// </summary>
        /// <param name="animal">The animal to add.</param>
        public void AddAnimal(Animal animal)
        {
            this.animals.Add(animal);

            animal.IsActive = true;

            if (this.OnAddAnimal != null)
            {
                this.OnAddAnimal(animal);
            }

            this.cages[animal.GetType()].Add(animal);

            if (animal.IsPregnant)
            {
                this.b168.PregnantAnimals.Enqueue(animal);
            }

            animal.OnPregnant = a => this.b168.PregnantAnimals.Enqueue(a);
        }

        /// <summary>
        /// Adds a list of animals to the zoo.
        /// </summary>
        /// <param name="animals">The list of animals to add to zoo.</param>
        public void AddAnimalsToZoo(IEnumerable<Animal> animals)
        {
            // loop through passed-in list of animals
            foreach (Animal a in animals)
            {
                // add the animal to the list (use AddAnimal)
                this.AddAnimal(a);

                // using recursion, add the current animal's children to the zoo
                this.AddAnimalsToZoo(a.Children);
            }
        }

        /// <summary>
        /// Adds a guest to the zoo.
        /// </summary>
        /// <param name="guest">The guest to add.</param>
        /// <param name="ticket">The guest's ticket.</param>
        public void AddGuest(Guest guest, Ticket ticket)
        {
            if (ticket == null || ticket.IsRedeemed)
            {
                throw new NullReferenceException("Guest " + guest.Name + " was not added because they did not have a ticket.");
            }
            else
            {
                ticket.Redeem();
                guest.GetVendingMachine = this.ProvideVendingMachine;
                this.guests.Add(guest);

                if (this.OnAddGuest != null)
                {
                    this.OnAddGuest(guest);
                }
            }
        }

        /// <summary>
        /// Aids a reproducer in giving birth.
        /// </summary>
        public void BirthAnimal()
        {
            // Birth animal.
            IReproducer baby = this.b168.BirthAnimal();

            // If the baby is an animal...
            if (baby is Animal)
            {
                // Add the baby to the zoo's list of animals.
                this.AddAnimal(baby as Animal);
            }
        }

        /// <summary>
        /// Finds the first cage that holds the specified type of animal.
        /// </summary>
        /// <param name="animalType">The type of animal whose cage to find.</param>
        /// <returns>The found cage.</returns>
        public Cage FindCage(Type animalType)
        {
            Cage result = null;

            this.cages.TryGetValue(animalType, out result);

            return result;
        }

        /// <summary>
        /// Gets all animals of a specified type.
        /// </summary>
        /// <param name="type">The type of animals to get.</param>
        /// <returns>The collection of animals.</returns>
        public IEnumerable<Animal> GetAnimals(Type type)
        {
            List<Animal> result = new List<Animal>();

            foreach (Animal a in this.animals)
            {
                if (a.GetType() == type)
                {
                    result.Add(a);
                }
            }

            return result;
        }

        /// <summary>
        /// Removes an animal from the zoo.
        /// </summary>
        /// <param name="animal">The animal to remove.</param>
        public void RemoveAnimal(Animal animal)
        {
            this.animals.Remove(animal);

            animal.IsActive = false;

            if (this.OnRemoveAnimal != null)
            {
                this.OnRemoveAnimal(animal);
            }

            this.cages[animal.GetType()].Remove(animal);

            foreach (Guest g in this.Guests)
            {
                if (g.AdoptedAnimal == animal)
                {
                    g.AdoptedAnimal = null;
                }
            }
        }

        /// <summary>
        /// Removes a guest from the zoo.
        /// </summary>
        /// <param name="guest">The guest to remove.</param>
        public void RemoveGuest(Guest guest)
        {
            this.guests.Remove(guest);

            guest.IsActive = false;

            if (guest.AdoptedAnimal != null)
            {
                Cage cage = this.FindCage(guest.AdoptedAnimal.GetType());

                cage.Remove(guest);
            }

            if (this.OnRemoveGuest == null)
            {
                this.OnRemoveGuest(guest);
            }
        }

        /// <summary>
        /// On action, perform a task.
        /// </summary>
        public void OnDeserialized()
        {
            if (this.OnAddGuest != null)
            {
                this.guests.ForEach(g => this.OnAddGuest(g));
            }

            if (this.OnAddAnimal != null)
            {
                foreach (Animal a in this.animals)
                {
                    this.OnAddAnimal(a);

                    if (a.IsPregnant)
                    {
                        a.OnPregnant = animal =>
                        {
                            this.b168.PregnantAnimals.Enqueue(animal);
                        };
                    }
                }
            }

            if (this.onBirthingRoomTemperatureChange != null)
            {
                this.OnBirthingRoomTemperatureChange(this.b168.Temperature, this.b168.Temperature);
            }
        }

        /// <summary>
        /// Saves the zoo to a file.
        /// </summary>
        /// <param name="fileName">The name of the file.</param>
        public void SaveToFile(string fileName)
        {
            // Create a binary formatter
            BinaryFormatter formatter = new BinaryFormatter();

            // Create a file using the passed-in file name
            // Use a using statement to automatically clean up object references
            // and close the file handle when the serialization process is complete
            using (Stream stream = File.Create(fileName))
            {
                // Serialize (save) the current instance of the zoo
                formatter.Serialize(stream, this);
            }
        }

        /// <summary>
        /// Sells a ticket to a guest.
        /// </summary>
        /// <param name="guest">The guest to whom to sell the ticket.</param>
        /// <returns>The sold ticket.</returns>
        public Ticket SellTicket(Guest guest)
        {
            Ticket result = null;

            result = guest.VisitTicketBooth(this.ticketBooth);

            guest.VisitInformationBooth(this.informationBooth);

            return result;
        }

        /// <summary>
        /// Sorts the list of animals.
        /// </summary>
        /// <param name="sortType">The type of sort.</param>
        /// <param name="sortValue">The value to sort on.</param>
        /// <returns>The sorted list of animals.</returns>
        public SortResult SortAnimals(SortType sortType, string sortValue)
        {
            return this.SortObjects(sortType, sortValue, this.animals);
        }

        /// <summary>
        /// Sorts the list of guests.
        /// </summary>
        /// <param name="sortType">The type of sort.</param>
        /// <param name="sortValue">The value to sort on.</param>
        /// <returns>The list of sorted guests.</returns>
        public SortResult SortGuests(SortType sortType, string sortValue)
        {
            return this.SortObjects(sortType, sortValue, this.guests);
        }

        /// <summary>
        /// Sorts the zoo's list of animals.
        /// </summary>
        /// <param name="sortType">The type of sort to perform.</param>
        /// <param name="sortValue">The value on which to sort.</param>
        /// <param name="list">The list to be sorted.</param>
        /// <returns>The result of sorting (number of swaps and number of comparisons).</returns>
        public SortResult SortObjects(SortType sortType, string sortValue, IList list)
        {
            SortResult result = null;

            Func<object, object, int> comparer;

            switch (sortType)
            {
                case SortType.Bubble:
                    if (sortValue == "animalName")
                    {
                        comparer = AnimalNameSortComparer;
                        result = SortHelper.BubbleSort(list, comparer);
                    }
                    else if (sortValue == "guestName")
                    {
                        comparer = GuestNameSortComparer;
                        result = SortHelper.BubbleSort(list, comparer);
                    }
                    else if (sortValue == "moneyBalance")
                    {
                        comparer = MoneyBalanceSortComparer;
                        result = SortHelper.BubbleSort(list, comparer);
                    }
                    else if (sortValue == "age")
                    {
                        comparer = AgeSortComparer;
                        result = SortHelper.BubbleSort(list, comparer);
                    }
                    else
                    {
                        comparer = WeightSortComparer;
                        result = SortHelper.BubbleSort(list, comparer);
                    }

                    break;
                case SortType.Selection:
                    if (sortValue == "animalName")
                    {
                        comparer = AnimalNameSortComparer;
                        result = SortHelper.SelectionSort(list, comparer);
                    }
                    else if (sortValue == "guestName")
                    {
                        comparer = GuestNameSortComparer;
                        result = SortHelper.SelectionSort(list, comparer);
                    }
                    else if (sortValue == "moneyBalance")
                    {
                        comparer = MoneyBalanceSortComparer;
                        result = SortHelper.SelectionSort(list, comparer);
                    }
                    else if (sortValue == "age")
                    {
                        comparer = AgeSortComparer;
                        result = SortHelper.SelectionSort(list, comparer);
                    }
                    else
                    {
                        comparer = WeightSortComparer;
                        result = SortHelper.SelectionSort(list, comparer);
                    }

                    break;
                case SortType.Insertion:
                    if (sortValue == "animalName")
                    {
                        comparer = AnimalNameSortComparer;
                        result = SortHelper.InsertionSort(list, comparer);
                    }
                    else if (sortValue == "guestName")
                    {
                        comparer = GuestNameSortComparer;
                        result = SortHelper.InsertionSort(list, comparer);
                    }
                    else if (sortValue == "moneyBalance")
                    {
                        comparer = MoneyBalanceSortComparer;
                        result = SortHelper.InsertionSort(list, comparer);
                    }
                    else if (sortValue == "age")
                    {
                        comparer = AgeSortComparer;
                        result = SortHelper.InsertionSort(list, comparer);
                    }
                    else
                    {
                        comparer = WeightSortComparer;
                        result = SortHelper.InsertionSort(list, comparer);
                    }

                    break;
                case SortType.Shell:
                    if (sortValue == "animalName")
                    {
                        comparer = AnimalNameSortComparer;
                        result = SortHelper.ShellSort(list, comparer);
                    }
                    else if (sortValue == "guestName")
                    {
                        comparer = GuestNameSortComparer;
                        result = SortHelper.ShellSort(list, comparer);
                    }
                    else if (sortValue == "moneyBalance")
                    {
                        comparer = MoneyBalanceSortComparer;
                        result = SortHelper.ShellSort(list, comparer);
                    }
                    else if (sortValue == "age")
                    {
                        comparer = AgeSortComparer;
                        result = SortHelper.ShellSort(list, comparer);
                    }
                    else
                    {
                        comparer = WeightSortComparer;
                        result = SortHelper.ShellSort(list, comparer);
                    }

                    break;
                case SortType.Quick:
                    Stopwatch sw = new Stopwatch();
                    sw.Start();
                    SortResult sortResult = new SortResult();

                    if (sortValue == "animalName")
                    {
                        comparer = AnimalNameSortComparer;
                        SortHelper.QuickSort(list, 0, list.Count - 1, sortResult, comparer);
                    }
                    else if (sortValue == "guestName")
                    {
                        comparer = GuestNameSortComparer;
                        SortHelper.QuickSort(list, 0, list.Count - 1, sortResult, comparer);
                    }
                    else if (sortValue == "moneyBalance")
                    {
                        comparer = MoneyBalanceSortComparer;
                        SortHelper.QuickSort(list, 0, list.Count - 1, sortResult, comparer);
                    }
                    else if (sortValue == "age")
                    {
                        comparer = AgeSortComparer;
                        SortHelper.QuickSort(list, 0, list.Count - 1, sortResult, comparer);
                    }
                    else
                    {
                        comparer = WeightSortComparer;
                        SortHelper.QuickSort(list, 0, list.Count - 1, sortResult, comparer);
                    }

                    sw.Stop();
                    sortResult.ElapsedMilliseconds = sw.Elapsed.TotalMilliseconds;

                    result = sortResult;
                    break;
                default:
                    break;
            }

            return result;
        }

        /// <summary>
        /// The animal name sort comparer.
        /// </summary>
        /// <param name="object1">The first object to switch on.</param>
        /// <param name="object2">The second object to switch on.</param>
        /// <returns>A number from one to negative one.</returns>
        private static int AnimalNameSortComparer(object object1, object object2)
        {
            int comparer = 0;

            if (object1 is Animal && object2 is Animal)
            {
                Animal animal1 = (Animal)object1;
                Animal animal2 = (Animal)object2;
                comparer = string.Compare(animal1.Name, animal2.Name);
            }

            return comparer;
        }

        /// <summary>
        /// The guest name sort comparer.
        /// </summary>
        /// <param name="object1">The first object to switch on.</param>
        /// <param name="object2">The second object to switch on.</param>
        /// <returns>A number from one to negative one.</returns>
        private static int GuestNameSortComparer(object object1, object object2)
        {
            int comparer = 0;

            if (object1 is Guest && object2 is Guest)
            {
                Guest guest1 = (Guest)object1;
                Guest guest2 = (Guest)object2;
                comparer = string.Compare(guest1.Name, guest2.Name);
            }

            return comparer;
        }

        /// <summary>
        /// The weight sort comparer.
        /// </summary>
        /// <param name="object1">The first object to switch on.</param>
        /// <param name="object2">The second object to switch on.</param>
        /// <returns>A number from one to negative one.</returns>
        private static int WeightSortComparer(object object1, object object2)
        {
            int comparer = 0;

            if (object1 is Animal && object2 is Animal)
            {
                Animal animal1 = (Animal)object1;
                Animal animal2 = (Animal)object2;

                if (animal1.Weight == animal2.Weight)
                {
                    comparer = 0;
                }
                else if (animal1.Weight > animal2.Weight)
                {
                    comparer = 1;
                }
                else
                {
                    comparer = -1;
                }
            }

            return comparer;
        }

        /// <summary>
        /// The money balance sort comparer.
        /// </summary>
        /// <param name="object1">The first object to switch on.</param>
        /// <param name="object2">The second object to switch on.</param>
        /// <returns>A number from one to negative one.</returns>
        private static int MoneyBalanceSortComparer(object object1, object object2)
        {
            int comparer = 0;

            if (object1 is Guest && object2 is Guest)
            {
                Guest guest1 = (Guest)object1;
                Guest guest2 = (Guest)object2;

                decimal guest1Balance = guest1.CheckingAccount.MoneyBalance + guest1.Wallet.MoneyBalance;
                decimal guest2Balance = guest2.CheckingAccount.MoneyBalance + guest2.Wallet.MoneyBalance;

                if (guest1Balance == guest2Balance)
                {
                    comparer = 0;
                }
                else if (guest1Balance > guest2Balance)
                {
                    comparer = 1;
                }
                else
                {
                    comparer = -1;
                }
            }

            return comparer;
        }

        /// <summary>
        /// The age sort comparer.
        /// </summary>
        /// <param name="object1">The first object to switch on.</param>
        /// <param name="object2">The second object to switch on.</param>
        /// <returns>A number from one to negative one.</returns>
        private static int AgeSortComparer(object object1, object object2)
        {
            int comparer = 0;

            if (object1 is Animal && object2 is Animal)
            {
                Animal animal1 = (Animal)object1;
                Animal animal2 = (Animal)object2;

                if (animal1.Age == animal2.Age)
                {
                    comparer = 0;
                }
                else if (animal1.Age > animal2.Age)
                {
                    comparer = 1;
                }
                else
                {
                    comparer = -1;
                }
            }

            return comparer;
        }

        /// <summary>
        /// Provides the zoo's vending machine.
        /// </summary>
        /// <returns>The vending machine.</returns>
        private VendingMachine ProvideVendingMachine()
        {
            return this.AnimalSnackMachine;
        }
    }
}
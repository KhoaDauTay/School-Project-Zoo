using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text.RegularExpressions;
using System.Timers;
using CagedItems;
using Foods;
using Reproducers;
using Utilities;

namespace Animals
{
    /// <summary>
    /// The class which is used to represent an animal.
    /// </summary>
    [Serializable]
    public abstract class Animal : IEater, IMover, IReproducer, ICageable
    {
        /// <summary>
        /// A random object used to randomize the movement of each animal.
        /// </summary>
        private static Random random = new Random(DateTime.Now.Millisecond);

        /// <summary>
        /// The weight of a newborn baby (as a percentage of the parent's weight).
        /// </summary>
        private double babyWeightPercentage;

        /// <summary>
        /// The age of the animal.
        /// </summary>
        private int age;

        /// <summary>
        /// A list of the animal's children.
        /// </summary>
        private List<Animal> children;

        /// <summary>
        /// The gender of the animal.
        /// </summary>
        private Gender gender;

        /// <summary>
        /// A value indicating whether or not the animal is pregnant.
        /// </summary>
        private bool isPregnant;

        /// <summary>
        /// A timer that moves the animal when it goes off.
        /// </summary>
        [NonSerialized]
        private Timer moveTimer;

        /// <summary>
        /// A timer that starts the animal's hunger when it goes off.
        /// </summary>
        [NonSerialized]
        private Timer hungerTimer;

        /// <summary>
        /// On text change.
        /// </summary>
        [NonSerialized]
        private Action<Animal> onTextChange;

        /// <summary>
        /// On pregnant of the animal.
        /// </summary>
        [NonSerialized]
        private Action<IReproducer> onPregnant;

        /// <summary>
        /// On animal hunger.
        /// </summary>
        [NonSerialized]
        private Action<Animal> onHunger;

        /// <summary>
        /// The name of the animal.
        /// </summary>
        private string name;

        /// <summary>
        /// The weight of the animal (in pounds).
        /// </summary>
        private double weight;

        /// <summary>
        /// Initializes a new instance of the Animal class.
        /// </summary>
        /// <param name="name">The name of the animal.</param>
        /// <param name="weight">The weight of the animal.</param>
        /// <param name="gender">The gender of the animal.</param>
        public Animal(string name, double weight, Gender gender)
            : this(name, 0, weight, gender)
        {
        }

        /// <summary>
        /// Initializes a new instance of the Animal class.
        /// </summary>
        /// <param name="name">The name of the animal.</param>
        /// <param name="age">The age of the animal.</param>
        /// <param name="weight">The weight of the animal (in pounds).</param>
        /// <param name="gender">The gender of the animal.</param>
        public Animal(string name, int age, double weight, Gender gender)
        {
            this.age = age;
            this.gender = gender;
            this.name = name;
            this.weight = weight;

            this.YPositionMax = 400;
            this.XPositionMax = 800;

            this.MoveDistance = Animal.random.Next(1, 11);
            this.CreateTimers();
            
            this.XPosition = Animal.random.Next(0, this.XPositionMax + 1);
            this.YPosition = Animal.random.Next(0, this.YPositionMax + 1);
            this.XDirection = Animal.random.Next(0, 2) == 0 ? HorizontalDirection.Left : HorizontalDirection.Right;
            this.YDirection = Animal.random.Next(0, 2) == 0 ? VerticalDirection.Up : VerticalDirection.Down;

            this.children = new List<Animal>();
        }

        /// <summary>
        /// Gets or sets the age of the animal.
        /// </summary>
        public int Age
        {
            get
            {
                return this.age;
            }

            set
            {
                if (value < 0 || value > 100)
                {
                    throw new ArgumentOutOfRangeException("age", "Age must be between 0 and 100.");
                }

                this.age = value;

                if (this.OnTextChange != null)
                {
                    this.OnTextChange(this);
                }
            }
        }

        /// <summary>
        /// Gets or sets the weight of a newborn baby (as a percentage of the parent's weight).
        /// </summary>
        public double BabyWeightPercentage
        {
            get
            {
                return this.babyWeightPercentage;
            }

            protected set
            {
                this.babyWeightPercentage = value;
            }
        }

        /// <summary>
        /// Gets a list of the animal's children.
        /// </summary>
        public IEnumerable<Animal> Children
        {
            get
            {
                return this.children;
            }
        }

        /// <summary>
        /// Gets or sets the on hunger action.
        /// </summary>
        public Action<Animal> OnHunger { get; set; }

        /// <summary>
        /// Gets or sets the on pregnant action.
        /// </summary>
        public Action<IReproducer> OnPregnant { get; set; }

        /// <summary>
        /// Gets or sets the gender of the animal.
        /// </summary>
        public Gender Gender
        {
            get
            {
                return this.gender;
            }

            set
            {
                this.gender = value;
            }
        }

        /// <summary>
        /// Gets a value indicating whether or not the animal is pregnant.
        /// </summary>
        public bool IsPregnant
        {
            get
            {
                return this.isPregnant;
            }
        }

        /// <summary>
        /// Gets or sets the animal's move behavior.
        /// </summary>
        public IMoveBehavior MoveBehavior { get; set; }

        /// <summary>
        /// Gets or sets the animal's reproduce behavior.
        /// </summary>
        public IReproduceBehavior ReproduceBehavior { get; set; }

        /// <summary>
        /// Gets or sets the animal's eat behavior.
        /// </summary>
        public IEatBehavior EatBehavior { get; set; }

        /// <summary>
        /// Gets or sets the distance of one step.
        /// </summary>
        public int MoveDistance { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the animal is active.
        /// </summary>
        public bool IsActive
        {
            get
            {
                return this.moveTimer.Enabled;
            }
            
            set
            {
                this.moveTimer.Enabled = value;
            }
        }

        /// <summary>
        /// Gets or sets the name of the animal.
        /// </summary>
        public string Name
        {
            get
            {
                return this.name;
            }

            set
            {
                if (!Regex.IsMatch(value, @"^[a-zA-Z ]+$"))
                {
                    throw new ArgumentException("Names can contain only upper- and lowercase letters A-Z and spaces.");
                }

                this.name = value;

                if (this.OnTextChange != null)
                {
                    this.OnTextChange(this);
                }
            }
        }

        /// <summary>
        /// Gets or sets the animal's weight (in pounds).
        /// </summary>
        public double Weight
        {
            get
            {
                return this.weight;
            }

            set
            {
                if (value < 0 || value > 1000)
                {
                    throw new ArgumentOutOfRangeException("weight", "Weight must be between 0 and 1000 lbs.");
                }

                this.weight = value;

                if (this.OnTextChange != null)
                {
                    this.OnTextChange(this);
                }
            }
        }

        /// <summary>
        /// Gets or sets the on text change.
        /// </summary>
        public Action<Animal> OnTextChange
        {
            get
            {
                return this.onTextChange;
            }

            set
            {
                this.onTextChange = value;
            }
        }

        /// <summary>
        /// Gets or sets the horizontal direction.
        /// </summary>
        public HorizontalDirection XDirection { get; set; }

        /// <summary>
        /// Gets or sets the horizontal position.
        /// </summary>
        public int XPosition { get; set; }

        /// <summary>
        /// Gets or sets the position at which an animal will change direction.
        /// </summary>
        public int XPositionMax { get; set; }

        /// <summary>
        /// Gets or sets the vertical direction.
        /// </summary>
        public VerticalDirection YDirection { get; set; }

        /// <summary>
        /// Gets or sets the vertical position.
        /// </summary>
        public int YPosition { get; set; }

        /// <summary>
        /// Gets or sets the position at which an animal will change direction.
        /// </summary>
        public int YPositionMax { get; set; }

        /// <summary>
        /// Gets the proportion at which to display the animal.
        /// </summary>
        public virtual double DisplaySize
        {
            get
            {
                return this.Age == 0 ? 0.4 : 1;
            }
        }

        /// <summary>
        /// Gets or sets the animal's hunger state.
        /// </summary>
        public HungerState HungerState { get; set; }

        /// <summary>
        /// Gets or sets the on image.
        /// </summary>
        public Action<ICageable> OnImageUpdate { get; set; }

        /// <summary>
        /// Gets the resource key of the animal.
        /// </summary>
        public string ResourceKey
        {
            get
            {
                return this.GetType().Name + (this.Age == 0 ? "Baby" : "Adult");
            }
        }

        /// <summary>
        /// Converts an animal type enumeration value to a .NET type.
        /// </summary>
        /// <param name="animalType">The animal type to convert.</param>
        /// <returns>The associated .NET type.</returns>
        public static Type ConvertAnimalTypeToType(AnimalType animalType)
        {
            Type result = null;

            switch (animalType)
            {
                case AnimalType.Chimpanzee:
                    result = typeof(Chimpanzee);
                    break;
                case AnimalType.Dingo:
                    result = typeof(Dingo);
                    break;
                case AnimalType.Eagle:
                    result = typeof(Eagle);
                    break;
                case AnimalType.Hummingbird:
                    result = typeof(Hummingbird);
                    break;
                case AnimalType.Kangaroo:
                    result = typeof(Kangaroo);
                    break;
                case AnimalType.Ostrich:
                    result = typeof(Ostrich);
                    break;
                case AnimalType.Platypus:
                    result = typeof(Platypus);
                    break;
                case AnimalType.Shark:
                    result = typeof(Shark);
                    break;
                case AnimalType.Squirrel:
                    result = typeof(Squirrel);
                    break;
            }

            return result;
        }

        /// <summary>
        /// Adds a child to the animal's family tree.
        /// </summary>
        /// <param name="animal">The child to be added to the family.</param>
        public void AddChild(Animal animal)
        {
            if (animal != null)
            {
                this.children.Add(animal);
            }
        }

        /// <summary>
        /// Eats the specified food.
        /// </summary>
        /// <param name="food">The food to eat.</param>
        public virtual void Eat(Food food)
        {
            this.EatBehavior.Eat(this, food);
            this.HungerState = HungerState.Satisfied;
            this.hungerTimer.Start();
        }

        /// <summary>
        /// Makes the animal pregnant.
        /// </summary>
        public void MakePregnant()
        {
            this.isPregnant = true;
            if (this.OnPregnant != null)
            {
                this.OnPregnant(this);
            }

            if (this.OnTextChange != null)
            {
                this.OnTextChange(this);
            }
        }

        /// <summary>
        /// Moves the animal.
        /// </summary>
        public void Move()
        {
            this.MoveBehavior.Move(this);

            if (this.OnImageUpdate != null)
            {
                this.OnImageUpdate(this);
            }
        }

        /// <summary>
        /// Gives birth to a baby animal and feeds it.
        /// </summary>
        /// <returns>The baby animal.</returns>
        public IReproducer Reproduce()
        {
            // Make mother animal to be no longer pregnant.
            this.isPregnant = false;

            IReproducer baby = this.ReproduceBehavior.Reproduce(this);

            if (baby is Animal)
            {
                this.AddChild(baby as Animal);
            }

            return baby;
        }

        /// <summary>
        /// Generates a string representation of the animal.
        /// </summary>
        /// <returns>A string representation of the animal.</returns>
        public override string ToString()
        {
            return this.Name + ": " + this.GetType().Name + " (" + this.Age + ", " + this.Weight + ") " + (this.IsPregnant ? "***" : string.Empty);
        }

        /// <summary>
        /// Handles the hunger state change.
        /// </summary>
        /// <param name="sender">The sender of the object.</param>
        /// <param name="e">The elapsed event argument.</param>
        private void HandleHungerStateChange(object sender, ElapsedEventArgs e)
        {
            switch (this.HungerState)
            {
                case HungerState.Satisfied:
                    this.HungerState = HungerState.Hungry;

                    break;
                case HungerState.Hungry:
                    this.HungerState = HungerState.Starving;

                    break;
                case HungerState.Starving:
                    this.HungerState = HungerState.Unconscious;

                    this.hungerTimer.Stop();

                    if (this.OnHunger != null)
                    {
                        this.OnHunger(this);
                    }

                    break;
            }
        }

        /// <summary>
        /// Creates a timer.
        /// </summary>
        private void CreateTimers()
        {
            this.moveTimer = new Timer(1000);
            this.moveTimer.Elapsed += this.MoveHandler;
            this.moveTimer.Start();

            this.hungerTimer = new Timer(random.Next(10, 21) * 1000);
            this.hungerTimer.Elapsed += this.HandleHungerStateChange;
            this.hungerTimer.Start();
        }

        /// <summary>
        /// Does something on activation.
        /// </summary>
        /// <param name="context">The context to perform.</param>
        [OnDeserialized]
        private void OnDeserialized(StreamingContext context)
        {
            this.CreateTimers();
        }

        /// <summary>
        /// Handles the event of the move timer going off.
        /// </summary>
        /// <param name="sender">The object that initiated the event.</param>
        /// <param name="e">The arguments of the event.</param>
        private void MoveHandler(object sender, ElapsedEventArgs e)
        {
            ////#if DEBUG
            ////            this.moveTimer.Enabled = false;
            ////#endif
            this.Move();
            ////#if DEBUG
            ////            this.moveTimer.Enabled = true;
            ////#endif
        }
    }
}
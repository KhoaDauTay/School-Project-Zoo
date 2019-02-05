using System;
using System.Runtime.Serialization;
using System.Text.RegularExpressions;
using System.Timers;
using Animals;
using BoothItems;
using CagedItems;
using Foods;
using MoneyCollectors;
using Reproducers;
using Utilities;
using VendingMachines;
using Wallets;

namespace People
{
    /// <summary>
    /// The class which is used to represent a guest.
    /// </summary>
    [Serializable]
    public class Guest : IEater, ICageable
    {
        /// <summary>
        /// The guest's adopted animal.
        /// </summary>
        private Animal adoptedAnimal;

        /// <summary>
        /// The age of the guest.
        /// </summary>
        private int age;

        /// <summary>
        /// The is active setting of the guest.
        /// </summary>
        private bool isActive;

        /// <summary>
        /// On text change.
        /// </summary>
        [NonSerialized]
        private Action<Guest> onTextChange;

        /// <summary>
        /// The checking account for collecting money.
        /// </summary>
        private IMoneyCollector checkingAccount;

        /// <summary>
        /// The feed timer of the guest.
        /// </summary>
        private Timer feedTimer;

        /// <summary>
        /// The gender of the guest.
        /// </summary>
        private Gender gender;

        /// <summary>
        /// The name of the guest.
        /// </summary>
        private string name;

        /// <summary>
        /// The guest's wallet.
        /// </summary>
        private Wallet wallet;

        /// <summary>
        /// Initializes a new instance of the Guest class.
        /// </summary>
        /// <param name="name">The name of the guest.</param>
        /// <param name="age">The age of the guest.</param>
        /// <param name="walletColor">The color of the guest's wallet.</param>
        /// <param name="moneyBalance">The money balance of the guest's wallet.</param>
        /// <param name="gender">The gender of the guest.</param>
        /// <param name="checkingAccount">The account for collecting money.</param>
        public Guest(string name, int age, WalletColor walletColor, decimal moneyBalance, Gender gender, IMoneyCollector checkingAccount)
        {
            this.age = age;
            this.checkingAccount = checkingAccount;
            this.checkingAccount.OnBalanceChange += this.HandleBalanceChange;
            this.gender = gender;
            this.name = name;
            this.CreateTimers();
            this.wallet = new Wallet(walletColor, new MoneyPocket());
            this.wallet.AddMoney(moneyBalance);
            this.wallet.OnBalanceChange += this.HandleBalanceChange;
            this.XPosition = 0;
            this.YPosition = 0;
        }

        /// <summary>
        /// Gets or sets the age of the guest.
        /// </summary>
        public int Age
        {
            get
            {
                return this.age;
            }

            set
            {
                if (value < 0 || value > 120)
                {
                    throw new ArgumentOutOfRangeException("age", "Age must be between 0 and 120.");
                }

                this.age = value;

                if (this.OnTextChange != null)
                {
                    this.OnTextChange(this);
                }
            }
        }

        /// <summary>
        /// Gets or sets the guest's adopted animal.
        /// </summary>
        public Animal AdoptedAnimal
        {
            get
            {
                return this.adoptedAnimal;
            }

            set
            {
                if (this.adoptedAnimal != null)
                {
                    this.adoptedAnimal.OnHunger = null;
                }

                this.adoptedAnimal = value;

                if (this.adoptedAnimal != null)
                {
                    this.adoptedAnimal.OnHunger += this.HandleAnimalHungry;
                }

                if (this.OnTextChange != null)
                {
                    this.OnTextChange(this);
                }
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the guest is active.
        /// </summary>
        public bool IsActive
        {
            get
            {
                if (this.isActive == true && this.adoptedAnimal == null)
                {
                    return this.isActive = false;
                }
                else
                {
                    return this.isActive = true;
                }
            }

            set
            {
                this.isActive = value;
            }
        }

        /// <summary>
        /// Gets the proportion at which to display the guest.
        /// </summary>
        public double DisplaySize
        {
            get
            {
                return 0.6;
            }
        }

        /// <summary>
        /// Gets the guest's checking account.
        /// </summary>
        public IMoneyCollector CheckingAccount
        {
            get
            {
                return this.checkingAccount;
            }
        }

        /// <summary>
        /// Gets or sets the function of the vending machine.
        /// </summary>
        public Func<VendingMachine> GetVendingMachine { get; set; }

        /// <summary>
        /// Gets the guest's hunger state.
        /// </summary>
        public HungerState HungerState { get; }

        /// <summary>
        /// Gets or sets the on image update.
        /// </summary>
        public Action<ICageable> OnImageUpdate { get; set; }

        /// <summary>
        /// Gets or sets the on text change.
        /// </summary>
        public Action<Guest> OnTextChange
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
        /// Gets or sets the guest's gender.
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
        /// Gets or sets the name of the guest.
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
        /// Gets the resource key of the guest.
        /// </summary>
        public string ResourceKey
        {
            get
            {
                return "Guest";
            }
        }

        /// <summary>
        /// Gets the guest's wallet.
        /// </summary>
        public Wallet Wallet
        {
            get
            {
                return this.wallet;
            }
        }

        /// <summary>
        /// Gets or sets the weight of the guest.
        /// </summary>
        public double Weight { get; set; }

        /// <summary>
        /// Gets or sets the horizontal position of the guest.
        /// </summary>
        public int XPosition { get; set; }

        /// <summary>
        /// Gets or sets the vertical position of the guest.
        /// </summary>
        public int YPosition { get; set; }

        /// <summary>
        /// Gets or sets the horizontal direction of the guest.
        /// </summary>
        public HorizontalDirection XDirection { get; set; }

        /// <summary>
        /// Gets or sets the vertical direction of the guest.
        /// </summary>
        public VerticalDirection YDirection { get; set; }

        /// <summary>
        /// Handles the animal when it is hungry.
        /// </summary>
        /// <param name="animal">The adopted animal to the guest.</param>
        public void HandleAnimalHungry(Animal animal)
        {
            this.feedTimer.Start();
        }

        /// <summary>
        /// Handles the ready to feed.
        /// </summary>
        /// <param name="sender">The sender of the object.</param>
        /// <param name="e">The elapsed event argument.</param>
        public void HandleReadyToFeed(object sender, ElapsedEventArgs e)
        {
            if (this.AdoptedAnimal != null)
            {
                this.FeedAnimal(this.AdoptedAnimal);
            }

            this.feedTimer.Stop();
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
        /// Feeds the specified eater.
        /// </summary>
        /// <param name="eater">The eater to be fed.</param>
        public void FeedAnimal(IEater eater)
        {
            VendingMachine animalSnackMachine = this.GetVendingMachine();

            // Find food price.
            decimal price = animalSnackMachine.DetermineFoodPrice(eater.Weight);

            // Check if guest has enough money on hand and withdraw from account if necessary.
            if (this.wallet.MoneyBalance < price)
            {
                this.WithdrawMoney(price * 10);
            }

            // Get money from wallet.
            decimal payment = this.wallet.RemoveMoney(price);

            // Buy food.
            Food food = animalSnackMachine.BuyFood(payment);

            // Feed animal.
            eater.Eat(food);
        }

        /// <summary>
        /// Custom string representation of the guest.
        /// </summary>
        /// <returns>A string value representing the guest.</returns>
        public override string ToString()
        {
            string result = string.Format("{0}: {1} [${2} / ${3}]", this.Name, this.Age, this.Wallet.MoneyBalance, this.CheckingAccount.MoneyBalance);

            if (this.AdoptedAnimal != null)
            {
                result += ", " + this.AdoptedAnimal.Name;
            }

            return result;
        }

        /// <summary>
        /// Visits a booth to purchase a ticket and other booth items.
        /// </summary>
        /// <param name="ticketBooth">The booth to visit.</param>
        /// <returns>A purchased ticket.</returns>
        public Ticket VisitTicketBooth(MoneyCollectingBooth ticketBooth)
        {
            Ticket result = null;

            if (this.wallet.MoneyBalance < ticketBooth.TicketPrice)
            {
                this.WithdrawMoney(ticketBooth.TicketPrice * 2);
            }

            decimal ticketPayment = this.wallet.RemoveMoney(ticketBooth.TicketPrice);

            result = ticketBooth.SellTicket(ticketPayment);

            if (this.wallet.MoneyBalance < ticketBooth.WaterBottlePrice)
            {
                this.WithdrawMoney(ticketBooth.WaterBottlePrice * 2);
            }

            decimal waterPayment = this.wallet.RemoveMoney(ticketBooth.WaterBottlePrice);

            WaterBottle bottle = ticketBooth.SellWaterBottle(waterPayment);

            return result;
        }

        /// <summary>
        /// Visits a booth to acquire information.
        /// </summary>
        /// <param name="informationBooth">The booth to visit.</param>
        public void VisitInformationBooth(GivingBooth informationBooth)
        {
            Map map = informationBooth.GiveFreeMap();

            CouponBook couponBook = informationBooth.GiveFreeCouponBook();
        }

        /// <summary>
        /// Handles the balance change.
        /// </summary>
        private void HandleBalanceChange()
        {
            if (this.OnTextChange != null)
            {
                this.OnTextChange(this);
            }
        }

        /// <summary>
        /// Creates the guest's timers.
        /// </summary>
        private void CreateTimers()
        {
            this.feedTimer = new Timer(1000);
            this.feedTimer.Elapsed += this.HandleReadyToFeed;
            this.feedTimer.Start();
        }

        /// <summary>
        /// Does an action once activated.
        /// </summary>
        /// <param name="context">The context of the streaming method.</param>
        private void OnDeserialized(StreamingContext context)
        {
            this.CreateTimers();
        }

        /// <summary>
        /// Withdraws money from the checking account and puts it into the wallet.
        /// </summary>
        /// <param name="amount">The amount of money to withdraw.</param>
        private void WithdrawMoney(decimal amount)
        {
            decimal retrievedAmount = this.checkingAccount.RemoveMoney(amount);

            this.wallet.AddMoney(retrievedAmount);
        }
    }
}
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using Animals;
using BirthingRooms;
using BoothItems;
using Microsoft.Win32;
using MoneyCollectors;
using People;
using Reproducers;
using Wallets;
using Zoos;

namespace ZooScenario
{
    /// <summary>
    /// Contains interaction logic for MainWindow.xaml.
    /// </summary>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.StyleCop.CSharp.NamingRules", "SA1300:ElementMustBeginWithUpperCaseLetter", Justification = "Event handlers may begin with lower-case letters.")]
    public partial class MainWindow : Window
    {
        /// <summary>
        /// A constant variable of a file name.
        /// </summary>
        private const string autoSaveFileName = "Autosave.zoo";

        /// <summary>
        /// Minnesota's Como Zoo.
        /// </summary>
        private Zoo comoZoo;

        /// <summary>
        /// Initializes a new instance of the MainWindow class.
        /// </summary>
        public MainWindow()
        {
            this.InitializeComponent();

#if DEBUG
            this.Title += " [DEBUG]";
#endif
        }

        /// <summary>
        /// Adds an new animal to the zoo.
        /// </summary>
        /// <param name="sender">The object that initiated the event.</param>
        /// <param name="e">The event arguments of the event.</param>
        private void addAnimalButton_Click(object sender, RoutedEventArgs e)
        {
            AnimalType animalType = (AnimalType)this.animalTypeComboBox.SelectedItem;

            Animal animal = AnimalFactory.CreateAnimal(animalType, "Animal", 0, 0.0, Gender.Female);

            AnimalWindow window = new AnimalWindow(animal);

            window.ShowDialog();

            if (window.DialogResult == true)
            {
                this.comoZoo.AddAnimal(animal);
            }
        }

        /// <summary>
        /// Has the specified guest adopt the specified animal.
        /// </summary>
        /// <param name="sender">The object that initiated the event.</param>
        /// <param name="e">The arguments of the event.</param>
        private void adoptAnimalButton_Click(object sender, RoutedEventArgs e)
        {
            Animal animal = this.animalListBox.SelectedItem as Animal;

            Guest guest = this.comoZoo.FindGuest(g => g.AdoptedAnimal == null);

            if (guest.AdoptedAnimal == null)
            {
                if (animal != null && guest != null)
                {
                    guest.AdoptedAnimal = animal;

                    Cage cage = this.comoZoo.FindCage(animal.GetType());

                    cage.Add(guest);
                }
                else
                {
                    MessageBox.Show("Select a guest and an animal for that guest to adopt.");
                }
            }
            else
            {
                MessageBox.Show("A guest is only allowed to adopt one animal, you can unadopt and re-adopt a different animal.");
            }
        }

        /// <summary>
        /// Adds a new guest to the zoo.
        /// </summary>
        /// <param name="sender">The object that initiated the event.</param>
        /// <param name="e">The event arguments of the event.</param>
        private void addGuestButton_Click(object sender, RoutedEventArgs e)
        {
            Guest guest = new Guest("Guest", 0, WalletColor.Black, 0m, Gender.Female, new Account());

            GuestWindow window = new GuestWindow(guest);

            window.ShowDialog();

            if (window.DialogResult == true)
            {
                try
                {
                    Ticket ticket = this.comoZoo.SellTicket(guest);

                    this.comoZoo.AddGuest(guest, ticket);
                }
                catch (NullReferenceException ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        /// <summary>
        /// Edits the animal on the double-click event.
        /// </summary>
        /// <param name="sender">The object that initiated the event.</param>
        /// <param name="e">The arguments of the event.</param>
        private void animalListBox_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            Animal animal = this.animalListBox.SelectedItem as Animal;

            if (animal != null)
            {
                AnimalWindow window = new AnimalWindow(animal);

                window.ShowDialog();
            }
        }

        /// <summary>
        /// Attaches the delegates.
        /// </summary>
        private void AttachDelegates()
        {
            this.comoZoo.OnBirthingRoomTemperatureChange = (previousTemp, currentTemp) =>
            {
                this.birthingRoomTemperatureLabel.Content = string.Format("{0:0.0} ° F", this.comoZoo.BirthingRoomTemperature);
                this.birthingRoomTemperatureBorder.Height = this.comoZoo.BirthingRoomTemperature * 2;

                double colorLevel = ((this.comoZoo.BirthingRoomTemperature - BirthingRoom.MinTemperature) * 255) / (BirthingRoom.MaxTemperature - BirthingRoom.MinTemperature);

                this.birthingRoomTemperatureBorder.Background = new SolidColorBrush(Color.FromRgb(
                    Convert.ToByte(colorLevel),
                    Convert.ToByte(255 - colorLevel),
                    Convert.ToByte(255 - colorLevel)));
            };

            this.comoZoo.OnAddGuest = guest =>
            {
                this.guestListBox.Items.Add(guest);

                guest.OnTextChange += this.UpdateGuestDisplay;
            };

            this.comoZoo.OnRemoveGuest += guest => 
            {
                this.guestListBox.Items.Remove(guest);

                guest.OnTextChange -= this.UpdateGuestDisplay;
            };

            this.comoZoo.OnAddAnimal = animal =>
            {
                this.animalListBox.Items.Add(animal);

                animal.OnTextChange += this.UpdateAnimalDisplay;
            };

            this.comoZoo.OnRemoveAnimal = animal =>
            {
                this.animalListBox.Items.Remove(animal);

                animal.OnTextChange -= this.UpdateAnimalDisplay;
            };

            this.comoZoo.OnDeserialized();
        }

        /// <summary>
        /// Births a pregnant animal.
        /// </summary>
        /// <param name="sender">The object that initiated the event.</param>
        /// <param name="e">The arguments of the event.</param>
        private void birthAnimalButton_Click(object sender, RoutedEventArgs e)
        {
            this.comoZoo.BirthAnimal();
        }

        /// <summary>
        /// Decreases the birthing room temperature.
        /// </summary>
        /// <param name="sender">The object that initiated the event.</param>
        /// <param name="e">The event arguments of the event.</param>
        private void decreaseTemperatureButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                this.comoZoo.BirthingRoomTemperature--;
            }
            catch (ArgumentOutOfRangeException ex)
            {
                MessageBox.Show(ex.Message);
            }
            catch (NullReferenceException)
            {
                MessageBox.Show("You must create a zoo before changing the temperature.");
            }
        }

        /// <summary>
        /// Feeds a zoo animal.
        /// </summary>
        /// <param name="sender">The object that initiated the event.</param>
        /// <param name="e">The event arguments of the event.</param>
        private void feedAnimalButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Guest guest = this.guestListBox.SelectedItem as Guest;

                Animal animal = this.animalListBox.SelectedItem as Animal;

                if (guest != null && animal != null)
                {
                    guest.FeedAnimal(animal);
                }
                else
                {
                    MessageBox.Show("You must choose a guest and an animal to feed an animal.");
                }

                this.guestListBox.SelectedItem = guest;
                this.animalListBox.SelectedItem = animal;
            }
            catch
            {
                MessageBox.Show("You must create a zoo before feeding animals.");
            }
        }

        /// <summary>
        /// Edits the guest on the double-click event.
        /// </summary>
        /// <param name="sender">The object that initiated the event.</param>
        /// <param name="e">The arguments of the event.</param>
        private void guestListBox_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            Guest guest = this.guestListBox.SelectedItem as Guest;

            if (guest != null)
            {
                GuestWindow window = new GuestWindow(guest);

                window.ShowDialog();
            }
        }

        /// <summary>
        /// Increases the birthing room temperature.
        /// </summary>
        /// <param name="sender">The object that initiated the event.</param>
        /// <param name="e">The event arguments of the event.</param>
        private void increaseTemperatureButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                this.comoZoo.BirthingRoomTemperature++;
            }
            catch (ArgumentOutOfRangeException ex)
            {
                MessageBox.Show(ex.Message);
            }
            catch (NullReferenceException)
            {
                MessageBox.Show("You must create a zoo before changing the temperature.");
            }
        }

        /// <summary>
        /// Removes the selected animal from the zoo.
        /// </summary>
        /// <param name="sender">The object that initiated the event.</param>
        /// <param name="e">The event arguments of the event.</param>
        private void removeAnimalButton_Click(object sender, RoutedEventArgs e)
        {
            Animal animal = this.animalListBox.SelectedItem as Animal;

            if (animal != null)
            {
                if (MessageBox.Show(string.Format("Are you sure you want to remove animal: {0}?", animal.Name), "Confirmation", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    // Remove the selected animal.
                    this.comoZoo.RemoveAnimal(animal);
                }
            }
            else
            {
                MessageBox.Show("Select an animal to remove.");
            }
        }

        /// <summary>
        /// Updates the guests display window.
        /// </summary>
        /// <param name="guest">The guest to display.</param>
        private void UpdateGuestDisplay(Guest guest)
        {
            this.Dispatcher.Invoke(new Action(delegate()
            {
                int index = this.guestListBox.Items.IndexOf(guest);

                if (index >= 0)
                {
                    // disconnect the guest
                    this.guestListBox.Items.RemoveAt(index);

                    // create new guest item in the same spot
                    this.guestListBox.Items.Insert(index, guest);

                    // re-select the guest
                    this.guestListBox.SelectedItem = this.guestListBox.Items[index];
                }
            }));
        }

        /// <summary>
        /// Updates the animal display.
        /// </summary>
        /// <param name="animal">The animal to be updated.</param>
        private void UpdateAnimalDisplay(Animal animal)
        {
            this.Dispatcher.Invoke(new Action(delegate()
            {
                int index = this.animalListBox.Items.IndexOf(animal);

                if (index >= 0)
                {
                    // disconnect the guest
                    this.animalListBox.Items.RemoveAt(index);

                    // create new guest item in the same spot
                    this.animalListBox.Items.Insert(index, animal);

                    // re-select the guest
                    this.animalListBox.SelectedItem = this.animalListBox.Items[index];
                }
            }));
        }

        /// <summary>
        /// Removes the selected guest from the zoo.
        /// </summary>
        /// <param name="sender">The object that initiated the event.</param>
        /// <param name="e">The event arguments of the event.</param>
        private void removeGuestButton_Click(object sender, RoutedEventArgs e)
        {
            Guest guest = this.guestListBox.SelectedItem as Guest;

            if (guest != null)
            {
                if (MessageBox.Show(string.Format("Are you sure you want to remove guest: {0}?", guest.Name), "Confirmation", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    // Remove the selected animal.
                    this.comoZoo.RemoveGuest(guest);
                }
            }
            else
            {
                MessageBox.Show("Select a guest to remove.");
            }
        }

        /// <summary>
        /// Shows the cage of the specified animal.
        /// </summary>
        /// <param name="sender">The object that initiated the event.</param>
        /// <param name="e">The arguments of the event.</param>
        private void showCageButton_Click(object sender, RoutedEventArgs e)
        {
            Animal animal = this.animalListBox.SelectedItem as Animal;

            if (animal != null)
            {
                Cage cage = this.comoZoo.FindCage(animal.GetType());

                CageWindow window = new CageWindow(cage);

                window.Show();
            }
            else
            {
                MessageBox.Show("Select an animal whose cage to show.");
            }
        }

        /// <summary>
        /// Sorts the animals in the zoo by name.
        /// </summary>
        /// <param name="sender">The object that initiated the event.</param>
        /// <param name="e">The arguments of the event.</param>
        private void sortAnimalsByNameButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                SortType sortType = (SortType)this.sortTypeComboBox.SelectedItem;

                SortResult sortResult = this.comoZoo.SortAnimals(sortType, "animalName");

                this.swapCountLabel.Content = sortResult.SwapCount;
                this.compareCountLabel.Content = sortResult.CompareCount;
                this.millisecondsLabel.Content = sortResult.ElapsedMilliseconds;

                this.ClearWindow();
                this.comoZoo.OnDeserialized();
            }
            catch (InvalidCastException)
            {
                MessageBox.Show("Please choose a sort type before sorting animals.");
            }
        }

        /// <summary>
        /// Sorts the animals in the zoo by weight.
        /// </summary>
        /// <param name="sender">The object that initiated the event.</param>
        /// <param name="e">The arguments of the event.</param>
        private void sortAnimalsByWeightButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                SortType sortType = (SortType)this.sortTypeComboBox.SelectedItem;

                SortResult sortResult = this.comoZoo.SortAnimals(sortType, "weight");

                this.swapCountLabel.Content = sortResult.SwapCount;
                this.compareCountLabel.Content = sortResult.CompareCount;
                this.millisecondsLabel.Content = sortResult.ElapsedMilliseconds;

                this.ClearWindow();
                this.comoZoo.OnDeserialized();
            }
            catch (InvalidCastException)
            {
                MessageBox.Show("Please choose a sort type before sorting animals.");
            }
        }

        /// <summary>
        /// Sorts the animals in the zoo by age.
        /// </summary>
        /// <param name="sender">The object that initiated the event.</param>
        /// <param name="e">The arguments of the event.</param>
        private void sortAnimalsByAge_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                SortType sortType = (SortType)this.sortTypeComboBox.SelectedItem;

                SortResult sortResult = this.comoZoo.SortAnimals(sortType, "age");

                this.swapCountLabel.Content = sortResult.SwapCount;
                this.compareCountLabel.Content = sortResult.CompareCount;
                this.millisecondsLabel.Content = sortResult.ElapsedMilliseconds;

                this.ClearWindow();
                this.comoZoo.OnDeserialized();
            }
            catch (InvalidCastException)
            {
                MessageBox.Show("Please choose a sort type before sorting animals.");
            }
        }

        /// <summary>
        /// Sorts the guests in the zoo by name.
        /// </summary>
        /// <param name="sender">The object that initiated the event.</param>
        /// <param name="e">The arguments of the event.</param>
        private void sortGuestByName_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                SortType sortType = (SortType)this.sortTypeComboBox.SelectedItem;

                SortResult sortResult = this.comoZoo.SortGuests(sortType, "guestName");

                this.swapCountLabel.Content = sortResult.SwapCount;
                this.compareCountLabel.Content = sortResult.CompareCount;
                this.millisecondsLabel.Content = sortResult.ElapsedMilliseconds;

                this.ClearWindow();
                this.comoZoo.OnDeserialized();
            }
            catch (InvalidCastException)
            {
                MessageBox.Show("Please choose a sort type before sorting guests.");
            }
        }

        /// <summary>
        /// Sorts the guests in the zoo by money balance.
        /// </summary>
        /// <param name="sender">The object that initiated the event.</param>
        /// <param name="e">The arguments of the event.</param>
        private void sortGuestByMoneyBalance_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                SortType sortType = (SortType)this.sortTypeComboBox.SelectedItem;

                SortResult sortResult = this.comoZoo.SortGuests(sortType, "moneyBalance");

                this.swapCountLabel.Content = sortResult.SwapCount;
                this.compareCountLabel.Content = sortResult.CompareCount;
                this.millisecondsLabel.Content = sortResult.ElapsedMilliseconds;

                this.ClearWindow();
                this.comoZoo.OnDeserialized();
            }
            catch (InvalidCastException)
            {
                MessageBox.Show("Please choose a sort type before sorting guests.");
            }
        }

        /// <summary>
        /// Has the specified guest un-adopt their adopted animal.
        /// </summary>
        /// <param name="sender">The object that initiated the event.</param>
        /// <param name="e">The arguments of the event.</param>
        private void unadoptAnimalButton_Click(object sender, RoutedEventArgs e)
        {
            Guest guest = this.guestListBox.SelectedItem as Guest;

            if (guest != null && guest.AdoptedAnimal != null)
            {
                Cage cage = this.comoZoo.FindCage(guest.AdoptedAnimal.GetType());

                guest.AdoptedAnimal = null;

                cage.Remove(guest);
            }
            else
            {
                MessageBox.Show("Select a guest to unadopt their animal.");
            }
        }

        /// <summary>
        /// This code runs when the window is loaded.
        /// </summary>
        /// <param name="sender">The object that initiated the event.</param>
        /// <param name="e">The event arguments of the event.</param>
        private void window_Loaded(object sender, RoutedEventArgs e)
        {
            bool zoo = this.LoadZoo(autoSaveFileName);

            if (zoo == false)
            {
                this.comoZoo = Zoo.NewZoo();

                this.AttachDelegates();
            }

            this.animalTypeComboBox.ItemsSource = Enum.GetValues(typeof(AnimalType));

            this.animalTypeComboBox.SelectedItem = AnimalType.Chimpanzee;

            this.moveBehaviorTypeComboBox.ItemsSource = Enum.GetValues(typeof(MoveBehaviorType));

            this.sortTypeComboBox.ItemsSource = Enum.GetValues(typeof(SortType));
        }

        /// <summary>
        /// Changes the move behavior of the specified animal.
        /// </summary>
        /// <param name="sender">The object that initiated the event.</param>
        /// <param name="e">The arguments of the event.</param>
        private void changeMoveBehaviorButton_Click(object sender, RoutedEventArgs e)
        {
            Animal animal = this.animalListBox.SelectedItem as Animal;

            object behaviorType = this.moveBehaviorTypeComboBox.SelectedItem;

            if (animal != null && behaviorType != null)
            {
                animal.MoveBehavior = MoveBehaviorFactory.CreateMoveBehavior((MoveBehaviorType)behaviorType);
            }
            else
            {
                MessageBox.Show("Select an animal and a move behavior type to change its move behavior.");
            }
        }

        /// <summary>
        /// Uses a linear search to find an animal by name.
        /// </summary>
        /// <param name="sender">The object that initiated the event.</param>
        /// <param name="e">The arguments of the event.</param>
        private void linearSearchButton_Click(object sender, RoutedEventArgs e)
        {
            int loopCount = 0;
            string animalName = this.animalNameTextBox.Text;

            if (!string.IsNullOrWhiteSpace(animalName))
            {
                foreach (Animal a in this.comoZoo.Animals)
                {
                    loopCount++;
                    if (a.Name == animalName)
                    {
                        MessageBox.Show(string.Format("{0} found. {1} loops complete.", a.Name, loopCount));
                        break;
                    }
                }
            }
        }

        /// <summary>
        /// Uses a binary search to find an animal by name.
        /// </summary>
        /// <param name="sender">The object that initiated the event.</param>
        /// <param name="e">The arguments of the event.</param>
        private void binarySearchButton_Click(object sender, RoutedEventArgs e)
        {
            int loopCount = 0;

            string animalName = this.animalNameTextBox.Text;
            List<Animal> animals = this.comoZoo.Animals.ToList();

            if (!string.IsNullOrWhiteSpace(animalName))
            {
                int minPosition = 0;
                int maxPosition = animals.Count - 1;

                while (minPosition <= maxPosition)
                {
                    int middlePosition = (minPosition + maxPosition) / 2;

                    loopCount++;
                    int comparer = string.Compare(animalName, animals[middlePosition].Name);

                    if (comparer > 0)
                    {
                        minPosition = middlePosition + 1;
                    }
                    else if (comparer < 0)
                    {
                        maxPosition = middlePosition - 1;
                    }
                    else
                    {
                        MessageBox.Show(string.Format("{0} found. {1} loops complete.", animals[middlePosition].Name, loopCount));
                        break;
                    }
                }
            }
        }

        /// <summary>
        /// Saves the zoo's progress.
        /// </summary>
        /// <param name="fileName">The file name to save.</param>
        private void SaveZoo(string fileName)
        {
            this.comoZoo.SaveToFile(fileName);
            this.SetWindowTitle(fileName);
        }

        /// <summary>
        /// Sets the window title.
        /// </summary>
        /// <param name="fileName">The file name.</param>
        private void SetWindowTitle(string fileName)
        {
            // Set the title of the window using the current file name
            this.Title = string.Format("Object-Oriented Programming 2: Zoo [{0}]", Path.GetFileName(fileName));
        }

        /// <summary>
        /// Saves the file on a button click.
        /// </summary>
        /// <param name="sender">The sender of the object.</param>
        /// <param name="e">The routed event arguments.</param>
        private void saveButton_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog dialog = new SaveFileDialog();

            dialog.Filter = "Zoo save-game files (*.zoo)|*.zoo";

            if (dialog.ShowDialog() == true)
            {
                this.SaveZoo(dialog.FileName);
            }
        }

        /// <summary>
        /// Loads a new instance of the zoo.
        /// </summary>
        /// <param name="fileName">The name of the file to load.</param>
        /// <returns>A true or false value.</returns>
        private bool LoadZoo(string fileName)
        {
            bool result = true;
            try
            {
                this.comoZoo = Zoo.LoadFromFile(fileName);
                this.AttachDelegates();
                this.SetWindowTitle(fileName);
                return result;
            }
            catch
            {
                result = false;
                MessageBox.Show("The file could not be loaded.");
                return result;
            }
        }
        
        /// <summary>
        /// Clears the current zoo window.
        /// </summary>
        private void ClearWindow()
        {
            this.animalListBox.Items.Clear();
            this.guestListBox.Items.Clear();
        }

        /// <summary>
        /// The click handler of the load button.
        /// </summary>
        /// <param name="sender">The sender of the object.</param>
        /// <param name="e">The routed event arguments.</param>
        private void loadButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();

            dialog.Filter = "Zoo save-game files (*.zoo)|*.zoo";

            if (dialog.ShowDialog() == true)
            {
                this.ClearWindow();
                this.LoadZoo(dialog.FileName);
            }
        }

        /// <summary>
        /// Restarts the zoo from the button click.
        /// </summary>
        /// <param name="sender">The sender of the object.</param>
        /// <param name="e">The routed event argument.</param>
        private void restartButton_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to start over?", "Confirmation", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                this.ClearWindow();
                this.comoZoo = Zoo.NewZoo();
                this.AttachDelegates();
            }
        }

        /// <summary>
        /// Saves when the window is closed.
        /// </summary>
        /// <param name="sender">The sender of the object.</param>
        /// <param name="e">The cancel event argument.</param>
        private void window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            this.SaveZoo(autoSaveFileName);
        }

        /// <summary>
        /// Launches the query window.
        /// </summary>
        /// <param name="sender">The sender of the object.</param>
        /// <param name="e">The routed event argument.</param>
        private void launchQueryWindowButton_Click(object sender, RoutedEventArgs e)
        {
            QueryWindow queryWindow = new QueryWindow(this.comoZoo);

            queryWindow.Show();
        }
    }
}
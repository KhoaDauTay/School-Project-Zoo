using System.Collections.Generic;
using System.Linq;
using System.Windows;
using Animals;
using People;
using Zoos;

namespace ZooScenario
{
    /// <summary>
    /// Interaction logic for QueryWindow.xaml.
    /// </summary>
    public partial class QueryWindow : Window
    {
        /// <summary>
        /// The zoo copy.
        /// </summary>
        private Zoo zoo;

        /// <summary>
        /// Initializes a new instance of the QueryWindow class.
        /// </summary>
        /// <param name="zoo">The copy of the zoo.</param>
        public QueryWindow(Zoo zoo)
        {
            this.zoo = zoo;
            this.InitializeComponent();
        }

        /// <summary>
        /// The total animal weight button click.
        /// </summary>
        /// <param name="sender">The sender of the object.</param>
        /// <param name="e">The routed event argument.</param>
        private void totalAnimalWeightButton_Click(object sender, RoutedEventArgs e)
        {
            double totalWeight = this.zoo.Animals.ToList().Sum(a => a.Weight);
            this.resultTextBox.Text = totalWeight.ToString();
        }

        /// <summary>
        /// The average animal weight button click.
        /// </summary>
        /// <param name="sender">The sender of the object.</param>
        /// <param name="e">The routed event argument.</param>
        private void averageAnimalWeightButton_Click(object sender, RoutedEventArgs e)
        {
            double averageWeight = this.zoo.Animals.ToList().Average(a => a.Weight);
            this.resultTextBox.Text = averageWeight.ToString();
        }

        /// <summary>
        /// The animal count button click.
        /// </summary>
        /// <param name="sender">The sender of the object.</param>
        /// <param name="e">The routed event argument.</param>
        private void animalCountButton_Click(object sender, RoutedEventArgs e)
        {
            int numberOfAnimals = this.zoo.Animals.Count();
            this.resultTextBox.Text = numberOfAnimals.ToString();
        }

        /// <summary>
        /// The first heavy animal button click.
        /// </summary>
        /// <param name="sender">The sender of the object.</param>
        /// <param name="e">The routed event argument.</param>
        private void firstHeavyAnimalButton_Click(object sender, RoutedEventArgs e)
        {
            this.resultDataGrid.ItemsSource = this.zoo.GetHeavyAnimals();
        }

        /// <summary>
        /// The first youngest guest button click.
        /// </summary>
        /// <param name="sender">The sender of the object.</param>
        /// <param name="e">The routed event argument.</param>
        private void firstYoungGuestButton_Click(object sender, RoutedEventArgs e)
        {
            this.resultDataGrid.ItemsSource = this.zoo.GetYoungGuests();
        }

        /// <summary>
        /// The first female dingo button click.
        /// </summary>
        /// <param name="sender">The sender of the object.</param>
        /// <param name="e">The routed event argument.</param>
        private void firstFemaleDingoButton_Click(object sender, RoutedEventArgs e)
        {
            this.resultDataGrid.ItemsSource = this.zoo.GetFemaleDingoes();
        }

        /// <summary>
        /// The adopted animals.
        /// </summary>
        /// <param name="sender">The sender of the object.</param>
        /// <param name="e">The routed event argument.</param>
        private void GetAdoptedAnimalsButton_Click(object sender, RoutedEventArgs e)
        {
            this.resultDataGrid.ItemsSource = this.zoo.GetAdoptedAnimals();
        }

        /// <summary>
        /// The flying animals.
        /// </summary>
        /// <param name="sender">The sender of the object.</param>
        /// <param name="e">The routed event argument.</param>
        private void GetFlyingAnimalsButton_Click(object sender, RoutedEventArgs e)
        {
            this.resultDataGrid.ItemsSource = this.zoo.GetFlyingAnimals();
        }

        /// <summary>
        /// The guests by age.
        /// </summary>
        /// <param name="sender">The sender of the object.</param>
        /// <param name="e">The routed event argument.</param>
        private void GetGuestsByAgeButton_Click(object sender, RoutedEventArgs e)
        {
            this.resultDataGrid.ItemsSource = this.zoo.GetGuestsByAge();
        }

        /// <summary>
        /// The total balance by wallet color.
        /// </summary>
        /// <param name="sender">The sender of the object.</param>
        /// <param name="e">The routed event argument.</param>
        private void GetTotalBalanceByWalletColor_Click(object sender, RoutedEventArgs e)
        {
            this.resultDataGrid.ItemsSource = this.zoo.GetTotalBalanceByWalletColor();
        }

        /// <summary>
        /// The average animal weight by animal type.
        /// </summary>
        /// <param name="sender">The sender of the object.</param>
        /// <param name="e">The routed event argument.</param>
        private void GetAverageWeightByAnimalType_Click(object sender, RoutedEventArgs e)
        {
            this.resultDataGrid.ItemsSource = this.zoo.GetAverageWeightByAnimalType();
        }
    }
}

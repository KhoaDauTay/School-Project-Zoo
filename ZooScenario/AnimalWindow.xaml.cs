using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using Animals;
using Reproducers;

namespace ZooScenario
{
    /// <summary>
    /// Interaction logic for AnimalWindow.xaml.
    /// </summary>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.StyleCop.CSharp.NamingRules", "SA1300:ElementMustBeginWithUpperCaseLetter", Justification = "Event handlers may begin with lower-case letters.")]
    public partial class AnimalWindow : Window
    {
        /// <summary>
        /// The animal with which to interact.
        /// </summary>
        private Animal animal;

        /// <summary>
        /// Initializes a new instance of the AnimalWindow class.
        /// </summary>
        /// <param name="animal">The animal with which to interact.</param>
        public AnimalWindow(Animal animal)
        {
            this.animal = animal;
            this.InitializeComponent();
        }

        /// <summary>
        /// Sets the animal's age.
        /// </summary>
        /// <param name="sender">The object that initiated the event.</param>
        /// <param name="e">The arguments of the event.</param>
        private void ageTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            try
            {
                this.animal.Age = int.Parse(this.ageTextBox.Text);
            }
            catch (ArgumentOutOfRangeException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// Sets the animal's gender.
        /// </summary>
        /// <param name="sender">The object that initiated the event.</param>
        /// <param name="e">The arguments of the event.</param>
        private void genderComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            this.animal.Gender = (Gender)this.genderComboBox.SelectedItem;
        }

        /// <summary>
        /// Sets the animal's pregnancy status.
        /// </summary>
        /// <param name="sender">The object that initiated the event.</param>
        /// <param name="e">The arguments of the event.</param>
        private void makePregnantButton_Click(object sender, RoutedEventArgs e)
        {
            this.animal.MakePregnant();
            this.makePregnantButton.IsEnabled = false;
            this.pregnancyStatusLabel.Content = "Yes";
        }

        /// <summary>
        /// Sets the animal's name.
        /// </summary>
        /// <param name="sender">The object that initiated the event.</param>
        /// <param name="e">The arguments of the event.</param>
        private void nameTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            try
            {
                this.animal.Name = this.nameTextBox.Text;
            }
            catch (ArgumentException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// Sets the dialog result to true.
        /// </summary>
        /// <param name="sender">The object that initiated the event.</param>
        /// <param name="e">The arguments of the event.</param>
        private void okButton_Click(object sender, RoutedEventArgs e)
        {
                this.DialogResult = true;
        }

        /// <summary>
        /// Sets the animal's weight.
        /// </summary>
        /// <param name="sender">The object that initiated the event.</param>
        /// <param name="e">The arguments of the event.</param>
        private void weightTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            try
            {
                this.animal.Weight = double.Parse(this.weightTextBox.Text);
            }
            catch (ArgumentOutOfRangeException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// Initializes the windows controls.
        /// </summary>
        /// <param name="sender">The object that initiated the event.</param>
        /// <param name="e">The arguments of the event.</param>
        private void window_Loaded(object sender, RoutedEventArgs e)
        {
            this.genderComboBox.ItemsSource = Enum.GetValues(typeof(Gender));
            this.nameTextBox.Text = this.animal.Name;
            this.genderComboBox.SelectedItem = this.animal.Gender;
            this.ageTextBox.Text = this.animal.Age.ToString();
            this.weightTextBox.Text = this.animal.Weight.ToString();
            this.pregnancyStatusLabel.Content = this.animal.IsPregnant ? "Yes" : "No";
        }
    }
}
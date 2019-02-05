using System;
using System.Windows;
using System.Windows.Controls;
using People;
using Reproducers;
using Wallets;

namespace ZooScenario
{
    /// <summary>
    /// Interaction logic for GuestWindow.xaml.
    /// </summary>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.StyleCop.CSharp.NamingRules", "SA1300:ElementMustBeginWithUpperCaseLetter", Justification = "Event handlers may begin with lower-case letters.")]
    public partial class GuestWindow : Window
    {
        /// <summary>
        /// The guest with which to interact.
        /// </summary>
        private Guest guest;

        /// <summary>
        /// Initializes a new instance of the GuestWindow class.
        /// </summary>
        /// <param name="guest">The guest with which to interact.</param>
        public GuestWindow(Guest guest)
        {
            this.guest = guest;
            this.InitializeComponent();
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
        /// Initializes the window's controls.
        /// </summary>
        /// <param name="sender">The object that initiated the event.</param>
        /// <param name="e">The arguments of the event.</param>
        private void window_Loaded(object sender, RoutedEventArgs e)
        {
            this.genderComboBox.ItemsSource = Enum.GetValues(typeof(Gender));
            this.walletColorComboBox.ItemsSource = Enum.GetValues(typeof(WalletColor));
            this.nameTextBox.Text = this.guest.Name;
            this.genderComboBox.SelectedItem = this.guest.Gender;
            this.ageTextBox.Text = this.guest.Age.ToString();
            this.walletColorComboBox.SelectedItem = this.guest.Wallet.Color;
            this.walletMoneyBalanceLabel.Content = this.guest.Wallet.MoneyBalance.ToString("C");
            this.walletMoneyAmountComboBox.Items.Add(1);
            this.walletMoneyAmountComboBox.Items.Add(5);
            this.walletMoneyAmountComboBox.Items.Add(10);
            this.walletMoneyAmountComboBox.Items.Add(20);
            this.walletMoneyAmountComboBox.SelectedItem = this.walletMoneyAmountComboBox.Items[0];
            this.accountMoneyBalanceLabel.Content = this.guest.CheckingAccount.MoneyBalance.ToString("C");
            this.accountMoneyAmountComboBox.Items.Add(1);
            this.accountMoneyAmountComboBox.Items.Add(5);
            this.accountMoneyAmountComboBox.Items.Add(10);
            this.accountMoneyAmountComboBox.Items.Add(20);
            this.accountMoneyAmountComboBox.Items.Add(50);
            this.accountMoneyAmountComboBox.Items.Add(100);
            this.accountMoneyAmountComboBox.SelectedItem = this.accountMoneyAmountComboBox.Items[0];
        }

        /// <summary>
        /// Sets the guest's name.
        /// </summary>
        /// <param name="sender">The object that initiated the event.</param>
        /// <param name="e">The arguments of the event.</param>
        private void nameTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            try
            {
                this.guest.Name = this.nameTextBox.Text;
            }
            catch (ArgumentException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// Sets the guest's age.
        /// </summary>
        /// <param name="sender">The object that initiated the event.</param>
        /// <param name="e">The arguments of the event.</param>
        private void ageTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            try
            {
                this.guest.Age = int.Parse(this.ageTextBox.Text);
            }
            catch (ArgumentOutOfRangeException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// Adds money to the guest's checking account.
        /// </summary>
        /// <param name="sender">The object that initiated the event.</param>
        /// <param name="e">The arguments of the event.</param>
        private void accountAddMoneyButton_Click(object sender, RoutedEventArgs e)
        {
            this.guest.CheckingAccount.AddMoney(decimal.Parse(this.accountMoneyAmountComboBox.Text));
            this.accountMoneyBalanceLabel.Content = this.guest.CheckingAccount.MoneyBalance.ToString("C");
        }

        /// <summary>
        /// Removes money from the guest's checking account.
        /// </summary>
        /// <param name="sender">The object that initiated the event.</param>
        /// <param name="e">The arguments of the event.</param>
        private void accountSubtractMoneyButton_Click(object sender, RoutedEventArgs e)
        {
            this.guest.CheckingAccount.RemoveMoney(decimal.Parse(this.accountMoneyAmountComboBox.Text));
            this.accountMoneyBalanceLabel.Content = this.guest.CheckingAccount.MoneyBalance.ToString("C");
        }

        /// <summary>
        /// Sets the guest's gender.
        /// </summary>
        /// <param name="sender">The object that initiated the event.</param>
        /// <param name="e">The arguments of the event.</param>
        private void genderComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            this.guest.Gender = (Gender)this.genderComboBox.SelectedItem;
        }

        /// <summary>
        /// Adds money to the guest's checking account.
        /// </summary>
        /// <param name="sender">The object that initiated the event.</param>
        /// <param name="e">The arguments of the event.</param>
        private void walletAddMoneyButton_Click(object sender, RoutedEventArgs e)
        {
            this.guest.Wallet.AddMoney(decimal.Parse(this.walletMoneyAmountComboBox.Text));
            this.walletMoneyBalanceLabel.Content = this.guest.Wallet.MoneyBalance.ToString("C");
        }

        /// <summary>
        /// Sets the guest's wallet color.
        /// </summary>
        /// <param name="sender">The object that initiated the event.</param>
        /// <param name="e">The arguments of the event.</param>
        private void walletColorComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            this.guest.Wallet.Color = (WalletColor)this.walletColorComboBox.SelectedItem;
        }

        /// <summary>
        /// Removes money from the guest's checking account.
        /// </summary>
        /// <param name="sender">The object that initiated the event.</param>
        /// <param name="e">The arguments of the event.</param>
        private void walletSubtractMoneyButton_Click(object sender, RoutedEventArgs e)
        {
            this.guest.Wallet.RemoveMoney(decimal.Parse(this.walletMoneyAmountComboBox.Text));
            this.walletMoneyBalanceLabel.Content = this.guest.Wallet.MoneyBalance.ToString("C");
        }
    }
}
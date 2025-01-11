using LibraryManagementSystem.Helpers;
using LibraryManagementSystem.Models;
using System;
using System.Linq;
using System.Windows;

namespace LibraryManagementSystem
{
    public partial class PatronPage : Window
    {
        public PatronPage()
        {
            InitializeComponent();
            LoadPatrons();
        }

        // Load all patrons into the ListBox
        private void LoadPatrons()
        {
            var patrons = DBHelper.GetPatrons();
            PatronsListBox.ItemsSource = patrons;
        }

        // Add a new patron
        private void AddPatronButton_Click(object sender, RoutedEventArgs e)
        {
            var patron = new Patron
            {
                FullName = FullNameTextBox.Text,
                Email = EmailTextBox.Text,
                PhoneNumber = PhoneNumberTextBox.Text,
                Address = AddressTextBox.Text
            };

            DBHelper.AddPatron(patron);
            LoadPatrons();
            ClearFields();
        }

        // Update an existing patron
        private void UpdatePatronButton_Click(object sender, RoutedEventArgs e)
        {
            var selectedPatron = (Patron)PatronsListBox.SelectedItem;
            if (selectedPatron != null)
            {
                selectedPatron.FullName = FullNameTextBox.Text;
                selectedPatron.Email = EmailTextBox.Text;
                selectedPatron.PhoneNumber = PhoneNumberTextBox.Text;
                selectedPatron.Address = AddressTextBox.Text;

                DBHelper.UpdatePatron(selectedPatron);
                LoadPatrons();
                ClearFields();
            }
            else
            {
                MessageBox.Show("Please select a patron to update.");
            }
        }

        // Delete a selected patron
        private void DeletePatronButton_Click(object sender, RoutedEventArgs e)
        {
            var selectedPatron = (Patron)PatronsListBox.SelectedItem;
            if (selectedPatron != null)
            {
                var confirmResult = MessageBox.Show("Are you sure you want to delete this patron?",
                                                     "Delete Confirmation",
                                                     MessageBoxButton.YesNo,
                                                     MessageBoxImage.Warning);
                if (confirmResult == MessageBoxResult.Yes)
                {
                    DBHelper.DeletePatron(selectedPatron.Id);
                    LoadPatrons();
                }
            }
            else
            {
                MessageBox.Show("Please select a patron to delete.");
            }
        }

        // Clear input fields
        private void ClearFields()  
        {
            FullNameTextBox.Clear();
            EmailTextBox.Clear();
            PhoneNumberTextBox.Clear();
            AddressTextBox.Clear();
        }
    }
}

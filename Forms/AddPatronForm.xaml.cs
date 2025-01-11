using System;
using System.Windows;
using System.Windows.Controls;
using LibraryManagementSystem.Helpers;
using LibraryManagementSystem.Models;

namespace LibraryManagementSystem
{
    public partial class AddPatronForm : Window
    {
        public AddPatronForm()
        {
            InitializeComponent();
        }

        private void AddPatronButton_Click(object sender, RoutedEventArgs e)
        {
            string fullName = FullNameTextBox.Text;
            string membershipId = MembershipIdTextBox.Text;
            string email = EmailTextBox.Text;
            string phoneNumber = PhoneNumberTextBox.Text;
            string address = AddressTextBox.Text;
            DateTime? dateOfBirth = DateOfBirthDatePicker.SelectedDate;
            string membershipType = (MembershipTypeComboBox.SelectedItem as ComboBoxItem)?.Content.ToString();

            if (string.IsNullOrEmpty(fullName) || string.IsNullOrEmpty(membershipId) || string.IsNullOrEmpty(email) ||
                string.IsNullOrEmpty(phoneNumber) || string.IsNullOrEmpty(address) || dateOfBirth == null || string.IsNullOrEmpty(membershipType))
            {
                MessageBox.Show("Please fill in all fields.");
                return;
            }

            // Create a new Patron object
            Patron newPatron = new Patron
            {
                FullName = fullName,
                MembershipId = membershipId,
                Email = email,
                PhoneNumber = phoneNumber,
                Address = address,
                DateOfBirth = dateOfBirth.Value,
                MembershipType = membershipType,
                CreatedAt = DateTime.Now
            };

            // Call DBHelper to insert the patron into the database
            DBHelper.AddPatron(newPatron);
            MessageBox.Show("Patron added successfully!");

            // Optionally, close the window after adding the patron
            this.Close();
        }
    }
}

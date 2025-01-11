using System.Windows;
using LibraryManagementSystem.Helpers;
using System.Collections.Generic;
using LibraryManagementSystem.Models;
using System.Windows.Controls;

namespace LibraryManagementSystem
{
    public partial class MainWindow : Window
    {
        private User AuthenticatedUser;

        // Constructor accepts the User object passed from LoginWindow
        public MainWindow(User user)
        {
            InitializeComponent();
            AuthenticatedUser = user;

            // Load data into grids
            LoadData();

            // Adjust UI based on user role
            if (AuthenticatedUser.Role == "Librarian")
            {
                UsersTab.Visibility = Visibility.Collapsed; // Hide the Users tab for Librarians
                AddUserButton.IsEnabled = false; // Disable Add User button
                EditUserButton.IsEnabled = false; // Disable Edit User button
                DeleteUserButton.IsEnabled = false; // Disable Delete User button
            }
        }

        private void LoadData()
        {
            // Load data for each table and bind it to the respective DataGrid
            BooksDataGrid.ItemsSource = DBHelper.GetBooks();
            PatronsDataGrid.ItemsSource = DBHelper.GetPatrons();
            TransactionsDataGrid.ItemsSource = DBHelper.GetTransactions();
            FinesDataGrid.ItemsSource = DBHelper.GetFines();
            UsersDataGrid.ItemsSource = DBHelper.GetUsers();
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            string selectedTab = ((TabItem)MainTabControl.SelectedItem).Header.ToString();

            switch (selectedTab)
            {
                case "Books":
                    AddBook(); // Implement method to add book
                    break;
                case "Patrons":
                    AddPatron(); // Implement method to add patron
                    break;
                case "Transactions":
                    AddTransaction(); // Implement method to add transaction
                    break;
                case "Fines":
                    AddFine(); // Implement method to add fine
                    break;
                case "Users":
                    AddUser(); // Implement method to add user
                    break;
                default:
                    MessageBox.Show("Unknown tab selected.");
                    break;
            }
        }

        private void AddBook()
        {
            // Example logic to add a book
            // Open a dialog for adding a book and then update the database
            MessageBox.Show("Add functionality for Books.");
        }

        private void AddPatron()
        {
            // Example logic to add a patron
            // Open a dialog for adding a patron and then update the database
            MessageBox.Show("Add functionality for Patrons.");
        }

        private void AddTransaction()
        {
            // Example logic to add a transaction
            // Open a dialog for adding a transaction and then update the database
            MessageBox.Show("Add functionality for Transactions.");
        }

        private void AddFine()
        {
            // Example logic to add a fine
            // Open a dialog for adding a fine and then update the database
            MessageBox.Show("Add functionality for Fines.");
        }

        private void AddUser()
        {
            // Example logic to add a user
            // Open a dialog for adding a user and then update the database
            MessageBox.Show("Add functionality for Users.");
        }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            string selectedTab = ((TabItem)MainTabControl.SelectedItem).Header.ToString();
            MessageBox.Show($"Edit functionality for {selectedTab} not yet implemented.");
            // Implement logic for editing selected records in the currently selected table
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            string selectedTab = ((TabItem)MainTabControl.SelectedItem).Header.ToString();

            // Get the selected item from the current DataGrid
            switch (selectedTab)
            {
                case "Books":
                    Book selectedBook = BooksDataGrid.SelectedItem as Book;
                    if (selectedBook != null)
                    {
                        DBHelper.DeleteBook(selectedBook.Id);
                        LoadData();
                    }
                    break;
                case "Patrons":
                    Patron selectedPatron = PatronsDataGrid.SelectedItem as Patron;
                    if (selectedPatron != null)
                    {
                        DBHelper.DeletePatron(selectedPatron.Id);
                        LoadData();
                    }
                    break;
                case "Transactions":
                    Transaction selectedTransaction = TransactionsDataGrid.SelectedItem as Transaction;
                    if (selectedTransaction != null)
                    {
                        DBHelper.DeleteTransaction(selectedTransaction.Id);
                        LoadData();
                    }
                    break;
                case "Fines":
                    Fine selectedFine = FinesDataGrid.SelectedItem as Fine;
                    if (selectedFine != null)
                    {
                        DBHelper.DeleteFine(selectedFine.Id);
                        LoadData();
                    }
                    break;
                case "Users":
                    User selectedUser = UsersDataGrid.SelectedItem as User;
                    if (selectedUser != null)
                    {
                        DBHelper.DeleteUser(selectedUser.Id);
                        LoadData();
                    }
                    break;
                default:
                    MessageBox.Show("Unknown tab selected.");
                    break;
            }
        }

        private void MainTabControl_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            // Optional: Logic for handling tab change events
        }
    }
}

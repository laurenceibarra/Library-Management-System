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

        // Apply overdue fines at the start of the application
        private void LoadData()
        {
            // Load data for each table and bind it to the respective DataGrid
            BooksDataGrid.ItemsSource = DBHelper.GetBooks();
            PatronsDataGrid.ItemsSource = DBHelper.GetPatrons();
            TransactionsDataGrid.ItemsSource = DBHelper.GetTransactions();
            FinesDataGrid.ItemsSource = DBHelper.GetFines();
            UsersDataGrid.ItemsSource = DBHelper.GetUsers();
            BooksDataGrid.ItemsSource = DBHelper.GetBooks();
            TransactionsDataGrid.ItemsSource = DBHelper.GetTransactions();
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
            LoadData();
        }

        private void AddBook()
        {
            // Open the AddBookForm dialog
            AddBookForm addBookForm = new AddBookForm();
            addBookForm.ShowDialog(); // Show the dialog and wait for user input
        }

        private void AddPatron()
        {
            AddPatronForm addPatronForm = new AddPatronForm();
            addPatronForm.ShowDialog();
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
        private void BorrowButton_Click(object sender, RoutedEventArgs e)
        {
            // Get the selected book from the DataGrid
            Book selectedBook = BooksDataGrid.SelectedItem as Book;
            if (selectedBook == null)
            {
                MessageBox.Show("Please select a book to borrow.");
                return;
            }

            // Check if there are available copies to borrow
            if (selectedBook.Copies <= 0)
            {
                MessageBox.Show("No copies available to borrow.");
                return;
            }

            // Get the selected patron (you could have a separate UI for this, or use a predefined patron)
            Patron selectedPatron = PatronsDataGrid.SelectedItem as Patron;
            if (selectedPatron == null)
            {
                MessageBox.Show("Please select a patron.");
                return;
            }

            // Reduce the number of available copies
            selectedBook.Copies -= 1;

            // Record the checkout date and due date (e.g., due date is 7 days from checkout)
            DateTime checkoutDate = DateTime.Now;
            DateTime dueDate = checkoutDate.AddDays(7);

            // Create a new transaction for the book borrowing
            Transaction newTransaction = new Transaction
            {
                PatronId = selectedPatron.Id,
                BookId = selectedBook.Id,
                CheckoutDate = checkoutDate,
                DueDate = dueDate,
                ReturnDate = null, // Return date is null until the book is returned
                OverdueFine = 0,   // No fine yet
                CreatedAt = DateTime.Now
            };

            // Insert the transaction into the database
            DBHelper.AddTransaction(newTransaction);

            // Update the number of available copies in the database
            DBHelper.UpdateBookCopies(selectedBook.Id, selectedBook.Copies);

            // Optionally, show a success message
            MessageBox.Show($"Book '{selectedBook.Title}' borrowed successfully. Due date: {dueDate.ToShortDateString()}.");

            // Reload data to reflect the updated book copies
            LoadData();
        }
        private void ReturnButton_Click(object sender, RoutedEventArgs e)
        {
            // Get the selected book from the DataGrid
            Book selectedBook = BooksDataGrid.SelectedItem as Book;
            if (selectedBook == null)
            {
                MessageBox.Show("Please select a book to return.");
                return;
            }

            // Get the selected transaction (you might have a separate DataGrid or UI element for this)
            Transaction selectedTransaction = TransactionsDataGrid.SelectedItem as Transaction;
            if (selectedTransaction == null)
            {
                MessageBox.Show("Please select a transaction to return.");
                return;
            }

            // Check if the book was already returned
            if (selectedTransaction.ReturnDate != null)
            {
                MessageBox.Show("This book has already been returned.");
                return;
            }

            // Set the return date (current date)
            DateTime returnDate = DateTime.Now;

            // Check if the return date is overdue and calculate any fine
            double overdueFine = 0;
            if (returnDate > selectedTransaction.DueDate)
            {
                // Calculate fine (example: 1 unit of currency per day overdue)
                overdueFine = (returnDate - selectedTransaction.DueDate).Days;
            }

            // Update the transaction with the return date and fine
            selectedTransaction.ReturnDate = returnDate;
            selectedTransaction.OverdueFine = (decimal)overdueFine;
            DBHelper.UpdateTransaction(selectedTransaction);

            // Increase the number of available copies for the book
            selectedBook.Copies += 1;
            DBHelper.UpdateBookCopies(selectedBook.Id, selectedBook.Copies);

            // Optionally, show a success message
            MessageBox.Show($"Book '{selectedBook.Title}' returned successfully. Overdue fine: {overdueFine}.");

            // Reload data to reflect the updated transaction and book copies
            LoadData();
        }

        private void MainTabControl_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            // Optional: Logic for handling tab change events
        }
    }
}

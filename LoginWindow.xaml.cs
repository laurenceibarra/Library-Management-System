using LibraryManagementSystem.Models;
using LibraryManagementSystem.Helpers;
using System.Security.Cryptography;
using System.Text;
using System.Windows;

namespace LibraryManagementSystem
{
    public partial class LoginWindow : Window
    {
        public LoginWindow()
        {
            InitializeComponent();
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            string username = UsernameTextBox.Text;
            string password = PasswordBox.Password;

            // Hash the entered password to compare it with the stored hashed password
            string hashedPassword = PasswordHelper.HashPassword(password);

            // Validate login and get the user object (you can implement your authentication logic here)
            User authenticatedUser = ValidateLogin(username, hashedPassword);

            if (authenticatedUser != null)
            {
                // If login is successful, pass the authenticated user to the MainWindow
                MainWindow mainWindow = new MainWindow(authenticatedUser); // Passing the user object
                mainWindow.Show();

                // Close the login window
                this.Close();
            }
            else
            {
                MessageBox.Show("Invalid username or password. Please try again.");
            }
        }

        // Simulating a simple login validation (replace with real logic)
        private User ValidateLogin(string username, string hashedPassword)
        {
            // Query the database for the stored hash for the given username
            string storedPasswordHash = DBHelper.GetStoredPasswordHash(username); // Assuming this method exists in DBHelper

            if (storedPasswordHash == null)
            {
                return null; // User does not exist or error occurred
            }

            // Compare the entered hashed password with the stored hash
            if (hashedPassword == storedPasswordHash)
            {
                // Query the role from the database (assuming the role is also stored in the database)
                string role = DBHelper.GetUserRole(username); // This should return the user's role from the DB

                return new User { Username = username, Role = role };
            }

            return null; // Invalid login
        }


        private void RegisterButton_Click(object sender, RoutedEventArgs e)
        {
            // Open the registration window (you could create a RegisterWindow.xaml for this)
            RegisterWindow registerWindow = new RegisterWindow(); // Assume you have a RegisterWindow class
            registerWindow.Show();

            // Optionally, close the current Login window
            this.Close();
        }
    }
}

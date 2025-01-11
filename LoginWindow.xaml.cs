using LibraryManagementSystem.Models;
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

            // Validate login and get the user object (you can implement your authentication logic here)
            User authenticatedUser = ValidateLogin(username, password);

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
        private User ValidateLogin(string username, string password)
        {
            // For demonstration purposes: Replace with your actual authentication logic (e.g., database query)
            if (username == "admin" && password == "password")
            {
                return new User { Username = "admin", Role = "Admin" }; // Example user
            }
            else if (username == "librarian" && password == "librarianpass")
            {
                return new User { Username = "librarian", Role = "Librarian" }; // Example librarian user
            }
            else
            {
                return null; // Invalid login
            }
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

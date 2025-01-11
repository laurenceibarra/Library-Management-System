using System.Windows;
using LibraryManagementSystem.Helpers;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Controls;
using System.Reflection.Metadata;

namespace LibraryManagementSystem
{
    public partial class RegisterWindow : Window
    {
        public RegisterWindow()
        {
            InitializeComponent();
        }

        // Handle Register Button Click
    private void RegisterButton_Click(object sender, RoutedEventArgs e)
        {
            string username = UsernameTextBox.Text;
            string password = PasswordBox.Password;
            string confirmPassword = ConfirmPasswordBox.Password;

            // Check if passwords match
            if (password != confirmPassword)
            {
                MessageBox.Show("Passwords do not match.");
                return;
            }

            // Hash the password before storing it
            string hashedPassword = PasswordHelper.HashPassword(password);


            // Get the selected role from ComboBox
            string role = (RoleComboBox.SelectedItem as ComboBoxItem)?.Content.ToString();

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(role))
            {
                MessageBox.Show("Please fill in all fields.");
                return;
            }

            // Register the user with the hashed password
            bool registrationSuccess = DBHelper.RegisterUser(username, hashedPassword, role);
            if (registrationSuccess)
            {
                MessageBox.Show("Registration Successful!");

                // Optionally, you can automatically login after registration
                LoginWindow loginWindow = new LoginWindow();
                loginWindow.Show();
                this.Close();
            }
            else
            {
                MessageBox.Show("Registration failed. Username may already be taken.");
            }
        }

        // Handle Back to Login Button Click
        private void BackToLoginButton_Click(object sender, RoutedEventArgs e)
        {
            // Close the current registration window and show the login window again
            LoginWindow loginWindow = new LoginWindow();
            loginWindow.Show();
            this.Close();
        }

    }
}

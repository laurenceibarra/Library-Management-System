using System;
using System.Windows;
using LibraryManagementSystem.DataAccess;

namespace LibraryManagementSystem
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        private void AddBookTest_Click(object sender, RoutedEventArgs e)
        {
            string query = "INSERT INTO books (title, author, isbn, genre, publisher, year_published, copies) VALUES ('Test Book', 'Test Author', '1234567890', 'Fiction', 'Test Publisher', 2024, 5)";
            try
            {
                int rowsAffected = DBHelper.ExecuteNonQuery(query);
                MessageBox.Show($"{rowsAffected} row(s) inserted successfully!");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
            }
        }

        private void TestConnection_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                using (var connection = DBHelper.GetConnection())
                {
                    connection.Open();
                    ConnectionStatus.Content = "Connection Successful!";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
            }
        }
    }
}

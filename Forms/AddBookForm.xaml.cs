using System;
using System.Windows;
using LibraryManagementSystem.Helpers;
using LibraryManagementSystem.Models;

namespace LibraryManagementSystem
{
    public partial class AddBookForm : Window
    {
        public AddBookForm()
        {
            InitializeComponent();
        }

        private void AddBookButton_Click(object sender, RoutedEventArgs e)
        {
            string title = TitleTextBox.Text;
            string author = AuthorTextBox.Text;
            string isbn = IsbnTextBox.Text;
            string publisher = PublisherTextBox.Text;
            string yearPublishedText = YearPublishedTextBox.Text;
            string copiesText = CopiesTextBox.Text;
            string genre = GenreTextBox.Text; // Retrieve Genre from the text box

            if (string.IsNullOrEmpty(title) || string.IsNullOrEmpty(author) || string.IsNullOrEmpty(isbn) ||
                string.IsNullOrEmpty(publisher) || string.IsNullOrEmpty(yearPublishedText) || string.IsNullOrEmpty(copiesText) || string.IsNullOrEmpty(genre))
            {
                MessageBox.Show("Please fill in all fields.");
                return;
            }

            // Convert year and copies to appropriate types
            if (!int.TryParse(yearPublishedText, out int yearPublished) || !int.TryParse(copiesText, out int copies))
            {
                MessageBox.Show("Please enter valid numbers for year and copies.");
                return;
            }

            // Create a new Book object
            Book newBook = new Book
            {
                Title = title,
                Author = author,
                ISBN = isbn,
                Publisher = publisher,
                YearPublished = yearPublished,
                Copies = copies,
                Genre = genre, // Assign Genre to the Book object
                CreatedAt = DateTime.Now
            };

            // Call DBHelper to insert the book into the database
            DBHelper.AddBook(newBook);
            MessageBox.Show("Book added successfully!");

            // Optionally, close the window after adding the book
            this.Close();
        }
    }
}


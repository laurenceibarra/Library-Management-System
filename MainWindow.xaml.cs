using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using LibraryManagementSystem.Helpers;
using LibraryManagementSystem.Models;

namespace LibraryManagementSystem
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            LoadBooks();
        }

        // Load books from the database and bind them to the DataGrid
        private void LoadBooks()
        {
            try
            {
                List<Book> books = DBHelper.GetBooks();
                BooksDataGrid.ItemsSource = books;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading books: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        // Add a new book
        private void AddBook_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var book = new Book
                {
                    Title = TitleTextBox.Text,
                    Author = AuthorTextBox.Text,
                    ISBN = IsbnTextBox.Text,
                    Genre = GenreTextBox.Text,
                    Publisher = PublisherTextBox.Text,
                    YearPublished = int.Parse(YearPublishedTextBox.Text),
                    Copies = int.Parse(CopiesTextBox.Text)
                };
                DBHelper.AddBook(book);
                LoadBooks();
                MessageBox.Show("Book added successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error adding book: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        // Update an existing book
        private void UpdateBook_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (BooksDataGrid.SelectedItem is Book selectedBook)
                {
                    selectedBook.Title = TitleTextBox.Text;
                    selectedBook.Author = AuthorTextBox.Text;
                    selectedBook.ISBN = IsbnTextBox.Text;
                    selectedBook.Genre = GenreTextBox.Text;
                    selectedBook.Publisher = PublisherTextBox.Text;
                    selectedBook.YearPublished = int.Parse(YearPublishedTextBox.Text);
                    selectedBook.Copies = int.Parse(CopiesTextBox.Text);
                    DBHelper.UpdateBook(selectedBook);
                    LoadBooks();
                    MessageBox.Show("Book updated successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    MessageBox.Show("Please select a book to update.", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error updating book: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        // Delete a book
        private void DeleteBook_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (BooksDataGrid.SelectedItem is Book selectedBook)
                {
                    DBHelper.DeleteBook(selectedBook.Id);
                    LoadBooks();
                    MessageBox.Show("Book deleted successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    MessageBox.Show("Please select a book to delete.", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error deleting book: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        // Export books to CSV
        private void ExportBooks_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                List<Book> books = DBHelper.GetBooks();
                if (books.Count == 0)
                {
                    MessageBox.Show("No books available to export.", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
                    return;
                }

                // Specify the file path
                string filePath = "BooksExport.csv";

                using (StreamWriter writer = new StreamWriter(filePath))
                {
                    // Write the header
                    writer.WriteLine("Id,Title,Author,ISBN,Genre,Publisher,Year Published,Copies");

                    // Write book details
                    foreach (var book in books)
                    {
                        writer.WriteLine($"{book.Id},{book.Title},{book.Author},{book.ISBN},{book.Genre},{book.Publisher},{book.YearPublished},{book.Copies}");
                    }
                }

                MessageBox.Show($"Books exported successfully to {filePath}!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error exporting books: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}

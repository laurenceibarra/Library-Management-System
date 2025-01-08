using MySql.Data.MySqlClient;
using LibraryManagementSystem.Models;
using System;
using System.Collections.Generic;

namespace LibraryManagementSystem.Helpers
{
    public class DBHelper
    {
        // Database connection string
        private static readonly string connectionString = "Server=127.0.0.1;Port=3306;Database=library_management;Uid=root;Pwd=your_password;";

        // Method to get the database connection
        public static MySqlConnection GetConnection()
        {
            return new MySqlConnection(connectionString);
        }

        #region Books CRUD Operations

        // Add a new book to the database
        public static void AddBook(Book book)
        {
            using (var conn = GetConnection())
            {
                conn.Open();
                string query = "INSERT INTO books (title, author, isbn, genre, publisher, year_published, copies) " +
                               "VALUES (@title, @author, @isbn, @genre, @publisher, @year_published, @copies)";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@title", book.Title);
                cmd.Parameters.AddWithValue("@author", book.Author);
                cmd.Parameters.AddWithValue("@isbn", book.ISBN);
                cmd.Parameters.AddWithValue("@genre", book.Genre);
                cmd.Parameters.AddWithValue("@publisher", book.Publisher);
                cmd.Parameters.AddWithValue("@year_published", book.YearPublished);
                cmd.Parameters.AddWithValue("@copies", book.Copies);
                cmd.ExecuteNonQuery();
            }
        }

        // Get all books from the database
        public static List<Book> GetBooks()
        {
            var books = new List<Book>();
            using (var conn = GetConnection())
            {
                conn.Open();
                string query = "SELECT * FROM books";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                MySqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    books.Add(new Book
                    {
                        Id = reader.GetInt32("id"),
                        Title = reader.GetString("title"),
                        Author = reader.GetString("author"),
                        ISBN = reader.GetString("isbn"),
                        Genre = reader.GetString("genre"),
                        Publisher = reader.GetString("publisher"),
                        YearPublished = reader.GetInt32("year_published"),
                        Copies = reader.GetInt32("copies"),
                        CreatedAt = reader.GetDateTime("created_at")
                    });
                }
            }
            return books;
        }

        // Update book details in the database
        public static void UpdateBook(Book book)
        {
            using (var conn = GetConnection())
            {
                conn.Open();
                string query = "UPDATE books SET title = @title, author = @author, isbn = @isbn, " +
                               "genre = @genre, publisher = @publisher, year_published = @year_published, copies = @copies " +
                               "WHERE id = @id";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@id", book.Id);
                cmd.Parameters.AddWithValue("@title", book.Title);
                cmd.Parameters.AddWithValue("@author", book.Author);
                cmd.Parameters.AddWithValue("@isbn", book.ISBN);
                cmd.Parameters.AddWithValue("@genre", book.Genre);
                cmd.Parameters.AddWithValue("@publisher", book.Publisher);
                cmd.Parameters.AddWithValue("@year_published", book.YearPublished);
                cmd.Parameters.AddWithValue("@copies", book.Copies);
                cmd.ExecuteNonQuery();
            }
        }

        // Delete a book from the database
        public static void DeleteBook(int bookId)
        {
            using (var conn = GetConnection())
            {
                conn.Open();
                string query = "DELETE FROM books WHERE id = @id";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@id", bookId);
                cmd.ExecuteNonQuery();
            }
        }

        #endregion

        #region Patrons CRUD Operations

        // Add a new patron to the database
        public static void AddPatron(Patron patron)
        {
            using (var conn = GetConnection())
            {
                conn.Open();
                string query = "INSERT INTO patrons (full_name, membership_id, email, phone_number, address, " +
                               "date_of_birth, membership_type) VALUES (@full_name, @membership_id, @email, @phone_number, " +
                               "@address, @date_of_birth, @membership_type)";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@full_name", patron.FullName);
                cmd.Parameters.AddWithValue("@membership_id", patron.MembershipId);
                cmd.Parameters.AddWithValue("@email", patron.Email);
                cmd.Parameters.AddWithValue("@phone_number", patron.PhoneNumber);
                cmd.Parameters.AddWithValue("@address", patron.Address);
                cmd.Parameters.AddWithValue("@date_of_birth", patron.DateOfBirth);
                cmd.Parameters.AddWithValue("@membership_type", patron.MembershipType);
                cmd.ExecuteNonQuery();
            }
        }

        // Get all patrons from the database
        public static List<Patron> GetPatrons()
        {
            var patrons = new List<Patron>();
            using (var conn = GetConnection())
            {
                conn.Open();
                string query = "SELECT * FROM patrons";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                MySqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    patrons.Add(new Patron
                    {
                        Id = reader.GetInt32("id"),
                        FullName = reader.GetString("full_name"),
                        MembershipId = reader.GetString("membership_id"),
                        Email = reader.GetString("email"),
                        PhoneNumber = reader.GetString("phone_number"),
                        Address = reader.GetString("address"),
                        DateOfBirth = reader.GetDateTime("date_of_birth"),
                        MembershipType = reader.GetString("membership_type"),
                        CreatedAt = reader.GetDateTime("created_at")
                    });
                }
            }
            return patrons;
        }

        // Update patron details in the database
        public static void UpdatePatron(Patron patron)
        {
            using (var conn = GetConnection())
            {
                conn.Open();
                string query = "UPDATE patrons SET full_name = @full_name, email = @email, phone_number = @phone_number, " +
                               "address = @address, date_of_birth = @date_of_birth, membership_type = @membership_type " +
                               "WHERE id = @id";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@id", patron.Id);
                cmd.Parameters.AddWithValue("@full_name", patron.FullName);
                cmd.Parameters.AddWithValue("@email", patron.Email);
                cmd.Parameters.AddWithValue("@phone_number", patron.PhoneNumber);
                cmd.Parameters.AddWithValue("@address", patron.Address);
                cmd.Parameters.AddWithValue("@date_of_birth", patron.DateOfBirth);
                cmd.Parameters.AddWithValue("@membership_type", patron.MembershipType);
                cmd.ExecuteNonQuery();
            }
        }

        // Delete a patron from the database
        public static void DeletePatron(int patronId)
        {
            using (var conn = GetConnection())
            {
                conn.Open();
                string query = "DELETE FROM patrons WHERE id = @id";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@id", patronId);
                cmd.ExecuteNonQuery();
            }
        }

        #endregion

        #region Fines CRUD Operations

        // Add a fine for a patron
        public static void AddFine(Fine fine)
        {
            using (var conn = GetConnection())
            {
                conn.Open();
                string query = "INSERT INTO fines (patron_id, amount, date_applied) " +
                               "VALUES (@patron_id, @amount, @date_applied)";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@patron_id", fine.PatronId);
                cmd.Parameters.AddWithValue("@amount", fine.Amount);
                cmd.Parameters.AddWithValue("@date_applied", fine.DateApplied);
                cmd.ExecuteNonQuery();
            }
        }

        // Get fines for a specific patron
        public static List<Fine> GetFines(int patronId)
        {
            var fines = new List<Fine>();
            using (var conn = GetConnection())
            {
                conn.Open();
                string query = "SELECT * FROM fines WHERE patron_id = @patron_id";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@patron_id", patronId);
                MySqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    fines.Add(new Fine
                    {
                        Id = reader.GetInt32("id"),
                        PatronId = reader.GetInt32("patron_id"),
                        Amount = reader.GetDecimal("amount"),
                        DateApplied = reader.GetDateTime("date_applied")
                    });
                }
            }
            return fines;
        }

        #endregion

        #region Transactions CRUD Operations

        // Add a transaction (checkout or return)
        public static void AddTransaction(Transaction transaction)
        {
            using (var conn = GetConnection())
            {
                conn.Open();
                string query = "INSERT INTO transactions (patron_id, book_id, checkout_date, due_date, return_date, overdue_fine) " +
                               "VALUES (@patron_id, @book_id, @checkout_date, @due_date, @return_date, @overdue_fine)";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@patron_id", transaction.PatronId);
                cmd.Parameters.AddWithValue("@book_id", transaction.BookId);
                cmd.Parameters.AddWithValue("@checkout_date", transaction.CheckoutDate);
                cmd.Parameters.AddWithValue("@due_date", transaction.DueDate);
                cmd.Parameters.AddWithValue("@return_date", transaction.ReturnDate);
                cmd.Parameters.AddWithValue("@overdue_fine", transaction.OverdueFine);
                cmd.ExecuteNonQuery();
            }
        }

        // Get transactions for a specific patron
        public static List<Transaction> GetTransactions(int patronId)
        {
            var transactions = new List<Transaction>();
            using (var conn = GetConnection())
            {
                conn.Open();
                string query = "SELECT * FROM transactions WHERE patron_id = @patron_id";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@patron_id", patronId);
                MySqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    transactions.Add(new Transaction
                    {
                        Id = reader.GetInt32("id"),
                        PatronId = reader.GetInt32("patron_id"),
                        BookId = reader.GetInt32("book_id"),
                        CheckoutDate = reader.GetDateTime("checkout_date"),
                        DueDate = reader.GetDateTime("due_date"),
                        ReturnDate = reader.IsDBNull(reader.GetOrdinal("return_date")) ? (DateTime?)null : reader.GetDateTime("return_date"),
                        OverdueFine = reader.GetDecimal("overdue_fine")
                    });
                }
            }
            return transactions;
        }

        #endregion
    }
}

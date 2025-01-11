using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using LibraryManagementSystem.Models;
using System.Text;
using System.Security.Cryptography;

namespace LibraryManagementSystem.Helpers
{
    public static class DBHelper
    {
        private static readonly string connectionString = "Server=127.0.0.1;Port=3306;Database=library_system;Uid=root;Pwd=;";

        public static List<Book> GetBooks()
        {
            List<Book> books = new List<Book>();
            string query = "SELECT id, title, author, isbn, genre, publisher, year_published, copies, created_at FROM books";

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                MySqlCommand command = new MySqlCommand(query, connection);
                using (MySqlDataReader reader = command.ExecuteReader())
                {
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
            }
            return books;
        }

        public static List<Patron> GetPatrons()
        {
            List<Patron> patrons = new List<Patron>();
            string query = "SELECT id, full_name, membership_id, email, phone_number, address, date_of_birth, membership_type, created_at FROM patrons";

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                MySqlCommand command = new MySqlCommand(query, connection);
                using (MySqlDataReader reader = command.ExecuteReader())
                {
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
            }
            return patrons;
        }

        public static List<Transaction> GetTransactions()
        {
            List<Transaction> transactions = new List<Transaction>();
            string query = "SELECT id, patron_id, book_id, checkout_date, due_date, return_date, overdue_fine, created_at FROM transactions";

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                MySqlCommand command = new MySqlCommand(query, connection);
                using (MySqlDataReader reader = command.ExecuteReader())
                {
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
                            OverdueFine = reader.GetDecimal("overdue_fine"),
                            CreatedAt = reader.GetDateTime("created_at")
                        });
                    }
                }
            }
            return transactions;
        }

        public static List<Fine> GetFines()
        {
            List<Fine> fines = new List<Fine>();
            string query = "SELECT id, patron_id, amount, date_applied FROM fines";

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                MySqlCommand command = new MySqlCommand(query, connection);
                using (MySqlDataReader reader = command.ExecuteReader())
                {
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
            }
            return fines;
        }

        public static List<User> GetUsers()
        {
            List<User> users = new List<User>();
            string query = "SELECT id, username, password, role, created_at FROM users";

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                MySqlCommand command = new MySqlCommand(query, connection);
                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        users.Add(new User
                        {
                            Id = reader.GetInt32("id"),
                            Username = reader.GetString("username"),
                            Password = reader.GetString("password"),
                            Role = reader.GetString("role"),
                            CreatedAt = reader.GetDateTime("created_at")
                        });
                    }
                }
            }
            return users;
        }
        public static void DeleteBook(int id)
        {
            ExecuteNonQuery($"DELETE FROM books WHERE id = {id}");
        }

        public static void DeletePatron(int id)
        {
            ExecuteNonQuery($"DELETE FROM patrons WHERE id = {id}");
        }

        public static void DeleteTransaction(int id)
        {
            ExecuteNonQuery($"DELETE FROM transactions WHERE id = {id}");
        }

        public static void DeleteFine(int id)
        {
            ExecuteNonQuery($"DELETE FROM fines WHERE id = {id}");
        }

        public static void DeleteUser(int id)
        {
            ExecuteNonQuery($"DELETE FROM users WHERE id = {id}");
        }

        private static void ExecuteNonQuery(string query)
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                MySqlCommand command = new MySqlCommand(query, connection);
                command.ExecuteNonQuery();
            }
        }
        public static User AuthenticateUser(string username, string password)
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT * FROM users WHERE username = @username AND password = @password";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@username", username);
                command.Parameters.AddWithValue("@password", password); // Ideally, hash the password for security

                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return new User
                        {
                            Id = reader.GetInt32("id"),
                            Username = reader.GetString("username"),
                            Role = reader.GetString("role"),
                            CreatedAt = reader.GetDateTime("created_at")
                        };
                    }
                }
            }
            return null; // Return null if authentication fails
        }

        public static bool RegisterUser(string username, string password, string role)
        {
            // Use the hashed password passed from RegisterWindow
            string hashedPassword = password;

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                string query = "INSERT INTO users (username, password, role) VALUES (@username, @password, @role)";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@username", username);
                command.Parameters.AddWithValue("@password", hashedPassword); // Store the hashed password
                command.Parameters.AddWithValue("@role", role);

                try
                {
                    command.ExecuteNonQuery();
                    return true;
                }
                catch
                {
                    return false; // Return false if registration fails (e.g., duplicate username)
                }
            }
        }
        public static string GetStoredPasswordHash(string username)
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT password FROM users WHERE username = @username";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@username", username);

                var result = command.ExecuteScalar();
                return result?.ToString(); // Return the hashed password or null if no user found
            }
        }
        public static string GetUserRole(string username)
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT role FROM users WHERE username = @username";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@username", username);

                var result = command.ExecuteScalar();
                return result?.ToString(); // Return the role or null if no user found
            }
        }
        public static void AddBook(Book book)
        {
            string query = "INSERT INTO books (title, author, isbn, genre, publisher, year_published, copies, created_at) " +
                           "VALUES (@Title, @Author, @ISBN, @Genre, @Publisher, @YearPublished, @Copies, @CreatedAt)";
            ExecuteNonQueryWithParams(query, new MySqlParameter("@Title", book.Title), new MySqlParameter("@Author", book.Author),
                                      new MySqlParameter("@ISBN", book.ISBN), new MySqlParameter("@Genre", book.Genre),
                                      new MySqlParameter("@Publisher", book.Publisher), new MySqlParameter("@YearPublished", book.YearPublished),
                                      new MySqlParameter("@Copies", book.Copies), new MySqlParameter("@CreatedAt", DateTime.Now));
        }

        public static void AddPatron(Patron patron)
        {
            string query = "INSERT INTO patrons (full_name, membership_id, email, phone_number, address, date_of_birth, membership_type, created_at) " +
                           "VALUES (@FullName, @MembershipId, @Email, @PhoneNumber, @Address, @DateOfBirth, @MembershipType, @CreatedAt)";
            ExecuteNonQueryWithParams(query, new MySqlParameter("@FullName", patron.FullName), new MySqlParameter("@MembershipId", patron.MembershipId),
                                      new MySqlParameter("@Email", patron.Email), new MySqlParameter("@PhoneNumber", patron.PhoneNumber),
                                      new MySqlParameter("@Address", patron.Address), new MySqlParameter("@DateOfBirth", patron.DateOfBirth),
                                      new MySqlParameter("@MembershipType", patron.MembershipType), new MySqlParameter("@CreatedAt", DateTime.Now));
        }

        public static void AddTransaction(Transaction transaction)
        {
            string query = "INSERT INTO transactions (patron_id, book_id, checkout_date, due_date, return_date, overdue_fine, created_at) " +
                           "VALUES (@PatronId, @BookId, @CheckoutDate, @DueDate, @ReturnDate, @OverdueFine, @CreatedAt)";
            ExecuteNonQueryWithParams(query, new MySqlParameter("@PatronId", transaction.PatronId), new MySqlParameter("@BookId", transaction.BookId),
                                      new MySqlParameter("@CheckoutDate", transaction.CheckoutDate), new MySqlParameter("@DueDate", transaction.DueDate),
                                      new MySqlParameter("@ReturnDate", transaction.ReturnDate ?? (object)DBNull.Value), new MySqlParameter("@OverdueFine", transaction.OverdueFine),
                                      new MySqlParameter("@CreatedAt", DateTime.Now));
        }

        public static void AddFine(Fine fine)
        {
            string query = "INSERT INTO fines (patron_id, amount, date_applied) " +
                           "VALUES (@PatronId, @Amount, @DateApplied)";
            ExecuteNonQueryWithParams(query, new MySqlParameter("@PatronId", fine.PatronId), new MySqlParameter("@Amount", fine.Amount),
                                      new MySqlParameter("@DateApplied", fine.DateApplied));
        }

        public static void AddUser(User user)
        {
            string query = "INSERT INTO users (username, password, role, created_at) " +
                           "VALUES (@Username, @Password, @Role, @CreatedAt)";
            ExecuteNonQueryWithParams(query, new MySqlParameter("@Username", user.Username), new MySqlParameter("@Password", user.Password),
                                      new MySqlParameter("@Role", user.Role), new MySqlParameter("@CreatedAt", DateTime.Now));
        }

        private static void ExecuteNonQueryWithParams(string query, params MySqlParameter[] parameters)
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddRange(parameters);
                command.ExecuteNonQuery();
            }
        }

    }
}

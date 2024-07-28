using MySql.Data.MySqlClient;
using ternet.entities;
using ternet.interfaces;
using ternet.connection;
using Spectre.Console;

namespace ternet.repositories
{
    public class UserRepository : IUserRepository
    {
        public List<User> GetAllUsers()
        {
            User user;
            List<User> users = new List<User>();
             try
            {
                using (var connection = new MySqlConnection(DBConnection.connString))
                {
                    connection.Open();
                    string query = $"SELECT * FROM users;";

                    using (var command = new MySqlCommand(query, connection))
                    {
                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                int userId = reader.GetInt32("user_id");
                                string userName = reader.GetString("user_name");
                                string userPass = reader.GetString("user_pass");
                                bool userIsAdmin = reader.GetBoolean("user_isAdmin");

                                user = new User(userId, userName, userPass, userIsAdmin);
                                users.Add(user);
                            }
                        }
                    }
                }
            }
            catch (MySqlException ex)
            {
                Console.WriteLine($"Database error: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }

            return users;
        }

        public User GetUserInfoByName(string userName)
        {
            User user = new User();
            try
            {
                using (var connection = new MySqlConnection(DBConnection.connString))
                {
                    connection.Open();
                    string query = $"SELECT * FROM users WHERE user_name = '{userName}';";

                    using (var command = new MySqlCommand(query, connection))
                    {
                        using (var reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                int userId = reader.GetInt32("user_id");
                                userName = reader.GetString("user_name");
                                string userPass = reader.GetString("user_pass");
                                bool userIsAdmin = reader.GetBoolean("user_isAdmin");

                                user = new User(userId, userName, userPass, userIsAdmin);
                            }
                        }
                    }
                }
            }
            catch (MySqlException ex)
            {
                Console.WriteLine($"Database error: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }

            return user;
        }

        public User GetUserInfoById(int userId)
        {
            User user = new User();
            try
            {
                using (var connection = new MySqlConnection(DBConnection.connString))
                {
                    connection.Open();
                    string query = $"SELECT * FROM users WHERE user_id = '{userId}';";

                    using (var command = new MySqlCommand(query, connection))
                    {
                        using (var reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                userId = reader.GetInt32("user_id");
                                string userName = reader.GetString("user_name");
                                string userPass = reader.GetString("user_pass");
                                bool userIsAdmin = reader.GetBoolean("user_isAdmin");

                                user = new User(userId, userName, userPass, userIsAdmin);
                            }
                        }
                    }
                }
            }
            catch (MySqlException ex)
            {
                Console.WriteLine($"Database error: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }

            return user;
        }
        public bool LogInUser(string userName, string password)
        {
            bool loginSuccessful = false;
            try
            {
                using (var connection = new MySqlConnection(DBConnection.connString))
                {
                    connection.Open();
                    string query = $"SELECT * FROM users WHERE user_name = '{userName}' AND user_pass = SHA2('{password}', 512);";

                    using (var command = new MySqlCommand(query, connection))
                    {
                        using (var reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                loginSuccessful = true;
                            }
                        }
                    }
                }
            }
            catch (MySqlException ex)
            {
                Console.WriteLine($"Database error: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
            return loginSuccessful;
        }

        public int CheckAdminPrivileges (string userName) 
        {
            int isAdmin = 0;
            try
            {
                using (var connection = new MySqlConnection(DBConnection.connString))
                {
                    connection.Open();
                    string query = $"SELECT user_isAdmin FROM users WHERE user_name = '{userName}';";

                    using (var command = new MySqlCommand(query, connection))
                    {
                        using (var reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                isAdmin = reader.GetInt32("user_isAdmin");
                            }
                        }
                    }
                }
            }
            catch (MySqlException ex)
            {
                Console.WriteLine($"Database error: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
            return isAdmin;
        }

        public void InsertUser(string userName, string userPass, bool isAdmin)
        {
            try
            {
                using (var connection = new MySqlConnection(DBConnection.connString)) 
                {
                    connection.Open();
                    // Sender is always going to be the logged in user.
                    string query = $"INSERT INTO users VALUES (null, @userName, SHA2(@userPass, 512), @isAdmin)";

                    using (var command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@userName", userName);
                        command.Parameters.AddWithValue("@userPass", userPass);
                        command.Parameters.AddWithValue("@isAdmin", isAdmin);
                        
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (MySqlException ex)
            {
                Console.WriteLine($"Database error: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        public void UpdateUser(int userId, string? userName, string? userPass, bool isAdmin)
        {
            try
            {
                using (var connection = new MySqlConnection(DBConnection.connString)) 
                {
                    connection.Open();
                    // Sender is always going to be the logged in user.
                    string query = $"UPDATE users SET user_name = @userName, user_pass = @userPass, user_isAdmin = @isAdmin WHERE user_id = @userId)";

                    using (var command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@userName", userName);
                        command.Parameters.AddWithValue("@userPass", userPass);
                        command.Parameters.AddWithValue("@isAdmin", isAdmin);
                        command.Parameters.AddWithValue("@userId", userId);
                        
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (MySqlException ex)
            {
                Console.WriteLine($"Database error: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        public void DeleteUser(int userId)
        {
            try
            {
                using(var connection = new MySqlConnection(DBConnection.connString))
                {
                    connection.Open();
                    string query = "DELETE FROM users WHERE user_id = @userId";

                    using (var command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@userId", userId);

                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (MySqlException ex)
            {
                Console.WriteLine($"Database error: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }
    }
}
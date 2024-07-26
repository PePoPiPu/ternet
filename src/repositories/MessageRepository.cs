using MySql.Data.MySqlClient;
using ternet.entities;
using ternet.interfaces;
using ternet.connection; 

namespace ternet.repositories
{
    public class MessageRepository : IMessageRepository
    {
        public MessageRepository()
        {
        }

        public List<Message> GetAllMessages(int userId)
        {
            Message message;
            List<Message> messages = new List<Message>();

            try
            {
                using (var connection = new MySqlConnection(DBConnection.connString))
                {
                    connection.Open();
                    string query = $"SELECT m.*, u.user_name FROM messages AS m, users AS u WHERE m.message_sender = u.user_id AND m.message_receiver = {userId};";

                    using (var command = new MySqlCommand(query, connection))
                    {
                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                int messageId = reader.GetInt32("message_id");
                                string messageTitle = reader.GetString("message_title");
                                string messageBody = reader.GetString("message_body");
                                int messageSender = reader.GetInt32("message_sender");
                                int messageReceiver = reader.GetInt32("message_receiver");
                                string senderUserName = reader.GetString("user_name");

                                message = new Message (messageId, messageTitle, messageBody, messageSender, messageReceiver, senderUserName);
                                messages.Add(message);
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

            return messages;
        }

        public List<Message> GetMessagesBySenderId(int userId)
        {
            Message message;
            List<Message> messages = new List<Message>();

            try
            {
                using (var connection = new MySqlConnection(DBConnection.connString))
                {
                    connection.Open();
                    string query = $"SELECT m.*, u.user_name FROM messages AS m, users AS u WHERE m.message_sender = u.user_id AND m.message_receiver = {userId};";

                    using (var command = new MySqlCommand(query, connection))
                    {
                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                int messageId = reader.GetInt32("message_id");
                                string messageTitle = reader.GetString("message_title");
                                string messageBody = reader.GetString("message_body");
                                int messageSender = reader.GetInt32("message_sender");
                                int messageReceiver = reader.GetInt32("message_receiver");
                                string senderUserName = reader.GetString("user_name");

                                message = new Message (messageId, messageTitle, messageBody, messageSender, messageReceiver, senderUserName);
                                messages.Add(message);
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

            return messages;   
        }

        public Message GetMessageById(int messageId, int userId)
        {
            Message message = new Message();

            try
            {
                using (var connection = new MySqlConnection(DBConnection.connString))
                {
                    connection.Open();
                    string query = $"SELECT m.*, u.user_name FROM messages AS m, users AS u WHERE m.message_id = {messageId} AND m.message_receiver = {userId}  GROUP BY message_id";
                    
                    using (var command = new MySqlCommand(query, connection))
                    {
                        using (var reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                string messageTitle = reader.GetString("message_title");
                                string messageBody = reader.GetString("message_body");
                                int messageSender = reader.GetInt32("message_sender");
                                int messageReceiver = reader.GetInt32("message_receiver");
                                string senderUserName = reader.GetString("user_name");

                                message = new Message (messageId, messageTitle, messageBody, messageSender, messageReceiver, senderUserName);
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
            // Handle posible null exception in main function
            return message;
        }

        public void InsertMessage(string messageTitle, string messageBody, int receiverId, int userId)
        {
            try
            {
                using (var connection = new MySqlConnection(DBConnection.connString)) 
                {
                    connection.Open();
                    // Sender is always going to be the logged in user.
                    string query = $"INSERT INTO messages VALUES (null, @messageTitle, @messageBody, @receiverId, @userId)";

                    using (var command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@messageTitle", messageTitle);
                        command.Parameters.AddWithValue("@messageBody", messageBody);
                        command.Parameters.AddWithValue("@receiverId", receiverId);
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

        public void UpdateMessage(int messageId, string messageTitle, string messageBody, int senderId, int receiverId)
        {
            try
            {
                using (var connection = new MySqlConnection(DBConnection.connString))
                {
                    connection.Open();
                    string query = "UPDATE messages SET message_title = @messageTitle, message_body = @messageBody, message_sender = @message_sender, message_receiver = @message_receiver WHERE message_id = @messageId";
                    using (var command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@messageTitle", messageTitle);
                        command.Parameters.AddWithValue("@messageBody", messageBody);
                        command.Parameters.AddWithValue("@messageSender", senderId);
                        command.Parameters.AddWithValue("@messageReceiver", receiverId);
                        command.Parameters.AddWithValue("@messageId", messageId);

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

        public void DeleteMessage(int messageId)
        {
            try
            {
                using(var connection = new MySqlConnection(DBConnection.connString))
                {
                    connection.Open();
                    string query = "DELETE FROM messages WHERE message_id = @messageId";

                    using (var command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@messageId", messageId);

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
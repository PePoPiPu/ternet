using MySql.Data.MySqlClient;
using System;

namespace ternet.connection
{
    public class DBConnection
    {
        // Properties to store connection details
        public string Server { get; set; } = "localhost";
        public string DatabaseName { get; set; } = "ternet";
        public string UserName { get; set; } = "root"; 
        public string Password { get; set; } = "password";

        public MySqlConnection? Connection { get; private set; }

        // Singleton instance (only one instance throught the app)
        // ? Indicates its a nullable reference
        private static DBConnection? _instance;
        private static readonly object _lock = new object();

        // Private constructor to prevent instantiation
        private DBConnection()
        {
        }

        // Public method to get the singleton instance
        public static DBConnection Instance()
        {
            if (_instance == null)
            {
                // synchronize access to the singleton instance
                lock (_lock)
                {
                    if (_instance == null)
                    {
                        _instance = new DBConnection();
                    }
                }
            }
            return _instance;
        }

        // Method to open a connection
        public bool IsConnect()
        {
            if (Connection == null || Connection.State == System.Data.ConnectionState.Closed)
            {
                if (String.IsNullOrEmpty(DatabaseName))
                    return false;

                try
                {
                    string connstring = $"Server={Server}; database={DatabaseName}; UID={UserName}; password={Password}";
                    Connection = new MySqlConnection(connstring);
                    Connection.Open();
                }
                catch (Exception ex)
                {
                    // Handle the exception (log it, rethrow it, etc.)
                    Console.WriteLine($"Error connecting to the database: {ex.Message}");
                    return false;
                }
            }

            return Connection != null && Connection.State == System.Data.ConnectionState.Open;
        }

        // Method to close the connection
        public void Close()
        {
            if (Connection != null && Connection.State == System.Data.ConnectionState.Open)
            {
                try
                {
                    Connection.Close();
                }
                catch (Exception ex)
                {
                    // Handle the exception (log it, rethrow it, etc.)
                    Console.WriteLine($"Error closing the database connection: {ex.Message}");
                }
            }
        }
    }
}

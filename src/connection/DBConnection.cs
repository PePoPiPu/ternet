using MySql.Data.MySqlClient;
using System;

namespace ternet.connection
{
    public class DBConnection
    {
        // Properties to store connection details
        public static string Server { get; set; } = "localhost";
        public static string DatabaseName { get; set; } = "ternet_env";
        public  static string UserName { get; set; } = "root"; 
        public static string Password { get; set; } = "password";

        public static string connString { get; set; } = $"Server={Server}; database={DatabaseName}; UID={UserName}; password={Password}";
        // Private constructor to prevent instantiation
        private DBConnection()
        {
        }

        
    }
}

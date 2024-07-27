using MySql.Data.MySqlClient;
using System;

namespace ternet.entities 
{
    public class User 
    {
        public int user_id { get; set; }
        public string? user_name { get; set; }
        public string? user_pass { get; set; }
        public bool isAdmin { get; set; }

        public User ( int user_id, string user_name, string user_pass, bool isAdmin) 
        {
            this.user_id = user_id;
            this.user_name = user_name;
            this.user_pass = user_pass;
            this.isAdmin = isAdmin;
        }

        public User()
        {}
        
    }
}
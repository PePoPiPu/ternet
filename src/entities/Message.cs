using System;
using MySql.Data.MySqlClient;

namespace ternet.entities 
{
    public class Message
    {
        public int message_id { get; set; }
        public string? message_title { get; set; }
        public string? message_body { get; set; }
        public int message_sender { get; set; }
        public int message_receiver { get; set; }
        public string? senderUserName { get; set; }

        public Message ( int message_id, string message_title, string message_body, int message_sender, int message_receiver, string senderUserName)
        {
            this.message_id = message_id;
            this.message_title = message_title;
            this.message_body = message_body;
            this.message_sender = message_sender;
            this.message_receiver = message_receiver;
            this.senderUserName = senderUserName;
        }

        public Message ()
        {

        }

    }
}
using System;
using MySql.Data.MySqlClient;

namespace ternet.entities 
{
    public class Post 
    {
        public int post_id { get; set; }
        public string post_title { get; set; }
        public string post_body { get; set; }
        public int post_likes { get; set; }
        public int post_creator { get; set; }

        public Post (int post_id, string post_title, string post_body, int post_likes, int post_creator)
        {
            this.post_id = post_id;
            this.post_title = post_title;
            this.post_body = post_body;
            this.post_likes = post_likes;
            this.post_creator = post_creator;
        }
    }
}
using System;
using MySql.Data.MySqlClient;

namespace ternet.entities 
{
    public class PostComment 
    {
        public int pc_id { get; set; }
        public string pc_text { get; set; }
        public int pc_likes { get; set; }
        public int pc_creator { get; set; }
        public int pc_post { get; set; }
        public string posterUserName { get; set; }
        public string postTitle { get; set; }

        public PostComment (int pc_id, string pc_text, int pc_likes, int pc_creator, int pc_post, string posterUserName, string postTitle)
        {
            this.pc_id = pc_id;
            this.pc_text = pc_text;
            this.pc_likes = pc_likes;
            this.pc_creator = pc_creator;
            this.pc_post = pc_post;
            this.posterUserName = posterUserName;
            this.postTitle = postTitle;
        }
    }
}
using MySql.Data.MySqlClient;
using System;

namespace ternet.entities
{
    public class CommentPost
    {
        public int cp_post_id { get; set; }
        public int cp_comment_id { get; set; }

        public CommentPost (int cp_post_id, int cp_comment_id)
        {
            this.cp_post_id = cp_post_id;
            this.cp_comment_id = cp_comment_id;
        }
    }
}
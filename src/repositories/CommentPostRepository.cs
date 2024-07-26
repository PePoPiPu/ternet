using MySql.Data.MySqlClient;
using ternet.entities;
using ternet.interfaces;
using ternet.connection; // Add this namespace to use DBConnection

namespace ternet.repositories
{
    public class CommentPostRepository : ICommentPostRepository
    {
        public CommentPostRepository()
        {
        }
        

        public List<CommentPost> GetCommentPost()
        {
            CommentPost commentPost;
            List<CommentPost> commentPostList = new List<CommentPost>(); 
            
            try
            {
                using (var connection = new MySqlConnection(DBConnection.connString))
                {
                    connection.Open();
                    string query = "SELECT * FROM comments_posts";

                    using (var command = new MySqlCommand(query, connection))
                    {
                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read()) 
                            {
                                int postId = reader.GetInt32("cp_post_id");
                                int commentId = reader.GetInt32("cp_comment_id");

                                commentPost = new CommentPost(postId, commentId);
                                commentPostList.Add(commentPost);
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

            return commentPostList;
        }

        public void Insert(int commentId, int postId)
        {
            try
            {
                using(var connection = new MySqlConnection(DBConnection.connString))
                {
                    connection.Open();
                    string query = "INSERT INTO comments_posts (cp_post_id, cp_comment_id) VALUES (@postId, @commentId)";

                    using (var command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@postId", postId);
                        command.Parameters.AddWithValue("@commentId", commentId);

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

        public void Update(int commentId, int postId)
        {
            try
            {
                using(var connection = new MySqlConnection(DBConnection.connString))
                {
                    connection.Open();
                    string query = "UPDATE comments_posts SET cp_comment_id = @commentId WHERE cp_post_id = @postId";

                    using (var command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@postId", postId);
                        command.Parameters.AddWithValue("@commentId", commentId);

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

        public void Delete(int commentId)
        {
            try
            {
                using(var connection = new MySqlConnection(DBConnection.connString))
                {
                    connection.Open();
                    string query = "DELETE FROM comments_posts WHERE cp_comment_id = @commentId";

                    using (var command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@commentId", commentId);

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
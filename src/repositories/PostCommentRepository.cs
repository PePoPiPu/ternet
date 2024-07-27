using MySql.Data.MySqlClient;
using ternet.entities;
using ternet.interfaces;
using ternet.connection; 

namespace ternet.repositories
{
    public class PostCommentRepository : IPostCommentRepository
    {
        public PostCommentRepository()
        {
        }

        public List<PostComment> GetCommentsByPost(int postId)
        {
            PostComment comment;
            List<PostComment> postComments = new List<PostComment>();

            try
            {
                using (var connection = new MySqlConnection(DBConnection.connString))
                {
                    connection.Open();
                    string query = $"SELECT pc.*, u.user_name, p.post_title FROM post_comments AS pc, users AS u, posts AS p WHERE pc_post = {postId} GROUP BY pc_id";

                    using (var command = new MySqlCommand(query, connection))
                    {
                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                int pcId = reader.GetInt32("pc_id");
                                string pcText = reader.GetString("pc_text");
                                int pcLikes = reader.GetInt32("pc_likes");
                                int pcCreator = reader.GetInt32("pc_creator");
                                int pcPost = reader.GetInt32("pc_post");
                                string posterUsername = reader.GetString("user_name");
                                string postTitle = reader.GetString("post_title");

                                comment = new PostComment(pcId, pcText, pcLikes, pcCreator, pcPost, posterUsername, postTitle);
                                postComments.Add(comment);
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
            return postComments;
        }

        public List<PostComment> GetMostLikedComments(int postId)
        {
            PostComment comment;
            List<PostComment> postComments = new List<PostComment>();

            try
            {
                using (var connection = new MySqlConnection(DBConnection.connString))
                {
                    connection.Open();
                    string query = $"SELECT pc.*, u.user_name, p.post_title FROM post_comments AS pc, users AS u, posts AS p WHERE pc_post = {postId} GROUP BY pc_id ORDER BY pc_likes DESC";

                    using (var command = new MySqlCommand(query, connection))
                    {
                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                int pcId = reader.GetInt32("pc_id");
                                string pcText = reader.GetString("pc_text");
                                int pcLikes = reader.GetInt32("pc_likes");
                                int pcCreator = reader.GetInt32("pc_creator");
                                int pcPost = reader.GetInt32("pc_post");
                                string posterUsername = reader.GetString("user_name");
                                string postTitle = reader.GetString("post_title");

                                comment = new PostComment(pcId, pcText, pcLikes, pcCreator, pcPost, posterUsername, postTitle);
                                postComments.Add(comment);
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
            return postComments;
        }

        public List<PostComment> GetLessLikedComments(int postId)
        {
            PostComment comment;
            List<PostComment> postComments = new List<PostComment>();

            try
            {
                using (var connection = new MySqlConnection(DBConnection.connString))
                {
                    connection.Open();
                    string query = $"SELECT pc.*, u.user_name, p.post_title FROM post_comments AS pc, users AS u, posts AS p WHERE pc_post = {postId} GROUP BY pc_id ORDER BY pc_likes DESC";

                    using (var command = new MySqlCommand(query, connection))
                    {
                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                int pcId = reader.GetInt32("pc_id");
                                string pcText = reader.GetString("pc_text");
                                int pcLikes = reader.GetInt32("pc_likes");
                                int pcCreator = reader.GetInt32("pc_creator");
                                int pcPost = reader.GetInt32("pc_post");
                                string posterUsername = reader.GetString("user_name");
                                string postTitle = reader.GetString("post_title");

                                comment = new PostComment(pcId, pcText, pcLikes, pcCreator, pcPost, posterUsername, postTitle);
                                postComments.Add(comment);
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
            return postComments;
        }

        public List<PostComment> GetCommentsByCreator(int userId)
        {
            PostComment comment;
            List<PostComment> postComments = new List<PostComment>();

            try
            {
                using (var connection = new MySqlConnection(DBConnection.connString))
                {
                    connection.Open();
                    string query = $"SELECT pc.*, u.user_name, p.post_title FROM post_comments AS pc, users AS u, posts AS p WHERE pc_creator = {userId} GROUP BY pc_id";

                    using (var command = new MySqlCommand(query, connection))
                    {
                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                int pcId = reader.GetInt32("pc_id");
                                string pcText = reader.GetString("pc_text");
                                int pcLikes = reader.GetInt32("pc_likes");
                                int pcCreator = reader.GetInt32("pc_creator");
                                int pcPost = reader.GetInt32("pc_post");
                                string posterUsername = reader.GetString("user_name");
                                string postTitle = reader.GetString("post_title");

                                comment = new PostComment(pcId, pcText, pcLikes, pcCreator, pcPost, posterUsername, postTitle);
                                postComments.Add(comment);
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
            return postComments;
        }

        public void InsertComment(string commentText, int userId, int postId)
        {
            try
            {
                using (var connection = new MySqlConnection(DBConnection.connString)) 
                {
                    connection.Open();
                    // Commenter is always going to be the logged in user.
                    string query = $"INSERT INTO post_comments VALUES (null, @pcText, 0, @userId, @pcPost)";

                    using (var command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@pcText", commentText);
                        command.Parameters.AddWithValue("@userId", userId);
                        command.Parameters.AddWithValue("@pcPost", postId);
                        
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

        public void UpdateComment(int commentId, string commentText, int userId, int postId)
        {
            try
            {
                using (var connection = new MySqlConnection(DBConnection.connString))
                {
                    connection.Open();
                    string query = "UPDATE post_comments SET pc_text = @pcText, pc_creator = @userId, pc_post = @postId WHERE pc_id = @commentId";
                    using (var command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@pcText", commentText);
                        command.Parameters.AddWithValue("@userId", userId);
                        command.Parameters.AddWithValue("@pcPost", postId);
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

        public void UpdateCommentLikes(int commentId, int commentLikes)
        {
            try
            {
                using (var connection = new MySqlConnection(DBConnection.connString))
                {
                    connection.Open();
                    string query = "UPDATE post_comments SET pc_likes = @pcLikes WHERE pc_id = @commentId";
                    using (var command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@pcLikes", commentLikes);
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

        public void DeleteComment(int commentId)
        {
            try
            {
                using(var connection = new MySqlConnection(DBConnection.connString))
                {
                    connection.Open();
                    string query = "DELETE FROM post_comments WHERE pc_id = @commentId";

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
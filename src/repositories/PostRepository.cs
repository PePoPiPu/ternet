using MySql.Data.MySqlClient;
using ternet.entities;
using ternet.interfaces;
using ternet.connection; 

namespace ternet.repositories
{
    public class PostRepository : IPostRepository
    {
        public PostRepository()
        {
        }

        public List<Post> GetAllPosts()
        {
            Post post;
            List<Post> posts = new List<Post>();

            try
            {
                using (var connection = new MySqlConnection(DBConnection.connString))
                {
                    connection.Open();
                    string query = "SELECT p.*, u.user_name FROM posts AS p, users AS u GROUP BY post_id";

                    using (var command = new MySqlCommand(query, connection))
                    {
                        using (var reader = command.ExecuteReader()) 
                        {
                            while(reader.Read())
                            {
                            int postId = reader.GetInt32("post_id");
                            string postTitle = reader.GetString("post_title");
                            string postBody = reader.GetString("post_body");
                            int postLikes = reader.GetInt32("post_likes");
                            int postCreator = reader.GetInt32("post_creator");
                            string creatorUserName = reader.GetString("user_name");

                            post = new Post(postId, postTitle, postBody, postLikes, postCreator, creatorUserName);
                            posts.Add(post);
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
            return posts;
        }

        public List<Post> GetPostByCreator(int userId)
        {
            Post post;
            List<Post> posts = new List<Post>();
            try 
            {
                using (var connection = new MySqlConnection(DBConnection.connString))
                {
                    connection.Open();
                    string query = $"SELECT p.*, u.user_name FROM posts AS p, users AS u WHERE post_creator = {userId} GROUP BY post_id";

                    using (var command = new MySqlCommand(query, connection))
                    {
                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                int postId = reader.GetInt32("post_id");
                                string postTitle = reader.GetString("post_title");
                                string postBody = reader.GetString("post_body");
                                int postLikes = reader.GetInt32("post_likes");
                                int postCreator = reader.GetInt32("post_creator");
                                string creatorUserName = reader.GetString("user_name");

                                post = new Post(postId, postTitle, postBody, postLikes, postCreator, creatorUserName);
                                posts.Add(post);
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
            return posts;
        }

        public List<Post> GetPostByTitle(string postTitle)
        {
            Post post;
            List<Post> posts = new List<Post>();
            try
            {
                using (var connection = new MySqlConnection(DBConnection.connString))
                {
                    connection.Open();
                    string query = $"SELECT p.*, u.user_name FROM posts AS p, users AS u WHERE post_title LIKE '%{postTitle}%' GROUP BY post_id";

                    using (var command = new MySqlCommand(query, connection))
                    {
                        using(var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                int postId = reader.GetInt32("post_id");
                                postTitle = reader.GetString("post_title");
                                string postBody = reader.GetString("post_body");
                                int postLikes = reader.GetInt32("post_likes");
                                int postCreator = reader.GetInt32("post_creator");
                                string creatorUserName = reader.GetString("user_name");

                                post = new Post(postId, postTitle, postBody, postLikes, postCreator, creatorUserName);
                                posts.Add(post);
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
            return posts;
        }

        public Post GetPostById(int postId)
        {
            Post post = new Post();
            try
            {
                using (var connection = new MySqlConnection(DBConnection.connString))
                {
                    connection.Open();
                    string query = $"SELECT p.*, u.user_name FROM posts AS p, users AS u WHERE post_id = {postId} GROUP BY post_id";

                    using (var command = new MySqlCommand(query, connection))
                    {
                        using(var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                postId = reader.GetInt32("post_id");
                                string postTitle = reader.GetString("post_title");
                                string postBody = reader.GetString("post_body");
                                int postLikes = reader.GetInt32("post_likes");
                                int postCreator = reader.GetInt32("post_creator");
                                string creatorUserName = reader.GetString("user_name");

                                post = new Post(postId, postTitle, postBody, postLikes, postCreator, creatorUserName);
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
            return post;
        }

        public List<Post> GetPostsByMostLiked()
        {
            Post post;
            List<Post> posts = new List<Post>();
            try
            {
                using (var connection = new MySqlConnection(DBConnection.connString))
                {
                    connection.Open();
                    string query = $"SELECT p.*, u.user_name FROM posts AS p, users AS u GROUP BY post_id ORDER BY post_likes DESC";

                    using (var command = new MySqlCommand(query, connection))
                    {
                        using(var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                int postId = reader.GetInt32("post_id");
                                string postTitle = reader.GetString("post_title");
                                string postBody = reader.GetString("post_body");
                                int postLikes = reader.GetInt32("post_likes");
                                int postCreator = reader.GetInt32("post_creator");
                                string creatorUserName = reader.GetString("user_name");

                                post = new Post(postId, postTitle, postBody, postLikes, postCreator, creatorUserName);
                                posts.Add(post);
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
            return posts;
        }

        public List<Post> GetPostsByLessLiked()
        {
Post post;
            List<Post> posts = new List<Post>();
            try
            {
                using (var connection = new MySqlConnection(DBConnection.connString))
                {
                    connection.Open();
                    string query = $"SELECT p.*, u.user_name FROM posts AS p, users AS u GROUP BY post_id ORDER BY post_likes ASC";

                    using (var command = new MySqlCommand(query, connection))
                    {
                        using(var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                int postId = reader.GetInt32("post_id");
                                string postTitle = reader.GetString("post_title");
                                string postBody = reader.GetString("post_body");
                                int postLikes = reader.GetInt32("post_likes");
                                int postCreator = reader.GetInt32("post_creator");
                                string creatorUserName = reader.GetString("user_name");

                                post = new Post(postId, postTitle, postBody, postLikes, postCreator, creatorUserName);
                                posts.Add(post);
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
            return posts;
        }

        public void InsertPost (string postTitle, string postBody, int userId)
        {
            try
            {
                using (var connection = new MySqlConnection(DBConnection.connString)) 
                {
                    connection.Open();
                    // Poster is always going to be the logged in user.
                    string query = $"INSERT INTO posts VALUES (null, @postTitle, @postBody, 0, @userId)";

                    using (var command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@postTitle", postTitle);
                        command.Parameters.AddWithValue("@postBody", postBody);
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

        public void UpdatePost (int postId, string postTitle, string postBody, int userId)
        {
            try
            {
                using (var connection = new MySqlConnection(DBConnection.connString))
                {
                    connection.Open();
                    string query = "UPDATE posts SET post_title = @postTitle, post_body = @postBody, post_creator = @userId WHERE post_id = @postId";
                    using (var command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@postTitle", postTitle);
                        command.Parameters.AddWithValue("@postBody", postBody);
                        command.Parameters.AddWithValue("@userId", userId);
                        command.Parameters.AddWithValue("@postId", postId);

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
        public void DeletePost(int postId)
        {
            try
            {
                using(var connection = new MySqlConnection(DBConnection.connString))
                {
                    connection.Open();
                    string query = "DELETE FROM posts WHERE post_id = @postId";

                    using (var command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@postId", postId);

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
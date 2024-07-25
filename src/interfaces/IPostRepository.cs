using ternet.entities;

namespace ternet.interfaces
{
    public interface IPostRepository
    {
        List<Post> GetAllPosts();
        List<Post> GetPostByCreator(int userId);
        List<Post> GetPostByTitle(string postTitle);
        Post GetPostById(int postId);
        List<Post> GetPostsByMostLiked();
        List<Post> GetPostsByLessLiked();

        void InsertPost (string postTitle, string postBody);
        void UpdatePost (int postId, string postTitle, string postBody, int userId);
        void DeletePost(int postId);
    }
}
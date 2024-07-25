using ternet.entities;

namespace ternet.entities
{
    public interface IPostCommentRepository
    {
        List<Post> GetCommentsByPost(int commentId);
        List<Post> GetMostLikedComments(int commentId);
        List<Post> GetLessLikedComments(int commentId);
        List<Post> GetCommentsByCreator(int userId);    
        void InsertComment(string commentText, int userId, int postId);
        void UpdateComment(int commentId, string commentText, int userId, int postId);
        void DeleteComment(int commentId);
    }
}
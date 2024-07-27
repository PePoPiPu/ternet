using ternet.entities;

namespace ternet.entities
{
    public interface IPostCommentRepository
    {
        List<PostComment> GetCommentsByPost(int postId);
        List<PostComment> GetMostLikedComments(int postId);
        List<PostComment> GetLessLikedComments(int postId);
        List<PostComment> GetCommentsByCreator(int userId);    
        void InsertComment(string commentText, int userId, int postId);
        void UpdateComment(int commentId, string commentText, int userId, int postId);
        void UpdateCommentLikes(int commentId, int commentLikes);
        void DeleteComment(int commentId);
    }
}
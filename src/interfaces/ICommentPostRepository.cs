using ternet.entities;

namespace ternet.interfaces 
{
    public interface ICommentPostRepository
    {
        List<CommentPost> GetCommentPost ();
        void Insert(int postId, int commentId);
        void Update(int commentId, int postId);
        void Delete(int commentId);
    }
}
using ternet.entities;

namespace ternet.interfaces 
{
    public interface ICommentPostRepository
    {
        List<CommentPost> GetCommentPost ();
        void Insert(int commentId, int postId);
        void Update(int commentId);
        void Delete(int commentId);
    }
}
using ternet.entities;

namespace ternet.interfaces 
{
    public interface IMessageRepository
    {
        List<Message> GetAllMessages(int userId);
        // Thinking in the context of a logged in user
        List<Message> GetMessagesBySenderId(int userId);
        Message GetMessageById(int messageId, int userId);
        void InsertMessage(string messageTitle, string messageBody, int receiverId, int userId);
        void UpdateMessage(int messageId, string messageTitle, string messageBody, int senderId, int receiverId);
        void DeleteMessage(int messageId);
    }
}
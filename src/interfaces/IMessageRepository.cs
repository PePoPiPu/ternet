using ternet.entities;

namespace ternet.interfaces 
{
    public interface IMessageRepository
    {
        List<Message> GetAllMessages();
        // Thinking in the context of a logged in user
        List<Message> GetMessagesBySenderId(int userId);
        List<Message> GetMessagesByReceiverId(int userId);
        Message GetMessageById(int messageId);
        void InsertMessage(string messageTitle, string messageBody, int receiverId);
        void UpdateMessage(int messageId, string messageTitle, string messageBody, int senderId, int receiverId);
        void DeleteMessage(int messageId);
    }
}
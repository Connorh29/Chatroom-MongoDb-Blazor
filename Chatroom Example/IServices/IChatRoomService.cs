using Chatroom_Example.Data;

namespace Chatroom_Example.IServices
{
    public interface IChatRoomService
    {
        public Task CreateChatRoomAsync(ChatRoomModel chatlog);
        public Task DeleteMessageAsync(string chatId, string messageId);
        public Task AddMessageAsync(string chatId, Message message);
        public Task<(bool, ChatRoomModel)> TryGetChatRoomAsync(string chatId);
        public Task<List<ChatRoomModel>> GetAllChatRoomsAsync();
    }
}

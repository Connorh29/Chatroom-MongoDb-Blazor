using Chatroom_Example.Data;
using Chatroom_Example.IServices;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Chatroom_Example.Services
{
    public class ChatRoomService : IChatRoomService
    {
        private MongoClient _mongoClient;
        private IMongoDatabase _database;
        private IMongoCollection<ChatRoomModel> _chatLogsTable;
        public ChatRoomService()
        {
            _mongoClient = new MongoClient("mongodb://localhost:27017");
            _database = _mongoClient.GetDatabase("ChatExample2");
            _chatLogsTable = _database.GetCollection<ChatRoomModel>("ChatRooms");
        }
        public async Task CreateChatRoomAsync(ChatRoomModel chatRoomModel)
        {
            var cursor = await _chatLogsTable.FindAsync(x => x.Id == chatRoomModel.Id);
            var chatObj = await cursor.FirstOrDefaultAsync();
            if (chatObj == null)
            {
                _chatLogsTable.InsertOne(chatRoomModel);
            }
            else
            {
                _chatLogsTable.ReplaceOne(x => x.Id == chatRoomModel.Id, chatRoomModel);
                Console.WriteLine("ERROR: SHOULD NEVER HAPPEN");
            }
        }
        public async Task<(bool, ChatRoomModel)> TryGetChatRoomAsync(string chatId)
        {
            var cursor = await _chatLogsTable.FindAsync(x => x.Id == chatId);
            var chatLog = await cursor.FirstOrDefaultAsync();
            if (chatLog != null) return (true, chatLog);
            return (false, default);
        }
        public async Task<List<ChatRoomModel>> GetAllChatRoomsAsync()   
        {
            var result = await _chatLogsTable.FindAsync(x => true);
            return await result.ToListAsync();
        }

        public async Task AddMessageAsync(string chatId, Message message)
        {
            var filter = Builders<ChatRoomModel>.Filter.Eq(x => x.Id, chatId);
            var update = Builders<ChatRoomModel>.Update.Push(x => x.Messages, message);
            await _chatLogsTable.UpdateOneAsync(filter, update);
        }
        public async Task DeleteMessageAsync(string chatId, string messageId)
        {
            var filter = Builders<ChatRoomModel>.Filter.Eq(x => x.Id, chatId);
            var update = Builders<ChatRoomModel>.Update
                .Set("Messages.$[f].Deleted", true)
                .Set("Messages.$[f].Content", string.Empty)
                .Set("Messages.$[f].SenderUsername", string.Empty);
            string arrayFilter = "{'f._id': ObjectId('" + messageId + "')}";

            var arrayFilters = new[]
            {
                new  JsonArrayFilterDefinition<Message>(arrayFilter)
            };
            await _chatLogsTable.UpdateOneAsync(filter, update, new UpdateOptions() { ArrayFilters = arrayFilters, IsUpsert = true });
        }
    }
}

using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Chatroom_Example.Data
{
    public class ChatRoomModel
    {
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; } = ObjectId.GenerateNewId().ToString();

        public string CreationTime { get; set; }

        public string Name { get; set; }

        public string Password { get; set; }

        public List<Message> Messages { get; set; } = new List<Message>();

        public ChatRoomModel(string name, string password)
        {
            Name = name;
            CreationTime = DateTime.Now.ToShortTimeString() + " " + DateTime.Now.ToShortDateString();
            Password = password;
        }
    }
}

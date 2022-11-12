using System.Diagnostics;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Chatroom_Example.Data
{
    public class Message
    {
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        public string SenderUsername { get; set; }
        public string Content { get; set; }
        public string CreationTime { get; set; }
        public bool Deleted { get; set; }
        public MessageType MessageType { get; set; }

        public bool IsMine(string username)
        {
            return username == SenderUsername;
        }

        public Message(string senderUsername, string content, MessageType messageType = MessageType.Normal)
        {
            SenderUsername = senderUsername;
            Content = content;
            MessageType = messageType;
            CreationTime = DateTime.Now.ToShortTimeString();
            Id = ObjectId.GenerateNewId().ToString();
            Deleted = false;
        }
    }

    public enum MessageType
    {
        PlayerLeave,
        PlayerJoin,
        Normal
    }
}

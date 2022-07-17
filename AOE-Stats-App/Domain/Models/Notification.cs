using Domain.Enums;

namespace Domain.Models
{
    public class Notification : BaseEntity
    {
        public string Message { get; set; }
        public MessageType MessageType { get; set; }

        public Notification() { }

        public Notification(string message, MessageType messageType)
        {
            Message = message;
            MessageType = messageType;
        }
    }
}

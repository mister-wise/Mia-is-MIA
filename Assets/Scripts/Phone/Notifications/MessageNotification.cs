using SODefinitions;

namespace Phone.Notifications
{
    public class MessageNotification : Notification
    {
        public MessageNotification(Message message) : base(
            NotificationType.Message,
            message.Text.Length > 100 ? $"{message.Text.Substring(0, 97)}..." : message.Text,
            message.Contact
        )
        {
        }
    }
}
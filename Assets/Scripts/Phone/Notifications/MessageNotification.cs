namespace Phone.Notifications
{
    public class MessageNotification : Notification
    {
        public MessageNotification(Message message) : base(
            NotificationType.Message,
            message.Contact.Name,
            message.Text.Length > 100 ? $"{message.Text.Substring(0, 97)}..." : message.Text
        )
        {
        }
    }
}
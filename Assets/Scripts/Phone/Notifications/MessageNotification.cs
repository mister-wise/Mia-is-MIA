using SODefinitions;

namespace Phone.Notifications
{
    public class MessageNotification : Notification
    {
        public MessageNotification(Message message) : base(
            NotificationType.Message,
            message.GetShortText(),
            message.Contact
        )
        {
        }
    }
}
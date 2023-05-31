using UnityEngine;

namespace Phone
{

    public class Notification
    {
        public NotificationType Type;
        public string Title;
        public string Message;

        public Notification(NotificationType type, string title, string message)
        {
            Type = type;
            Title = title;
            Message = message;
        }
    }

}
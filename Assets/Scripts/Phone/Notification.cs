using UnityEngine;

namespace Phone
{

    public class Notification
    {
        public string Title;
        public string Message;
        public Sprite Icon;

        public Notification(string title, string message, Sprite icon)
        {
            Title = title;
            Message = message;
            Icon = icon;
        }
    }

}
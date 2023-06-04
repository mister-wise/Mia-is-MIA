using SODefinitions;
using UnityEngine;

namespace Phone
{

    public class Notification
    {
        public NotificationType Type;
        public string Title;
        public string Message;
        public ContactSO Contact;
        
        public Notification(NotificationType type, string message, ContactSO contact)
        {
            Type = type;
            Title = contact.Name;
            Message = message;
            Contact = contact;
        }
    }

}
using SODefinitions;

namespace Phone.Notifications
{
    public class MissedCallNotification : Notification
    {
        public MissedCallNotification(ContactSO contact, int count = 1) : base(NotificationType.MissedCall,
            count > 1 ? $"Missed call ({count})" : "Missed call", contact)
        {
        }
    }
}
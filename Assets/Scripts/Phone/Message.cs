using System;
using SODefinitions;

namespace Phone
{
    [Serializable]
    public class Message
    {
        public ContactSO Contact;
        public string Text;
        public bool Read = false;
        public DateTime Time;
        
        public Message(ContactSO contact, string text, bool read, DateTime time)
        {
            Contact = contact;
            Text = text;
            Read = read;
            Time = time;
        }
    }
}

using System;
using SODefinitions;
using UnityEngine;

namespace Phone
{
    [Serializable]
    public class Message
    {
        public ContactSO Contact;
        public string Text;
        public bool Read = false;
        public DateTime Time;
        public bool Owner = false;
        public bool Failed = false;

        [SerializeField] private string stringTime;
        
        
        public Message(ContactSO contact, string text, bool read, DateTime time)
        {
            Contact = contact;
            Text = text;
            Read = read;
            Time = time;
            stringTime = time.ToShortTimeString();

        }
        
        public Message(ContactSO contact, string text, bool read, string stringTime)
        {
            Contact = contact;
            Text = text;
            Read = read;
            Time = DateTime.Parse(stringTime);
            this.stringTime = stringTime;
        }

        public DateTime GetDateTime()
        {
            try
            {
                return DateTime.Parse(stringTime);
            }
            catch
            {
                return DateTime.Now;
            }
        }

        public string GetDateTimeToString(string pattern = "ddd, dd MMMM yyyy, HH:mm")
        {
            return GetDateTime().ToString(pattern);
        }
    }
}

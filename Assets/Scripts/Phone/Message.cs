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
        public bool Owner = false;
        public bool Failed = false;

        private DateTime time;

        [SerializeField] private string stringTime;
        
        
        public Message(ContactSO contact, string text, bool read, DateTime time)
        {
            Contact = contact;
            Text = text;
            Read = read;
            this.time = time;
            stringTime = time.ToShortTimeString();

        }
        
        public Message(ContactSO contact, string text, bool read, string stringTime)
        {
            Contact = contact;
            Text = text;
            Read = read;
            // time = DateTime.Parse(stringTime);
            time = DateTime.ParseExact(stringTime, "MM/dd/yyyy HH:mm", null);
            this.stringTime = stringTime;
        }

        public DateTime GetDateTime()
        {
            try
            {
                // return DateTime.Parse(stringTime);
                return DateTime.ParseExact(stringTime, "MM/dd/yyyy HH:mm", null);
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

        public string GetShortText(int limit = 100)
        {
            var flatText = Text.Replace("<br>", " ");
            return flatText.Length > limit ? $"{flatText.Substring(0, limit - 3)}..." : flatText;
        }
    }
}

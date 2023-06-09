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
        
        public NotebookItemSO Unlock = null;

        public string AttachmentName;
        public string AttachmentUrl;
        public NotebookItemSO AttachmentUnlock;

        private DateTime time;

        [SerializeField] private string stringTime;
        
        
        public Message(ContactSO contact, string text, bool read, DateTime time, NotebookItemSO unlock = null)
        {
            Contact = contact;
            Text = text;
            Read = read;
            this.time = time;
            stringTime = time.ToShortTimeString();
            Unlock = unlock;

        }
        
        public Message(ContactSO contact, string text, bool read, string stringTime, NotebookItemSO unlock = null)
        {
            Contact = contact;
            Text = text;
            Read = read;
            // time = DateTime.Parse(stringTime);
            time = DateTime.ParseExact(stringTime, "MM/dd/yyyy HH:mm", null);
            this.stringTime = stringTime;
            Unlock = unlock;
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
                return GameManager.Instance.GetGameTime();
            }
        }

        public string GetDateTimeToString(string pattern = "ddd, dd MMMM yyyy, HH:mm")
        {
            return GetDateTime().ToString(pattern);
        }

        public string GetShortText(int limit = 40)
        {
            var flatText = Text.Replace("<br>", " ");
            return flatText.Length > limit ? $"{flatText.Substring(0, limit - 3)}..." : flatText;
        }

        public void SetAsRead()
        {
            if (Read) return;
            Read = true;
            if(Unlock) Notebook.Instance.Unlock(Unlock);
        }
    }
}

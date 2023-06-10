using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using SODefinitions;
using UnityEngine;

namespace Phone
{
    public class MessageList : MonoBehaviour
    {
        [SerializeField] private List<Message> messages;

        [SerializeField] private Transform messagesListContainer;
        [SerializeField] private GameObject messagesPrefab;

        void Start()
        {
            Rebuild();
        }

        public void Rebuild()
        {
            foreach (Transform child in messagesListContainer)
            {
                Destroy(child.gameObject);
            }

            var uniqueNames = messages
                .OrderBy(message => message.Read).ThenByDescending(message => message.GetDateTime())
                .Select(m => m.Contact)
                .Distinct(new MessageEqualityComparer());

            foreach (var contact in uniqueNames)
            {
                var lastMessage = messages.Where(message => message.Contact == contact)
                    .OrderByDescending(message => message.GetDateTime()).First();
                Instantiate(messagesPrefab, messagesListContainer).GetComponent<MessageThreadItem>()
                    ?.SetItem(lastMessage, IsThreadUnread(contact));
            }
        }
        
        public MessageItem AddMessage(Message message)
        {
            MessageItem messageItem = null;
            messages.Add(message);
            Rebuild();
            if (
                PhoneController.Instance.IsMessageTheadOpen() &&
                PhoneController.Instance.MessageThread.Contact == message.Contact)
            {
                messageItem = PhoneController.Instance.MessageThread.AddMessageToOpenedThread(message);
            }

            return messageItem;
        }

        public IEnumerable<Message> GetContactMessages(ContactSO contact)
        {
            var contactMessages = messages.Where(message => message.Contact == contact).ToList();
            foreach (var message in contactMessages)
            {
                message.SetAsRead();
            }

            PhoneController.Instance.MessageList.Rebuild();
            return contactMessages;
        }

        private bool IsThreadUnread(ContactSO contact)
        {
            return messages.Where(message => message.Contact == contact).FirstOrDefault(m => !m.Read) != null;
        }

        private class MessageEqualityComparer : IEqualityComparer<ContactSO>
        {
            public bool Equals(ContactSO x, ContactSO y)
            {
                if (ReferenceEquals(x, y))
                    return true;

                if (x is null || y is null)
                    return false;

                return x.Name == y.Name;
            }

            public int GetHashCode(ContactSO obj)
            {
                return obj.Name.GetHashCode();
            }
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Phone
{
    public class MessageThread : MonoBehaviour
    {
        [SerializeField] private List<Message> messages;
        
        [SerializeField] private Transform messageContainer;
        [SerializeField] private GameObject messagePrefab;

        public void Start()
        {
            Rebuild();
        }

        public void Rebuild()
        {
            foreach (Transform child in messageContainer) {
                Destroy(child.gameObject);
            }

            foreach (var message in messages.OrderBy(message => message.Time))
            {
                Instantiate(messagePrefab, messageContainer).GetComponent<MessageItem>()?.SetItem(message);
            }
        }
    }
}

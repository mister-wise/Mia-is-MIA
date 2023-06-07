using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using SODefinitions;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Phone
{
    public class MessageThread : MonoBehaviour
    {
        public ContactSO Contact { get; private set; }
        
        [SerializeField] private List<Message> debugMessages;
        
        [SerializeField] private TMP_Text messageHeader;
        [SerializeField] private ScrollRect scrollRect;
        [SerializeField] private Transform messageContainer;
        [SerializeField] private GameObject incomingMessagePrefab;
        [SerializeField] private GameObject outgoingMessagePrefab;
        [SerializeField] private TMP_InputField messageInput;

        public void Start()
        {
            // Rebuild(debugMessages);
        }

        public void Rebuild(ContactSO contact, IEnumerable<Message> messages)
        {
            Contact = contact;
            messageHeader.text = contact.Name;
                
            foreach (Transform child in messageContainer) {
                Destroy(child.gameObject);
            }

            foreach (var message in messages.OrderBy(message => message.GetDateTime()))
            {
                if (message.Owner)
                {
                    Instantiate(outgoingMessagePrefab, messageContainer).GetComponent<MessageItem>()?.SetItem(message);
                }
                else
                {
                    Instantiate(incomingMessagePrefab, messageContainer).GetComponent<MessageItem>()?.SetItem(message);

                }
            }
        }

        public MessageItem AddMessageToOpenedThread(Message message)
        {
            var newMessage = Instantiate(message.Owner ? outgoingMessagePrefab : incomingMessagePrefab, messageContainer);
            var messageItem = newMessage.GetComponent<MessageItem>();
            messageItem.SetItem(message);
            messageItem.PlayPopUpAnimation();

            ScrollToBottom();
            
            return messageItem;
        }
        
        public void ScrollToBottom()
        {
            Canvas.ForceUpdateCanvases();

            scrollRect.verticalNormalizedPosition = 0f;
        }

        public void SendMessage()
        {
            if (messageInput.text.Trim() == "") return;
            var message = new Message(Contact, messageInput.text, true, DateTime.Now)
            {
                Owner = true
            };
            messageInput.text = "";
            StartCoroutine(PhoneController.Instance.HandleSendMessage(message));
        }
    }
}

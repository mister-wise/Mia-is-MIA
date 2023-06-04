using System;
using System.Collections;
using DG.Tweening;
using Phone.Notifications;
using SODefinitions;
using TMPro;
using UnityEngine;

namespace Phone
{
    [RequireComponent(typeof(AudioSource))]
    public class PhoneController : MonoBehaviour
    {
        public static PhoneController Instance;
        
        private AudioSource audioSource;
        
        [SerializeField] private TMP_Text clockText;
        [SerializeField] private float closedWindowOffset = -750f;
        
        [Header("General")]
        public bool CommunicationBlocked = false;
        [SerializeField] private AudioClip errorSound;
        [SerializeField] private ContactSO miaContact;
        [SerializeField] private ContactSO antagonistContact;

        [Header("Debug")]
        [SerializeField] private ContactSO debugContact; 

        [Header("Notification")]
        [SerializeField] private Transform notificationContainer;
        [SerializeField] private GameObject notificationPrefab;
        [SerializeField] private AudioClip notificationSound;
        
        [Header("Recent Calls")]
        [SerializeField] private GameObject recentCallsWindow;
        [SerializeField] private Transform recentCallsContainer;
        [SerializeField] private GameObject recentCallsPrefab;
        
        [Header("Contacts List")]
        public ContactsList ContactsList;
        [SerializeField] private GameObject contactsListWindow;

        [Header("Contact Details")]
        [SerializeField] private GameObject contactDetailsWindow;
        [SerializeField] private ContactDetails contactDetailsController;

        [Header("Message List")]
        public MessageList MessageList;
        [SerializeField] private GameObject messageListWindow;
        
        [Header("Message Thread")]
        public MessageThread MessageThread;
        [SerializeField] private GameObject messageThreadWindow;

        [Header("Call")]
        public CallController Call;
        [SerializeField] private AudioSource callAudioSource;
        [SerializeField] private GameObject callWindow;
        
        [Header("Call KeyPad")]
        public CallKeyPad CallKeyPad;
        [SerializeField] private GameObject callKeyPadWindow;
        
        private void Awake()
        {
            Instance = this;

            BackToHome();
        }

        void Start()
        {
            audioSource = GetComponent<AudioSource>();
        }

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.F1))
            {
                ReceiveMessage(new Message(debugContact, "Nie próbuj tego więcej", false, DateTime.Now));
            }
            if (Input.GetKeyDown(KeyCode.F2))
            {
                AddMissingCall(debugContact);
            }

            if (Input.GetKeyDown(KeyCode.Escape))
            {
                Back();
            }

            clockText.text = GetGameTime();
        }

        // TODO: Change to real in game time
        private string GetGameTime() => DateTime.Now.ToString("HH:mm");
        
        public void BackToHome()
        {
            CloseContactDetailsWindow();
            CloseRecentCallsWindow();
            CloseContactsListWindow();
            CloseMessageListWindow();
            CloseMessageTheadWindow();
            CloseCallWindow();
            CloseCallKeyPadWindow();
        }
        
        public void Back()
        {
            if (IsWindowOpen(callWindow))
            {
                CloseCallWindow();
            }
            else if (IsWindowOpen(contactDetailsWindow))
            {
                CloseContactDetailsWindow();
            }
            else if (IsMessageTheadOpen())
            {
                CloseMessageTheadWindow();
            }
            else
            {
                CloseRecentCallsWindow();
                CloseContactsListWindow();
                CloseMessageListWindow();
                CloseCallKeyPadWindow();
            }
        }
        
        public void CloseOtherApplication(GameObject expectedWindow)
        {
            if (expectedWindow == contactDetailsWindow)
            {
                
            }
        }

        public bool IsMessageTheadOpen() => IsWindowOpen(messageThreadWindow);

        public void OpenRecentCallsWindow()
        {
            BackToHome();
            recentCallsWindow.transform.DOMoveY(0, .5f);
        }
        
        public void CloseRecentCallsWindow()
        {
            recentCallsWindow.transform.DOMoveY(closedWindowOffset, .5f);
        }
        
        public void OpenContactsListWindow()
        {
            BackToHome();
            contactsListWindow.transform.DOMoveY(0, .5f);
        }
        
        public void CloseContactsListWindow()
        {
            contactsListWindow.transform.DOMoveY(closedWindowOffset, .5f);
        }
        
        public void OpenContactDetailsWindow(ContactSO contact)
        {
            contactDetailsController.SetContactData(contact);
            contactDetailsWindow.transform.DOMoveY(0, .5f);
        }
        
        public void CloseContactDetailsWindow()
        {
            contactDetailsWindow.transform.DOMoveY(closedWindowOffset, .5f);
        }
        
        public void OpenMessageListWindow()
        {
            BackToHome();
            messageListWindow.transform.DOMoveY(0, .5f);
        }
        
        public void CloseMessageListWindow()
        {
            messageListWindow.transform.DOMoveY(closedWindowOffset, .5f);
        }
        
        public void OpenMessageTheadWindow(ContactSO contact)
        {
            BackToHome();
            MessageThread.Rebuild(contact, MessageList.GetContactMessages(contact));
            messageThreadWindow.transform.DOMoveY(0, .5f);
        }
        
        public void CloseMessageTheadWindow()
        {
            messageThreadWindow.transform.DOMoveY(closedWindowOffset, .5f);
        }
        
        public void OpenCallWindow(ContactSO contact)
        {
            Call.StartCall(contact);
            callWindow.transform.DOMoveY(0, .5f);
        }
        
        public void CloseCallWindow()
        {
            StopCallSound();
            callWindow.transform.DOMoveY(closedWindowOffset, .5f);
        }
        
        public void OpenCallKeyPadWindow()
        {
            callKeyPadWindow.transform.DOMoveY(0, .5f);
        }
        
        public void CloseCallKeyPadWindow()
        {
            callKeyPadWindow.transform.DOMoveY(closedWindowOffset, .5f);
        }

        public void AddMissingCall(ContactSO contact, int count = 1)
        {
            AddNotification(new MissedCallNotification(contact, count));
            AddCallToHistory(contact, RecentCallStatus.Missed, count);
        }

        public void ReceiveMessage(Message message)
        {
            MessageList.AddMessage(message);
            AddNotification(new MessageNotification(message));
        }


        public void AddNotification(Notification notification)
        {
            var notificationObject = Instantiate(notificationPrefab, notificationContainer);
            notificationObject.GetComponent<NotificationController>()?.SetNotification(notification);
            audioSource.PlayOneShot(notificationSound);
        }

        private bool IsWindowOpen(GameObject window)
        {
            return Mathf.Round(window.transform.position.y) == 0;
        }

        private void AddCallToHistory(ContactSO contact, RecentCallStatus status, int count=1)
        {
            var childCount = recentCallsContainer.transform.childCount;
            if (childCount > 0)
            {
                var lastItemIndex = recentCallsContainer.transform.childCount - 1;
                var lastItem =  recentCallsContainer.transform.GetChild(lastItemIndex).gameObject;
                if (lastItem)
                {
                    var recentCall = lastItem.GetComponent<RecentCallItem>();
                    if (recentCall.Contact == contact && recentCall.Status == status)
                    {
                        recentCall.IncreaseCounter();
                        return;
                    }
                }
            }
            
            var recentCallItem = Instantiate(recentCallsPrefab, recentCallsContainer);
            recentCallItem.GetComponent<RecentCallItem>()?.SetItem(contact, status, count);
        }
        
        public void StartCall(ContactSO contact)
        {
            StartCoroutine(HandleCall(contact));
        }

        public IEnumerator HandleCall(ContactSO contact)
        {
            AddCallToHistory(contact, RecentCallStatus.Outgoing);
            OpenCallWindow(contact);
            
            if (!CommunicationBlocked)
            {
                PlayCallSound();
                yield return new WaitForSeconds(5f);
            }

            yield return new WaitForSeconds(1.5f);
            if (contact != antagonistContact && contact != miaContact)
            {
                Call.FailedCall();
                yield return new WaitForSeconds(3f);
                CloseCallWindow();
                if (CommunicationBlocked) yield break;
                
                yield return new WaitForSeconds(2f);
                Instance.SendWarning(contact);
            }
        }

        public IEnumerator HandleSendMessage(Message message)
        {
            var messageItem = MessageList.AddMessage(message);
            yield return new WaitForSeconds(1.5f);
            if (message.Contact == antagonistContact || message.Contact == miaContact)
            {
                
            }
            else
            {
                messageItem.SetNotDeliveredStatus();
                MessageThread.ScrollToBottom();
                Instance.SendWarning(message.Contact);
            }
        }

        public void SendWarning(ContactSO contact)
        {
            CommunicationBlocked = true;
            var messageText = "This is just between us, don't involve others.";
            if (contact.Name is "911" or "112")
            {
                messageText = "Don't try it. This is the only warning.";
            }
           
            ReceiveMessage(new Message(antagonistContact, messageText, false, DateTime.Now));
        }

        public void PlayErrorSound()
        {
            audioSource.PlayOneShot(errorSound);
        }
        
        public void PlayCallSound()
        {
            callAudioSource.Play();
        }
        
        public void StopCallSound()
        {
            callAudioSource.Stop();
        }
    }
}

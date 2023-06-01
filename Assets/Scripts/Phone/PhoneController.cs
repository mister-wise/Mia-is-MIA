using System;
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
            if (Input.GetKeyDown(KeyCode.Space))
            {
                ReceiveMessage(new Message(debugContact, "Nie próbuj tego więcej", false, DateTime.Now));
            }
            if (Input.GetKeyDown(KeyCode.X))
            {
                OpenMessageListWindow();
            }
            if (Input.GetKeyDown(KeyCode.Z))
            {
                AddMissingCall(debugContact);
            }
            if (Input.GetKeyDown(KeyCode.C))
            {
                OpenContactsListWindow();
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
        }
        
        public void Back()
        {
            if (Mathf.Round(contactDetailsWindow.transform.position.y) == 0)
            {
                CloseContactDetailsWindow();
            }
            else
            {
                CloseRecentCallsWindow();
                CloseContactsListWindow();
                CloseMessageListWindow();
            }
        }

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

        private void AddCallToHistory(ContactSO contact, RecentCallStatus status, int count)
        {
            var childCount = recentCallsContainer.transform.childCount;
            if (childCount > 0)
            {
                var lastItemIndex = recentCallsContainer.transform.childCount - 1;
                var lastItem =  recentCallsContainer.transform.GetChild(lastItemIndex).gameObject;
                if (lastItem)
                {
                    var recentCall = lastItem.GetComponent<RecentCallItem>();
                    if (recentCall.Contact == contact)
                    {
                        recentCall.IncreaseCounter();
                        return;
                    }
                }
            }
            
            var recentCallItem = Instantiate(recentCallsPrefab, recentCallsContainer);
            recentCallItem.GetComponent<RecentCallItem>()?.SetItem(contact, status, count);
        }
        
        
    }
}

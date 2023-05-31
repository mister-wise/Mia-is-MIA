using System;
using DG.Tweening;
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

        private int i = 1;

        private void Awake()
        {
            Instance = this;
        }

        void Start()
        {
            audioSource = GetComponent<AudioSource>();
        }

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                AddNotification(new Notification(NotificationType.Message,"Anonimowy", $"{i} Nie próbuj tego więcej"));
                i++;
            }
            if (Input.GetKeyDown(KeyCode.Z))
            {
                AddNotification(new Notification(NotificationType.MissedCall,"Anonimowy", "Missed Call"));
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
            }
        }

        public void OpenRecentCallsWindow()
        {
            recentCallsWindow.transform.DOMoveY(0, .5f);
        }
        
        public void CloseRecentCallsWindow()
        {
            recentCallsWindow.transform.DOMoveY(-750, .5f);
        }
        
        public void OpenContactsListWindow()
        {
            contactsListWindow.transform.DOMoveY(0, .5f);
        }
        
        public void CloseContactsListWindow()
        {
            contactsListWindow.transform.DOMoveY(-750, .5f);
        }
        
        public void OpenContactDetailsWindow(ContactSO contact)
        {
            contactDetailsController.SetContactData(contact);
            contactDetailsWindow.transform.DOMoveY(0, .5f);
        }
        
        public void CloseContactDetailsWindow()
        {
            contactDetailsWindow.transform.DOMoveY(-750, .5f);
        }


        public void AddNotification(Notification notification)
        {
            var notificationObject = Instantiate(notificationPrefab, notificationContainer);
            notificationObject.GetComponent<NotificationController>()?.SetNotification(notification);
            audioSource.PlayOneShot(notificationSound);

            if (notification.Type == NotificationType.MissedCall)
            {
                AddCallToHistory(notification.Title, RecentCallStatus.Missed);
            }
        }

        private void AddCallToHistory(string callerName, RecentCallStatus status)
        {
            var recentCallItem = Instantiate(recentCallsPrefab, recentCallsContainer);
            recentCallItem.GetComponent<RecentCallItem>()?.SetItem(callerName, status);
        }
        
        
    }
}

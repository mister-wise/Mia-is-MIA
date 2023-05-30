using System;
using System.Collections.Generic;
using System.ComponentModel.Design.Serialization;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Phone
{
    [RequireComponent(typeof(AudioSource))]
    public class PhoneController : MonoBehaviour
    {
        public static PhoneController Instance;
        
        private AudioSource audioSource;
        
        [SerializeField] private Transform notificationContainer;
        [SerializeField] private GameObject notificationPrefab;
        [SerializeField] private AudioClip notificationSound;
        
        [SerializeField] private Transform recentCallsContainer;
        [SerializeField] private GameObject recentCallsPrefab;
        
        [SerializeField] private GameObject recentCallsWindow;


        private List<string> recentCalls = new List<string>();

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
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                CloseRecentCallsWindow();
            }
        }

        public void BackToHome()
        {
            Debug.Log("HOME");
            CloseRecentCallsWindow();
        }

        public void OpenRecentCallsWindow()
        {
            recentCallsWindow.transform.DOMoveY(0, .5f);
        }
        
        public void CloseRecentCallsWindow()
        {
            recentCallsWindow.transform.DOMoveY(-750, .5f);
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
            recentCalls.Add(callerName);
            var recentCallItem = Instantiate(recentCallsPrefab, recentCallsContainer);
            recentCallItem.GetComponent<RecentCallItem>()?.SetItem(callerName, status);
        }
        
        
    }
}

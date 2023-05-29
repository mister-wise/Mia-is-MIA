using System;
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
        }

        public void AddNotification(Notification notification)
        {
            var notificationObject = Instantiate(notificationPrefab, notificationContainer);
            notificationObject.GetComponent<NotificationController>()?.SetNotification(notification);
            notificationObject.transform.DOScale(1, 1);
            audioSource.PlayOneShot(notificationSound);
        }
        
    }
}

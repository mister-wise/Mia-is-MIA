using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Phone
{
    [RequireComponent(typeof(AudioSource))]
    public class PhoneController : MonoBehaviour
    {
        private AudioSource audioSource;
        
        [SerializeField] private Transform notificationContainer;
        [SerializeField] private GameObject notificationPrefab;
        [SerializeField] private AudioClip notificationSound;

        private int i = 1;
        
        void Start()
        {
            audioSource = GetComponent<AudioSource>();
            AddNotification(new Notification("Anonymouse", $"Don't try it again!", null));
        }

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                AddNotification(new Notification("Anonimowy", $"{i} Nie próbuj tego więcej", null));
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

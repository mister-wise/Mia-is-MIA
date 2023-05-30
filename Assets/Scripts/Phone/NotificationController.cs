using System;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Phone
{
    public enum NotificationType
    {
        Message,
        MissedCall
    }

    public class NotificationController : MonoBehaviour
    {
        [SerializeField] private TMP_Text titleText;
        [SerializeField] private TMP_Text messageText;
        [SerializeField] private Image notificationIcon;

        [SerializeField] private Sprite messageIcon;
        [SerializeField] private Sprite missedCallIcon;

        private Notification notification;


        private void Awake()
        {
            transform.localScale = Vector3.zero;
        }

        private void Start()
        {
            transform.DOScale(1, 1);
        }

        public void SetNotification(Notification notification)
        {
            this.notification = notification;
            
            titleText.text = notification.Title;
            messageText.text = notification.Message;
            notificationIcon.sprite = notification.Type switch
            {
                NotificationType.Message => messageIcon,
                NotificationType.MissedCall => missedCallIcon,
                _ => messageIcon
            };
        }

        public void Remove()
        {
            var removeSequence = DOTween.Sequence();
            removeSequence
                .Append(transform.DOLocalMoveX(320, .2f))
                .OnComplete(() => { Destroy(gameObject); });
        }

        public void HandleClick()
        {
            switch(notification.Type)
            {
                case NotificationType.Message:
                    Debug.Log("Open Message");
                    break;
                case NotificationType.MissedCall:
                    PhoneController.Instance.OpenRecentCallsWindow();
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            Remove();
        }
    }
}

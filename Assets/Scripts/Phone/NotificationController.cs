using System;
using DG.Tweening;
using Phone.Notifications;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Phone
{
    public enum NotificationType
    {
        Message,
        MissedCall
    }

    public class NotificationController : MonoBehaviour, IPointerClickHandler, IPointerDownHandler, IDragHandler, IEndDragHandler
    {
        [SerializeField] private TMP_Text titleText;
        [SerializeField] private TMP_Text messageText;
        [SerializeField] private Image notificationIcon;

        [SerializeField] private Sprite messageIcon;
        [SerializeField] private Sprite missedCallIcon;
        [SerializeField] private Color missedCallColorIcon;
        [SerializeField] private float missedCallIconScale;

        private Notification notification;
        private Vector3 initPosition;
        private RectTransform rectTransform;


        private void Awake()
        {
            transform.localScale = Vector3.zero;
            rectTransform = GetComponent<RectTransform>();
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
            notificationIcon.color = notification.Type switch
            {
                NotificationType.MissedCall => missedCallColorIcon,
                _ => Color.white
            };
            var scaleValue = notification.Type switch
            {
                NotificationType.MissedCall => missedCallIconScale,
                _ => 1f
            };
            notificationIcon.transform.localScale = new Vector3(scaleValue, scaleValue, scaleValue);
        }

        public void Remove()
        {
            var removeSequence = DOTween.Sequence();
            removeSequence
                .Append(transform.DOLocalMoveX(320, .2f))
                .OnComplete(() => { Destroy(gameObject); });
        }

        public void OnDrag(PointerEventData eventData)
        {
            rectTransform.anchoredPosition += new Vector2(eventData.delta.x, 0);
            if (rectTransform.anchoredPosition.x < 0)
            {
                rectTransform.anchoredPosition = Vector2.zero;
            }
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            if (rectTransform.anchoredPosition.x >= 100f)
            {
                Remove();
            }
            else
            {
                transform.position = initPosition;
            }
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            initPosition = transform.position;
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            if (rectTransform.anchoredPosition.x != 0) return;

            switch(notification.Type)
            {
                case NotificationType.Message:
                    PhoneController.Instance.OpenMessageTheadWindow(notification.Contact);
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

using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Phone
{
    public class NotificationController : MonoBehaviour
    {
        [SerializeField] private TMP_Text titleText;
        [SerializeField] private TMP_Text messageText;
        [SerializeField] private Image notificationIcon;

        private void Awake()
        {
            transform.localScale = Vector3.zero;
        }

        public void SetNotification(Notification notification)
        {
            titleText.text = notification.Title;
            messageText.text = notification.Message;
            if (notification.Icon)
            {
                notificationIcon.sprite = notification.Icon;
            }
        }
    }
}

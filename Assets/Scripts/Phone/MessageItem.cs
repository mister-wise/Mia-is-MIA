using System;
using DG.Tweening;
using TMPro;
using UnityEngine;

namespace Phone
{
    public class MessageItem : MonoBehaviour
    {
        private Message message;
        
        [SerializeField] private TMP_Text messageText;
        [SerializeField] private TMP_Text datetimeText;
        [SerializeField] private GameObject notDeliveredObject;
        
        [SerializeField] private GameObject attachment;
        
        public void SetItem(Message message)
        {
            this.message = message;
            messageText.text = message.Text;
            datetimeText.text = message.GetDateTimeToString();
            notDeliveredObject.SetActive(message.Failed);

            if (message.AttachmentName != "" && message.AttachmentUrl != "")
            {
                attachment.GetComponent<TMP_Text>().text = message.AttachmentName;
                attachment.SetActive(true);
            }
        }

        public void PlayPopUpAnimation()
        {
            // transform.localScale = Vector3.zero;
            // transform.DOScale(Vector3.one, 1);
            // // transform.DOScale(Vector3.one, 1);
        }

        public void SetNotDeliveredStatus()
        {
            notDeliveredObject.SetActive(true);
            message.Failed = true;
            PhoneController.Instance.PlayErrorSound();
        }

        public void OpenAttachment()
        {
            Debug.Log($"Open: {message.AttachmentUrl}");
            Application.OpenURL(message.AttachmentUrl);
        }
    }
}

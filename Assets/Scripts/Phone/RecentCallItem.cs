using System;
using DG.Tweening;
using SODefinitions;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Phone
{
    public enum RecentCallStatus
    {
        Outgoing,
        Incoming,
        Missed
    }
    
    public class RecentCallItem : MonoBehaviour, IPointerClickHandler
    {
        private ContactSO contact;
        private RecentCallStatus status;
        private int count;
        
        [SerializeField] private TMP_Text titleText;
        [SerializeField] private Image Icon;

        [SerializeField] private Sprite messageIcon;
        [SerializeField] private Sprite missedCallIcon;
        [SerializeField] private Color missingCallColor;

        public ContactSO Contact => contact;
        public RecentCallStatus Status => status;

        public void SetItem(ContactSO contact, RecentCallStatus status, int count = 1)
        {
            this.contact = contact;
            this.count = count;
            this.status = status;
            titleText.text = count > 1 ? $"{contact.Name} ({count})" : contact.Name;
            if (status == RecentCallStatus.Missed)
            {
                titleText.color = missingCallColor;
            }
            Icon.sprite = status switch
            {
                RecentCallStatus.Outgoing => missedCallIcon,
                RecentCallStatus.Incoming => missedCallIcon,
                RecentCallStatus.Missed => missedCallIcon,
                _ => messageIcon
            };
        }
        
        
        public void OnPointerClick(PointerEventData eventData)
        {
            PhoneController.Instance.OpenContactDetailsWindow(contact);
        }

        public void IncreaseCounter()
        {
            count++;
            titleText.text = count > 1 ? $"{contact.Name} ({count})" : contact.Name;
        }
    }
}

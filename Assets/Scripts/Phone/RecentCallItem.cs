using System;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Phone
{
    public enum RecentCallStatus
    {
        Outgoing,
        Incoming,
        Missed
    }
    
    public class RecentCallItem : MonoBehaviour
    {
        [SerializeField] private TMP_Text titleText;
        [SerializeField] private Image Icon;

        [SerializeField] private Sprite messageIcon;
        [SerializeField] private Sprite missedCallIcon;

        public void SetItem(string caller, RecentCallStatus status)
        {
            
            titleText.text = caller;
            Icon.sprite = status switch
            {
                RecentCallStatus.Outgoing => missedCallIcon,
                RecentCallStatus.Incoming => missedCallIcon,
                RecentCallStatus.Missed => missedCallIcon,
                _ => messageIcon
            };
        }
        
    }
}

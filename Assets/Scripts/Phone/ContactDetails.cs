using System.Collections.Generic;
using System.Linq;
using SODefinitions;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using Image = UnityEngine.UI.Image;

namespace Phone
{
    public class ContactDetails : MonoBehaviour
    {
        private ContactSO contact;

        public ContactUnlocker[] Unlockers;

        [SerializeField] private TMP_Text nameText;
        [SerializeField] private Image profileImage;
        [SerializeField] private MessageButton messageButton;
        [SerializeField] private CallButton callButton;

        public void SetContactData(ContactSO contact)
        {
            this.contact = contact;
            nameText.text = contact.Name;
            profileImage.sprite = contact.Image;
            messageButton.Contact = contact;
            callButton.Contact = contact;
    
            var unlocker = Unlockers.Where(item => item.contact == contact);
            Notebook.Instance.Unlock(unlocker.FirstOrDefault().item);
        }
    }
}

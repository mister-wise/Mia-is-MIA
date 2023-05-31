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
        
        [SerializeField] private TMP_Text nameText;
        [SerializeField] private Image profileImage;

        public void SetContactData(ContactSO contact)
        {
            this.contact = contact;
            nameText.text = contact.Name;
            profileImage.sprite = contact.Image;
        }
    }
}

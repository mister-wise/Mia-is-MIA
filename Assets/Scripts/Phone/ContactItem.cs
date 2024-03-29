using SODefinitions;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using Image = UnityEngine.UI.Image;

namespace Phone
{
    public class ContactItem : MonoBehaviour, IPointerClickHandler
    {
        private ContactSO contact;
        
        [SerializeField] private TMP_Text nameText;
        [SerializeField] private Image profileImage;

        public void SetItem(ContactSO contact)
        {
            this.contact = contact;
            nameText.text = contact.Name;
            profileImage.sprite = contact.Image;
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            PhoneController.Instance.OpenContactDetailsWindow(contact);
        }
    }
}

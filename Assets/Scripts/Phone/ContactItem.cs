using SODefinitions;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;
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
            Debug.Log($"Open contact: {contact.Name}");
            PhoneController.Instance.OpenContactDetailsWindow(contact);
        }
    }
}

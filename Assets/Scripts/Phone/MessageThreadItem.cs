using SODefinitions;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using Image = UnityEngine.UI.Image;

namespace Phone
{
    public class MessageThreadItem : MonoBehaviour, IPointerClickHandler
    {
        private ContactSO contact;
        
        [SerializeField] private TMP_Text nameText;
        [SerializeField] private Image profileImage;

        public void SetItem(ContactSO contact, bool unread)
        {
            this.contact = contact;
            nameText.text = unread ? $"<color=\"red\"> • </color>{contact.Name}" : contact.Name;
            if (unread)
            {
                nameText.fontStyle = FontStyles.Bold;
            }
            profileImage.sprite = contact.Image;
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            PhoneController.Instance.OpenContactDetailsWindow(contact);
        }
    }
}

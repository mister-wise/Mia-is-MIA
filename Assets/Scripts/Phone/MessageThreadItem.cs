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
        [SerializeField] private TMP_Text lastMessageText;
        [SerializeField] private Image profileImage;

        public void SetItem(Message lastMessage, bool unread)
        {
            contact = lastMessage.Contact;
            nameText.text = unread ? $"<color=\"red\">• </color>{contact.Name}" : contact.Name;
            if (unread)
            {
                nameText.fontStyle = FontStyles.Bold;
            }
            profileImage.sprite = contact.Image;
            lastMessageText.text = lastMessage.GetShortText(22);
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            PhoneController.Instance.OpenMessageTheadWindow(contact);
        }
    }
}

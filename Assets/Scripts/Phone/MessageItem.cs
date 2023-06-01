using TMPro;
using UnityEngine;

namespace Phone
{
    public class MessageItem : MonoBehaviour
    {
        private Message message;
        
        [SerializeField] private TMP_Text messageText;
        [SerializeField] private TMP_Text datetimeText;
        
        public void SetItem(Message message)
        {
            messageText.text = message.Text;
            datetimeText.text = message.Time.ToString("ddd, dd MMMM yyyy, HH:mm");
        }
    }
}

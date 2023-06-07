using SODefinitions;
using TMPro;
using UnityEngine;

namespace Phone
{
    public class CallKeyPad : MonoBehaviour
    {
        [SerializeField] private TMP_Text phoneInput;

        public void HandleKeyPadButton(string buttonValue)
        {
            phoneInput.text += buttonValue;
        }
        
        public void RemoveLastCharacter()
        {
            if(phoneInput.text.Length > 0)
                phoneInput.text = phoneInput.text[..^1];
        }
        
        public void StartCall()
        {
            if (phoneInput.text.Trim().Length < 3) return;
            var customContact = ScriptableObject.CreateInstance<ContactSO>();
            customContact.Name = phoneInput.text;
            PhoneController.Instance.StartCall(customContact);
        }
    }
}

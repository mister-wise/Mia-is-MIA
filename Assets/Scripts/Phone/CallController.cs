using SODefinitions;
using TMPro;
using UnityEngine;

namespace Phone
{
    public class CallController : MonoBehaviour
    {
        [SerializeField] private TMP_Text contactNameText;
        [SerializeField] private TMP_Text timeText;
        [SerializeField] private GameObject communicationError;

        private float time = 0;
        private bool activeTimer = false;

        public void Update()
        {
            HandleTimer();
        }

        public void StartCall(ContactSO contact)
        {
            activeTimer = true;
            communicationError.SetActive(false);
            time = 0;
            contactNameText.text = contact.Name;
        }
        
        private  void HandleTimer()
        {
            if(activeTimer) time += Time.deltaTime;

            var minutes = time / 60;
            var seconds = time % 60;

            timeText.text = $"{minutes:00}:{seconds:00}";
        }

        public void FailedCall()
        {
            activeTimer = false;
            PhoneController.Instance.StopCallSound();
            PhoneController.Instance.PlayErrorSound();
            communicationError.SetActive(true);
        }
    }
}

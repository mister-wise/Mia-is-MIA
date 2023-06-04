using SODefinitions;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Phone
{
    public class EndCallButton : MonoBehaviour, IPointerClickHandler
    {
        public void OnPointerClick(PointerEventData eventData)
        {
            PhoneController.Instance.CloseCallWindow();
        }
    }
}

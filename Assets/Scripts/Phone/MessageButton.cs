using SODefinitions;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Phone
{
    public class MessageButton : MonoBehaviour, IPointerClickHandler
    {
        public ContactSO Contact { get; set; }


        public void OnPointerClick(PointerEventData eventData)
        {
            PhoneController.Instance.OpenMessageTheadWindow(Contact);
        }
    }
}

using UnityEngine;

namespace SODefinitions
{
    [CreateAssetMenu(fileName = "Contact", menuName = "ScriptableObjects/ContactSO")]
    public class ContactSO : ScriptableObject
    {
        public string Name;
        public Sprite Image;
    }
}

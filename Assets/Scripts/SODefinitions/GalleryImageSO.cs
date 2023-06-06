using UnityEngine;

namespace SODefinitions
{
    [CreateAssetMenu(fileName = "GalleryImage", menuName = "ScriptableObjects/GalleryImageSO")]
    public class GalleryImageSO : ScriptableObject
    {
        public string Name;
        public Sprite Image;
        public string Geolocation = "n/a";
        public string Date = "n/a";
    }
}

using SODefinitions;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Phone
{
    [RequireComponent(typeof(Image))]
    public class GalleryImage : MonoBehaviour, IPointerClickHandler
    {
        private GalleryImageSO galleryImage;
        
        public void SetImage(GalleryImageSO galleryImage)
        {
            this.galleryImage = galleryImage;
            GetComponent<Image>().sprite = galleryImage.Image;
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            PhoneController.Instance.GalleryApplication.ZoomPhoto(galleryImage);
        }
    }
}

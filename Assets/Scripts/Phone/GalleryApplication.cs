using System.Collections.Generic;
using SODefinitions;
using UnityEngine;

namespace Phone
{
    public class GalleryApplication : MonoBehaviour
    {
        [SerializeField] private List<GalleryImageSO> images;
        [SerializeField] private GalleryImageZoom galleryZoomImage;
        [SerializeField] private GameObject imagePrefab;
        [SerializeField] private Transform listContainer;
        
        

        void Awake()
        {
            galleryZoomImage.Disable();
        }

        private void Start()
        {
            foreach (var image in images)
            {
                Instantiate(imagePrefab, listContainer).GetComponent<GalleryImage>()?.SetImage(image);
            }
        }

        public bool Back()
        {
            if (!galleryZoomImage.gameObject.activeSelf) return false;
            galleryZoomImage.Disable();
            return true;
        }

        public void ZoomPhoto(GalleryImageSO galleryImage)
        {
            galleryZoomImage.gameObject.SetActive(true);
            galleryZoomImage.SetItem(galleryImage);

            if (galleryImage.Unlock)
            {
                Notebook.Instance.Unlock(galleryImage.Unlock);
            }
        }
    }
}
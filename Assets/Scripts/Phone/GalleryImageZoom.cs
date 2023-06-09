using System;
using DG.Tweening;
using SODefinitions;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Phone
{
    public class GalleryImageZoom : MonoBehaviour
    {
        [SerializeField] private Image image;
        [SerializeField] private TMP_Text detailsText;

        private void OnEnable()
        {
            transform.DOScale(1, .25f);
        }
        
        public void Disable()
        {
            transform.DOScale(0, .25f);
            gameObject.SetActive(false);
        }
        
        public void SetItem(GalleryImageSO galleryImage)
        {
            image.sprite = galleryImage.Image;
            detailsText.text = getDetailsText(galleryImage);
        }

        private string getDetailsText(GalleryImageSO galleryImage)
        {
            return $"<b>Filename:</b><br>{galleryImage.Name}<br><br><b>Time:</b><br>{galleryImage.Date}<br><br><b>Location:</b><br>{galleryImage.Geolocation}";
        }
    }
}
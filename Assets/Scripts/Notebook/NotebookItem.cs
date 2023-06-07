using SODefinitions;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

    public class NotebookItem : MonoBehaviour
    {
        [SerializeField] private TMP_Text itemName;
        [SerializeField] private TMP_Text itemDescription;
        [SerializeField] private Image image;

        public void SetItem(NotebookItemSO item)
        {
            itemName.text = item.Name;
            itemDescription.text = item.Descritpion;
            image.sprite = item.Image;
        }
    }


using System.Collections.Generic;
using SODefinitions;
using UnityEngine;

namespace Phone
{
    public class ContactsList : MonoBehaviour
    {

        [SerializeField] private List<ContactSO> contacts;
        [SerializeField] private Transform contactsListContainer;
        [SerializeField] private GameObject contactsListPrefab;
    
        void Start()
        {
            contacts.Sort((x, y) => x.Name.CompareTo(y.Name));
            foreach (var contact in contacts)
            {
                Instantiate(contactsListPrefab, contactsListContainer).GetComponent<ContactItem>()?.SetItem(contact);
            }
        }
    }
}
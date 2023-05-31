using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour
{
    [SerializeField] List<Canvas> menuItems = new List<Canvas>();

    public void OpenMenu (string menu) // let's change it into enum later
    {
        foreach (Canvas item in menuItems)
        {
            if (item.name == menu)
            {
                item.enabled = true;
            }
            else 
            item.enabled = false;
        }
    }

}

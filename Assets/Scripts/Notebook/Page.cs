using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Page : MonoBehaviour
{
    public void Open()
    {
        transform.SetAsLastSibling();
    }
}

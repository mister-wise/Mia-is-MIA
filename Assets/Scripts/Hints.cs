using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Hints : MonoBehaviour
{
    [SerializeField] private List<string> hints = new List<string>();
    [SerializeField] private TMP_Text hintText;
    private IEnumerator coroutine;
    private int currentIndex;

    void Start()
    {
        StartCoroutine(DisplayHint(25));
    }

    private IEnumerator DisplayHint(int seconds)
    {
        while (true)
        {
            hintText.text = hints[currentIndex];
            currentIndex = (currentIndex + 1) % hints.Count;
            yield return new WaitForSeconds(seconds);
        }
    }

}

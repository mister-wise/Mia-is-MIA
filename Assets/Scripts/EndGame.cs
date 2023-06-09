using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EndGame : MonoBehaviour
{
    [SerializeField] private Image endImage;
    [SerializeField] private TMP_Text endText;

    [SerializeField] private Sprite Image1;
    [SerializeField] private Sprite Image2;
    [SerializeField] private Sprite Image3;

    [SerializeField] private string A1Alone;
    [SerializeField] private string A1Police;
    [SerializeField] private string A1Frank;
    [SerializeField] private string A0Alone;
    [SerializeField] private string A0Police;
    [SerializeField] private string A0Frank;
    [SerializeField] private string B1Alone;
    [SerializeField] private string B1Police;
    [SerializeField] private string B1Frank;
    [SerializeField] private string B0Alone;
    [SerializeField] private string B0Police;
    [SerializeField] private string B0Frank;
    
    [SerializeField] private Sprite outOfTimeImage;
    [SerializeField] private string outOfTimeText;
    
    public void SetEnding(bool correctWhere, bool correctWho, bool correctWhy, int with = 0, bool outOfTime = false)
    {
        string textA;
        string textB;
        
        // Image logic here!
        if (outOfTime)
        {
            endImage.sprite = outOfTimeImage;
            endText.text = outOfTimeText;
            gameObject.SetActive(true);
            return;
        } 
        
        
        if (correctWho && correctWhere)
        {
            endImage.sprite = Image1;
        } else
        if (correctWho || correctWhere)
        {
            endImage.sprite = Image2;
        } else
        {
            endImage.sprite = Image3;
        }


        if (correctWhere)
        {
            textA = with switch
            {
                1 => A1Police,
                2 => A1Frank,
                _ => A1Alone
            };
        }
        else
        {
            textA = with switch
            {
                1 => A0Police,
                2 => A0Frank,
                _ => A0Alone
            };
        }

        if (correctWho)
        {
            textB = with switch
            {
                1 => B1Police,
                2 => B1Frank,
                _ => B1Alone
            };
        }
        else
        {
            textB = with switch
            {
                1 => B0Police,
                2 => B0Frank,
                _ => B0Alone
            };
        }

        endText.text = $"{textA} {textB}";
        
        gameObject.SetActive(true);
    }
}
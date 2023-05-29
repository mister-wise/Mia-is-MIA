using System.Collections;
using System.Collections.Generic;
using Phone;
using UnityEngine;

public class Intro : MonoBehaviour
{

    void Start()
    {
        StartCoroutine(PlayIntro());
    }

    IEnumerator PlayIntro()
    {
        yield return new WaitForSeconds(3);
        PhoneController.Instance.AddNotification(new Notification(NotificationType.MissedCall, "Sister", $"Missed call (3)"));
    }
}

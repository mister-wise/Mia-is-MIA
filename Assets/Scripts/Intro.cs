using System.Collections;
using System.Collections.Generic;
using Phone;
using Phone.Notifications;
using SODefinitions;
using UnityEngine;

public class Intro : MonoBehaviour
{
    [SerializeField] private ContactSO miaContact;
    [SerializeField] private ContactSO frankContact;

    void Start()
    {
        PhoneController.Instance.AddCallToHistory(miaContact, RecentCallStatus.Outgoing, 2);
        PhoneController.Instance.AddCallToHistory(frankContact, RecentCallStatus.Incoming);
        PhoneController.Instance.AddCallToHistory(miaContact, RecentCallStatus.Missed, 5);
        StartCoroutine(PlayIntro());
    }

    IEnumerator PlayIntro()
    {
        yield return new WaitForSeconds(3);
        PhoneController.Instance.ReceiveMessage(new Message(miaContact, "The owner of this number is available again",
            false, GameManager.Instance.GetGameTime()));
    }
}
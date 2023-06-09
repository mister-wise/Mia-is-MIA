using System.Collections;
using System.Collections.Generic;
using Phone;
using Phone.Notifications;
using SODefinitions;
using UnityEngine;

public class Intro : MonoBehaviour
{
    [SerializeField] private ContactSO miaContact;

    void Start()
    {
        StartCoroutine(PlayIntro());
    }

    IEnumerator PlayIntro()
    {
        yield return new WaitForSeconds(3);
        PhoneController.Instance.ReceiveMessage(new Message(miaContact, "The owner of this number is available again",
            false, GameManager.Instance.GetGameTime()));
    }
}
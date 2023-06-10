using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Net.Mail;
using UnityEngine;
using UnityEngine.UI;

public class SendReport : MonoBehaviour
{
    public static SendReport instance;
    string From = "game@aemaze.studio";
    string Name = "Aemaze";
    public string To = "game.log@aemaze.studio";
    public string Subject = "Game Log";
    public string Message = "Game report log";
    
    public string AttachmentFilename = "logo.jpg";

    public void Start() 
    {
        instance = this;
    }

    public void SendPlainMail()
    {
        MailSingleton.Instance.SendPlainMail(
            From, 
            Name, 
            To, 
            Subject, 
            Message
        );
    }

    public void SetAndSendMessage (bool correctWhere, bool correctWho, bool correctWhy, int with, bool outOfTime)
    {
        Subject = "Raport ukonczenia gry "  ; 
        Message =  "Gracz  wybrał opcje: miejsce " + correctWhere + ", podejrzany: " +correctWho + ", motyw: " + correctWhy + ", razem z: " + with + ", i czas skończył się: " + outOfTime + ", a zrobił to o:  " + System.DateTime.Now; 
        SendPlainMail();
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using Phone;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    
    public static int ClickCost = 1;
    public static int EventCost = 50;

    [SerializeField] private TimeTriggerMessage[] timeTriggerMessages;
    
    [SerializeField] private string startStringTime;
    [SerializeField] private int gameHoursLimit = 8;

    [SerializeField] private float currentPoint = 0;
    [SerializeField] private int maxCurrentPoint = 0;
    [SerializeField] private int maxPoint = 500;
    [SerializeField] private float increasePointSpeed = 0.2f;

    private DateTime startTime;
    private DateTime endTime;
    private DateTime currentTime;
    
    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        startTime = DateTime.ParseExact(startStringTime, "MM/dd/yyyy HH:mm", null);
        endTime = startTime.AddHours(gameHoursLimit);
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            IncreaseGameTime(ClickCost);
        }

        HandleGameTime();

        CheckForTimeTriggerMessage();
    }

    private void CheckForTimeTriggerMessage()
    {
        for (var i = 0; i < timeTriggerMessages.Length; i++)
        {
            if (timeTriggerMessages[i].Points <= currentPoint && !timeTriggerMessages[i].Send)
            {
                PhoneController.Instance.ReceiveMessage(timeTriggerMessages[i].Message);
                timeTriggerMessages[i].Send = true;
            }
        }
    }

    public DateTime GetGameTime() => LerpDateTime(startTime, endTime, (float)currentPoint / maxPoint);

    public void IncreaseGameTime(int point = 1)
    {
        if(maxCurrentPoint < maxPoint)
            maxCurrentPoint += point;
    }
    
    private void HandleGameTime()
    {
        if(currentPoint == maxCurrentPoint) return;
        currentPoint += increasePointSpeed * Time.deltaTime;
        if (currentPoint > maxCurrentPoint)
        {
            currentPoint = maxCurrentPoint;
        }
            
        Notebook.Instance.CheckForResultSectionAvailable();
        CheckForEndGame();
    }

    private void CheckForEndGame()
    {
        if (currentPoint >= maxPoint)
        {
            EndGame(false, false, false);
        }
    }

    public void EndGame(bool correctWhere, bool correctWho, bool correctWhy)
    {
        throw new NotImplementedException();
    }

    private static DateTime LerpDateTime(DateTime startDateTime, DateTime endDateTime, double t)
    {
        var timeSpan = endDateTime - startDateTime;
        var lerpedTicks = (long)(timeSpan.Ticks * t);

        var interpolatedTimeSpan = TimeSpan.FromTicks(lerpedTicks);

        return startDateTime.Add(interpolatedTimeSpan);
    }

    public bool IsAfterMidnight()
    {
        return currentPoint >= 334;
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Globalization;
using UnityEngine.UI;

public class TimeBonusScript : MonoBehaviour
{
    // This script should probably be attached to Intro Scene where everything goes to shit. idk

    public static bool quizReady = true;
    public static bool invasionReady = true;

    public static bool dailyBonusReady;

    public Text quizTimerText;
    public Text invasionTimerText;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("CheckQuizReady", 1, 1);
        InvokeRepeating("CheckInvasionReady", 1, 1);
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void DailyBonus()
    {
        DateTime currentDate = DateTime.UtcNow;

    }

    // What I'm going to do: Check current time. Check when quiz ready (but this has to be done someplace else). 
    // If quiz ready before current time, then glow quiz. 
    // Problem: How do I set the time that quiz is ready. 
    public void CheckQuizReady()
    {
        DateTime currentDate = DateTime.UtcNow;
        if (currentDate < QuizOverScript.quizReadyTime)
        {
            TimeSpan diff = QuizOverScript.quizReadyTime - currentDate;
            float totalTime = Convert.ToSingle(diff.TotalSeconds);
            float totalHours = Mathf.FloorToInt(totalTime / 3600);
            float totalMinutes = Mathf.FloorToInt((totalTime % 3600) / 60);
            float totalSeconds = Mathf.FloorToInt(totalTime % 60);
            quizTimerText.text = string.Format("{0:00}:{1:00}:{2:00}", totalHours, totalMinutes, totalSeconds);
            quizReady = false;
        }
        else
        {
            quizTimerText.text = "Ready!";
            quizReady = true;
        }
    }


    public void CheckInvasionReady()
    {
        DateTime currentDate = DateTime.UtcNow;
        if (DataAcrossScenes.numIons < 5)
        {
            invasionTimerText.text = "Trial";
            invasionReady = false;
        }
        else if (currentDate < InvasionSceneManager.invasionReadyTime)
        {
            TimeSpan diff = InvasionSceneManager.invasionReadyTime - currentDate;
            float totalTime = Convert.ToSingle(diff.TotalSeconds);
            float totalHours = Mathf.FloorToInt(totalTime / 3600);
            float totalMinutes = Mathf.FloorToInt((totalTime % 3600) / 60);
            float totalSeconds = Mathf.FloorToInt(totalTime % 60);
            invasionTimerText.text = string.Format("{0:00}:{1:00}:{2:00}", totalHours, totalMinutes, totalSeconds);
            invasionReady = false;
        }
        else
        {
            invasionTimerText.text = "Ready!";
            invasionReady = true;
            Debug.Log("InvasionReady is true!");
        }
    }
}

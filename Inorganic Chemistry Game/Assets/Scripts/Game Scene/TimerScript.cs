using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Linq;

public class TimerScript : MonoBehaviour
{

    public Text timerText;
    private float timeRemaining;
    public static float startingTime = 60;
    public static bool timerActive = false;
    private float totalTime;

    private float minutes;
    private float seconds;

    public GameObject timeUp;

    // Start is called before the first frame update
    void Start()
    {
        totalTime = startingTime + DataAcrossScenes.additionalTime;
        timeRemaining = totalTime;
        DisplayTime(timeRemaining);
        
    }

    // Update is called once per frame
    void Update()
    {
        if (timerActive)
        {
            timeRemaining -= Time.deltaTime;
            DisplayTime(timeRemaining);
        }
        if (timeRemaining <= 0 && timerActive == true)
        {
            timerActive = false;
            Debug.Log("Time's up!");
            Instantiate(timeUp);
        }

        
    }



    private void DisplayTime(float timeToDisplay)
    {
        timeToDisplay += 1;
        minutes = Mathf.FloorToInt(timeToDisplay / 60);
        seconds = Mathf.FloorToInt(timeToDisplay % 60);
        if (timeToDisplay == totalTime + 1)
        {
            seconds--;
        }
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);


    }


    //private void EndOfGame()
    //{
    //    IonSpawner.spawnIons = false;
    //    foreach (GameObject ion in IonSpawner.ionsOnScreen)
    //    {
    //        Destroy(ion);
    //        Debug.Log("One ion destroyed");
    //    }
    //    IonSpawner.ionsOnScreen.Clear();
    //    Debug.Log("ionsOnScreen cleared");
    //    // Not sure if this needed
    //    foreach (Vector3 keys in IonSpawner.positions.Keys.ToList())
    //    {
    //        IonSpawner.positions[keys] = true;
    //    }
    //    IonSpawner.positions.Clear();
    //    IonSpawner.positionsForChecking.Clear();
    //    IonSpawner.numberOfCorrectCompounds = 0;


    //    // The basic stats except totalAddedScore
    //    IonSceneManager.allIons.Clear();
    //    IonSceneManager.totalCharge = 0;
    //    IonSceneManager.typesOfIons = 0;
    //    IonSceneManager.hcf = 0;
    //    IonSceneManager.numberOfIons.Clear();
    //    IonSceneManager.destroyIonsList.Clear();
    //    IonSceneManager.streak = 0;

    //    // The streaks
    //    IonSceneManager.streakMultiplier = 1;
    //    IonSceneManager.quizMultiplier = 1;
    //    IonSceneManager.powerUpSuperMultiplier = 1;
    //    IonSceneManager.powerUpMultiMultiplier = 1;
    //    IonSceneManager.numIonsMultiplier = 1;

    //}
}

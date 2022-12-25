using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TimeManager : MonoBehaviour
{
    //public float slowDownFactor = 0.25f;
    //public float slowDownLength = 2f;

    public static bool timeSlowed;

    public Text timerText;
    private float timeRemaining;
    public static float startingTime = 180;

    private float minutes;
    private float seconds;

    public static bool timerActive;
    public static bool timesUp = false;

    public GameObject quitMenu;



    private void Start()
    {
        timeRemaining = startingTime;
        DisplayTime(timeRemaining);
        timerActive = true;
    }


    // Update is called once per frame
    // Runs the timer. Takes into account the various times time is slowed, or when time runs out. 
    void Update()
    {
        if (Time.timeScale < 1 && timeSlowed == true)
        {
            timeSlowed = false;
            StartCoroutine("WaitBeforeResetTime");
        }

        if (timerActive)
        {
            timeRemaining -= Time.deltaTime;
            Debug.Log("Hello there, timeManager");
            DisplayTime(timeRemaining);
        }

        if (timeRemaining <= 0 && timerActive)
        {
            timerActive = false;
            timesUp = true;
            
        }

    }

    IEnumerator WaitBeforeResetTime()
    {
        yield return new WaitForSeconds(0.3f);
        Time.timeScale = 1f;
        Time.fixedDeltaTime = Time.timeScale * 0.02f;
    }

    
    private void DisplayTime(float timeToDisplay)
    {
        timeToDisplay += 1;
        minutes = Mathf.FloorToInt(timeToDisplay / 60);
        seconds = Mathf.FloorToInt(timeToDisplay % 60);
        if (timeToDisplay == startingTime + 1)
        {
            seconds--;
        }
        timerText.text = string.Format("{0:0}:{1:00}", minutes, seconds);
    }


    // The following are functions for the pause menu 
    public void QuitGame()
    {
        quitMenu.SetActive(true);
    }

    public void ContinueGame()
    {
        quitMenu.SetActive(false);
    }

    // This is also used to quit game after game end, so I don't have to duplicate this function again. 
    public void TrulyQuitGame()
    {
        SceneManager.LoadScene("Intro Scene");
    }

    public void ReplayGame()
    {
        SceneManager.LoadScene("Invasion Scene");
    }
}

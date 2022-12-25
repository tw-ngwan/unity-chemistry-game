using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuizTimerScript : MonoBehaviour
{
    [SerializeField]
    public Text timerText;
    public GameObject quizCanvas;
    private float timeRemaining;
    public static bool timerActive = false;
    private float minutes;
    private float seconds;

    // Start is called before the first frame update
    void Start()
    {
        timerActive = true;
        timeRemaining = 30;
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
            Destroy(GameObject.FindGameObjectWithTag("QuizPanel"));
            timerActive = false;
            QuizContinueManager.wrongAnswers += 1;
            QuizManager.quizSolved = true;
        }


    }



    private void DisplayTime(float timeToDisplay)
    {
        timeToDisplay += 1;
        minutes = Mathf.FloorToInt(timeToDisplay / 60);
        seconds = Mathf.FloorToInt(timeToDisplay % 60);

        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}

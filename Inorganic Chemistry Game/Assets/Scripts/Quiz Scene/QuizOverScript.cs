using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;
using System.Globalization;

public class QuizOverScript : MonoBehaviour
{
    public Text quizGold;
    public Text quizScore;
    public Animator transition;

    public static DateTime quizReadyTime;
    // Start is called before the first frame update
    void Start()
    {
        if (TimeBonusScript.quizReady)
        {
            quizGold.text = string.Format("{0} gold obtained!", QuizContinueManager.gold);
            DataAcrossScenes.totalMoney += QuizContinueManager.gold;
            quizReadyTime = DateTime.UtcNow.AddHours(8);
            TimeBonusScript.quizReady = false;
        }
        else
        {
            quizGold.text = "Practice Round Over!";
        }
        quizScore.text = string.Format("{0} questions solved!", QuizContinueManager.round);
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void PlayAgain()
    {
        SceneManager.LoadScene("Quiz Scene");
    }

    public void QuitGame()
    {
        StartCoroutine("QuitGameSceneChange");
    }

    IEnumerator QuitGameSceneChange()
    {
        transition.SetTrigger("Start");
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene("Intro Scene");
    }
}

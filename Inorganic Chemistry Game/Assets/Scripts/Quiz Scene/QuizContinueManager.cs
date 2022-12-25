using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class QuizContinueManager : MonoBehaviour
{

    public GameObject quizPanel;
    public GameObject quizCanvas;

    public GameObject quizOver;
    public GameObject practiceGame;

    public Text carryOnQuestion;
    public static int round;
    public static int gold;
    private int firstRoundGold = 10;

    public static int wrongAnswers;
    private bool quizOverSpawned = false;

    // Start is called before the first frame update
    void Start()
    {
        round = 0;
        wrongAnswers = 0;
        quizOverSpawned = false;
        if (TimeBonusScript.quizReady)
        {
            practiceGame.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        gold = (int)Math.Round(firstRoundGold * (Math.Pow(2, (round - 1))));
        carryOnQuestion.text = string.Format(@"Round {0} / 8 cleared. Current gold: {1}
Each correct answer doubles your current score. However, 2 incorrect answers, and you get nothing.
Do you wish to continue?",
        round, gold);

        // These don't work, it spawns a whole hecklot of them
        if (round >= 8 && quizOverSpawned == false)
        {
            StartCoroutine("SpawnQuizOver");
        }

        else if (wrongAnswers >= 2 && quizOverSpawned == false)
        {
            gold = 0;
            StartCoroutine("SpawnQuizOver");
        }
    }

    public void YesCarryOn()
    {
        Debug.Log("Button pressed");
        Instantiate(quizPanel);
        Instantiate(quizCanvas, new Vector3(0, -10, 0), transform.rotation);
    }

    public void NoQuit()
    {
        Instantiate(quizOver);
    }

    IEnumerator SpawnQuizOver()
    {
        quizOverSpawned = true;
        Instantiate(quizPanel);
        yield return new WaitForSeconds(2f);

        Instantiate(quizOver);
    }
}

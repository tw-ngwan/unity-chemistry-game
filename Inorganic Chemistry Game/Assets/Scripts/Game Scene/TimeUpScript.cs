using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Linq;
using System;

public class TimeUpScript : MonoBehaviour
{

    public Text ScoreText;
    public Animator transition;

    // Start is called before the first frame update
    void Start()
    {
        IonSceneManager.currentMoney = (int)Math.Round((double)IonSceneManager.totalAddedScore * DataAcrossScenes.moneyMultiplier);
        ScoreText.text = string.Format("Total Score: {0}", IonSceneManager.totalAddedScore);
        MoneyCounterScript.moneyAdded = false;
        if (HighScoreDisplayScript.newHighScore < IonSceneManager.totalAddedScore)
        {
            HighScoreDisplayScript.newHighScore = IonSceneManager.totalAddedScore;
        }
        EndOfGame();
        // Hot tip: Maybe you want to bring EndOfGame() here
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayAgain()
    {
        SceneManager.LoadScene("Game Scene");
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


    private void EndOfGame()
    {
        IonSpawner.spawnIons = false;
        foreach (GameObject ion in IonSpawner.ionsOnScreen)
        {
            Destroy(ion);
            Debug.Log("One ion destroyed");
        }
        IonSpawner.ionsOnScreen.Clear();
        Debug.Log("ionsOnScreen cleared");
        // Not sure if this needed
        foreach (Vector3 keys in IonSpawner.positions.Keys.ToList())
        {
            IonSpawner.positions[keys] = true;
        }
        IonSpawner.positions.Clear();
        IonSpawner.positionsForChecking.Clear();
        IonSpawner.numberOfCorrectCompounds = 0;


        // The basic stats except totalAddedScore
        IonSceneManager.allIons.Clear();
        IonSceneManager.totalCharge = 0;
        IonSceneManager.typesOfIons = 0;
        IonSceneManager.hcf = 0;
        IonSceneManager.numberOfIons.Clear();
        IonSceneManager.destroyIonsList.Clear();
        IonSceneManager.streak = 0;
        IonSceneManager.totalAddedScore = 0;
        IonSceneManager.totalScore = 0;

        // The streaks
        IonSceneManager.streakMultiplier = 1;
        IonSceneManager.quizMultiplier = 1;
        //DataAcrossScenes.powerUpSuperMultiplier = 1;
        //DataAcrossScenes.powerUpMultiMultiplier = 1;
        IonSceneManager.numIonsMultiplier = 1;

        // QuizManager 
        QuizManager.quizScore = 0;
        QuizManager.quizSolved = false;

        IonSceneManager.quizSpawned = false;


    }
}

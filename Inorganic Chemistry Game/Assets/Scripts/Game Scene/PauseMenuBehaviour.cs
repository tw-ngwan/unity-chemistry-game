using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class PauseMenuBehaviour : MonoBehaviour
{

    public GameObject pausePanel;
    public GameObject timeUp;
    public Text scoreText;

    // Start is called before the first frame update
    void Start()
    {
        scoreText.text = string.Format("Current Score: {0}", IonSceneManager.totalAddedScore);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        scoreText.text = string.Format("Current Score: {0}", IonSceneManager.totalAddedScore);
    }

    public void InvokePause()
    {
        pausePanel.SetActive(true);
        TimerScript.timerActive = false;
    }

    public void InvokeContinue()
    {
        pausePanel.SetActive(false);
        TimerScript.timerActive = true;
    }

    public void InvokeQuit()
    {
        Instantiate(timeUp);
        pausePanel.SetActive(false);
    }
}

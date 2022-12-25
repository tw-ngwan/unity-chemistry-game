using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HelpMenuManager : MonoBehaviour
{

    public GameObject helpMenu;

    public GameObject gameMode;
    public GameObject quizMode;
    public GameObject invasionMode;
    public GameObject topicSelection;
    public GameObject credits;

    [SerializeField]
    public GameObject[] checkboxes;

    public GameObject quizQuestion;

    // To set the topics active, refer to DataAcrossScenes


    // Start is called before the first frame update
    void Start()
    {
        ReloadActiveTopics();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DisplayHelpMenu()
    {
        helpMenu.SetActive(true);
    }

    public void CloseHelpMenu()
    {
        helpMenu.SetActive(false);
    }


    public void DisplayGameMode()
    {
        gameMode.SetActive(!gameMode.activeSelf);
    }

    public void DisplayQuizMode()
    {
        quizMode.SetActive(!quizMode.activeSelf);
    }

    public void DisplayInvasionMode()
    {
        invasionMode.SetActive(!invasionMode.activeSelf);
    }

    public void DisplayTopicSelection()
    {
        topicSelection.SetActive(!topicSelection.activeSelf);
    }

    public void DisplayCredits()
    {
        credits.SetActive(!credits.activeSelf);
    }

    public void ReloadActiveTopics()
    {
        DataAcrossScenes.numInactiveTopics = 0;
        QuizManager quizManager = quizQuestion.GetComponentInChildren<QuizManager>();
        for (int i = 0; i < 12; i++)
        {
            Toggle toggle = checkboxes[i].GetComponent<Toggle>();
            if (quizManager.topics[i].topicActive == false)
            {
                toggle.isOn = false;
            }

            else if (quizManager.topics[i].topicActive == true)
            {
                toggle.isOn = true;
            }

        }
    }

    public void SetTopicsActive(int i)
    {
        Toggle toggle = checkboxes[i].GetComponent<Toggle>();
        QuizManager quizManager = quizQuestion.GetComponentInChildren<QuizManager>();
        // Want to find a way to reference the toggle on self
        if (!toggle.isOn)
        {
            DataAcrossScenes.numInactiveTopics += 1;
            Debug.Log("numInactiveTopics is " + DataAcrossScenes.numInactiveTopics);
            quizManager.topics[i].topicActive = false;
        }

        else if (toggle.isOn)
        {
            DataAcrossScenes.numInactiveTopics -= 1;
            Debug.Log("numInactiveTopics is " + DataAcrossScenes.numInactiveTopics);
            quizManager.topics[i].topicActive = true;
        }


    }


}

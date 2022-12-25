using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{

    public Animator transition;

    // To ask user to select more topics 
    public GameObject notEnoughTopics;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayGame()
    {
        if (DataAcrossScenes.numInactiveTopics <= 9)
        {
            StartCoroutine("PlayGameSceneTransition");
        }
        else
        {
            StartCoroutine("NotEnoughTopicsController");
        }
        
    }

    public void QuizGame()
    {
        if (DataAcrossScenes.numInactiveTopics <= 9)
        {
            StartCoroutine("QuizGameSceneTransition");
        }
        else
        {
            StartCoroutine("NotEnoughTopicsController");
        }
        
    }

    public void GoToShop()
    {
        StartCoroutine("ShopSceneTransition");
    }

    public void GoToIntro()
    {
        StartCoroutine("IntroSceneTransition");
    }

    public void GoToIons()
    {
        StartCoroutine("IonsSceneTransition");
    }

    public void GoToIonBot()
    {
        StartCoroutine("IonBotSceneTransition");
    }

    public void GoToInvasion()
    {
        StartCoroutine("InvasionSceneTransition");
    }


    IEnumerator PlayGameSceneTransition()
    {
        transition.SetTrigger("Start");
        yield return new WaitForSeconds(0.8f);

        SceneManager.LoadScene("Game Scene");
    }

    IEnumerator QuizGameSceneTransition()
    {
        transition.SetTrigger("Start");
        yield return new WaitForSeconds(1f);

        SceneManager.LoadScene("Quiz Scene");
        
    }

    IEnumerator ShopSceneTransition()
    {
        transition.SetTrigger("Start");
        yield return new WaitForSeconds(1f);

        SceneManager.LoadScene("Shop Scene");
    }

    IEnumerator IntroSceneTransition()
    {
        transition.SetTrigger("Start");
        yield return new WaitForSeconds(1f);

        SceneManager.LoadScene("Intro Scene");
    }

    IEnumerator IonsSceneTransition()
    {
        transition.SetTrigger("Start");
        yield return new WaitForSeconds(1f);

        SceneManager.LoadScene("Ions Scene");
    }

    IEnumerator IonBotSceneTransition()
    {
        transition.SetTrigger("Start");
        yield return new WaitForSeconds(1f);

        SceneManager.LoadScene("IonBot Scene");
    }

    IEnumerator InvasionSceneTransition()
    {
        transition.SetTrigger("Start");
        yield return new WaitForSeconds(1f);

        SceneManager.LoadScene("Invasion Scene");
    }


    IEnumerator NotEnoughTopicsController()
    {
        notEnoughTopics.SetActive(true);
        yield return new WaitForSeconds(2f);

        notEnoughTopics.SetActive(false);
    }


} //class 

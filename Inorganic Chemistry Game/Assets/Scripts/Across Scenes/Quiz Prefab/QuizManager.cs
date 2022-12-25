using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;
using System;
using UnityEngine.EventSystems;
using UnityEngine.Experimental.Rendering.Universal;
using TMPro;

public class QuizManager : MonoBehaviour
{
    public GameObject quizCanvas;
    public Question[] questions;
    public Topics[] topics;

    public static List<Question> unansweredQuestions = new List<Question>();

    private Question currentQuestion;
    private int randomQuestionIndex;

    public TMP_Text questionText;
    public List<Text> optionsText = new List<Text>();
    private List<int> indexList = new List<int>();

    // Quizscore each time 
    public static int quizScore = 0;

    // Sliding quizCanvas in
    public static bool quizSolved = false;
    private Vector3 slidingSpeed = new Vector3(0, 20, 0);
    private Vector3 slowSlidingSpeed = new Vector3(0, 4, 0);
    private Vector3 finalSlidingSpeed = new Vector3(0, 10, 0);

    // Number of attempts to figure out what score to give
    private int numberOfAttempts = 0;

    // Popping up the congratulatory phrase or something 
    private List<GameObject> activeCongratsPhrases = new List<GameObject>();
    public List<GameObject> inactiveCongratsPhrases = new List<GameObject>();

    private List<GameObject> activeIncorrectPhrases = new List<GameObject>();
    public List<GameObject> inactiveIncorrectPhrases = new List<GameObject>();

    // To destroy quiz
    public Light2D destructionLight;
    public int lightIntensitySpeed = 16;
    public GameObject destroyPanel;

    // To add the question for explanation in QuizExplanationDisplay
    public static List<Question> answeredQuestions = new List<Question>();

    private void Start()
    {
        quizSolved = false;
        numberOfAttempts = 0;
        // Load everything from questions into unansweredQuestions
        if (unansweredQuestions.Count == 0)
        {
            foreach(Topics topic in topics)
            {
                if (topic.topicActive == true)
                {
                    foreach (Question question in topic.questions)
                    {
                        unansweredQuestions.Add(question);
                    }
                }
            }
            //unansweredQuestions = questions.ToList<Question>();
        }
        GetRandomQuestion();
        Debug.Log(currentQuestion.question);
    }

    private void Update()
    {
        if (quizCanvas.transform.position.y < -4 && quizSolved == false)
        {
            quizCanvas.transform.position += slidingSpeed * Time.deltaTime;
        }
        else if (quizCanvas.transform.position.y < -2 && quizSolved == false)
        {
            quizCanvas.transform.position += slowSlidingSpeed * Time.deltaTime;
        }
        else if (quizCanvas.transform.position.y < 0 && quizSolved == false)
        {
            quizCanvas.transform.position += finalSlidingSpeed * Time.deltaTime;
        }
        else if (quizSolved == false)
        {
            QuizTimerScript.timerActive = true;
        }
        else if (destructionLight.intensity >= 8 && quizSolved == true)
        {
            Destroy(quizCanvas);
            TimerScript.timerActive = true;
        }
        else if (quizSolved == true)
        {
            destructionLight.intensity += lightIntensitySpeed * Time.deltaTime;
        }

    }


    private void GetRandomQuestion()
    {
        //randomTopicIndex = UnityEngine.Random.Range(0, topics.Count());
        randomQuestionIndex = UnityEngine.Random.Range(0, unansweredQuestions.Count);
        currentQuestion = unansweredQuestions[randomQuestionIndex];
        answeredQuestions.Add(currentQuestion);

        questionText.text = currentQuestion.question;

        currentQuestion.shuffledOptions = currentQuestion.options.ToList();

        for (int i = 0; i < 4; i++)
        {
            currentQuestion.optionIndex = UnityEngine.Random.Range(0, currentQuestion.shuffledOptions.Count);
            Debug.Log(i + ": " + currentQuestion.shuffledOptions[currentQuestion.optionIndex].option);
            optionsText[i].text = currentQuestion.shuffledOptions[currentQuestion.optionIndex].option;
            currentQuestion.shuffledOptions.Remove(currentQuestion.shuffledOptions[currentQuestion.optionIndex]);
        }

        unansweredQuestions.RemoveAt(randomQuestionIndex);
    }


    public void OnClickUserSelectCorrect(Button button)
    {
        StartCoroutine(OnClickUserSelect(button));
    }


    private IEnumerator OnClickUserSelect(Button button)
    {
        Image img = button.gameObject.GetComponent<Image>();
        ColorBlock cb = button.colors;
        if (button.GetComponentInChildren<Text>().text == currentQuestion.options[0].option && numberOfAttempts == 0) // Correct, first try
        {
            img.color = Color.green;
            cb.normalColor = Color.green;
            cb.highlightedColor = Color.green;
            cb.pressedColor = Color.green;
            button.colors = cb;
            Debug.Log(button.GetComponent<Image>().color);
            Debug.Log("Button is green");
            quizScore += 500;
            IonSceneManager.totalAddedScore += quizScore;
            IonSceneManager.quizMultiplier *= 1.2f;
            //Debug.Log("CORRECT!");
            Destroy(GameObject.FindGameObjectWithTag("QuizPanel"));
            //Destroy(quizCanvas);
            QuizTimerScript.timerActive = false;
            destroyPanel.SetActive(true);

            QuizContinueManager.round += 1; // This is for quizContinueManager

            // To instantiate congratulatory phrase
            // TODO: You need to look at order of forming this. It seems like you're doing this after the whole panel is destroyed
            // Also you need to set it back to inactive ya 
            int randPhraseIndex = UnityEngine.Random.Range(0, inactiveCongratsPhrases.Count);
            inactiveCongratsPhrases[randPhraseIndex].SetActive(true);
            activeCongratsPhrases.Add(inactiveCongratsPhrases[randPhraseIndex]);
            inactiveCongratsPhrases.Remove(inactiveCongratsPhrases[randPhraseIndex]);

            yield return new WaitForSeconds(1f);
            ClearActivePhrases();
            quizSolved = true;

        }
        else if (button.GetComponentInChildren<Text>().text == currentQuestion.options[0].option && numberOfAttempts == 1) // Correct, second try
        {
            img.color = Color.green;
            cb.normalColor = Color.green;
            cb.highlightedColor = Color.green;
            cb.pressedColor = Color.green;
            button.colors = cb;
            IonSceneManager.totalAddedScore += 250;
            IonSceneManager.quizMultiplier *= 1.2f;
            //Debug.Log("CORRECT!");
            Destroy(GameObject.FindGameObjectWithTag("QuizPanel"));
            QuizTimerScript.timerActive = false;
            destroyPanel.SetActive(true);

            QuizContinueManager.round += 1;

            int randPhraseIndex = UnityEngine.Random.Range(0, inactiveCongratsPhrases.Count);
            inactiveCongratsPhrases[randPhraseIndex].SetActive(true);
            activeCongratsPhrases.Add(inactiveCongratsPhrases[randPhraseIndex]);
            inactiveCongratsPhrases.Remove(inactiveCongratsPhrases[randPhraseIndex]);

            yield return new WaitForSeconds(1f);
            ClearActivePhrases();
            quizSolved = true;

        }
        else if (numberOfAttempts == 1) // Incorrect, second try
        {
            img.color = Color.red;
            cb.normalColor = Color.red;
            cb.highlightedColor = Color.red;
            cb.pressedColor = Color.red;
            button.colors = cb;
            Destroy(GameObject.FindGameObjectWithTag("QuizPanel"));
            QuizTimerScript.timerActive = false;
            destroyPanel.SetActive(true);

            QuizContinueManager.wrongAnswers += 1;

            // Here the way to set it back to inactive is a bit different isn't it... it must appear, then fade out or something 
            int randPhraseIndex = UnityEngine.Random.Range(0, inactiveIncorrectPhrases.Count);
            inactiveIncorrectPhrases[randPhraseIndex].SetActive(true);
            activeIncorrectPhrases.Add(inactiveIncorrectPhrases[randPhraseIndex]);
            inactiveIncorrectPhrases.Remove(inactiveIncorrectPhrases[randPhraseIndex]);

            yield return new WaitForSeconds(1f);
            ClearActivePhrases();
            quizSolved = true;

        }
        else // Incorrect, first try
        {
            img.color = Color.red;
            cb.normalColor = Color.red;
            cb.highlightedColor = Color.red;
            cb.pressedColor = Color.red;
            button.colors = cb;
            Debug.Log(button.GetComponent<Image>().color);
            numberOfAttempts += 1;

            int randPhraseIndex = UnityEngine.Random.Range(0, inactiveIncorrectPhrases.Count);
            inactiveIncorrectPhrases[randPhraseIndex].SetActive(true);
            activeIncorrectPhrases.Add(inactiveIncorrectPhrases[randPhraseIndex]);
            inactiveIncorrectPhrases.Remove(inactiveIncorrectPhrases[randPhraseIndex]);

        }

    }


    private void ClearActivePhrases()
    {
        foreach (GameObject phrase in activeCongratsPhrases)
        {
            phrase.SetActive(false);
            inactiveCongratsPhrases.Add(phrase);
        }
        activeCongratsPhrases.Clear();

        foreach (GameObject phrase in activeIncorrectPhrases)
        {
            phrase.SetActive(false);
            inactiveIncorrectPhrases.Add(phrase);
        }
        activeIncorrectPhrases.Clear();

    }
 



    // You may want to add a function here to make the topics active or inactive, based on something, idk. 
    // Maybe for the array of topics, create like 20 elements in the help menu or something, correspond the index of each element to one of them. 
    // So can set inactive using that? Idk. Maybe refer to character selection in monster chase
    // But also need to prevent people cheating the system, so must check no. of active topics. Must be at least 3
    //void Trial(int elem)
    //{
    //    topics[elem].topicActive = false;
    //}
}




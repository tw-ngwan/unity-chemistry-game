using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class QuizExplanationDisplay : MonoBehaviour
{

    public List<GameObject> questionFields = new List<GameObject>();

    public GameObject quizReview;


    // Start is called before the first frame update
    void Start()
    {
        foreach (GameObject questionField in questionFields)
        {
            questionField.SetActive(false);
        }

        if (QuizManager.answeredQuestions.Count >= 1)
        {
            for (int i = 0; i < QuizManager.answeredQuestions.Count; i++)
            {
                questionFields[i].SetActive(true);
                DisplayQuestionExplanation(i);
            }
        }

        QuizManager.answeredQuestions.Clear();
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    void DisplayQuestionExplanation(int questionFieldIndex)
    {
        GameObject questionField = questionFields[questionFieldIndex];
        TMP_Text questionText = questionField.gameObject.transform.GetChild(0).gameObject.GetComponent<TMP_Text>();
        TMP_Text questionAnswer = questionField.gameObject.transform.GetChild(1).gameObject.GetComponent<TMP_Text>();
        TMP_Text questionExplanation = questionField.gameObject.transform.GetChild(2).gameObject.GetComponent<TMP_Text>();

        questionText.text = QuizManager.answeredQuestions[questionFieldIndex].question;
        questionAnswer.text = QuizManager.answeredQuestions[questionFieldIndex].options[0].option;
        questionExplanation.text = QuizManager.answeredQuestions[questionFieldIndex].explanation;

    }


    public void OpenQuizReview()
    {
        quizReview.SetActive(true);
    }

    public void CloseQuizReview()
    {
        quizReview.SetActive(false);
    }
}

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;



public class Option<T>
{
    public List<T> option;
}

[System.Serializable]
public class Question 
{
    public string question;
    public Answer[] options;
    [HideInInspector]
    public List<Answer> shuffledOptions = new List<Answer>();
    //public bool isTrue;
    [HideInInspector]
    public int optionIndex;

    public string explanation;


}



[System.Serializable]
public class Answer
{
    public string option;
    public bool isCorrect;
}

[System.Serializable]
public class Topics
{
    public string topic;
    public bool topicActive = true;
    public List<Question> questions = new List<Question>();
}
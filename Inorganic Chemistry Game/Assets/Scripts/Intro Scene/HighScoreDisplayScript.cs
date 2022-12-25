using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HighScoreDisplayScript : MonoBehaviour
{
    private Text highScoreText;
    public static int newHighScore;


    // Start is called before the first frame update
    void Start()
    {
        highScoreText = GetComponent<Text>();
        highScoreText.text = newHighScore.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

}

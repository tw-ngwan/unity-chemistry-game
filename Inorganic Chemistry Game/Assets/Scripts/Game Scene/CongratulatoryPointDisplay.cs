using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CongratulatoryPointDisplay : MonoBehaviour
{

    public GameObject congratulatoryTextWrapper;
    public Text pointsText;
    private List<string> congratulatoryPhrases = new List<string>() { "Good job!", "Sweet!", "Spot on!", "Excellent!", "Well done!", "Bingo!", "Awesome!", "Congrats!" };

    // Start is called before the first frame update
    void Start()
    {
        int phraseIndex = Random.Range(0, congratulatoryPhrases.Count);
        pointsText.text = string.Format(congratulatoryPhrases[phraseIndex] + "\n+" + IonSceneManager.totalMultipliedScore);
        Debug.Log(pointsText.text);
        StartCoroutine("DestroyPointDisplay");
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    IEnumerator DestroyPointDisplay()
    {
        yield return new WaitForSeconds(1.333f);
        Destroy(congratulatoryTextWrapper);
    }
}

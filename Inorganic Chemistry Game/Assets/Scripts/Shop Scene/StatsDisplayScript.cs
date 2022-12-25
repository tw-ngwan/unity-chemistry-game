using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class StatsDisplayScript : MonoBehaviour
{

    private Text statDisplay;

    // Start is called before the first frame update
    void Start()
    {
        statDisplay = GetComponent<Text>();
        StatDisplay();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        StatDisplay();
    }

    private void StatDisplay()
    {
        statDisplay.text = string.Format("Game Stats: \nSuper Multiplier: {0}\nMulti Multiplier: {1}\nStreak Reset: {2}%\nGold Mine: {3}%",
            Math.Round(DataAcrossScenes.powerUpSuperMultiplier, 2),
            Math.Round(DataAcrossScenes.powerUpMultiMultiplier, 2),
            Math.Round(DataAcrossScenes.powerUpStreakReset * 100),
            Math.Round(DataAcrossScenes.moneyMultiplier * 100, 2)
            );
    }
}

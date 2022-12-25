using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class IonBotSceneManager : MonoBehaviour
{
    public Text ionBotStats;
    public GameObject ionBotGrey;
    // Start is called before the first frame update
    void Start()
    {
        if (DataAcrossScenes.ionBotPurchased)
        {
            ionBotGrey.SetActive(false);
        }
        DisplayText();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public int CountTotalIons()
    {
        int totalIons = 0;

        foreach (Ionite ion in DataAcrossScenes.totalIonites.Keys)
        {
            totalIons += DataAcrossScenes.totalIonites[ion];
        }

        return totalIons;
    }

    void DisplayText()
    {
        ionBotStats.text = string.Format("Number of ions: {0} \nAttack: {1} + {2}\nHitpoints: {3} + {4}\nAttack Power Boost: {5}%\nHitpoints Boost: {6}%",
            CountTotalIons(),
            DataAcrossScenes.ionBotBaseAttackPower, 
            Math.Round(DataAcrossScenes.ionBotBaseAttackPower * DataAcrossScenes.attackMultiplier, 0),
            DataAcrossScenes.ionBotBaseHitpoints,
            Math.Round(DataAcrossScenes.ionBotBaseHitpoints * DataAcrossScenes.hitpointsMultiplier, 0),
            Math.Round(DataAcrossScenes.attackMultiplier * 100),
            Math.Round(DataAcrossScenes.hitpointsMultiplier * 100));
    }
}

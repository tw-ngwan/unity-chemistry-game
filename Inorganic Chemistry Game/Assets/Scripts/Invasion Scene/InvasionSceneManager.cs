using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Globalization;

public class InvasionSceneManager : MonoBehaviour
{

    public static float totalStrength;

    public static float initialNinjaAttack;
    public static float initialNinjaHitpoints;

    public static float initialShieldHitpoints;
    public static float initialBossHitpoints;


    public List<GameObject> ninjaSpawnLocations = new List<GameObject>();

    public static DateTime invasionReadyTime;

    private bool checkEndOfGame;

    // For end of game 
    public GameObject winningPanel;
    public GameObject losingPanel;

    public Text winningGold;
    public Text losingGold;

    public Text practiceRound;

    private float ionBotAttackPower;
    private float ionBotHitpoints;


    private void Awake()
    {
        Time.timeScale = 1f;

        // Initiating everything based on totalStrength
        //totalStrength = DataAcrossScenes.ionBotAttackPower * DataAcrossScenes.ionBotHitpoints;

        ionBotAttackPower = DataAcrossScenes.ionBotAttackPower;
        ionBotHitpoints = DataAcrossScenes.ionBotHitpoints;
        if (DataAcrossScenes.numIons < 5)
        {
            ionBotAttackPower = 200;
            ionBotHitpoints = 2000;
        }

        initialNinjaAttack = 312.25f + 0.040f * ionBotHitpoints;
        initialNinjaHitpoints = 39.75f + 0.80f * ionBotAttackPower;
        Debug.Log("initialNinjaAttack is " + initialNinjaAttack);
        Debug.Log("initialNinjaHitpoints is " + initialNinjaHitpoints);

        initialShieldHitpoints = 222.3f + 8.883f * ionBotAttackPower;
        initialBossHitpoints = 538.87f +  13.056f * ionBotAttackPower;
        Debug.Log("initialShieldHitpoints is " + initialShieldHitpoints);
        Debug.Log("initialBossHitpoints is " + initialBossHitpoints);

    }

    // Start is called before the first frame update
    void Start()
    {
        checkEndOfGame = true;
        StartCoroutine(SpawnNinjaLocations());
    }

    // Update is called once per frame
    void Update()
    {
        if ((!IonBot.playerAlive || !Headquarters.headquartersAlive || TimeManager.timesUp) 
            && checkEndOfGame)
        {
            checkEndOfGame = false;
            GameEndedDefeat();
        }
        else if (!BossCollider.bossAlive && checkEndOfGame)
        {
            checkEndOfGame = false;
            GameEndedVictory();
        }
       
    }


    IEnumerator SpawnNinjaLocations()
    {
        while (true)
        {
            int locationIndex = UnityEngine.Random.Range(0, ninjaSpawnLocations.Count);
            if (!ninjaSpawnLocations[locationIndex].activeSelf)
            {
                ninjaSpawnLocations[locationIndex].SetActive(true);
                float timeToWait = UnityEngine.Random.Range(6f, 12f);
                yield return new WaitForSeconds(timeToWait);
            }
        }
        
    }

    private int GoldReward()
    {
        float totalStrength = DataAcrossScenes.ionBotAttackPower * DataAcrossScenes.ionBotHitpoints;
        int goldReward = (int)Math.Round(0.3689f * Math.Sqrt(totalStrength) + 1867);
        return goldReward;
    }

    private void GameEndedVictory()
    {
        TimeManager.timerActive = false;
        winningPanel.SetActive(true);
        int averageGold = GoldReward();
        int amountOfGold = UnityEngine.Random.Range(averageGold - 300, averageGold + 300);
        winningGold.text = amountOfGold.ToString();
        invasionReadyTime = DateTime.UtcNow.AddHours(8);
        if (TimeBonusScript.invasionReady)
        {
            DataAcrossScenes.totalMoney += amountOfGold;
        }
        else
        {
            practiceRound.text = "Practice Round";
        }

        TimeBonusScript.invasionReady = false;

    }

    private void GameEndedDefeat()
    {
        TimeManager.timerActive = false;
        losingPanel.SetActive(true);
        int amountOfGold = 0;
        losingGold.text = amountOfGold.ToString();
        DataAcrossScenes.totalMoney += amountOfGold;

    }
}

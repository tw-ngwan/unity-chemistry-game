using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;


public class DataAcrossScenes : MonoBehaviour
{
    // This shows all the powerUps and their related constants
    // The list's values are respectively level and cost.
    public static Dictionary<string, List<int>> powerUps = new Dictionary<string, List<int>>() {
        { "superMultiplier", new List<int>(){0, 200 } },
    { "multiMultiplier", new List<int>(){0, 200 } },
    { "streakReset", new List<int>(){0, 200 } },
    { "timeBooster", new List<int>(){0, 200 } },
    };

    // This shows all the other boosts
    public static Dictionary<string, List<int>> otherBoosts = new Dictionary<string, List<int>>()
    {
        { "goldStorage", new List<int>(){0, 500 }  },
        { "goldMine", new List<int>(){0, 500 } },
        { "ancientSword", new List<int>(){0, 500 } },
        { "jadeSanctuary", new List<int>(){0, 500 } },
        { "ionMembrane", new List<int>(){0, 500 } },
    };

    // The multipliers
    public static float powerUpSuperMultiplier = 1; // SuperMultiplier 
    public static float powerUpMultiMultiplier = 1; // MultiMultiplier
    public static float powerUpStreakReset = 0; // The streak reset amount
    public static float additionalTime = 0;

    // Matters with respect to money 
    public static int maxMoney = 10000;
    public static float moneyMultiplier = 0.002f;

    // Matters with respect to IonBot
    public static float ionBotAttackPower = 0;
    public static float ionBotHitpoints = 0;
    public static float ionBotBaseAttackPower = 0;
    public static float ionBotBaseHitpoints = 0;
    public static float attackMultiplier = 0;
    public static float hitpointsMultiplier = 0;

    public static int maxIons = 0;
    public static int numIons = 0;

    public static bool ionBotPurchased = false;

    public static int totalMoney = 100;

    // The ionites 



    public static List<Ionite> typesOfIonites = new List<Ionite>()
    {
        DisplayAllIons.sodium,
        DisplayAllIons.chloride,
        DisplayAllIons.carbonate,
        DisplayAllIons.calcium,

        DisplayAllIons.lithium,
        DisplayAllIons.sulfite,
        DisplayAllIons.beryllium,
        DisplayAllIons.thiocyanate,

        DisplayAllIons.vanadium,
        DisplayAllIons.mercury,
        DisplayAllIons.azide,
        DisplayAllIons.peroxodisulfate,

        DisplayAllIons.gold,
        DisplayAllIons.ozonide,
        DisplayAllIons.uranium,
        DisplayAllIons.polyiodide


    };

    public static List<int> totalIonitesCount = new List<int>()
    {
        0, 0, 0, 0,
        0, 0, 0, 0,
        0, 0, 0, 0,
        0, 0, 0, 0
    };


    public static Dictionary<Ionite, int> totalIonites = new Dictionary<Ionite, int>()
    {
        {DisplayAllIons.sodium, 0 },
        {DisplayAllIons.chloride, 0 },
        {DisplayAllIons.carbonate, 0 },
        {DisplayAllIons.calcium, 0 },

        {DisplayAllIons.lithium, 0 },
        {DisplayAllIons.sulfite, 0 },
        {DisplayAllIons.beryllium, 0 },
        {DisplayAllIons.thiocyanate, 0 },

        {DisplayAllIons.vanadium, 0 },
        {DisplayAllIons.mercury, 0 },
        {DisplayAllIons.azide, 0 },
        {DisplayAllIons.peroxodisulfate, 0 },

        {DisplayAllIons.gold, 0 },
        {DisplayAllIons.ozonide, 0 },
        {DisplayAllIons.uranium, 0 },
        {DisplayAllIons.polyiodide, 0 },

    };

    public static List<Ionite> commonIons = new List<Ionite>()
    {
        DisplayAllIons.sodium,
        DisplayAllIons.chloride,
        DisplayAllIons.carbonate,
        DisplayAllIons.calcium,

    };

    public static List<Ionite> uncommonIons = new List<Ionite>()
    {
        DisplayAllIons.lithium,
        DisplayAllIons.sulfite,
        DisplayAllIons.beryllium,
        DisplayAllIons.thiocyanate,

    };

    public static List<Ionite> rareIons = new List<Ionite>()
    {
        DisplayAllIons.vanadium,
        DisplayAllIons.mercury,
        DisplayAllIons.azide,
        DisplayAllIons.peroxodisulfate,

    };

    public static List<Ionite> uniqueIons = new List<Ionite>()
    {
        DisplayAllIons.gold,
        DisplayAllIons.ozonide,
        DisplayAllIons.uranium,
        DisplayAllIons.polyiodide,

    };



    // Checking number of active topics
    public static int numInactiveTopics = 0;





    //public void SaveGame()
    //{
    //    SaveSystem.SaveGame();
    //}

    //public void LoadGame()
    //{
    //    SaveData data = SaveSystem.LoadGame();
    //}



    public void SaveGame()
    {
        string path = Application.persistentDataPath + "/MySaveData.dat";
        Debug.Log("path is " + path);
        BinaryFormatter bf = new BinaryFormatter();
        using (FileStream file = File.Create(path))
        {
            SaveData data = new SaveData();

            // Add all the stuff that you want to save
            data.savedPowerUps = powerUps;
            data.savedOtherBoosts = otherBoosts;

            data.savedPowerUpMultiMultiplier = powerUpMultiMultiplier;
            data.savedPowerUpSuperMultiplier = powerUpSuperMultiplier;
            data.savedPowerUpStreakReset = powerUpStreakReset;
            data.savedAdditionalTime = additionalTime;

            data.savedMaxMoney = maxMoney;
            data.savedMoneyMultiplier = moneyMultiplier;
            Debug.Log(data.savedMoneyMultiplier);
            data.savedIonBotBaseAttackPower = ionBotBaseAttackPower;
            data.savedIonBotBaseHitpoints = ionBotBaseHitpoints;
            data.savedAttackMultiplier = attackMultiplier;
            data.savedHitpointsMultiplier = hitpointsMultiplier;

            data.savedMaxIons = maxIons;
            data.savedNumIons = numIons;

            data.savedIonBotPurchased = ionBotPurchased;

            //data.savedTotalIonites = totalIonites;
            UpdateIoniteCountData();
            data.savedTotalIonitesCount = totalIonitesCount;

            data.savedTotalMoney = totalMoney;
            Debug.Log("data.savedMoney is " + data.savedTotalMoney);

            data.savedQuizReadyTime = QuizOverScript.quizReadyTime;
            data.savedInvasionReadyTime = InvasionSceneManager.invasionReadyTime;

            data.savedHighScore = HighScoreDisplayScript.newHighScore;

            data.savedNumInactiveTopics = numInactiveTopics;

            bf.Serialize(file, data);
        }

        Debug.Log("Game data saved!");
    }


    public void LoadGame()
    {
        string path = Application.persistentDataPath + "/MySaveData.dat";
        Debug.Log(path);
        if (File.Exists(path))
        {
            BinaryFormatter bf = new BinaryFormatter();
            using (FileStream file =
                       File.Open(path, FileMode.Open))
            {
                SaveData data = (SaveData)bf.Deserialize(file);

                powerUps = data.savedPowerUps;
                otherBoosts = data.savedOtherBoosts;

                powerUpMultiMultiplier = data.savedPowerUpMultiMultiplier;
                powerUpSuperMultiplier = data.savedPowerUpSuperMultiplier;
                powerUpStreakReset = data.savedPowerUpStreakReset;
                additionalTime = data.savedAdditionalTime;

                maxMoney = data.savedMaxMoney;
                moneyMultiplier = data.savedMoneyMultiplier;
                Debug.Log(moneyMultiplier);

                ionBotBaseAttackPower = data.savedIonBotBaseAttackPower;
                ionBotBaseHitpoints = data.savedIonBotBaseHitpoints;
                attackMultiplier = data.savedAttackMultiplier;
                hitpointsMultiplier = data.savedHitpointsMultiplier;

                maxIons = data.savedMaxIons;
                numIons = data.savedNumIons;

                ionBotPurchased = data.savedIonBotPurchased;

                //totalIonites = data.savedTotalIonites;

                //Debug.Log(totalMoney);
                //Debug.Log(data.savedTotalMoney);
                totalMoney = data.savedTotalMoney;


                totalIonitesCount = data.savedTotalIonitesCount;
                UpdateIoniteData();

                // Somehow I can't get this to not return null so... I'll have to take drastic measures
                //if (data.savedTotalIonitesCount != null)
                //{
                //    totalIonitesCount = data.savedTotalIonitesCount;
                //}

                QuizOverScript.quizReadyTime = data.savedQuizReadyTime;
                Debug.Log(QuizOverScript.quizReadyTime + " is quizReadyTime");
                InvasionSceneManager.invasionReadyTime = data.savedInvasionReadyTime;

                HighScoreDisplayScript.newHighScore = data.savedHighScore;

                numInactiveTopics = data.savedNumInactiveTopics;

                Debug.Log("data.savedMoney is " + data.savedTotalMoney);
            }

            // Add the stuff you want to load 


            //Debug.Log("powerUps is " + powerUps);
            //Debug.Log("otherBoosts is " + otherBoosts);
            //Debug.Log("powerUpMulti is " + powerUpMultiMultiplier);
            //Debug.Log("powerUpSuper is " + powerUpSuperMultiplier);
            //Debug.Log("powerUpStreak is " + powerUpStreakReset);
            //Debug.Log("additionalTime is " + additionalTime);
            //Debug.Log("moneyMultiplier is " + moneyMultiplier);
            Debug.Log("ionBotAttack is " + ionBotBaseAttackPower);

            Debug.Log("savedMoney is " + totalMoney);

            Debug.Log("Game data loaded!");
        }
        else
        {
            Debug.LogError("There is no save data!");
        }

    }

    private void Awake()
    {
        GameObject[] data = GameObject.FindGameObjectsWithTag("Data");
        if (data.Length > 1)
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
        ionBotAttackPower = ionBotBaseAttackPower * attackMultiplier;
        ionBotHitpoints = ionBotBaseHitpoints * hitpointsMultiplier;
    }


    private static bool initialized = false;
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("initialized is " + initialized);
        if (!initialized)
        {
            initialized = true;
            LoadGame();
            Debug.Log("game loaded!");
        }
        //UpdateIoniteData();
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        //UpdateIoniteData();
    }

    private void Update()
    {
        if (Input.GetKey("escape"))
        {
            SaveGame();
            Application.Quit();
        }
    }

    public void QuitGame()
    {
        Debug.Log("Game quit");
        SaveGame();
        Application.Quit();
    }

    public void UpdateIoniteCountData()
    {
        for (int i = 0; i < 16; i++)
        {
            totalIonitesCount[i] = totalIonites[typesOfIonites[i]];
        }
        //Debug.Log("Ok");
    }

    public void UpdateIoniteData()
    {
        //totalIonites[DisplayAllIons.sodium] = totalIonitesCount[typesOfIonites.IndexOf(DisplayAllIons.sodium)];

        foreach (Ionite ionite in typesOfIonites)
        {
            totalIonites[ionite] = totalIonitesCount[typesOfIonites.IndexOf(ionite)];
        }
        //Debug.Log("Ok");
    }

    public void CheckData()
    {
        //foreach (Ionite ionite in totalIonites.Keys)
        //{
        //    Debug.Log(string.Format("Total {0}: {1}", ionite.IonName, totalIonites[ionite]));
        //}
        //Debug.Log("ionBot attack power is " + ionBotBaseAttackPower);
        //Debug.Log("ionBost hitpoints is " + ionBotBaseHitpoints);

        //Debug.Log("powerUpSuperMultiplier is " + powerUpSuperMultiplier);
        //Debug.Log("powerUpMultiMultiplier is " + powerUpMultiMultiplier);
        //Debug.Log("powerUpStreakReset is " + powerUpStreakReset);
        //Debug.Log("additionalTime is " + additionalTime);
        //Debug.Log("maxMoney is " + maxMoney);
        //Debug.Log("moneyMultiplier is " + moneyMultiplier);
        //Debug.Log("attackMultiplier is " + attackMultiplier);
        //Debug.Log("hitpointsMultiplier is " + hitpointsMultiplier);
        //Debug.Log("maxIons is " + maxIons);
    }
}


[Serializable]
public class SaveData
{
    // This shows all the powerUps and their related constants
    // The list's values are respectively level and cost.
    public Dictionary<string, List<int>> savedPowerUps = new Dictionary<string, List<int>>() {
        { "superMultiplier", new List<int>(){0, 200 } },
    { "multiMultiplier", new List<int>(){0, 200 } },
    { "streakReset", new List<int>(){0, 200 } },
    { "timeBooster", new List<int>(){0, 200 } },
    };

    // This shows all the other boosts
    public Dictionary<string, List<int>> savedOtherBoosts = new Dictionary<string, List<int>>()
    {
        { "goldStorage", new List<int>(){0, 500 }  },
        { "goldMine", new List<int>(){0, 500 } },
        { "ancientSword", new List<int>(){0, 500 } },
        { "jadeSanctuary", new List<int>(){0, 500 } },
        { "ionMembrane", new List<int>(){0, 500 } },
    };

    // The multipliers
    public float savedPowerUpSuperMultiplier = 1; // SuperMultiplier 
    public float savedPowerUpMultiMultiplier = 1; // MultiMultiplier
    public float savedPowerUpStreakReset = 0; // The streak reset amount
    public float savedAdditionalTime = 0;

    // Matters with respect to money 
    public int savedMaxMoney = 10000;
    public float savedMoneyMultiplier = 0.002f;

    // Matters with respect to IonBot
    public float savedIonBotBaseAttackPower = 0;
    public float savedIonBotBaseHitpoints = 0;
    public float savedAttackMultiplier = 0;
    public float savedHitpointsMultiplier = 0;

    public int savedMaxIons = 0;
    public int savedNumIons = 0;

    public bool savedIonBotPurchased = false;

    public int savedTotalMoney = 100;

    public DateTime savedQuizReadyTime;
    public DateTime savedInvasionReadyTime;

    // Highscore
    public int savedHighScore;

    public int savedNumInactiveTopics;

    public List<int> savedTotalIonitesCount = new List<int>()
    {
        0, 0, 0, 0,
        0, 0, 0, 0,
        0, 0, 0, 0,
        0, 0, 0, 0
    };

    //public Dictionary<Ionite, int> savedTotalIonites = new Dictionary<Ionite, int>()
    //{
    //    {DisplayAllIons.sodium, 0 },
    //    {DisplayAllIons.chloride, 0 },
    //    {DisplayAllIons.carbonate, 0 },
    //    {DisplayAllIons.calcium, 0 },

    //    {DisplayAllIons.lithium, 0 },
    //    {DisplayAllIons.sulfite, 0 },
    //    {DisplayAllIons.beryllium, 0 },
    //    {DisplayAllIons.thiocyanate, 0 },

    //    {DisplayAllIons.vanadium, 0 },
    //    {DisplayAllIons.mercury, 0 },
    //    {DisplayAllIons.azide, 0 },
    //    {DisplayAllIons.peroxodisulfate, 0 },

    //    {DisplayAllIons.gold, 0 },
    //    {DisplayAllIons.ozonide, 0 },
    //    {DisplayAllIons.uranium, 0 },
    //    {DisplayAllIons.polyiodide, 0 },

    //};


    public SaveData ()
    {
        savedPowerUps = DataAcrossScenes.powerUps;
        savedOtherBoosts = DataAcrossScenes.otherBoosts;

        savedPowerUpSuperMultiplier = DataAcrossScenes.powerUpSuperMultiplier;
        savedPowerUpMultiMultiplier = DataAcrossScenes.powerUpMultiMultiplier;
        savedPowerUpStreakReset = DataAcrossScenes.powerUpStreakReset;
        savedAdditionalTime = DataAcrossScenes.additionalTime;

        savedMaxMoney = DataAcrossScenes.maxMoney;

        savedMoneyMultiplier = DataAcrossScenes.moneyMultiplier;
        
        savedIonBotBaseAttackPower = DataAcrossScenes.ionBotBaseAttackPower;
        savedIonBotBaseHitpoints = DataAcrossScenes.ionBotBaseHitpoints;
        savedAttackMultiplier = DataAcrossScenes.attackMultiplier;
        savedHitpointsMultiplier = DataAcrossScenes.hitpointsMultiplier;
        
        savedMaxIons = DataAcrossScenes.maxIons;
        
        savedIonBotPurchased = DataAcrossScenes.ionBotPurchased;
        
        //savedTotalIonites = DataAcrossScenes.totalIonites;
        
        savedTotalMoney = DataAcrossScenes.totalMoney;

        savedNumInactiveTopics = DataAcrossScenes.numInactiveTopics;

    }

}





public static class SaveSystem 
{
    
    public static void SaveGame()
    {
        BinaryFormatter bf = new BinaryFormatter();
        string path = Application.persistentDataPath + "/MySaveData.dat";
        using (FileStream file = new FileStream(path, FileMode.Create))
        {
            SaveData data = new SaveData();
            bf.Serialize(file, data);
        }
        Debug.Log("Game saved!");
    }

    public static SaveData LoadGame()
    {
        string path = Application.persistentDataPath + "/MySaveData.dat";

        if (File.Exists(path))
        {
            BinaryFormatter bf = new BinaryFormatter();
            using (FileStream file = new FileStream(path, FileMode.Open))
            {
                SaveData data = bf.Deserialize(file) as SaveData;
                return data;

            }
            //Debug.Log("Game loaded!");
        }
        else
        {
            return null;
        }
    }
}


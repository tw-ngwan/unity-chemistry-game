using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Experimental.Rendering.Universal;
using System;

public class IonSceneManager : MonoBehaviour
{

    // Dealing with setting transition scene to inactive
    public GameObject transitionScene;

    // Dealing with checking if compound is correct
    [HideInInspector]
    public static int totalCharge = 0; // To check if total charge of compound is 0
    [HideInInspector]
    public static int typesOfIons = 0; // To check if only 2 types of ions used
    [HideInInspector]
    public static Dictionary<string, int> allIons = new Dictionary<string, int>();
    public static List<GameObject> destroyIonsList = new List<GameObject>(); // This is to check which specific GameObjects were selected 
    [HideInInspector]
    public static List<int> numberOfIons = new List<int>();
    [HideInInspector]
    public static Dictionary<string, string> transitionMetalIons = new Dictionary<string, string>()
    {
        {"Cr3", "Cr" }, {"Cr2", "Cr"}, {"Fe3", "Fe"}, {"Fe2", "Fe"}, {"Cu2", "Cu"}, {"Cu1", "Cu"}, {"V2", "V"},
        {"V3", "V"}, {"Co3", "Co"}, {"Co2", "Co"}, {"Pb2", "Pb"}, {"Pb3", "Pb"}, {"Pb4", "Pb"}
    }; // This is to change the ion symbols of each of these ions 
    public bool isCompoundCorrect;


    // Dealing with score and streak
    public static int totalAddedScore = 0; // This is the summed-up total score of all compounds
    [HideInInspector]
    public static int totalScore = 0; // Total score before multipliers, used to tabulate score of compound
    [HideInInspector]
    public static int totalMultipliedScore; // Multiplied score to be added to total score
    public static int streak = 0; // Total streak for bonus 
    public Text ScoreText; // Text element containing score function
    public Text StreakText; // Text element containing streak function 
    public static float streakMultiplier = 1; // Multiplier from streak element. The quiz and others will be added later 
    public static float quizMultiplier = 1; // Multiplier from quiz element. Actual details to be worked out later
    
    private float multiplier = 1; // The total multiple of all multipliers 
    public static float numIonsMultiplier = 1;
    



    // Miscellaneous
    public static int hcf; // Highest Common Factor. Used to test if compound formed is simplest (eg: Al2Cl6 vs AlCl3)
    private Vector3 scaleChange = new Vector3(0.000092f, 0.000092f, 0.000092f); // This is for shrinking ions to destroy

    // Dealing with position of ion
    public Vector3 ionPosition;
    public List<Vector3> ionPositionList = new List<Vector3>();


    // Dealing with animation of destroying ion 
    public Light2D flashLight;

    // Dealing with the quiz
    public GameObject quizPanel;
    public GameObject quizCanvas;
    [HideInInspector]
    public static bool quizSpawned = false;


    // Dealing with the spawning of the notes
    public List<GameObject> congratsPhrases = new List<GameObject>();

    // Dealing with the spawning of text box
    public GameObject congratulatoryTextWrapper;

    // Dealing with the in-game cash
    public static int currentMoney;



    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("WaitForLoadScene");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        ScoreText.text = totalAddedScore.ToString();
        if (IonSpawner.numberOfCorrectCompounds % 5 == 0 && IonSpawner.numberOfCorrectCompounds != 0 && quizSpawned == false)
        {
            quizSpawned = true;
            Instantiate(quizPanel);
            //Instantiate(quizCanvas);
            Instantiate(quizCanvas, new Vector3(0, -10, 0), transform.rotation);
            TimerScript.timerActive = false;
        }
    }


    IEnumerator WaitForLoadScene()
    {
        yield return new WaitForSeconds(3f);
        transitionScene.SetActive(false);
        TimerScript.timerActive = true;

    }

    // This is to check how many types of ions are in compound
    public void checkIons()
    {
        typesOfIons = 0;
        foreach(string keys in allIons.Keys)
        {
            if (allIons[keys] > 0) {
                typesOfIons += 1;
            }
            
        }

    }


    // This is used to calculate hcf, to ensure simplest possible compound formed
    public void highestCommonFactor(int a, int b)
    {
        hcf = 0;
        int k = (a < b) ? a : b;
        for (int i = 1; i <= k; i++)
        {
            if (a % i == 0 && b % i == 0)
            {
                hcf = i;
            }
        }

    }


    // This is to check if the compound formed is correct. If so, points can be awarded.
    public bool compoundCorrect() // Remember to debug this, you left it like this to test AddScore and destroyIons! It doesn't work here
    {
        hcf = 0;
        foreach (int values in allIons.Values)
        {
            if (values > 0) {
                numberOfIons.Add(values);
            }
            
        }
        try
        {
            highestCommonFactor(numberOfIons[0], numberOfIons[1]);
            Debug.Log("From compoundCorrect, hcf func. hcf is " + hcf);
        }
        catch
        {
        }

        checkIons(); // Add to typeOfIons. Oh but because you call compoundCorrect twice it adds twice... 

        Debug.Log("From compoundCorrect: totalCharge is " + totalCharge + ", typesOfIons is " + typesOfIons + ", hcf is " + hcf);
        if (totalCharge == 0 && typesOfIons == 2 && hcf == 1)
        {
            // Resetting everything to 0 for the next compound
            totalCharge = 0;
            typesOfIons = 0;
            hcf = 0;

            isCompoundCorrect = true;

        }
        else
        {
            isCompoundCorrect = false;
        }

        numberOfIons.Clear();
        
        return isCompoundCorrect;

    }



    // This is to add points to the compound 
    public void AddScore()
    {
        if (compoundCorrect())
        {
            numIonsMultiplier = 1;
            foreach (string keys in allIons.Keys)
            {
                numIonsMultiplier += allIons[keys] * 0.1f;
            }
            Debug.Log("numIonsMultiplier is " + numIonsMultiplier);
            Debug.Log("powerUpSuperMultiplier is " + DataAcrossScenes.powerUpSuperMultiplier);
            Debug.Log("powerUpMultiMultiplier is " + DataAcrossScenes.powerUpMultiMultiplier);
            multiplier = streakMultiplier * quizMultiplier * DataAcrossScenes.powerUpSuperMultiplier * DataAcrossScenes.powerUpMultiMultiplier * numIonsMultiplier;
            //Debug.Log("Multiplier is " + multiplier);
            Debug.Log("Compound is correct!");
            totalMultipliedScore = (int)Mathf.Round(totalScore * multiplier);
            //Debug.Log(totalMultipliedScore);
            totalAddedScore += totalMultipliedScore;
            ScoreText.text = totalAddedScore.ToString();

            totalScore = 0;
        }

        else
        {
            Debug.Log("Compound is incorrect!");
        }


    }


    // This both adds to the streak and adds to the streakMultiplier 
    public void AddStreak()
    {
        if (isCompoundCorrect)
        {
            streak += 1;
            streakMultiplier += 0.1f;
        }
        else
        {
            streak = (int)Math.Round(streak * DataAcrossScenes.powerUpStreakReset);
            streakMultiplier = 1;
        }
        StreakText.text = streak.ToString();
    }


    public void DestroyIonsWrapper()
    {
        StartCoroutine("DestroyIons");
    }



    // This function does 2 things
    // First, it destroys all the clicked ions. Second, it adds to a list of positions where the ions are. This allows it to reset that there's no ion there
    // Thus, this allows IonSpawner to do it's work and spawn an ion
    // IEnumerator is used so that the new ion is only spawned after a fixed time 
    public IEnumerator DestroyIons()
    {
        if (isCompoundCorrect)
        {
            foreach (GameObject key in destroyIonsList)
            {

                // To figure out where the ions selected are
                ionPosition = key.transform.position;
                //Debug.Log(ionPosition);
                ionPositionList.Add(ionPosition);

                Instantiate(flashLight, ionPosition, key.transform.rotation);


                // To destroy the ions and reset score to 0
                //Debug.Log(key); // This turns up null for some reason
                // This is used to grow the ions (no longer growing in IonSpawner) 
                IonSpawner.ionsOnScreen.Remove(key);
                Debug.Log("One ion removed from ionsOnScreen");
                Destroy(key);

                //Debug.Log("Key destroyed");


            }

            destroyIonsList.Clear(); // To clear the whole list 

            yield return new WaitForSeconds(UnityEngine.Random.Range(2, 5));
            foreach (Vector3 pos in ionPositionList)
            {
                IonSpawner.positions[pos] = true;
            }

            ionPositionList.Clear();


        }
        

    }

    public void SpawnPhraseWrapper()
    {
        if (isCompoundCorrect)
        {
            Debug.Log("Spawned phrase");
            Instantiate(congratulatoryTextWrapper, new Vector3(0, 0, 0), transform.rotation, congratulatoryTextWrapper.transform.parent);
        }
        
        //StartCoroutine("SpawnPhrase");
    }

    //public IEnumerator SpawnPhrase()
    //{
    //    if (isCompoundCorrect)
    //    {
    //        congratulatoryTextWrapper.SetActive(true);

    //        int phraseIndex = Random.Range(0, congratulatoryPhrases.Count);
    //        congratulatoryText.text = string.Format(congratulatoryPhrases[phraseIndex] + "\n+" + totalMultipliedScore);
    //        yield return new WaitForSeconds(1.333f);
    //        congratulatoryTextWrapper.SetActive(false);
    //    }
    //}

    public void ResetAllIons()
    {
        if (isCompoundCorrect)
        {
            allIons.Clear();
            IonSpawner.numberOfCorrectCompounds += 1;
            IonSpawner.spawnIons = true;
            quizSpawned = false;
            //Debug.Log("allIons cleared");
            //Debug.Log("After clearing: totalCharge is " + totalCharge + ", typesOfIons is " + typesOfIons + ", hcf is " + hcf);
        }
        
    }

    //public IEnumerator ResetSpawnedIons()
    //{
    //    if (compoundCorrect())
    //    {
    //        //yield return new WaitForSeconds(Random.Range(1, 3));
    //        foreach(GameObject key in destroyIonsList)
    //        {

    //            IonSpawner.positions[ionPosition] = true;

    //        }

    //        yield return new WaitForSeconds(1f);
    //        destroyIonsList.Clear();
    //    }
    //}


    // This is to destroy the ion in the gamespace after it is used to create a compound 
    // This is also used to 
    //public void DestroyIon()
    //{
    //    if (compoundCorrect())
    //    {
    //        //Debug.Log(destroyIonsList.Count);
    //        foreach (GameObject key in destroyIonsList)
    //        {


    //            // This is supposed to destroy the used gameObject and reset score

    //        }

    //        // TODO: Once the ions are used, destroy them if the compound is correct (maybe use a bool or something)
    //    }
    //}

} // class

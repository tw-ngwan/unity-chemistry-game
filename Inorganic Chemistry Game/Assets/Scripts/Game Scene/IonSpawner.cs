using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;


[System.Serializable]
public class EachIon
{
    public List<GameObject> eachIon = new List<GameObject>();
}

[System.Serializable]
public class PossibleCompounds
{
    public List<EachIon> eachCompound = new List<EachIon>();
}

public class IonSpawner : MonoBehaviour
{

    public static Dictionary<Vector3,bool> positions = new Dictionary<Vector3,bool>(); // This is a dict of all possible positions for ions to be at 
    // These assist in filling up positions
    public static List<Vector3> positionsForChecking = new List<Vector3>();
    private List<float> horizontal = new List<float>() { -3.7f, -1.85f, 0f, 1.85f, 3.7f }; 
    private List<float> vertical = new List<float>() { -3.2f, -1.6f, 0f, 1.6f, 3.2f };

    // This is for the random index for spawning ions 
    private int index;

    // This is the list of prefab ions, to be spawned 
    [SerializeField]
    private List<GameObject> ionList = new List<GameObject>();

    // This is the list of all available ions 
    [HideInInspector]
    public static List<GameObject> ionsOnScreen = new List<GameObject>();
    //public static Dictionary<GameObject, bool> ionsOnScreen = new Dictionary<GameObject, bool>();


    // This is to make ions grow in size
    private Vector3 scaleChange = new Vector3(0.000092f, 0.000092f, 0.000092f);

    private Vector3 maxSize = new Vector3(0.00925f, 0.00925f, 0.00925f);

    private Vector3 currentSize = new Vector3(0.00005f, 0.00005f, 0.00005f);


    // This is for the advanced spawning method
    [SerializeField]
    public PossibleCompounds possibleCompounds = new PossibleCompounds();
    public static int numberOfCorrectCompounds = 0;
    public static bool spawnIons = true;
    private int compoundIndex;
    private int ionIndex;
    private int compoundListLength = 25;

    // This is to find number of empty spaces and for turn 6 generation
    private int numberOfEmptySpaces;
    private int numberOfPossibleCompounds;


    // This is for number of new ions spawned after reload 
    private int reloadNum;
    // This is for number of easy turns at the start 
    private int numberOfEasyTurns;


    // Start is called before the first frame update
    void Start()
    {
        numberOfEasyTurns = UnityEngine.Random.Range(3, 7);
        Debug.Log("numberOfEasyTurns is " + numberOfEasyTurns);
        // This is to get all the Vectors added in positions so that we can spawn ions to them immediately 
        foreach(float i in horizontal)
        {
            foreach(float j in vertical)
            {
                if (!positions.ContainsKey(new Vector3(i, j, 0)))
                {
                    positions.Add(new Vector3(i, j, 0), true);
                }
                
                if (!positionsForChecking.Contains(new Vector3(i, j, 0)))
                {
                    positionsForChecking.Add(new Vector3(i, j, 0));
                }
                
            }
        }

        spawnIons = true;


            //InvokeRepeating("SpawnIons", 0.1f, 0.05f); // Try and delay what's going on, so after ion destroyed, it doesn't pop up again immediately

        // One solution (idk) is creating an InitialSpawnIons function, then afterwards using an IEnumerator to wait a few seconds before every round. Though seems unlikely
    }

    // Update is called once per frame
    void Update()
    {

    }


    private void FixedUpdate()
    {
        if (TimerScript.timerActive)
        {
            SpawnIons();
        }
    }



    // This will basically spawn ions on a fixed basis, like every few seconds 
    void SpawnIons()
    {
        // Maybe one more cool feature: We determine how many cycles we want of this using UnityEngine.Random? 
        if (numberOfCorrectCompounds < numberOfEasyTurns && spawnIons == true) // For the first 5 turns
        {
            spawnIons = false;
            AdvancedSpawnIon(5);
            findNumEmptySpaces();
        }

        else if (numberOfCorrectCompounds == numberOfEasyTurns && spawnIons == true) // For the 6th turn after player has eased into game
        {
           
            spawnIons = false;
            findNumEmptySpaces();

            MultipleCompoundFormation();
            

        }

        else if (numberOfCorrectCompounds > numberOfEasyTurns && spawnIons == true) // For every other turn 
        {
            spawnIons = false;
            findNumEmptySpaces();

            if (numberOfEmptySpaces >= 10)
            {
                MultipleCompoundFormation();
            }

            else if (numberOfEmptySpaces >= 5)
            {
                if (UnityEngine.Random.value > 0.7)
                {
                    AdvancedSpawnIon(5);
                }
            }

            else
            {
                if (UnityEngine.Random.value > 0.3)
                {
                    RemainingSpawnIon();
                }
                // Add something that says that after a wait for a few seconds, it'll start to spawn ions randomly 
            }
            


        }
        

        
    }



    // My tests seem to say that it's ok, but I still have my doubts... maybe look through this one again sometime?
    public void ReloadIons()
    {

        foreach (GameObject ion in ionsOnScreen)
        {
            Destroy(ion);
            Debug.Log("One ion destroyed");
        }
        ionsOnScreen.Clear();
        Debug.Log("ionsOnScreen cleared");

        ResetEverything();

        foreach (Vector3 keys in positions.Keys.ToList())
        {
            positions[keys] = true;
        }
        findNumEmptySpaces();
        //Debug.Log("From ReloadIons: " + numberOfEmptySpaces + " empty spaces");

        if (numberOfCorrectCompounds >= numberOfEasyTurns)
        {
            spawnIons = false;
            for (int i = 0; i < 2; i++)
            {
                AdvancedSpawnIon(5);
            }
            reloadNum = UnityEngine.Random.Range(2, 4);
            for (int i = 0; i < reloadNum; i++)
            {
                AdvancedSpawnIon(2);
            }

        }

        else
        {
            spawnIons = true;
        }

    }


    // This is the code that basically spawns ions in an advanced manner 
    private void AdvancedSpawnIon(int hello) // For some reason this is the problem
    {
        ionIndex = 0;

        // Select a compound to be spawned
        compoundIndex = UnityEngine.Random.Range(0, compoundListLength - 1); // Change this as the list of compounds grows
        //Debug.Log("compoundIndex is " + compoundIndex);
        System.Random rand = new System.Random();
        int numIonsInCompound = possibleCompounds.eachCompound[compoundIndex].eachIon.Count;
        Debug.Log("numIonsInCompound is " + numIonsInCompound);

        Debug.Log("hello is " + hello);

        // This selects the 5 locations and spawns them at the locations 
        for (int i = 0; i < hello;) 
        {
            int randomPlace = rand.Next(0, 25);
            Debug.Log("randomPlace is " + randomPlace);
            if (positions[positionsForChecking[randomPlace]] == true) // positionsForChecking is the problem. Figure out wtf is wrong 
            {
                positions[positionsForChecking[randomPlace]] = false;
                if (ionIndex < numIonsInCompound)
                {
                    Debug.Log("ionIndex is " + ionIndex);
                    GameObject newIon = Instantiate(possibleCompounds.eachCompound[compoundIndex].eachIon[ionIndex], positionsForChecking[randomPlace],
                        transform.rotation, possibleCompounds.eachCompound[compoundIndex].eachIon[ionIndex].transform.parent);
                    ionsOnScreen.Add(newIon);
                    Debug.Log("One ion added to ionsOnScreen");
                    newIon.transform.localScale = currentSize;
                    ionIndex++;
                }
                else
                {
                    index = UnityEngine.Random.Range(0, 60);
                    Debug.Log("From AdvancedSpawnIon(): index is " + index);
                    GameObject newIon = Instantiate(ionList[index], positionsForChecking[randomPlace],
                        transform.rotation, ionList[index].transform.parent); // Now create the list of ions
                    ionsOnScreen.Add(newIon);
                    Debug.Log("One ion added to ionsOnScreen");
                    newIon.transform.localScale = currentSize;
                }

                i++;

            }
        }
    }


    // This is basically the rng spawn ion method. Brute force and simple, but less effective
    private void RemainingSpawnIon()
    {
        //Debug.Log("This is the " + numberOfCorrectCompounds + " turn. Calling RemainingSpawnIon");
        foreach (Vector3 keys in positions.Keys.ToList())
        {
            if (positions[keys] == true)
            {
                positions[keys] = false;
                //yield return new WaitForSeconds(0.1f);
                index = UnityEngine.Random.Range(0, 60); // The maximum index to be confirmed
                GameObject newIon = Instantiate(ionList[index], keys, transform.rotation, ionList[index].transform.parent); // Now create the list of ions
                //Debug.Log("Ions instantiated from RemainingSpawnIon");
                newIon.transform.localScale = currentSize;
                ionsOnScreen.Add(newIon);
                Debug.Log("One ion added to ionsOnScreen");

            }
        }

        
    }


    // This is basically how you set numberOfEmptySpaces to determine how many compounds to be formed from turn 6 onwards
    private void findNumEmptySpaces()
    {
        numberOfEmptySpaces = 0;
        foreach (Vector3 keys in positions.Keys.ToList())
        {
            if (positions[keys] == true)
            {
                numberOfEmptySpaces += 1;
            }
        }
        
    }


    // This basically is what happens in turn 6 and possibly after 
    private void MultipleCompoundFormation()
    {
        numberOfPossibleCompounds = numberOfEmptySpaces / 5;

        for (int i = 0; i < numberOfPossibleCompounds; i++)
        {
            AdvancedSpawnIon(5);
        }

        if ((numberOfEmptySpaces - numberOfPossibleCompounds * 5) >= 3)
        {
            AdvancedSpawnIon(3);
        }
    }



    private void ResetEverything()
    {
        IonSceneManager.allIons.Clear();
        IonSceneManager.totalCharge = 0;
        IonSceneManager.typesOfIons = 0;
        IonSceneManager.hcf = 0;
        IonSceneManager.numberOfIons.Clear();
        IonSceneManager.destroyIonsList.Clear();
        IonSceneManager.totalScore = 0;
    }

} // class



















// Old code for spawning ions at turn 6 
// This creates 2 compounds for player to form
//for (int j = 0; j < 2;)
//{
//    ionIndex = 0;
//    System.Random rand = new System.Random();
//    compoundIndex = UnityEngine.Random.Range(0, 9); // Change this as the list of compounds grows
//    int numIonsInCompound = possibleCompounds.eachCompound[compoundIndex].eachIon.Count;
//    Debug.Log(numIonsInCompound);
//    for (int i = 0; i < 4;)
//    {
//        int randomPlace = rand.Next(0, 25);
//        Debug.Log(randomPlace);
//        if (positions[positionsForChecking[randomPlace]] == true)
//        {
//            positions[positionsForChecking[randomPlace]] = false;
//            if (ionIndex < numIonsInCompound)
//            {
//                GameObject newIon = Instantiate(possibleCompounds.eachCompound[compoundIndex].eachIon[ionIndex], positionsForChecking[randomPlace],
//                    transform.rotation, possibleCompounds.eachCompound[compoundIndex].eachIon[ionIndex].transform.parent);
//                newIon.transform.localScale = currentSize;
//                ionIndex++;
//            }
//            else
//            {
//                index = UnityEngine.Random.Range(0, 60);
//                GameObject newIon = Instantiate(ionList[index], positionsForChecking[randomPlace],
//                    transform.rotation, ionList[index].transform.parent); // Now create the list of ions
//                newIon.transform.localScale = currentSize;
//            }

//            i++;
//        }
//    }
//}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Linq;

public class DisplayAllIons : MonoBehaviour
{

    // The ionites
    public static Ionite sodium = new Ionite("sodium", 2000, 20, 200);
    public static Ionite chloride = new Ionite("chloride", 2000, 20, 200);
    public static Ionite carbonate = new Ionite("carbonate", 2000, 10, 400);
    public static Ionite calcium = new Ionite("calcium", 2000, 30, 100);

    public static Ionite lithium = new Ionite("lithium", 5000, 25, 200);
    public static Ionite sulfite = new Ionite("sulfite", 5000, 20, 300);
    public static Ionite beryllium = new Ionite("beryllium", 6000, 25, 300);
    public static Ionite thiocyanate = new Ionite("thiocyanate", 6000, 30, 200);

    public static Ionite vanadium = new Ionite("vanadium", 20000, 60, 400);
    public static Ionite mercury = new Ionite("mercury", 24000, 80, 350);
    public static Ionite azide = new Ionite("azide", 24000, 40, 700);
    public static Ionite peroxodisulfate = new Ionite("peroxodisulfate", 28000, 60, 500);

    public static Ionite gold = new Ionite("gold", 50000, 40, 1200);
    public static Ionite ozonide = new Ionite("ozonide", 55000, 80, 650);
    public static Ionite uranium = new Ionite("uranium", 60000, 120, 400);
    public static Ionite polyiodide = new Ionite("polyiodide", 60000, 60, 800);

    // TextFields 
    public Text introText;

    public List<Text> textFields = new List<Text>();

    // Remove GreyOut
    public GameObject greyOut;

    // Start is called before the first frame update
    void Start()
    {
        DisplayText();
        if (DataAcrossScenes.ionBotPurchased)
        {
            greyOut.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DisplayIonStatsWrapper()
    {
        StartCoroutine("DisplayIonStats");
    }

    IEnumerator DisplayIonStats()
    {
        GameObject button = EventSystem.current.currentSelectedGameObject;
        GameObject ionStats = button.transform.GetChild(0).gameObject;
        ionStats.SetActive(true);

        yield return new WaitForSeconds(2f);

        ionStats.SetActive(false);
    }

    public void DisplayText()
    {
        introText.text = string.Format("Total Ions: {0} / {1}\nCommon Ions: {2}\nUncommon Ions: {3}\nRare Ions: {4}\nUnique Ions: {5}",
            CountTotalIons(), DataAcrossScenes.maxIons, CountCommonIons(), CountUncommonIons(), CountRareIons(), CountUniqueIons());

        foreach (Text textField in textFields)
        {
            foreach (Ionite ion in DataAcrossScenes.totalIonites.Keys)
            {
                if (textField.name == ion.IonName)
                {
                    textField.text = DataAcrossScenes.totalIonites[ion].ToString();
                    break;
                }
                
            }
        }
        
    }

    public int CountTotalIons()
    {
        Debug.Log(DataAcrossScenes.totalIonites[DisplayAllIons.sodium]);
        int totalIons = 0;

        foreach (Ionite ion in DataAcrossScenes.totalIonites.Keys)
        {
            totalIons += DataAcrossScenes.totalIonites[ion];
            Debug.Log("One ion counted");
        }
        Debug.Log("All ions counted");
        Debug.Log(totalIons);

        return totalIons;
    }

    // This thing is broken coz it can't access the new keys 
    // I think you need to access it by list or something, idk
    // Maybe create a list, reference the list, then act on it
    public int CountCommonIons()
    {
        int totalCommonIons = 0;

        foreach (Ionite ion in DataAcrossScenes.commonIons)
        {
            totalCommonIons += DataAcrossScenes.totalIonites[ion];
        }

        return totalCommonIons;
    }

    public int CountUncommonIons()
    {
        int totalUncommonIons = 0;

        foreach (Ionite ion in DataAcrossScenes.uncommonIons)
        {
            totalUncommonIons += DataAcrossScenes.totalIonites[ion];
        }

        return totalUncommonIons;
    }

    public int CountRareIons()
    {
        int totalRareIons = 0;

        foreach (Ionite ion in DataAcrossScenes.rareIons)
        {
            totalRareIons += DataAcrossScenes.totalIonites[ion];
        }

        return totalRareIons;
    }

    public int CountUniqueIons()
    {
        int totalUniqueIons = 0;

        foreach (Ionite ion in DataAcrossScenes.uniqueIons)
        {
            totalUniqueIons += DataAcrossScenes.totalIonites[ion];
        }

        return totalUniqueIons;
    }
}

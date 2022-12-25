using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;
using System.Linq;

public class ShopScript : MonoBehaviour
{
    //// Levels of the powerups 
    //private int superMultiplierLevel = 0;
    //private int multiMultiplierLevel = 0;
    //private int streakResetLevel = 0;
    //private int timeBoosterLevel = 0;

    //// Cost of the powerups 
    //private int superMultiplierCost = 200;
    //private int multiMultiplierCost = 200;
    //private int streakResetCost = 200;
    //private int timeBoosterCost = 200;

    // Text fields of the titles
    public Text superMultiplierTitle;
    public Text multiMultiplierTitle;
    public Text streakResetTitle;
    public Text timeBoosterTitle;

    // Text fields of the costs
    public Text superMultiplierCostText;
    public Text multiMultiplierCostText;
    public Text streakResetCostText;
    public Text timeBoosterCostText;

    // Buttons for changing colour 
    public GameObject superMultiplierButton;
    public GameObject multiMultiplierButton;
    public GameObject streakResetButton;
    public GameObject timeBoosterButton;


    // Other Boosts
    // Text fields of the titles 
    public Text goldStorageTitle;
    public Text goldMineTitle;
    public Text ancientSwordTitle;
    public Text jadeSanctuaryTitle;
    public Text ionMembraneTitle;

    // Text fields of the costs
    public Text goldStorageCostText;
    public Text goldMineCostText;
    public Text ancientSwordCostText;
    public Text jadeSanctuaryCostText;
    public Text ionMembraneCostText;

    // Buttons for changing colour 
    public GameObject goldStorageButton;
    public GameObject goldMineButton;
    public GameObject ancientSwordButton;
    public GameObject jadeSanctuaryButton;
    public GameObject ionMembraneButton;
    public GameObject ionBotButton;

    private GameObject maxLevelMessage;

    

    // This for getting rid of the cover off the otherBoosts
    public List<GameObject> maxOutCovers = new List<GameObject>();

    // This is for getting rid of cover off ions before ionBot bought 
    public List<GameObject> ionBotCovers = new List<GameObject>();

    // This is for checking the list of covers for ionStorage
    public List<GameObject> ionStorageCovers = new List<GameObject>();

    private bool areAllpowerUpsMaxed = false; // Remember to set this back to false!!
    //private bool areAllpowerUpsMaxed = false;
    private int checkAllpowerUpsMaxed = 0;





    // Start is called before the first frame update
    void Start()
    {

        CheckMaxOut(); // This checks if we can lift the greying on the 5 lower power-ups
        CheckIonBotBought();

        // Must do something to ensure that everything's price and level is reflected correctly 
        // This is not working... everything resets to zero once I do this
        // I think it has something to do with powerUps dictionary. Maybe I should instantiate another dictionary instead. 
        // Like maybe make something equate to powerUps. Or keep it in another script so it doesn't screw everything up? 
        if (DataAcrossScenes.powerUps["superMultiplier"][0] == 20)
        {
            superMultiplierTitle.text = "Super Multiplier: Level 20 (Max)";
            ChangeButtonColour(superMultiplierButton);
        }
        else
        {
            superMultiplierTitle.text = string.Format("Super Multiplier: Level {0}", (DataAcrossScenes.powerUps["superMultiplier"][0] + 1));
        }

        if (DataAcrossScenes.powerUps["multiMultiplier"][0] == 20)
        {
            multiMultiplierTitle.text = "Multi Multiplier: Level 20 (Max)";
            ChangeButtonColour(multiMultiplierButton);
        }
        else
        {
            multiMultiplierTitle.text = string.Format("Multi Multiplier: Level {0}", (DataAcrossScenes.powerUps["multiMultiplier"][0] + 1));
        }

        if (DataAcrossScenes.powerUps["streakReset"][0] == 20)
        {
            streakResetTitle.text = "Streak Reset: Level 20 (Max)";
            ChangeButtonColour(streakResetButton);
        }
        else
        {
            streakResetTitle.text = string.Format("Streak Reset: Level {0}", (DataAcrossScenes.powerUps["streakReset"][0] + 1));
        }

        if (DataAcrossScenes.powerUps["timeBooster"][0] == 20)
        {
            timeBoosterTitle.text = "Time Booster: Level 20 (Max)";
            ChangeButtonColour(timeBoosterButton);
        }
        else
        {
            timeBoosterTitle.text = string.Format("Time Booster: Level {0}", (DataAcrossScenes.powerUps["timeBooster"][0] + 1));
        }

        superMultiplierCostText.text = DataAcrossScenes.powerUps["superMultiplier"][1].ToString();
        multiMultiplierCostText.text = DataAcrossScenes.powerUps["multiMultiplier"][1].ToString();
        streakResetCostText.text = DataAcrossScenes.powerUps["streakReset"][1].ToString();
        timeBoosterCostText.text = DataAcrossScenes.powerUps["timeBooster"][1].ToString();



        if (DataAcrossScenes.otherBoosts["goldStorage"][0] == 50)
        {
            goldStorageTitle.text = "Gold Storage: Level 50 (Max)";
            ChangeButtonColour(goldStorageButton);
        }
        else
        {
            goldStorageTitle.text = string.Format("Gold Storage: Level {0}", (DataAcrossScenes.otherBoosts["goldStorage"][0] + 1));
        }

        if (DataAcrossScenes.otherBoosts["goldMine"][0] == 50)
        {
            goldMineTitle.text = "Gold Mine: Level 50 (Max)";
            ChangeButtonColour(goldMineButton);
        }
        else
        {
            goldMineTitle.text = string.Format("Gold Mine: Level {0}", (DataAcrossScenes.otherBoosts["goldMine"][0] + 1));
        }

        if (DataAcrossScenes.otherBoosts["ancientSword"][0] == 50)
        {
            ancientSwordTitle.text = "Ancient Sword: Level 50 (Max)";
            ChangeButtonColour(ancientSwordButton);
        }
        else
        {
            ancientSwordTitle.text = string.Format("Ancient Sword: Level {0}", (DataAcrossScenes.otherBoosts["ancientSword"][0] + 1));
        }

        if (DataAcrossScenes.otherBoosts["jadeSanctuary"][0] == 50)
        {
            jadeSanctuaryTitle.text = "Jade Sanctuary: Level 50 (Max)";
            ChangeButtonColour(jadeSanctuaryButton);
        }
        else
        {
            jadeSanctuaryTitle.text = string.Format("Jade Sanctuary: Level {0}", (DataAcrossScenes.otherBoosts["jadeSanctuary"][0] + 1));
        }

        if (DataAcrossScenes.otherBoosts["ionMembrane"][0] == 50)
        {
            ionMembraneTitle.text = "Ion-Exchange Membrane: Level 50 (Max)";
            ChangeButtonColour(ionMembraneButton);
        }
        else
        {
            ionMembraneTitle.text = string.Format("Ion-Exchange Membrane: Level {0}", (DataAcrossScenes.otherBoosts["ionMembrane"][0] + 1));
        }

        goldStorageCostText.text = DataAcrossScenes.otherBoosts["goldStorage"][1].ToString();
        goldMineCostText.text = DataAcrossScenes.otherBoosts["goldMine"][1].ToString();
        ancientSwordCostText.text = DataAcrossScenes.otherBoosts["ancientSword"][1].ToString();
        jadeSanctuaryCostText.text = DataAcrossScenes.otherBoosts["jadeSanctuary"][1].ToString();
        ionMembraneCostText.text = DataAcrossScenes.otherBoosts["ionMembrane"][1].ToString();

        if (DataAcrossScenes.ionBotPurchased)
        {
            ChangeButtonColour(ionBotButton);
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void FixedUpdate()
    {
        CheckMaxOut();
        CheckIonBotBought();
        SetIonNotEnoughStorageCover();
    }


    public void SuperMultiplierPurchase()
    {
        if (DataAcrossScenes.powerUps["superMultiplier"][0] < 20)
        {
            if (EnoughCash(DataAcrossScenes.powerUps["superMultiplier"][1]))
            {
                DataAcrossScenes.totalMoney -= DataAcrossScenes.powerUps["superMultiplier"][1];
                BasicLevelUp("superMultiplier");
                DataAcrossScenes.powerUpSuperMultiplier += 0.02f;

                if (DataAcrossScenes.powerUps["superMultiplier"][0] == 20)
                {
                    superMultiplierTitle.text = "Super Multiplier: Level 20 (Max)";
                    ChangeButtonColour(superMultiplierButton);
                }
                else
                {
                    superMultiplierTitle.text = string.Format("Super Multiplier: Level {0}", (DataAcrossScenes.powerUps["superMultiplier"][0] + 1));
                }
                superMultiplierCostText.text = DataAcrossScenes.powerUps["superMultiplier"][1].ToString();
            }
            else
            {
                OnClickNotEnoughCashWrapper();
            }

        }
        else
        {
            Debug.Log("Already at max level!");
        }
    }


    public void MultiMultiplierPurchase()
    {
        if (!(DataAcrossScenes.powerUps["multiMultiplier"][0] >= 20))
        {
            if (EnoughCash(DataAcrossScenes.powerUps["multiMultiplier"][1]))
            {
                DataAcrossScenes.totalMoney -= DataAcrossScenes.powerUps["multiMultiplier"][1];
                BasicLevelUp("multiMultiplier");
                DataAcrossScenes.powerUpMultiMultiplier += 0.05f;

                if (DataAcrossScenes.powerUps["multiMultiplier"][0] == 20)
                {
                    multiMultiplierTitle.text = "Multi Multiplier: Level 20 (Max)";
                    ChangeButtonColour(multiMultiplierButton);
                }
                else
                {
                    multiMultiplierTitle.text = string.Format("Multi Multiplier: Level {0}", (DataAcrossScenes.powerUps["multiMultiplier"][0] + 1));
                }

                multiMultiplierCostText.text = DataAcrossScenes.powerUps["multiMultiplier"][1].ToString();
            }
            else
            {
                OnClickNotEnoughCashWrapper();
            }

        }
        else
        {
            Debug.Log("Already at max level!");
        }
    }


    public void StreakResetPurchase()
    {
        if (!(DataAcrossScenes.powerUps["streakReset"][0] >= 20))
        {
            if (EnoughCash(DataAcrossScenes.powerUps["streakReset"][1]))
            {
                DataAcrossScenes.totalMoney -= DataAcrossScenes.powerUps["streakReset"][1];
                BasicLevelUp("streakReset");
                DataAcrossScenes.powerUpStreakReset += 0.04f;

                if (DataAcrossScenes.powerUps["streakReset"][0] == 20)
                {
                    streakResetTitle.text = "Streak Reset: Level 20 (Max)";
                    ChangeButtonColour(streakResetButton);
                }
                else
                {
                    streakResetTitle.text = string.Format("Streak Reset: Level {0}", (DataAcrossScenes.powerUps["streakReset"][0] + 1));
                }

                streakResetCostText.text = DataAcrossScenes.powerUps["streakReset"][1].ToString();
            }
            else
            {
                OnClickNotEnoughCashWrapper();
            }

        }
        else
        {
            Debug.Log("Already at max level!");
        }
    }


    public void TimeBoosterPurchase()
    {
        if (!(DataAcrossScenes.powerUps["timeBooster"][0] >= 20))
        {
            if (EnoughCash(DataAcrossScenes.powerUps["timeBooster"][1]))
            {
                DataAcrossScenes.totalMoney -= DataAcrossScenes.powerUps["timeBooster"][1];
                BasicLevelUp("timeBooster");
                DataAcrossScenes.additionalTime += 1f;

                if (DataAcrossScenes.powerUps["timeBooster"][0] == 20)
                {
                    timeBoosterTitle.text = "Time Booster: Level 20 (Max)";
                    ChangeButtonColour(timeBoosterButton);
                }
                else
                {
                    timeBoosterTitle.text = string.Format("Time Booster: Level {0}", (DataAcrossScenes.powerUps["timeBooster"][0] + 1));
                }

                timeBoosterCostText.text = DataAcrossScenes.powerUps["timeBooster"][1].ToString();
            }
            else
            {
                OnClickNotEnoughCashWrapper();
            }

        }
        else
        {
            Debug.Log("Already at max level!");
        }
    }




    public void GoldStoragePurchase()
    {

        if (DataAcrossScenes.otherBoosts["goldStorage"][0] < 50)
        {
            if (EnoughCash(DataAcrossScenes.otherBoosts["goldStorage"][1]))
            {
                DataAcrossScenes.totalMoney -= DataAcrossScenes.otherBoosts["goldStorage"][1];
                AdvancedLevelUp("goldStorage");
                DataAcrossScenes.maxMoney += 2000;

                if (DataAcrossScenes.otherBoosts["goldStorage"][0] == 50)
                {
                    goldStorageTitle.text = "Gold Storage: Level 50 (Max)";
                    ChangeButtonColour(goldStorageButton);
                }
                else
                {
                    goldStorageTitle.text = string.Format("Gold Storage: Level {0}", (DataAcrossScenes.otherBoosts["goldStorage"][0] + 1));
                }

                goldStorageCostText.text = DataAcrossScenes.otherBoosts["goldStorage"][1].ToString();
            }

            else
            {
                OnClickNotEnoughCashWrapper();
            }
        }
        else
        {
            Debug.Log("Already at max level!");
        }

    }


    public void GoldMinePurchase()
    {
        if (DataAcrossScenes.otherBoosts["goldMine"][0] < 50)
        {
            if (EnoughCash(DataAcrossScenes.otherBoosts["goldMine"][1]))
            {
                DataAcrossScenes.totalMoney -= DataAcrossScenes.otherBoosts["goldMine"][1];
                AdvancedLevelUp("goldMine");
                DataAcrossScenes.moneyMultiplier += 0.0001f;

                if (DataAcrossScenes.otherBoosts["goldMine"][0] == 50)
                {
                    goldMineTitle.text = "Gold Mine: Level 50 (Max)";
                    ChangeButtonColour(goldMineButton);
                }
                else
                {
                    goldMineTitle.text = string.Format("Gold Mine: Level {0}", (DataAcrossScenes.otherBoosts["goldMine"][0] + 1));
                }

                goldMineCostText.text = DataAcrossScenes.otherBoosts["goldMine"][1].ToString();
            }

            else
            {
                OnClickNotEnoughCashWrapper();
            }
        }
        else
        {
            Debug.Log("Already at max level!");
        }
    }



    public void AncientSwordPurchase()
    {
        if (DataAcrossScenes.otherBoosts["ancientSword"][0] < 50)
        {
            if (EnoughCash(DataAcrossScenes.otherBoosts["ancientSword"][1]))
            {
                DataAcrossScenes.totalMoney -= DataAcrossScenes.otherBoosts["ancientSword"][1];
                AdvancedLevelUp("ancientSword");
                DataAcrossScenes.attackMultiplier += 0.02f;

                if (DataAcrossScenes.otherBoosts["ancientSword"][0] == 50)
                {
                    ancientSwordTitle.text = "Ancient Sword: Level 50 (Max)";
                    ChangeButtonColour(ancientSwordButton);
                }
                else
                {
                    ancientSwordTitle.text = string.Format("Ancient Sword: Level {0}", (DataAcrossScenes.otherBoosts["ancientSword"][0] + 1));
                }

                ancientSwordCostText.text = DataAcrossScenes.otherBoosts["ancientSword"][1].ToString();
            }

            else
            {
                OnClickNotEnoughCashWrapper();
            }
        }
        else
        {
            Debug.Log("Already at max level!");
        }
    }

    public void JadeSanctuaryPurchase()
    {
        if (DataAcrossScenes.otherBoosts["jadeSanctuary"][0] < 50)
        {
            if (EnoughCash(DataAcrossScenes.otherBoosts["jadeSanctuary"][1]))
            {
                DataAcrossScenes.totalMoney -= DataAcrossScenes.otherBoosts["jadeSanctuary"][1];
                AdvancedLevelUp("jadeSanctuary");
                DataAcrossScenes.hitpointsMultiplier += 0.02f;

                if (DataAcrossScenes.otherBoosts["jadeSanctuary"][0] == 50)
                {
                    jadeSanctuaryTitle.text = "Jade Sanctuary: Level 50 (Max)";
                    ChangeButtonColour(jadeSanctuaryButton);
                }
                else
                {
                    jadeSanctuaryTitle.text = string.Format("Jade Sanctuary: Level {0}", (DataAcrossScenes.otherBoosts["jadeSanctuary"][0] + 1));
                }

                jadeSanctuaryCostText.text = DataAcrossScenes.otherBoosts["jadeSanctuary"][1].ToString();
            }

            else
            {
                OnClickNotEnoughCashWrapper();
            }
        }
        else
        {
            Debug.Log("Already at max level!");
        }
    }


    public void IonMembranePurchase()
    {
        if (DataAcrossScenes.otherBoosts["ionMembrane"][0] < 50)
        {
            if (EnoughCash(DataAcrossScenes.otherBoosts["ionMembrane"][1]))
            {
                DataAcrossScenes.totalMoney -= DataAcrossScenes.otherBoosts["ionMembrane"][1];
                AdvancedLevelUp("ionMembrane");
                DataAcrossScenes.maxIons += 10;

                if (DataAcrossScenes.otherBoosts["ionMembrane"][0] == 50)
                {
                    ionMembraneTitle.text = "Ion-Exchange Membrane: Level 50 (Max)";
                    ChangeButtonColour(ionMembraneButton);
                }
                else
                {
                    ionMembraneTitle.text = string.Format("Ion-Exchange Membrane: Level {0}", (DataAcrossScenes.otherBoosts["ionMembrane"][0] + 1));
                }

                ionMembraneCostText.text = DataAcrossScenes.otherBoosts["ionMembrane"][1].ToString();
            }

            else
            {
                OnClickNotEnoughCashWrapper();
            }
        }
        else
        {
            Debug.Log("Already at max level!");
        }
    }


    public void IonBotPurchase()
    {
        if (!DataAcrossScenes.ionBotPurchased)
        {
            if (DataAcrossScenes.totalMoney >= 5000)
            {
                DataAcrossScenes.totalMoney -= 5000;
                //DataAcrossScenes.maxIons += 10;
                DataAcrossScenes.ionBotPurchased = true;
                ChangeButtonColour(ionBotButton);
            }
        }
    }

    // This is dealing with each ion
    public void sodiumIonPurchase()
    {
        PurchaseIonite(DataAcrossScenes.totalIonites.Keys.ToList()[0]);
        Debug.Log(DataAcrossScenes.totalIonites.Keys);
    }

    public void chlorideIonPurchase()
    {
        PurchaseIonite(DataAcrossScenes.totalIonites.Keys.ToList()[1]);
    }

    public void carbonateIonPurchase()
    {
        PurchaseIonite(DataAcrossScenes.totalIonites.Keys.ToList()[2]);
    }

    public void calciumIonPurchase()
    {
        PurchaseIonite(DataAcrossScenes.totalIonites.Keys.ToList()[3]);
    }

    public void lithiumIonPurchase()
    {
        PurchaseIonite(DataAcrossScenes.totalIonites.Keys.ToList()[4]);
    }

    public void sulfiteIonPurchase()
    {
        PurchaseIonite(DataAcrossScenes.totalIonites.Keys.ToList()[5]);
    }

    public void berylliumIonPurchase()
    {
        PurchaseIonite(DataAcrossScenes.totalIonites.Keys.ToList()[6]);
    }

    public void thiocyanateIonPurchase()
    {
        PurchaseIonite(DataAcrossScenes.totalIonites.Keys.ToList()[7]);
    }

    public void vanadiumIonPurchase()
    {
        PurchaseIonite(DataAcrossScenes.totalIonites.Keys.ToList()[8]);
    }

    public void mercuryIonPurchase()
    {
        PurchaseIonite(DataAcrossScenes.totalIonites.Keys.ToList()[9]);
    }

    public void azideIonPurchase()
    {
        PurchaseIonite(DataAcrossScenes.totalIonites.Keys.ToList()[10]);
    }

    public void peroxodisulfateIonPurchase()
    {
        PurchaseIonite(DataAcrossScenes.totalIonites.Keys.ToList()[11]);
    }

    public void goldIonPurchase()
    {
        PurchaseIonite(DataAcrossScenes.totalIonites.Keys.ToList()[12]);
    }

    public void ozonideIonPurchase()
    {
        PurchaseIonite(DataAcrossScenes.totalIonites.Keys.ToList()[13]);
    }

    public void uraniumIonPurchase()
    {
        PurchaseIonite(DataAcrossScenes.totalIonites.Keys.ToList()[14]);
    }

    public void polyiodideIonPurchase()
    {
        PurchaseIonite(DataAcrossScenes.totalIonites.Keys.ToList()[15]);
    }

    private bool EnoughCash(int cash)
    {
        if (DataAcrossScenes.totalMoney >= cash)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private void BasicLevelUp(string powerUp)
    {
        DataAcrossScenes.powerUps[powerUp][0] += 1; // Adds one to level
        if (DataAcrossScenes.powerUps[powerUp][0] <= 6)
        {
            DataAcrossScenes.powerUps[powerUp][1] += 50;
        }
        else if (DataAcrossScenes.powerUps[powerUp][0] <= 11)
        {
            DataAcrossScenes.powerUps[powerUp][1] += 100;
        }
        else if (DataAcrossScenes.powerUps[powerUp][0] <= 16)
        {
            DataAcrossScenes.powerUps[powerUp][1] += 200;
        }
        else if (DataAcrossScenes.powerUps[powerUp][0] <= 18)
        {
            DataAcrossScenes.powerUps[powerUp][1] += 250;
        }
        else if (DataAcrossScenes.powerUps[powerUp][0] <= 19)
        {
            DataAcrossScenes.powerUps[powerUp][1] += 500;
        } 


    }

    private void ChangeButtonColour(GameObject button)
    {
        GameObject greyPanel = button.transform.GetChild(1).gameObject;
        greyPanel.SetActive(true);

    }


    //private void ChangeButtonColour(Button button)
    //{

    //    ColorBlock cb = button.colors;
    //    cb.normalColor = new Color(105f, 105f, 105f);
    //    cb.highlightedColor = new Color(105f, 105f, 105f);
    //    cb.pressedColor = new Color(105f, 105f, 105f);
    //    button.colors = cb;

    //    Debug.Log(button.GetComponent<Image>().color);

    //}

    public void OnClickMaxLevelWrapper()
    {
        StartCoroutine("OnClickMaxLevel");
    }

    private IEnumerator OnClickMaxLevel()
    {
        GameObject button = EventSystem.current.currentSelectedGameObject;
        maxLevelMessage = button.transform.GetChild(0).gameObject;
        maxLevelMessage.SetActive(true);
        yield return new WaitForSeconds(2f);

        maxLevelMessage.SetActive(false);
    }


    public void OnClickNotEnoughCashWrapper()
    {
        StartCoroutine("OnClickNotEnoughCash");
    }


    private IEnumerator OnClickNotEnoughCash()
    {
        GameObject button = EventSystem.current.currentSelectedGameObject;
        GameObject notEnoughCashMessage = button.transform.GetChild(2).gameObject;
        notEnoughCashMessage.SetActive(true);
        yield return new WaitForSeconds(2f);

        notEnoughCashMessage.SetActive(false);
    }




    private void AdvancedLevelUp(string otherBoost)
    {
        DataAcrossScenes.otherBoosts[otherBoost][0] += 1; // Adds one to level
        if (DataAcrossScenes.otherBoosts[otherBoost][0] <= 49)
        {
            DataAcrossScenes.otherBoosts[otherBoost][1] += 500;
        }

    }


    public void OnClickMaxOutFirstWrapper()
    {
        StartCoroutine("OnClickMaxOutFirst");
    }


    private IEnumerator OnClickMaxOutFirst()
    {
        GameObject button = EventSystem.current.currentSelectedGameObject;
        GameObject maxOutOthersFirstMessage = button.transform.GetChild(0).gameObject;
        maxOutOthersFirstMessage.SetActive(true);

        yield return new WaitForSeconds(2f);

        maxOutOthersFirstMessage.SetActive(false);
    }


    public void OnClickBuyIonBotFirstWrapper()
    {
        StartCoroutine("OnClickBuyIonBotFirst");
    }

    
    private IEnumerator OnClickBuyIonBotFirst() 
    {
        GameObject button = EventSystem.current.currentSelectedGameObject;
        GameObject buyIonBotFirstMessage = button.transform.GetChild(0).gameObject;
        buyIonBotFirstMessage.SetActive(true);

        yield return new WaitForSeconds(2f);

        buyIonBotFirstMessage.SetActive(false);

    }


    private void CheckMaxOut()
    {
        if (!areAllpowerUpsMaxed)
        {
            checkAllpowerUpsMaxed = 0;
            foreach (List<int> levels in DataAcrossScenes.powerUps.Values)
            {
                if (levels[0] == 20)
                {
                    checkAllpowerUpsMaxed += 1;
                }
            }

            if (checkAllpowerUpsMaxed == 4)
            {
                areAllpowerUpsMaxed = true;
            }
        }
        
        if (areAllpowerUpsMaxed)
        {
            foreach (GameObject maxOut in maxOutCovers)
            {
                maxOut.SetActive(false);
            }
        }

    }


    private void CheckIonBotBought()
    {

        if (!areAllpowerUpsMaxed || DataAcrossScenes.ionBotPurchased)
        {
            foreach (GameObject ionBotCover in ionBotCovers)
            {
                ionBotCover.SetActive(false);
            }
        }
        else
        {
            foreach (GameObject ionBotCover in ionBotCovers)
            {
                ionBotCover.SetActive(true);
            }
        }
    }


    private void PurchaseIonite(Ionite ionite)
    {
        if (DataAcrossScenes.totalMoney >= ionite.IonCost)
        {
            DataAcrossScenes.numIons += 1;
            Debug.Log("Ion purchased!");
            DataAcrossScenes.totalMoney -= ionite.IonCost;
            DataAcrossScenes.totalIonites[ionite] += 1;
            DataAcrossScenes.ionBotBaseAttackPower += ionite.AttackPower;
            DataAcrossScenes.ionBotBaseHitpoints += ionite.Hitpoints;
            Debug.Log(DataAcrossScenes.totalIonites[ionite]);

            StartCoroutine("DisplayIonBoughtMessage");

            //if (DataAcrossScenes.numIons >= DataAcrossScenes.maxIons)
            //{
            //    GameObject button = EventSystem.current.currentSelectedGameObject;
            //    GameObject notEnoughStorageMessage = button.transform.GetChild(5).gameObject;
            //    notEnoughStorageMessage.SetActive(true);
            //}
            

        }
        else
        {
            Debug.Log("Not enough cash!");
            OnClickNotEnoughCashWrapper();
        }
        
    }

    private IEnumerator DisplayIonBoughtMessage()
    {
        GameObject button = EventSystem.current.currentSelectedGameObject;
        GameObject ionBoughtMessage = button.transform.GetChild(4).gameObject;
        ionBoughtMessage.SetActive(true);

        yield return new WaitForSeconds(2f);

        ionBoughtMessage.SetActive(false);
    }


    public void OnClickNotEnoughStorageMessageWrapper()
    {
        StartCoroutine("DisplayNotEnoughStorageMessage");
    }

    private IEnumerator DisplayNotEnoughStorageMessage()
    {
        GameObject button = EventSystem.current.currentSelectedGameObject;
        GameObject notEnoughStorageMessage = button.transform.GetChild(0).gameObject;
        notEnoughStorageMessage.SetActive(true);

        yield return new WaitForSeconds(2f);

        notEnoughStorageMessage.SetActive(false);
    }

    private void SetIonNotEnoughStorageCover()
    {
        if (areAllpowerUpsMaxed && DataAcrossScenes.ionBotPurchased)
        {
            if (DataAcrossScenes.numIons >= DataAcrossScenes.maxIons)
            {
                foreach (GameObject ionCover in ionStorageCovers)
                {
                    //Debug.Log("ionCover is active");
                    ionCover.SetActive(true);
                }
            }
            
            else
            {
                foreach (GameObject ionCover in ionStorageCovers)
                {
                    //Debug.Log("ionCover is inactive");
                    ionCover.SetActive(false);
                }
            }
        }
    }


}




public class Ionite
{
    private string _ionName;
    public string IonName
    {
        get
        {
            return _ionName;
        }
        set
        {
            _ionName = value;
        }
    }
    private int _ionCost;
    public int IonCost
    {
        get
        {
            return _ionCost;
        }
        set
        {
            _ionCost = value;
        }
    }
    private float _attackPower;
    public float AttackPower
    {
        get
        {
            return _attackPower;
        }
        set
        {
            _attackPower = value;
        }
    }
    private float _hitpoints;
    public float Hitpoints
    {
        get
        {
            return _hitpoints;
        }
        set
        {
            _hitpoints = value;
        }
    }

    public Ionite(string ionName, int ionCost, float attackPower, float hitpoints)
    {
        _ionName = ionName;
        _ionCost = ionCost;
        _attackPower = attackPower;
        _hitpoints = hitpoints;
    }



}

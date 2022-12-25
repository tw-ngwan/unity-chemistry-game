using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MoneyCounterScript : MonoBehaviour
{

    public Text moneyText;
    public static bool moneyAdded;

    // Start is called before the first frame update
    void Start()
    {
        if (moneyAdded == false)
        {
            moneyAdded = true;
            DataAcrossScenes.totalMoney += IonSceneManager.currentMoney;
            Debug.Log(DataAcrossScenes.totalMoney);
            if (DataAcrossScenes.totalMoney >= DataAcrossScenes.maxMoney)
            {
                DataAcrossScenes.totalMoney = DataAcrossScenes.maxMoney;
            }
            else
            {

            }

            moneyText.text = DataAcrossScenes.totalMoney.ToString();

        }
        
        //Debug.Log("Money text updated");

    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void FixedUpdate()
    {
        // Comment this code out for testing purposes.
        if (DataAcrossScenes.totalMoney >= DataAcrossScenes.maxMoney)
        {
            DataAcrossScenes.totalMoney = DataAcrossScenes.maxMoney;
        }


        moneyText.text = DataAcrossScenes.totalMoney.ToString();
        Debug.Log("From moneycounter: " + DataAcrossScenes.totalMoney);
    }



    public void DisplayDetailedCashInfoWrapper()
    {
        StartCoroutine("DisplayDetailedCashInfo");
    }

    private IEnumerator DisplayDetailedCashInfo()
    {
        GameObject button = EventSystem.current.currentSelectedGameObject;
        GameObject detailedCashInfo = button.transform.GetChild(0).gameObject;
        detailedCashInfo.SetActive(true);
        GameObject detailedCashDetails = detailedCashInfo.transform.GetChild(0).gameObject;
        Text detailedCashInfoText = detailedCashDetails.GetComponent<Text>();
        detailedCashInfoText.text = string.Format("Current cash: {0} \nMax Storage: {1} ", DataAcrossScenes.totalMoney, DataAcrossScenes.maxMoney);

        yield return new WaitForSeconds(2f);

        detailedCashInfo.SetActive(false);
    }
}

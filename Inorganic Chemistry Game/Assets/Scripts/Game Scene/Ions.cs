using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;


public class Ions : MonoBehaviour
{
    public GameObject ion;
    public string ionSymbol; // Work on this
    public int ionCharge;
    public bool multiAtomic;
    public bool ionClicked = false;
    [HideInInspector]
    public int ionScore;


    private Vector3 scaleChange = new Vector3(0.0184f, 0.0184f, 0.0184f);

    private Vector3 maxSize = new Vector3(0.00925f, 0.00925f, 0.00925f);

    private Vector3 currentSize = new Vector3(0.00005f, 0.00005f, 0.00005f);


    // This is to control glow effect of ions 
    public Light2D myLight;
    public Rigidbody2D myBody;


    // This is to control if ion is to be destroyed
    public static Ions referenceIon;
    public bool destroyIon = false;

    private void Awake()
    {
        //myBody = GetComponent<Rigidbody2D>();
        //if (myBody != null)
        //{
        //    Debug.Log("Found");
        //}
        //else { Debug.Log("Not found"); }
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    public void selectIon()
    {

        // Getting the light 
        //myLight = ion.GetComponent<Light2D>();

        // Updating the ionsOnScreen dictionary so you know what to destroy 



        // Denoting the point system for the ions 
        if (ionCharge == 1 || ionCharge == -1)
        {
            ionScore = 500;
        }
        else if (ionCharge == 2 || ionCharge == -2)
        {
            ionScore = 800;
        }
        else
        {
            ionScore = 1000;
        }


        // Selecting an ion 
        if (ionClicked == false)
        {
            ionClicked = true;
            IonSceneManager.totalCharge += ionCharge;
            if (!IonSceneManager.allIons.ContainsKey(ionSymbol))
            {
                IonSceneManager.allIons[ionSymbol] = 1;
            }
            else
            {
                IonSceneManager.allIons[ionSymbol] += 1;
            }
            IonSceneManager.totalScore += ionScore;

            //Debug.Log(ionSymbol + " is selected");
            //Debug.Log(IonSceneManager.allIons);
            //Debug.Log("totalCharge is " + IonSceneManager.totalCharge);
            //Debug.Log("totalScore is " + IonSceneManager.totalScore);
            IonSceneManager.destroyIonsList.Add(ion);

            // Changing the brightness of light 
            myLight.intensity += 1.2f;
            
        }

        
        // Deselecting an ion 
        else
        {
            ionClicked = false;
            IonSceneManager.totalCharge -= ionCharge;
            IonSceneManager.allIons[ionSymbol] -= 1;
            IonSceneManager.totalScore -= ionScore;

            //Debug.Log(ionSymbol + " is deselected");
            //Debug.Log(IonSceneManager.allIons);
            //Debug.Log(IonSceneManager.totalCharge);
            //Debug.Log(IonSceneManager.totalScore);
            IonSceneManager.destroyIonsList.Remove(ion);

            // Changing glow back to 0
            myLight.intensity -= 1.2f;

        }

    }


    // Update is called once per frame
    void Update()
    {
        // This is used to grow the ions (no longer growing in IonSpawner) 
        if (ion.transform.localScale.x < 0.00925f)
        {
            //Debug.Log("Growing");
            ion.transform.localScale += scaleChange * Time.deltaTime;
        }


        // This is used to move the ion out of position and rotate it
        //ion.transform.position += new Vector3(0.2f, -5f, 0) * Time.deltaTime;
        //ion.transform.Rotate(0, 0, -0.3f);

        if (destroyIon)
        {
            if (ion.transform.localScale.x > 0.00002f)
            {
                ion.transform.localScale -= scaleChange * Time.deltaTime;
            }
        }
        
        if (ion.transform.localScale.x < 0.00005f)
        {
            Destroy(ion);
        }

    }
}

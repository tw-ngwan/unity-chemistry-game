using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Headquarters : MonoBehaviour
{

    public static int numberOfHitsRemaining;
    private int maxNumHits = 20;

    public Slider healthbar;

    public static bool headquartersAlive;


    private void Awake()
    {
        headquartersAlive = true;
        numberOfHitsRemaining = maxNumHits;
    }
    // Start is called before the first frame update
    void Start()
    {
        healthbar.maxValue = maxNumHits;
        healthbar.value = numberOfHitsRemaining;

    }

    // Update is called once per frame
    void Update()
    {
        if (numberOfHitsRemaining <= 0)
        {
            numberOfHitsRemaining = 0;
            headquartersAlive = false;
        }

        healthbar.value = numberOfHitsRemaining;
    }
}

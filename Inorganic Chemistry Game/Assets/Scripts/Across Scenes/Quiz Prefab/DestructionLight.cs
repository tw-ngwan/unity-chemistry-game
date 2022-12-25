using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;



public class DestructionLight : MonoBehaviour
{
    public Light2D flashLight;
    private bool growLight = true;
    

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // For some reason this just doesn't show up. You need to debug this...
        if (flashLight.pointLightOuterRadius < 1 && growLight == true)
        {
            //Debug.Log("Light is growing");
            flashLight.pointLightInnerRadius += 5f * Time.deltaTime;
            flashLight.pointLightOuterRadius += 5f * Time.deltaTime;
        }

        if (flashLight.pointLightOuterRadius >= 1 || growLight == false)
        {
            growLight = false;
            //Debug.Log("Light is shrinking");
            flashLight.pointLightInnerRadius -= 5f * Time.deltaTime;
            flashLight.pointLightOuterRadius -= 5f * Time.deltaTime;
        }
        if (flashLight.pointLightOuterRadius < 0.1 && growLight == false)
        {
            //Debug.Log("Light destroyed");
            Destroy(flashLight);
            growLight = true;
        }

    }
}

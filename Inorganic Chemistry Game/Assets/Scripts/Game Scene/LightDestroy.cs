using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class LightDestroy : MonoBehaviour
{
    public Light2D flashLight;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (flashLight.pointLightOuterRadius <= 1)
        {
            flashLight.pointLightInnerRadius += 2 * Time.deltaTime;
            flashLight.pointLightOuterRadius += 2 * Time.deltaTime;
        }

        if (flashLight.pointLightOuterRadius > 1)
        {
            flashLight.pointLightInnerRadius -= 2 * Time.deltaTime;
            flashLight.pointLightOuterRadius -= 2 * Time.deltaTime;
        }
        if (flashLight.pointLightOuterRadius < 0.1)
        {
            Destroy(flashLight);
        }
        
    }
}

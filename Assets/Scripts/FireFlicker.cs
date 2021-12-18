using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class FireFlicker : MonoBehaviour
{
    private Light2D lightSource;
    private float max, min, multiplier = 1f;
    void Start()
    {
        lightSource = GetComponent<Light2D>();
        max = lightSource.pointLightInnerRadius + 0.5f;
        min = lightSource.pointLightInnerRadius;
        InvokeRepeating("Flicker", 0.035f, 0.035f);
    }

    private void Flicker()
    {
        lightSource.pointLightInnerRadius += (0.05f * multiplier);
        if (lightSource.pointLightInnerRadius >= max)
        {
            multiplier *= -1;
        }

        if (lightSource.pointLightInnerRadius <= min)
        {
            multiplier *= -1;
        }
    }
}

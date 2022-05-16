using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class IndicatorState : MonoBehaviour
{
    public SpriteRenderer body;
    public Light2D bulb;
    public bool enabledFlash;
    public bool flashOn;
    public float flashOnTime;
    public float flashOffTime;
    public float flashCountdown;


    private void Start()
    {
        bulb = GetComponent<Light2D>();
    }

    private void Update()
    {

        flashCountdown -= Time.deltaTime;

        if (flashCountdown < 0)
        {

            if (enabledFlash && flashOn)
            {
                FlashOff();
            }
            else if (enabled && !flashOn)
            {
                FlashOn();
            }
            else if (!enabledFlash)
            {
                bulb.enabled = true;
            }
        }
    }
    public void SetLight(Color color, float intensity, bool flash, float flashTimeOn, float flashTimeOff)
    {
        bulb.color = color;
        bulb.intensity = intensity;
        enabledFlash = flash;
        flashOnTime = flashTimeOn;
        flashOffTime = flashTimeOff;

    }

    public void FlashOff()
    { 
        flashOn = false;
        bulb.enabled = false;
        flashCountdown = flashOffTime;
    }

    public void FlashOn()
    {
        bulb.enabled = true;
        flashOn = true;
        flashCountdown = flashOnTime;
    }
}


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class IndicatorState : MonoBehaviour
{
    public GameObject machine;
    public SpriteRenderer body;

    //Color indication
    public SpriteRenderer lightColor;
    public Sprite[] lights;
    public Color[] lightColors;
    public float[] lightsIntensity;
    public Light2D colorEmitted;        //works even if it doesn't seem to
    public float lightIntensity = 1f;
    public float blinkTimer;
    float blinkIntervall = 1f;          //seconds
    public bool doBlink;
    public bool blinked;
    public int _lightState = 0;

    //Sprite indicatorBody;
    //Sprite greenLight;
    //Sprite redLight;
    //Sprite blueLight;
    //Sprite yellowLight;

    private void Start()
    {
        //colorEmitted = GetComponent<Light2D>();
        body.sprite = lights[0];

        lightColors = new Color[5];
        lightColors[0] = Color.cyan;        //invalid state
        lightColors[1] = Color.green;        //running - green
        lightColors[2] = Color.yellow;        //stopped - yellow
        lightColors[3] = Color.blue;        //service - blue
        lightColors[4] = Color.red;         //broken - red

        lightsIntensity = new float[5];
        lightsIntensity[0] = 2;
        lightsIntensity[1] = 1;
        lightsIntensity[2] = 1;
        lightsIntensity[3] = 1;
        lightsIntensity[4] = 1;

        SetLight(2);
    }

    private void Update()
    {
        
        FlashLight();
    }

    public void SetLight(int lightState)    //use state as index for specific state indicator light parameters.
    {
        _lightState = lightState;
        lightColor.sprite = lights[lightState];
        colorEmitted.color = lightColors[lightState];
        colorEmitted.intensity = lightsIntensity[lightState];
        Debug.Log("Set lightIntensity to: " + lightState);


        Debug.Log("trying to set lights to: " + lightState);

        if (lightState != 1)
        {
            if (doBlink != true)
            {
                colorEmitted.intensity = 1f;

            }

            if (lightState == 4)
            {
                SetFlashing();
            }


        }
        else {
            ReSetFlashing();
        }

    }



    public void SetFlashing() {
        doBlink = true;
        //Debug.Log("SetFlashing called");
    }

    public void ReSetFlashing() {
        doBlink = false;
        //Debug.Log("ResetFlashing called");
    }
    public void FlashLight()
    {
        blinkTimer -= Time.deltaTime;


        if (blinkTimer <= 0f && doBlink && !blinked)
        {
            blinked = true;
            blinkTimer = blinkIntervall;
            //turn light on
            //colorEmitted.intensity = 5.0f;
            //Debug.Log("Blinklight on");
            
        }
        else if (blinkTimer <= 0f && doBlink && blinked)
        {
            blinked = false;
            blinkTimer = blinkIntervall;
            //turn light off
            colorEmitted.intensity = 0.1f;
            
            //Debug.Log("Blinklight off");
        }
    }
}


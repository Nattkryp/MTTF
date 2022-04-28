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
    public Light2D colorEmitted;        //works even if it doesn't seem to
    public float lightIntensity = 1f;
    public float blinkTimer;
    float blinkIntervall = 1f;          //seconds
    public bool doBlink;
    public bool blinked;

    //Sprite indicatorBody;
    //Sprite greenLight;
    //Sprite redLight;
    //Sprite blueLight;
    //Sprite yellowLight;

    private void Start()
    {
        colorEmitted = GetComponent<Light2D>();
        body.sprite = lights[0];

        lightColors = new Color[5];
        lightColors[0] = Color.cyan;        //invalid state
        lightColors[1] = Color.green;        //running - green
        lightColors[2] = Color.yellow;        //stopped - yellow
        lightColors[3] = Color.blue;        //service - blue
        lightColors[4] = Color.red;         //broken - red

        SetLight(2);
    }

    private void Update()
    {

        FlashLight();
    }

    public void SetLight(int lightState)
        { if (doBlink != true) {
            colorEmitted.intensity = 1f;
            lightColor.sprite = lights[lightState];
            colorEmitted.color = lightColors[lightState];

            if (lightState == 4)
            {
                SetFlashing();
            }
        }

        }

    public void SetFlashing() {
        doBlink = true;
        Debug.Log("SetFlashing called");
    }

    public void ReSetFlashing() {
        doBlink = false;
    }
    public void FlashLight()
    {
        blinkTimer -= Time.deltaTime;


        if (blinkTimer <= 0f && doBlink && !blinked)
        {
            blinkTimer = blinkIntervall;
            //turn light on
            colorEmitted.intensity = 5.0f;
            Debug.Log("Blinklight on");
            blinked = true;
        }
        else if (blinkTimer <= 0f && doBlink && blinked)
        {
            blinkTimer = blinkIntervall;
            //turn light off
            colorEmitted.intensity = 0.1f;
            blinked = false;
            Debug.Log("Blinklight off");
        }
    }
}


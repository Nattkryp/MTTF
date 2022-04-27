using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IndicatorState : MonoBehaviour
{
    public GameObject machine;
    public SpriteRenderer body;
    public SpriteRenderer lightColor;
    public Sprite[] lights;
    public AudioSource audio;
    //Sprite indicatorBody;
    //Sprite greenLight;
    //Sprite redLight;
    //Sprite blueLight;
    //Sprite yellowLight;

    private void Start()
    {
        SetLight(2);
    }

    public void SetLight(int lightState)
        {
        body.sprite = lights[0];
        lightColor.sprite = lights[lightState];
        audio.Play();
        }
}


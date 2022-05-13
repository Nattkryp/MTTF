using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Need : MonoBehaviour
{
    protected string needType = "BaseType";
    protected string description = "Just a BaseNeed";
    protected string txtCondition = "Lagom";
    public float maxValue = 100f;
    public float startValue = 100f;
    public float currentValue = 100f;
    public float decayRate = 0.1f;
    public float hhLimit = 80f;
    public float hLimit = 60f;
    public float lLimit = 30f;
    public float llLimit = 0f;
    public bool belowHH;
    public bool belowH;
    public bool belowL;
    public bool belowLL;
    public string vendorTag;

    public void AddValue(float amountToAdd) {
        currentValue += amountToAdd;
        Debug.Log("recovered " + amountToAdd);
    }

    protected void Update()
    {
        currentValue -= decayRate * Time.deltaTime;
        belowHH = EvaluateLimit(currentValue, hhLimit);
        belowH = EvaluateLimit(currentValue, hLimit);
        belowL = EvaluateLimit(currentValue, lLimit);
        belowLL = EvaluateLimit(currentValue, llLimit);
        currentValue = Mathf.Clamp(currentValue,0,maxValue);
    }

    protected virtual bool EvaluateLimit(float value, float limit)
    {
        if (value < limit)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    protected virtual void SetNewLimitValueHH(float value) { hhLimit = value; }
    protected virtual void SetNewLimitValueH(float value) { hhLimit = value; }
    protected virtual void SetNewLimitValueL(float value) { lLimit = value; }
    protected virtual void SetNewLimitValueLL(float value) { llLimit = value; }
    protected virtual void SetNewMaxValue(float value) { maxValue = value; }
    



}

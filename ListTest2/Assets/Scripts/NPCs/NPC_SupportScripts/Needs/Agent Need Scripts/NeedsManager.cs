using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NeedsManager : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;
    public List <Need> needList;

    private int numberOfNeeds;
    private float sumOfNeeds;
    public float overallValue;

    private float totalMaxValue;
    private float totalCurrValue;

    public Need highestPrioNeed;
    public int highestPrioNeedPrio = 999;

    private void Start()
    {
        needList = new List<Need>();
        foreach (var need in transform.GetComponentsInChildren<Need>())
        {
            needList.Add(need);
        }
        numberOfNeeds = needList.Count;

        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        overallValue = GetOverallHealth();
        //spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, Mathf.Clamp(GetOverallHealth(),0.2f,2f));
        
        highestPrioNeed = GetHighestPriorityNeed();
        //Debug.Log("Highest current priority need is: " + highestPrioNeed.ToString() + " with a priority of " + highestPrioNeedPrio);
    }

    public float GetOverallHealth() {
        float accCurrent = 0f;
        float accMax = 0f;
        

        foreach (Need need in needList)
        {
            accCurrent += need.currentValue;
            accMax += need.maxValue;
        }

        return accCurrent / accMax;
    }
    public int GetHighestPriorityNeedPriority() {
        return highestPrioNeedPrio;
    }
    

    public Need GetHighestPriorityNeed()
    {

        int highestPrio = 999;
        Need highestPrioNeed = null;

        foreach (Need need in needList)
        {
            
            int specificHighest = 999;
            if (need.belowHH == true)
            {
                specificHighest = 4;

                if (need.belowH == true)
                {
                    specificHighest = 3;
                    if (need.belowL == true)
                    {
                        specificHighest = 2;

                        if (need.belowLL == true)
                        {
                            specificHighest = 1;

                        }
                    }
                }
            }
            else
            {
                specificHighest = 998;
            }

            if (highestPrio > specificHighest) {
                highestPrio = specificHighest;
                highestPrioNeed = need;
                highestPrioNeedPrio = specificHighest;
            }
        }

        return highestPrioNeed;

        
    }

}

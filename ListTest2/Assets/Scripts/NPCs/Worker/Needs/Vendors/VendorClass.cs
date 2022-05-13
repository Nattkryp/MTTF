using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VendorClass : IVendor
{
    public Need need { get; set; }
    
    public float supplyAmount { get; set; }


    public VendorClass(float _supplyAmount, Need _need)
    {

        supplyAmount = _supplyAmount;
        need = _need;
    }

    public void PlayVendingAnimation()
    {
        Debug.Log("Playing animation");
    }
    public void SupplyNeed(AgentController agent, Need need, float supplyAmount)
    {
        string wantedType = need.GetType().Name;
        
        Need[] agentComponents = agent.GetComponents<Need>();
        foreach (var resultedNeed in agentComponents)
        {
            if (Equals(resultedNeed.GetType(), need.GetType()))
            {
                Debug.Log("found a match :)");
                resultedNeed.AddValue(supplyAmount);

            }
            else
            {
                Debug.Log("not corret type, not going to boost that!");
            }

        }

    }

    public void InteractVendor(AgentController agent, Need need)
    {
        SupplyNeed(agent, need, supplyAmount);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour, IInteractable
{
    public void InteractVendor(AgentController agent, Need need)
    {
        gameObject.GetComponent<VendingMachine>().vendor.InteractVendor(agent, need); 
    }
}
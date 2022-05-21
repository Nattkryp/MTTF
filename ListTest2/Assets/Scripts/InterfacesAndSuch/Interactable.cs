using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    public virtual void Interact(AgentController agent)
    {
        Debug.Log("Default interact to be overwritten by derived");
    }
}
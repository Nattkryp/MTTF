using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInteractable
{
    public void InteractVendor(AgentController agent, Need need);
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcEnergy : Need
{
    

    public string vendorTagname = "energyVendor";
    public void Start()
    {
        needType = "Energy";
        vendorTag = "EnergyVendor";

        
    }
}

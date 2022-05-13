using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcHealth : Need
{
    public string vendorTagname = "healthVendor";
    public void Start()
    {
        needType = "Health";
        vendorTag = "HealthVendor";

    }

    public void AddtoValue(float amountToAdd)
    {
        AddtoValue(amountToAdd);
    }
}

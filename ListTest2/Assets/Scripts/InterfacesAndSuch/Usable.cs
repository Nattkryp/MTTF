using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Usable : MonoBehaviour
{
    public bool isAvailable = true;
    public GameObject reservedby;

    public bool GetAvailability()
    {
        return isAvailable;
    }

    public void Reserve(GameObject reserver)
    {
        if (isAvailable)
        {
            isAvailable = false;
            reservedby = reserver;
        }
    }

    public void ResetAvailability()
    {
        isAvailable = true;
        reservedby = null;
    }
}

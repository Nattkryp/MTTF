using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakRoomStool : Usable
{
    Transform useTransformPosition;

    private void Start()
    {
        useTransformPosition = transform.GetChild(0).transform;
    }

    public Transform GetSitPosition()
    {
        return useTransformPosition;
    }
}

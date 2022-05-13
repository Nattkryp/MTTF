using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeScheduler : MonoBehaviour
{
    public TimeController timecontroller;
    public int hourOfDay = 3;
    [SerializeField] public List<Sched_HourClass> scheduleDay;
    

    // Start is called before the first frame update
    void Start()
    {


        hourOfDay = GetComponent<TimeController>().hour;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

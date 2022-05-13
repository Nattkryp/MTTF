using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeController : MonoBehaviour
{
    //[Range(1,100)] 
    public int timescale;

    public int tot_mins;
    public int hour;
    public int minute;
    public int day;

    public string gameTime;
    // Start is called before the first frame update
    void Start()
    {
      
    }

    // Update is called once per frame
    void Update()
    {
        //DailyCalendarScript.onHourChange += SetTimeScaleBasedOnActivity;
        //Time.timeScale = timescale;
        tot_mins = Mathf.FloorToInt(Time.time); 
        //hour = Mathf.FloorToInt(minute / 60);
        //day = Mathf.FloorToInt(hour / 24);
        

        //hour

        
        day = tot_mins / 1440;
        hour = (tot_mins % 1440) / 60;
        minute = tot_mins % 60;
        gameTime = "Day: " + day.ToString("00") + " " + hour.ToString("00") + ":" + minute.ToString("00");
    }
    public void SetTimeScaleBasedOnActivity(int i)
    {
        if (i == 3)
        {
            timescale = 75;
        }
        else
        {
            timescale = 10;
        }
    }
    public void SetTimeScale(int x)
    { 
        timescale = x;
    }
}

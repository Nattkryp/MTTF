using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Sched_HourClass : MonoBehaviour
{
    public int hour;
    public string activity;
    public int activityNo;
    public Text txtHour;
    public Text txtActivity;

    private void Start()
    {
        txtHour.text = hour.ToString("00");
        txtActivity.text = activity.ToString();
    }

    private void Update()
    {
        switch (activityNo)
        {
            case 0:
                //agents stop in track for some seconds
                activity = "Freeze";
                break;
            case 1:
                //agents go to the machine. Agent's needs are consumed
                activity = "Work";
                break;

            case 2:
                //agents figure out what they need and sort that out. Agent's need-decay is haltet.
                activity = "Breaktime";
                break;

            case 3:
                activity = "Spare time";
                break;

            default:
                // code block
                break;
        }
        txtActivity.text = activity.ToString();

    }

    //if the game hour is the same as this objects hour, this object will tell some manager to broadcast that it's time for a specific activity.

}

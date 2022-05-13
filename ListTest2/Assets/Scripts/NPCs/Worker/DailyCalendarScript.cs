using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class DailyCalendarScript : MonoBehaviour
{
    public TimeController timeController;
    public List<Sched_HourClass> scheduleDay;
    public GameObject rowPrefab;
    public int currentActivityNo;
    public string currentActivity;
    public int currentHour;
    public int lastHour;

    public delegate void OnHourChangedEvent(int currentActivityNo);
    public static event OnHourChangedEvent onHourChange;

    private void Start()
    {

        //Morning
        //for (int i = 0; i < 6; i++)
        //{
        //    GameObject go = Instantiate(rowPrefab, transform);
        //    go.GetComponent<Sched_HourClass>().hour = i;
        //    go.GetComponent<Sched_HourClass>().activityNo = 3;
        //}

        //Day
        for (int i = 0; i < 24; i++)
        {
            GameObject go = Instantiate(rowPrefab, transform);
            go.GetComponent<Sched_HourClass>().hour = i;
            go.GetComponent<Sched_HourClass>().activityNo = UnityEngine.Random.Range(1, 3);
        }


        //Evening
        //for (int i = 19; i < 24; i++)
        //{
        //    GameObject go = Instantiate(rowPrefab, transform);
        //    go.GetComponent<Sched_HourClass>().hour = i;
        //    go.GetComponent<Sched_HourClass>().activityNo = 3;
        //}

        transform.GetChild(currentHour).GetComponent<Image>().color = Color.green;
    }

    private void Update()
    {
        currentHour = timeController.hour;
        currentActivityNo = transform.GetChild(currentHour).GetComponent<Sched_HourClass>().activityNo;
        currentActivity = transform.GetChild(currentHour).GetComponent<Sched_HourClass>().activity;

        if (currentHour != lastHour)
        {

            if (transform.GetChild(currentHour).GetComponent<Image>().color == Color.green)
            {
                transform.GetChild(currentHour).GetComponent<Image>().color = Color.gray;
            }
            else
            {
                transform.GetChild(currentHour).GetComponent<Image>().color = Color.green;
            }

            onHourChange?.Invoke(currentActivityNo);

        }

        lastHour = currentHour;
    }

    public void hourChangedEvent()
    {
        
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class CalendarManager : MonoBehaviour
{
    public TimeController timeController;
    public List<CalendarRowScript> scheduleDay;
    public GameObject rowPrefab;
    public int currentActivityNo;
    public string currentActivity;
    public int currentHour;
    public int lastHour = 23;

    public delegate void OnHourChangedEvent(int currentActivityNo);
    public static event OnHourChangedEvent onHourChange;

    private void Start()
    {
        timeController = GameObject.Find("TimeController").GetComponent<TimeController>() ;


        //Test work
        //for (int i = 0; i < 6; i++)
        //{
        //    GameObject go = Instantiate(rowPrefab, transform);
        //    go.GetComponent<Sched_HourClass>().hour = i;
        //    go.GetComponent<Sched_HourClass>().activityNo = 1;
        //}

        //Test breakroom 2h, work 2h, 2h break
        //for (int i = 0; i < 2; i++)
        //{
        //    GameObject go = Instantiate(rowPrefab, transform);
        //    go.GetComponent<Sched_HourClass>().hour = i;
        //    go.GetComponent<Sched_HourClass>().activityNo = 1;
        //}

        //for (int i = 2; i < 5; i++)
        //{
        //    GameObject go = Instantiate(rowPrefab, transform);
        //    go.GetComponent<Sched_HourClass>().hour = i;
        //    go.GetComponent<Sched_HourClass>().activityNo = 2;
        //}

        //for (int i = 5; i < 8; i++)
        //{
        //    GameObject go = Instantiate(rowPrefab, transform);
        //    go.GetComponent<Sched_HourClass>().hour = i;
        //    go.GetComponent<Sched_HourClass>().activityNo = 1;
        //}


        //Test breakroom 6h in a row
        //for (int i = 0; i < 6; i++)
        //{
        //    GameObject go = Instantiate(rowPrefab, transform);
        //    go.GetComponent<Sched_HourClass>().hour = i;
        //    go.GetComponent<Sched_HourClass>().activityNo = 2;
        //}

        //Set morning
        //for (int i = 0; i < 6; i++)
        //{
        //    GameObject go = Instantiate(rowPrefab, transform);
        //    go.GetComponent<Sched_HourClass>().hour = i;
        //    go.GetComponent<Sched_HourClass>().activityNo = 1;
        //}

        //Day

        for (int i = 0; i < 7; i++)
        {
            GameObject go = Instantiate(rowPrefab, transform);
            go.GetComponent<CalendarRowScript>().hour = i;
            go.GetComponent<CalendarRowScript>().activityNo = 1;
        }

        for (int i = 7; i < 8; i++)
        {
            GameObject go = Instantiate(rowPrefab, transform);
            go.GetComponent<CalendarRowScript>().hour = i;
            go.GetComponent<CalendarRowScript>().activityNo = 2;
        }
        for (int i = 8; i < 12; i++)
        {
            GameObject go = Instantiate(rowPrefab, transform);
            go.GetComponent<CalendarRowScript>().hour = i;
            go.GetComponent<CalendarRowScript>().activityNo = 1;
        }
        for (int i = 12; i < 13; i++)
        {
            GameObject go = Instantiate(rowPrefab, transform);
            go.GetComponent<CalendarRowScript>().hour = i;
            go.GetComponent<CalendarRowScript>().activityNo = 2;
        }

        for (int i = 13; i < 24; i++)
        {
            GameObject go = Instantiate(rowPrefab, transform);
            go.GetComponent<CalendarRowScript>().hour = i;
            go.GetComponent<CalendarRowScript>().activityNo = 1;
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
        currentActivityNo = transform.GetChild(currentHour).GetComponent<CalendarRowScript>().activityNo;
        currentActivity = transform.GetChild(currentHour).GetComponent<CalendarRowScript>().activity;

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

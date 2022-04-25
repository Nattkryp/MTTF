using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorkOrder
{

/// <summary>
/// This is not complete or even working :)
/// </summary>

    //Workorder is a Container for a sequence of tasks.

    public WorkOrder workOrder;
    public float numberOfTasks;     //This number is supposed to be used to generate a Work-order specific tasklist which are to be executed in sequence
    public string description;      //"Repair Machine x"
    public int priority;            //Priority on workorder is urgency based on factory perspective, not to be confused with workers own task priority list



    //a created workorder should be added to some work-order list

    private WorkOrder()
    {
        workOrder = new WorkOrder();     //Create an instance of this workorder(?)
    }

    public void GenerateTasks()
    {
        //Should this be abstract method, and have each instance of a workorder (sub class?) define the type of tasks?
    }
}



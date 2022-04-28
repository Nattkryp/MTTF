using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;


public interface ITask
{
    enum Status
    {
        Available,
        Ongoing,
        OnHold,
        Completed
    }

    int priority { get; set; }
    string title { get; set; }
    string desc { get; set; }
    GameObject owner { get; set; }
    Status status { get; set; }

    void DoTask();
}

public class TaskWalkToPosition : ITask
{
    public int priority { get; set; }
    public string title { get; set; }
    public string desc { get; set; }
    public GameObject owner { get; set; }
    public ITask.Status status { get; set; }
    public Vector2 destination { get; set; }
    ITask.Status ITask.status { get; set; }

    public TaskWalkToPosition(int priority, Vector2 destination)
    {
        this.priority = priority;
        this.title = "Walk to position";
        this.desc = "A task of walking to a specific location";
        this.destination = destination;
        this.status = ITask.Status.Available;
    }
    public void DoTask()
    {
        if(owner != null && destination != null)
        {
            var ownerTask = owner.GetComponent<WorkerAIScript>().myCurrentTask;
            var ownerAIPath = owner.GetComponent<AIPath>();
            if (ownerTask.status != ITask.Status.Ongoing)
            {
                ownerTask.status = ITask.Status.Ongoing;
            }
            if (ownerAIPath.destination != (Vector3)destination)
            {
                ownerAIPath.destination = (Vector3)destination;
            }
            if (ownerAIPath.canMove != true)
            {
                ownerAIPath.canMove = true;
            }
            if (ownerAIPath.reachedEndOfPath)
            {
                if (ownerAIPath.canMove != false)
                {
                    ownerAIPath.canMove = false;
                }
                if(ownerTask.status != ITask.Status.Completed)
                {
                    ownerTask.status = ITask.Status.Completed;
                }
            }
        }        
    }
}

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
        Completed
    }

    int priority { get; set; }
    string title { get; set; }
    string desc { get; set; }
    GameObject owner { get; set; }
    Status status { get; set; }

    void DoTask();
}

public class Task_WalkToPosition : ITask
{
    public int priority { get; set; }
    public string title { get; set; }
    public string desc { get; set; }
    public GameObject owner { get; set; }
    public ITask.Status status { get; set; }
    public Vector2 destination { get; set; }
    ITask.Status ITask.status { get; set; }

    public Task_WalkToPosition(int priority, Vector2 destination)
    {
        this.priority = priority;
        this.title = "Walk to position";
        this.desc = "A task of walking to a specific location";
        this.destination = destination;
        this.status = ITask.Status.Available;
    }
    public void DoTask()
    {
        if (owner != null && destination != null)
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
                if (ownerTask.status != ITask.Status.Completed)
                {
                    ownerTask.status = ITask.Status.Completed;
                }

            }
        }
    }
}

public class Task_Patrol : ITask
{
    public int priority { get; set; }
    public string title { get; set; }
    public string desc { get; set; }
    public GameObject owner { get; set; }
    public ITask.Status status { get; set; }
    public List<Vector2> destinations { get; set; }
    public int activeDestinationId { get; set; }
    ITask.Status ITask.status { get; set; }
    public float seconds { get; set; }

    public Task_Patrol(int priority, List<Vector2> destinations, float seconds)
    {
        this.priority = priority;
        this.title = "Patrol";
        this.desc = "A patrol task of walking to different location within a specific time limit";
        this.destinations = destinations;
        this.status = ITask.Status.Available;
        this.seconds = seconds;
        this.activeDestinationId = -1;
    }
    public void DoTask()
    {
        if (owner != null && destinations.Count != 0)
        {
            var ownerTask = owner.GetComponent<WorkerAIScript>().myCurrentTask;
            var ownerAIPath = owner.GetComponent<AIPath>();

            if (ownerTask.status != ITask.Status.Ongoing)
            {
                ownerTask.status = ITask.Status.Ongoing;
            }

            this.seconds -= 1 * Time.deltaTime;
            if (this.seconds > 0)
            {
                if (activeDestinationId == -1)//Set closest position as starting position.
                {
                    float closestDistance = 1000;
                    for (int i = 0; i < destinations.Count - 1; i++)
                    {
                        float distance = Vector2.Distance((Vector2)owner.GetComponent<Transform>().position, destinations[i]);
                        if (distance < closestDistance)
                        {
                            closestDistance = distance;
                            activeDestinationId = i;
                        }
                    }
                }

                if (ownerAIPath.destination != (Vector3)destinations[activeDestinationId])
                {
                    ownerAIPath.destination = (Vector3)destinations[activeDestinationId];
                }
                if (ownerAIPath.canMove != true)
                {
                    ownerAIPath.canMove = true;
                }

                if (ownerAIPath.reachedEndOfPath)
                {
                    activeDestinationId++;
                    if (activeDestinationId >= destinations.Count)
                    {
                        activeDestinationId = 0;
                    }
                }
            }
            else
            {
                if (ownerAIPath.destination != owner.GetComponent<Transform>().position)
                {
                    ownerAIPath.destination = owner.GetComponent<Transform>().position;
                }
                if (ownerTask.status != ITask.Status.Completed)
                {
                    ownerTask.status = ITask.Status.Completed;
                    activeDestinationId = -1;
                }
            }
        }
    }
}

public class Task_SetStateOnMachine : ITask
{
    public int priority { get; set; }
    public string title { get; set; }
    public string desc { get; set; }
    public GameObject owner { get; set; }
    public ITask.Status status { get; set; }
    public GameObject machine { get; set; }
    ITask.Status ITask.status { get; set; }
    public int machineState { get; set; }
    public float clickingtime { get; set; }

    public Task_SetStateOnMachine(int priority, GameObject machine, int machineState)
    {
        this.priority = priority;
        this.title = "Change state on machine";
        this.desc = "A task of walking to a machine and changing its state";
        this.machine = machine;
        this.status = ITask.Status.Available;
        this.machineState = machineState;
        this.clickingtime = 1;
    }
    public void DoTask()
    {
        if (owner != null && machine != null)
        {
            var ownerTask = owner.GetComponent<WorkerAIScript>().myCurrentTask;
            var ownerAIPath = owner.GetComponent<AIPath>();

            if (ownerTask.status != ITask.Status.Ongoing)
            {
                ownerTask.status = ITask.Status.Ongoing;
            }

            Vector2 targetpos = new Vector2(machine.transform.position.x - 0.5f, machine.transform.position.y - 0.5f);//In front of.)
            if (ownerAIPath.destination != (Vector3)targetpos)
            {
                ownerAIPath.destination = (Vector3)targetpos;
            }
            if (ownerAIPath.canMove != true)
            {
                ownerAIPath.canMove = true;
            }

            if (Vector2.Distance((Vector2)owner.transform.position, targetpos) <= 0.2f)
            {
                if (ownerAIPath.destination != owner.transform.position)
                {
                    ownerAIPath.destination = owner.transform.position;
                }

                this.clickingtime -= 1 * Time.deltaTime;

                if (clickingtime <= 0)
                {
                    machine.GetComponent<Machine>().SetOrderedState(machineState);
                    if (ownerTask.status != ITask.Status.Completed)
                    {
                        ownerTask.status = ITask.Status.Completed;
                    }
                }
            }
        }
    }
}

public class Task_RepairMachine : ITask
{
    public int priority { get; set; }
    public string title { get; set; }
    public string desc { get; set; }
    public GameObject owner { get; set; }
    public ITask.Status status { get; set; }
    public GameObject machine { get; set; }
    ITask.Status ITask.status { get; set; }
    public float timeOnEachPosition { get; set; }
    public float timeOnEachPositionCounter { get; set; }
    public List<Vector2> destinations { get; set; }
    public Vector2 targetPos { get; set; }
    public bool repairOnEachPos { get; set; }

    public Task_RepairMachine(int priority, GameObject machine)
    {
        this.priority = priority;
        this.title = "Repair machine";
        this.desc = "A task of walking to a machine and repairing it";
        this.machine = machine;
        this.status = ITask.Status.Available;
        this.timeOnEachPosition = 4;//4 seconds on each position now.
        this.timeOnEachPositionCounter = 0;
    }
    public void DoTask()
    {
        if (owner != null && machine != null)
        {
            var ownerTask = owner.GetComponent<WorkerAIScript>().myCurrentTask;
            var ownerAIPath = owner.GetComponent<AIPath>();

            if (ownerTask.status != ITask.Status.Ongoing)
            {
                ownerTask.status = ITask.Status.Ongoing;
            }

            Vector2 targetPosFront = new Vector2(machine.transform.position.x, machine.transform.position.y - 0.5f);
            Vector2 targetPosLeft = new Vector2(machine.transform.position.x - 2f, machine.transform.position.y);
            Vector2 targetPosBack = new Vector2(machine.transform.position.x, machine.transform.position.y + 1.5f);
            Vector2 targetPosRight = new Vector2(machine.transform.position.x + 2f, machine.transform.position.y);

            timeOnEachPositionCounter -= 1 * Time.deltaTime;
            if (timeOnEachPositionCounter <= 0)
            {

                repairOnEachPos = true;
                timeOnEachPositionCounter = timeOnEachPosition;
                if (targetPos == targetPosFront)
                {
                    owner.GetComponent<WorkerAIScript>().SetIsInteracting(false);
                    targetPos = targetPosLeft;
                
                }
                else if (targetPos == targetPosLeft)
                {
                    owner.GetComponent<WorkerAIScript>().SetIsInteracting(false);
                    targetPos = targetPosBack;
                    
                }
                else if (targetPos == targetPosBack)
                {
                    owner.GetComponent<WorkerAIScript>().SetIsInteracting(false);
                    targetPos = targetPosRight;
                    
                }
                else
                {
                    owner.GetComponent<WorkerAIScript>().SetIsInteracting(false);
                    targetPos = targetPosFront;
                    
                }
            }
            else 
            {
                if (Vector2.Distance((Vector2)owner.transform.position, targetPos) <= 0.2f) {
                    owner.GetComponent<WorkerAIScript>().SetIsInteracting(true);
                    if (owner.transform.GetChild(0).GetComponent<AudioSource>().isPlaying != true)
                    {
                        owner.transform.GetChild(0).GetComponent<AudioSource>().Play();
                    }
                }
            }

            if (ownerAIPath.destination != (Vector3)targetPos)
            {
                ownerAIPath.destination = (Vector3)targetPos;
            }
            if (ownerAIPath.canMove != true)
            {
                ownerAIPath.canMove = true;
            }

            if (Vector2.Distance((Vector2)owner.transform.position, targetPos) <= 0.2f)
            {
                if (ownerAIPath.destination != owner.transform.position)
                {
                    ownerAIPath.destination = owner.transform.position;
                }

                if (repairOnEachPos == true)
                {
                    
                    machine.GetComponent<Machine>().Repair(2);
                    repairOnEachPos = false;
                    Debug.Log("Repair by 2");
                }

                if (machine.GetComponent<Machine>()._condition == machine.GetComponent<Machine>()._maxCondition)
                {
                    if (ownerTask.status != ITask.Status.Completed)
                    {
                        owner.GetComponent<WorkerAIScript>().SetIsInteracting(false); //Set false just in case it hasn't stopped yet
                        machine.GetComponent<Machine>().SetOrderedState(2);//JUST START MACHINE AFTER REPAIR! TEMPORARY
                        ownerTask.status = ITask.Status.Completed;
                    }
                }
            }
        }
    }
}
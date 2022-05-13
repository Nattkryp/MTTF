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
            var ownerTask = owner.GetComponent<AgentController>().myCurrentTask;
            var ownerAIPath = owner.GetComponent<AIPath>();

            SetTaskOngoing(ownerTask);
            MoveToDestination(ownerAIPath);
            AtDestination(ownerTask, ownerAIPath);
        }
    }

    void SetTaskOngoing(ITask ownerTask)
    {
        //Sets the task ongoing if not aleady ongoing.
        if (ownerTask.status != ITask.Status.Ongoing)
        {
            ownerTask.status = ITask.Status.Ongoing;
        }
    }

    void MoveToDestination(AIPath ownerAIPath)
    {
        //Move towards destination.
        if (ownerAIPath.destination != (Vector3)destination)
        {
            ownerAIPath.destination = (Vector3)destination;
        }
        if (ownerAIPath.canMove != true)
        {
            ownerAIPath.canMove = true;
        }
    }

    void AtDestination(ITask ownerTask, AIPath ownerAIPath)
    {
        //Set completed when you have reached your destination.
        if (ownerAIPath.reachedEndOfPath)
        {
            if (ownerTask.status != ITask.Status.Completed)
            {
                ownerTask.status = ITask.Status.Completed;
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
            var ownerTask = owner.GetComponent<AgentController>().myCurrentTask;
            var ownerAIPath = owner.GetComponent<AIPath>();

            SetTaskOngoing(ownerTask);
            CountdownPatrolTime();

            if (PatrolTimeLeftInSeconds() > 0)
            {
                SetInitialStartingPoint();
                MoveToDestination(ownerAIPath);
                AtDestination(ownerAIPath);
            }
            else
            {
                SetTaskCompleted(ownerTask, ownerAIPath);
            }
        }
    }

    void SetTaskOngoing(ITask ownerTask)
    {
        //Sets the task ongoing if not aleady ongoing.
        if (ownerTask.status != ITask.Status.Ongoing)
        {
            ownerTask.status = ITask.Status.Ongoing;
        }
    }

    void CountdownPatrolTime()
    {
        //Timelimit for the patrol task (total time)
        this.seconds -= 1 * Time.deltaTime;
    }

    void SetInitialStartingPoint()
    {
        //Set closest position as starting position in the init phase.
        if (activeDestinationId == -1)
        {
            float closestDistance = 9999;
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
    }

    void MoveToDestination(AIPath ownerAIPath)
    {
        //Move towards current destination selected in ActiveDestinationId.
        if (ownerAIPath.destination != (Vector3)destinations[activeDestinationId])
        {
            ownerAIPath.destination = (Vector3)destinations[activeDestinationId];
        }
        if (ownerAIPath.canMove != true)
        {
            ownerAIPath.canMove = true;
        }
    }

    void AtDestination(AIPath ownerAIPath)
    {
        //Set new destination when you have reached a destination.
        if (ownerAIPath.reachedEndOfPath)
        {
            activeDestinationId++;
            if (activeDestinationId >= destinations.Count)
            {
                activeDestinationId = 0;
            }
        }
    }

    float PatrolTimeLeftInSeconds()
    {
        //Check how much time is left for the patrol task.
        return this.seconds;
    }

    void SetTaskCompleted(ITask ownerTask, AIPath ownerAIPath)
    {
        //Set destination to current position for the AIPath. Else we will keep moving...
        if (ownerAIPath.destination != owner.GetComponent<Transform>().position)
        {
            ownerAIPath.destination = owner.GetComponent<Transform>().position;
        }

        //Set task completed
        if (ownerTask.status != ITask.Status.Completed)
        {
            ownerTask.status = ITask.Status.Completed;
            activeDestinationId = -1;
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
            var ownerTask = owner.GetComponent<AgentController>().myCurrentTask;
            var ownerAIPath = owner.GetComponent<AIPath>();
            Vector2 targetpos;

            SetTaskOngoing(ownerTask);
            targetpos = SetButtonPosition();
            MoveToButtonPosition(ownerAIPath, targetpos);
            AtButtonPosition(ownerTask, ownerAIPath, targetpos);
        }
    }

    void SetTaskOngoing(ITask ownerTask)
    {
        //Sets the task ongoing if not aleady ongoing.
        if (ownerTask.status != ITask.Status.Ongoing)
        {
            ownerTask.status = ITask.Status.Ongoing;
        }
    }

    Vector2 SetButtonPosition()
    {
        //Set position for button on machine.

        //Temporary information to give from machine later on!
        ///////////////////////////////////////////////////////////////////////////////////////////////////////
        return new Vector2(machine.transform.position.x - 0.5f, machine.transform.position.y - 0.5f);//In front of.
        ///////////////////////////////////////////////////////////////////////////////////////////////////////

    }

    void MoveToButtonPosition(AIPath ownerAIPath, Vector2 targetpos)
    {
        //Moves to position.
        if (ownerAIPath.destination != (Vector3)targetpos)
        {
            ownerAIPath.destination = (Vector3)targetpos;
        }
        if (ownerAIPath.canMove != true)
        {
            ownerAIPath.canMove = true;
        }
    }

    void AtButtonPosition(ITask ownerTask, AIPath ownerAIPath, Vector2 targetpos)
    {
        if (Vector2.Distance((Vector2)owner.transform.position, targetpos) <= 0.2f)
        {
            //Set destination to current position of character. Helping against precision which was a little buggy.
            //Can be tested later to remove but solved it like this for now.
            if (ownerAIPath.destination != owner.transform.position)
            {
                ownerAIPath.destination = owner.transform.position;
            }

            //Countdown the button click with a delay while at position.
            this.clickingtime -= 1 * Time.deltaTime;

            //When countdown is up then set new state
            if (clickingtime <= 0)
            {
                //Set new machine state
                machine.GetComponent<Machine>().SetOrderedState(machineState);

                //Set task completed
                if (ownerTask.status != ITask.Status.Completed)
                {
                    ownerTask.status = ITask.Status.Completed;
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
        this.timeOnEachPosition = 5;//5 seconds on each position now.
        this.timeOnEachPositionCounter = 0;
    }
    public void DoTask()
    {
        if (owner != null && machine != null)
        {
            var ownerTask = owner.GetComponent<AgentController>().myCurrentTask;
            var ownerAIPath = owner.GetComponent<AIPath>();

            SetTaskOngoing(ownerTask);
            CountdownOnEachRepairPosition();
            SetNewRepairPositionAfterCountdown();
            MoveToNextRepairPosition(ownerAIPath);
            AtRepairPosition(ownerTask, ownerAIPath);
        }
    }

    void SetTaskOngoing(ITask ownerTask)
    {
        //Sets the task ongoing if not aleady ongoing.
        if (ownerTask.status != ITask.Status.Ongoing)
        {
            ownerTask.status = ITask.Status.Ongoing;
        }
    }

    void CountdownOnEachRepairPosition()
    {
        //Countdown the time of each position.
        timeOnEachPositionCounter -= 1 * Time.deltaTime;
    }

    void SetNewRepairPositionAfterCountdown()
    {
        //Sets a new target position to repair at when countdown is zero.

        //Temporary information to give from machine later on!
        ///////////////////////////////////////////////////////////////////////////////////////////////////////
        Vector2 targetPosFront = new Vector2(machine.transform.position.x, machine.transform.position.y - 0.5f);
        Vector2 targetPosLeft = new Vector2(machine.transform.position.x - 2f, machine.transform.position.y);
        Vector2 targetPosBack = new Vector2(machine.transform.position.x, machine.transform.position.y + 1.5f);
        Vector2 targetPosRight = new Vector2(machine.transform.position.x + 2f, machine.transform.position.y);
        ///////////////////////////////////////////////////////////////////////////////////////////////////////

        if (timeOnEachPositionCounter <= 0)
        {
            //Stop owner from working on the current position
            if (owner.GetComponent<AgentController>().isInteracting == true)
            {
                owner.GetComponent<AgentController>().SetIsInteracting(false);

                //TEMPORARY HANDLE AUDIO FOR INTERACTION IN WORKER LATER ON.
                if (owner.transform.GetChild(0).GetComponent<AudioSource>().isPlaying == true)
                {
                    owner.transform.GetChild(0).GetComponent<AudioSource>().Stop();
                }
            }

            //Reset repair for each new position. (Will repair once per position)
            repairOnEachPos = true;

            //Reset time
            timeOnEachPositionCounter = timeOnEachPosition;

            //Set next in line position
            if (targetPos == targetPosFront)
            {
                targetPos = targetPosLeft;
            }
            else if (targetPos == targetPosLeft)
            {
                targetPos = targetPosBack;
            }
            else if (targetPos == targetPosBack)
            {
                targetPos = targetPosRight;
            }
            else
            {
                targetPos = targetPosFront;
            }
        }
    }

    void MoveToNextRepairPosition(AIPath ownerAIPath)
    {
        //Move to repair position
        if (ownerAIPath.destination != (Vector3)targetPos)
        {
            ownerAIPath.destination = (Vector3)targetPos;
        }
        if (ownerAIPath.canMove != true)
        {
            ownerAIPath.canMove = true;
        }
    }

    void AtRepairPosition(ITask ownerTask, AIPath ownerAIPath)
    {
        //When arrived at repair position
        if (Vector2.Distance((Vector2)owner.transform.position, targetPos) <= 0.2f)
        {
            //Set destination to current position of character. Helping against precision which was a little buggy.
            //Can be tested later to remove but solved it like this for now.
            if (ownerAIPath.destination != owner.transform.position)
            {
                ownerAIPath.destination = owner.transform.position;
            }

            //Repair once
            if (repairOnEachPos == true)
            {
                machine.GetComponent<Machine>().Repair(2);
                repairOnEachPos = false;
            }

            //If condition is 100% on machine. Complete the Task
            if (machine.GetComponent<Machine>()._condition == machine.GetComponent<Machine>()._maxCondition)
            {
                //Set task completed if not completed.
                if (ownerTask.status != ITask.Status.Completed)
                {
                    owner.GetComponent<AgentController>().SetIsInteracting(false); //Set false just in case it hasn't stopped yet
                    machine.GetComponent<Machine>().SetOrderedState(2);//JUST STOP MACHINE AFTER REPAIR! TEMPORARY
                    ownerTask.status = ITask.Status.Completed;

                    //TEMPORARY HANDLE AUDIO FOR INTERACTION IN WORKER LATER ON.
                    if (owner.transform.GetChild(0).GetComponent<AudioSource>().isPlaying == true)
                    {
                        owner.transform.GetChild(0).GetComponent<AudioSource>().Stop();
                    }
                }
            }

            //At repair position set 
            if(owner.GetComponent<AgentController>().isInteracting == false)
            {
                owner.GetComponent<AgentController>().SetIsInteracting(true);
            }

            //TEMPORARY HANDLE AUDIO FOR INTERACTION IN WORKER LATER ON.
            if (owner.transform.GetChild(0).GetComponent<AudioSource>().isPlaying != true)
            {
                owner.transform.GetChild(0).GetComponent<AudioSource>().Play();
            }
        }
    }
}
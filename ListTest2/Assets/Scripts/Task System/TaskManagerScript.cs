using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskManagerScript : MonoBehaviour
{
    //Global list of tasks.
    public List<ITask> workerTasks = new List<ITask>();
    public List<ITask> operatorTasks = new List<ITask>();

    public ITask WorkerRequestTask(GameObject owner)
    {
        if (workerTasks.Count > 0)
        {
            ITask task = workerTasks[0];
            workerTasks.RemoveAt(0);
            task.owner = owner;
            Debug.Log("Gave worker " + owner.ToString() + " a task");
            return task;
        }
        else
            return null;
    }

    public ITask OperatorRequestTask(GameObject owner)
    {
        if (operatorTasks.Count > 0)
        {
            ITask task = operatorTasks[0];
            operatorTasks.RemoveAt(0);
            task.owner = owner;
            Debug.Log("Gave operator " + owner.ToString() + " a task");
            return task;
        }
        else
            return null;
    }

    private void Start()
    {
        List<Vector2> listofpositions = new List<Vector2>();
        Vector2 pos1 = new Vector2(Random.Range(-25, 25), 0);
        Vector2 pos2 = new Vector2(Random.Range(-25, 25), 0);
        Vector2 pos3 = new Vector2(Random.Range(-25, 25), 0);
        Vector2 pos4 = new Vector2(Random.Range(-25, 25), 0);
        listofpositions.Add(pos1);
        listofpositions.Add(pos2);
        listofpositions.Add(pos3);
        listofpositions.Add(pos4);



        GameObject[] firstTargets;
        firstTargets = GameObject.FindGameObjectsWithTag("Machine");

        foreach (GameObject target in firstTargets)
        {
            Debug.Log("Creating a task to start machine");
            CreateOperatorTask_SetStateOnMachine(1, target, Machine.State.Running);
        }
    }

    public void CreateTask_WalkToPosition(int priority, Vector2 destination)
    {
        Task_WalkToPosition newTask = new Task_WalkToPosition(priority, destination);
        workerTasks.Add(newTask);
    }

    public void CreateTask_Patrol(int priority, List<Vector2> destinations, float seconds)
    {
        Task_Patrol newTask = new Task_Patrol(priority, destinations, seconds);
        workerTasks.Add(newTask);
    }
    public void CreateOperatorTask_SetStateOnMachine(int priority, GameObject machine, Machine.State machineState)
    {
        Task_SetStateOnMachine newTask = new Task_SetStateOnMachine(priority, machine, machineState);
        operatorTasks.Add(newTask);
    }

    public void CreateTask_RepairMachine(int priority, GameObject machine, int RepairAmount)
    {
        Task_RepairMachine newTask = new Task_RepairMachine(priority, machine, RepairAmount);
        workerTasks.Add(newTask);
        Debug.Log("Added repair machine task for: " + machine.ToString() + " for a total repair amount of: " + RepairAmount);
    }
}

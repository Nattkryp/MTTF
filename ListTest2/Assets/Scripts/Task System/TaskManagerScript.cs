using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskManagerScript : MonoBehaviour
{
    //Global list of tasks.
    public List<ITask> tasks = new List<ITask>();

    public ITask RequestTask(GameObject owner)
    {
        if (tasks.Count > 0)
        {
            ITask task = tasks[0];
            tasks.RemoveAt(0);
            task.owner = owner;
            return task;
        }
        else
            return null;
    }

    private void Start()
    {
        List<Vector2> listofpositions = new List<Vector2>();
        Vector2 pos1 = new Vector2(2, 0);
        Vector2 pos2 = new Vector2(-2, 0);
        Vector2 pos3 = new Vector2(-2, -3);
        Vector2 pos4 = new Vector2(2, -3);
        listofpositions.Add(pos1);
        listofpositions.Add(pos2);
        listofpositions.Add(pos3);
        listofpositions.Add(pos4);

        if(GameObject.Find("Machine01"))
        {
            CreateTask_SetStateOnMachine(1, GameObject.Find("Machine01"), 1);
        }
        CreateTask_WalkToPosition(1, new Vector2(0, 0));
    }

    public void CreateTask_WalkToPosition(int priority, Vector2 destination)
    {
        Task_WalkToPosition newTask = new Task_WalkToPosition(priority, destination);
        tasks.Add(newTask);
    }

    public void CreateTask_Patrol(int priority, List<Vector2> destinations, float seconds)
    {
        Task_Patrol newTask = new Task_Patrol(priority, destinations, seconds);
        tasks.Add(newTask);
    }
    public void CreateTask_SetStateOnMachine(int priority, GameObject machine, int machineState)
    {
        Task_SetStateOnMachine newTask = new Task_SetStateOnMachine(priority, machine, machineState);
        tasks.Add(newTask);
    }

    public void CreateTask_RepairMachine(int priority, GameObject machine)
    {
        Task_RepairMachine newTask = new Task_RepairMachine(priority, machine);
        tasks.Add(newTask);
    }
}

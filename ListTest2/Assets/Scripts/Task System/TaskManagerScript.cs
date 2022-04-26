using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskManagerScript : MonoBehaviour
{
    //List of tasks.
    public List<Task> tasks = new List<Task>();

    //All possible tasks!
    public void CreateTask_WalkToPosition(int prio, Vector2 pos)
    {
        int id = 1;
        var task = new Task().CreateTask(id, prio, "Walk to position", "Take a walk to a specific position", 0, pos);
        tasks.Add(task);
    }
    public void CreateTask_CheckMachineStatus(int prio, Vector2 pos, GameObject machine)
    {
        int id = 2;
        var task = new Task().CreateTask(id, prio, "Check Machine", "Machine needs a status check!", 0, pos);
        task.SetMachine(machine);
        tasks.Add(task);
    }


    public Task RequestNextTask(GameObject owner)
    {
        var worker = owner.GetComponent<WorkerAIScript>();
        if (tasks.Count > 0)
        {
            //Write check on which ids to get!

            var task = tasks[0];
            tasks.RemoveAt(0);

            //Check if owner is null to get it!
            tasks[0].SetOwner(owner);
            
            //need to check if is not completed?
            return task;
        }
        else
            return null;
    }

    public void SetTaskCompleted(Task task) {

        task.SetPrio(9999);  //we want to do this?
        task.SetCompleted(); //When requesting this on multiple places I guess we want to check vs this
        task.SetOwner(null); //owned by some kind of history object for reference and not be confused with "unassigned" tasks? also see prio. 
    }

    public List<Task> RequestListOfTasksForSpecificOwner(GameObject owner)//Only a copy list. Not the actual list.
    {
        List<Task> ownedTasks = new List<Task>();

        for(int i = 0; i<tasks.Count; i++)
        {
            if (tasks[i].GetOwner().name == owner.name)
            {
                ownedTasks.Add(tasks[i]);
            }
        }

        return ownedTasks;
    }

    public void SortListByPriority()
    {
        //TODO
    }
}

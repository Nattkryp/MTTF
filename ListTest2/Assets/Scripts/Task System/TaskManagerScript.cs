using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskManagerScript : MonoBehaviour
{
    //Global list of tasks.
    public List<ITask> globalTasks = new List<ITask>();

    void Start()
    {
        //Test
        globalTasks.Add(new TaskWalkToPosition(100, new Vector2(Random.Range(-5, 5), Random.Range(-5, 5))));
        globalTasks.Add(new TaskWalkToPosition(100, new Vector2(Random.Range(-5, 5), Random.Range(-5, 5))));
        globalTasks.Add(new TaskWalkToPosition(100, new Vector2(Random.Range(-5, 5), Random.Range(-5, 5))));
        globalTasks.Add(new TaskWalkToPosition(100, new Vector2(Random.Range(-5, 5), Random.Range(-5, 5))));
        globalTasks.Add(new TaskWalkToPosition(100, new Vector2(Random.Range(-5, 5), Random.Range(-5, 5))));
        globalTasks.Add(new TaskWalkToPosition(100, new Vector2(Random.Range(-5, 5), Random.Range(-5, 5))));
        globalTasks.Add(new TaskWalkToPosition(100, new Vector2(Random.Range(-5, 5), Random.Range(-5, 5))));
        globalTasks.Add(new TaskWalkToPosition(100, new Vector2(Random.Range(-5, 5), Random.Range(-5, 5))));
        globalTasks.Add(new TaskWalkToPosition(100, new Vector2(Random.Range(-5, 5), Random.Range(-5, 5))));
        globalTasks.Add(new TaskWalkToPosition(100, new Vector2(Random.Range(-5, 5), Random.Range(-5, 5))));
        globalTasks.Add(new TaskWalkToPosition(100, new Vector2(Random.Range(-5, 5), Random.Range(-5, 5))));
        globalTasks.Add(new TaskWalkToPosition(100, new Vector2(Random.Range(-5, 5), Random.Range(-5, 5))));
        globalTasks.Add(new TaskWalkToPosition(100, new Vector2(Random.Range(-5, 5), Random.Range(-5, 5))));
        globalTasks.Add(new TaskWalkToPosition(100, new Vector2(Random.Range(-5, 5), Random.Range(-5, 5))));
        globalTasks.Add(new TaskWalkToPosition(100, new Vector2(Random.Range(-5, 5), Random.Range(-5, 5))));
        globalTasks.Add(new TaskWalkToPosition(100, new Vector2(Random.Range(-5, 5), Random.Range(-5, 5))));
        globalTasks.Add(new TaskWalkToPosition(100, new Vector2(Random.Range(-5, 5), Random.Range(-5, 5))));
        globalTasks.Add(new TaskWalkToPosition(100, new Vector2(Random.Range(-5, 5), Random.Range(-5, 5))));
        globalTasks.Add(new TaskWalkToPosition(100, new Vector2(Random.Range(-5, 5), Random.Range(-5, 5))));
        globalTasks.Add(new TaskWalkToPosition(100, new Vector2(Random.Range(-5, 5), Random.Range(-5, 5))));
        globalTasks.Add(new TaskWalkToPosition(100, new Vector2(Random.Range(-5, 5), Random.Range(-5, 5))));
        globalTasks.Add(new TaskWalkToPosition(100, new Vector2(Random.Range(-5, 5), Random.Range(-5, 5))));
        globalTasks.Add(new TaskWalkToPosition(100, new Vector2(Random.Range(-5, 5), Random.Range(-5, 5))));
        globalTasks.Add(new TaskWalkToPosition(100, new Vector2(Random.Range(-5, 5), Random.Range(-5, 5))));
        globalTasks.Add(new TaskWalkToPosition(100, new Vector2(Random.Range(-5, 5), Random.Range(-5, 5))));
        globalTasks.Add(new TaskWalkToPosition(100, new Vector2(Random.Range(-5, 5), Random.Range(-5, 5))));
        globalTasks.Add(new TaskWalkToPosition(100, new Vector2(Random.Range(-5, 5), Random.Range(-5, 5))));
        globalTasks.Add(new TaskWalkToPosition(100, new Vector2(Random.Range(-5, 5), Random.Range(-5, 5))));
        globalTasks.Add(new TaskWalkToPosition(100, new Vector2(Random.Range(-5, 5), Random.Range(-5, 5))));
        globalTasks.Add(new TaskWalkToPosition(100, new Vector2(Random.Range(-5, 5), Random.Range(-5, 5))));
        globalTasks.Add(new TaskWalkToPosition(100, new Vector2(Random.Range(-5, 5), Random.Range(-5, 5))));
        globalTasks.Add(new TaskWalkToPosition(100, new Vector2(Random.Range(-5, 5), Random.Range(-5, 5))));
        globalTasks.Add(new TaskWalkToPosition(100, new Vector2(Random.Range(-5, 5), Random.Range(-5, 5))));
        globalTasks.Add(new TaskWalkToPosition(100, new Vector2(Random.Range(-5, 5), Random.Range(-5, 5))));
        globalTasks.Add(new TaskWalkToPosition(100, new Vector2(Random.Range(-5, 5), Random.Range(-5, 5))));
        globalTasks.Add(new TaskWalkToPosition(100, new Vector2(Random.Range(-5, 5), Random.Range(-5, 5))));
        globalTasks.Add(new TaskWalkToPosition(100, new Vector2(Random.Range(-5, 5), Random.Range(-5, 5))));
        globalTasks.Add(new TaskWalkToPosition(100, new Vector2(Random.Range(-5, 5), Random.Range(-5, 5))));
        globalTasks.Add(new TaskWalkToPosition(100, new Vector2(Random.Range(-5, 5), Random.Range(-5, 5))));
        globalTasks.Add(new TaskWalkToPosition(100, new Vector2(Random.Range(-5, 5), Random.Range(-5, 5))));
        globalTasks.Add(new TaskWalkToPosition(100, new Vector2(Random.Range(-5, 5), Random.Range(-5, 5))));
        globalTasks.Add(new TaskWalkToPosition(100, new Vector2(Random.Range(-5, 5), Random.Range(-5, 5))));
        globalTasks.Add(new TaskWalkToPosition(100, new Vector2(Random.Range(-5, 5), Random.Range(-5, 5))));
        globalTasks.Add(new TaskWalkToPosition(100, new Vector2(Random.Range(-5, 5), Random.Range(-5, 5))));
        globalTasks.Add(new TaskWalkToPosition(100, new Vector2(Random.Range(-5, 5), Random.Range(-5, 5))));
        globalTasks.Add(new TaskWalkToPosition(100, new Vector2(Random.Range(-5, 5), Random.Range(-5, 5))));
        globalTasks.Add(new TaskWalkToPosition(100, new Vector2(Random.Range(-5, 5), Random.Range(-5, 5))));
        globalTasks.Add(new TaskWalkToPosition(100, new Vector2(Random.Range(-5, 5), Random.Range(-5, 5))));
        globalTasks.Add(new TaskWalkToPosition(100, new Vector2(Random.Range(-5, 5), Random.Range(-5, 5))));
        globalTasks.Add(new TaskWalkToPosition(100, new Vector2(Random.Range(-5, 5), Random.Range(-5, 5))));
        globalTasks.Add(new TaskWalkToPosition(100, new Vector2(Random.Range(-5, 5), Random.Range(-5, 5))));
        globalTasks.Add(new TaskWalkToPosition(100, new Vector2(Random.Range(-5, 5), Random.Range(-5, 5))));
        globalTasks.Add(new TaskWalkToPosition(100, new Vector2(Random.Range(-5, 5), Random.Range(-5, 5))));
        globalTasks.Add(new TaskWalkToPosition(100, new Vector2(Random.Range(-5, 5), Random.Range(-5, 5))));
        globalTasks.Add(new TaskWalkToPosition(100, new Vector2(Random.Range(-5, 5), Random.Range(-5, 5))));
        globalTasks.Add(new TaskWalkToPosition(100, new Vector2(Random.Range(-5, 5), Random.Range(-5, 5))));
        globalTasks.Add(new TaskWalkToPosition(100, new Vector2(Random.Range(-5, 5), Random.Range(-5, 5))));
        globalTasks.Add(new TaskWalkToPosition(100, new Vector2(Random.Range(-5, 5), Random.Range(-5, 5))));
        globalTasks.Add(new TaskWalkToPosition(100, new Vector2(Random.Range(-5, 5), Random.Range(-5, 5))));
        globalTasks.Add(new TaskWalkToPosition(100, new Vector2(Random.Range(-5, 5), Random.Range(-5, 5))));
        globalTasks.Add(new TaskWalkToPosition(100, new Vector2(Random.Range(-5, 5), Random.Range(-5, 5))));
        globalTasks.Add(new TaskWalkToPosition(100, new Vector2(Random.Range(-5, 5), Random.Range(-5, 5))));
        globalTasks.Add(new TaskWalkToPosition(100, new Vector2(Random.Range(-5, 5), Random.Range(-5, 5))));
        globalTasks.Add(new TaskWalkToPosition(100, new Vector2(Random.Range(-5, 5), Random.Range(-5, 5))));
        globalTasks.Add(new TaskWalkToPosition(100, new Vector2(Random.Range(-5, 5), Random.Range(-5, 5))));
        globalTasks.Add(new TaskWalkToPosition(100, new Vector2(Random.Range(-5, 5), Random.Range(-5, 5))));
        globalTasks.Add(new TaskWalkToPosition(100, new Vector2(Random.Range(-5, 5), Random.Range(-5, 5))));
        globalTasks.Add(new TaskWalkToPosition(100, new Vector2(Random.Range(-5, 5), Random.Range(-5, 5))));
        globalTasks.Add(new TaskWalkToPosition(100, new Vector2(Random.Range(-5, 5), Random.Range(-5, 5))));
        globalTasks.Add(new TaskWalkToPosition(100, new Vector2(Random.Range(-5, 5), Random.Range(-5, 5))));
        globalTasks.Add(new TaskWalkToPosition(100, new Vector2(Random.Range(-5, 5), Random.Range(-5, 5))));
        globalTasks.Add(new TaskWalkToPosition(100, new Vector2(Random.Range(-5, 5), Random.Range(-5, 5))));
        globalTasks.Add(new TaskWalkToPosition(100, new Vector2(Random.Range(-5, 5), Random.Range(-5, 5))));
        globalTasks.Add(new TaskWalkToPosition(100, new Vector2(Random.Range(-5, 5), Random.Range(-5, 5))));
        globalTasks.Add(new TaskWalkToPosition(100, new Vector2(Random.Range(-5, 5), Random.Range(-5, 5))));
        globalTasks.Add(new TaskWalkToPosition(100, new Vector2(Random.Range(-5, 5), Random.Range(-5, 5))));
        globalTasks.Add(new TaskWalkToPosition(100, new Vector2(Random.Range(-5, 5), Random.Range(-5, 5))));
        globalTasks.Add(new TaskWalkToPosition(100, new Vector2(Random.Range(-5, 5), Random.Range(-5, 5))));
        globalTasks.Add(new TaskWalkToPosition(100, new Vector2(Random.Range(-5, 5), Random.Range(-5, 5))));
        globalTasks.Add(new TaskWalkToPosition(100, new Vector2(Random.Range(-5, 5), Random.Range(-5, 5))));
        globalTasks.Add(new TaskWalkToPosition(100, new Vector2(Random.Range(-5, 5), Random.Range(-5, 5))));
        globalTasks.Add(new TaskWalkToPosition(100, new Vector2(Random.Range(-5, 5), Random.Range(-5, 5))));
        globalTasks.Add(new TaskWalkToPosition(100, new Vector2(Random.Range(-5, 5), Random.Range(-5, 5))));
        globalTasks.Add(new TaskWalkToPosition(100, new Vector2(Random.Range(-5, 5), Random.Range(-5, 5))));
        globalTasks.Add(new TaskWalkToPosition(100, new Vector2(Random.Range(-5, 5), Random.Range(-5, 5))));
        globalTasks.Add(new TaskWalkToPosition(100, new Vector2(Random.Range(-5, 5), Random.Range(-5, 5))));
        globalTasks.Add(new TaskWalkToPosition(100, new Vector2(Random.Range(-5, 5), Random.Range(-5, 5))));
        globalTasks.Add(new TaskWalkToPosition(100, new Vector2(Random.Range(-5, 5), Random.Range(-5, 5))));
        globalTasks.Add(new TaskWalkToPosition(100, new Vector2(Random.Range(-5, 5), Random.Range(-5, 5))));
        globalTasks.Add(new TaskWalkToPosition(100, new Vector2(Random.Range(-5, 5), Random.Range(-5, 5))));
        globalTasks.Add(new TaskWalkToPosition(100, new Vector2(Random.Range(-5, 5), Random.Range(-5, 5))));
        globalTasks.Add(new TaskWalkToPosition(100, new Vector2(Random.Range(-5, 5), Random.Range(-5, 5))));
        globalTasks.Add(new TaskWalkToPosition(100, new Vector2(Random.Range(-5, 5), Random.Range(-5, 5))));
        globalTasks.Add(new TaskWalkToPosition(100, new Vector2(Random.Range(-5, 5), Random.Range(-5, 5))));
        globalTasks.Add(new TaskWalkToPosition(100, new Vector2(Random.Range(-5, 5), Random.Range(-5, 5))));
        globalTasks.Add(new TaskWalkToPosition(100, new Vector2(Random.Range(-5, 5), Random.Range(-5, 5))));
        globalTasks.Add(new TaskWalkToPosition(100, new Vector2(Random.Range(-5, 5), Random.Range(-5, 5))));
        globalTasks.Add(new TaskWalkToPosition(100, new Vector2(Random.Range(-5, 5), Random.Range(-5, 5))));
        globalTasks.Add(new TaskWalkToPosition(100, new Vector2(Random.Range(-5, 5), Random.Range(-5, 5))));
        globalTasks.Add(new TaskWalkToPosition(100, new Vector2(Random.Range(-5, 5), Random.Range(-5, 5))));
        globalTasks.Add(new TaskWalkToPosition(100, new Vector2(Random.Range(-5, 5), Random.Range(-5, 5))));
        globalTasks.Add(new TaskWalkToPosition(100, new Vector2(Random.Range(-5, 5), Random.Range(-5, 5))));
        globalTasks.Add(new TaskWalkToPosition(100, new Vector2(Random.Range(-5, 5), Random.Range(-5, 5))));
        globalTasks.Add(new TaskWalkToPosition(100, new Vector2(Random.Range(-5, 5), Random.Range(-5, 5))));
        globalTasks.Add(new TaskWalkToPosition(100, new Vector2(Random.Range(-5, 5), Random.Range(-5, 5))));
        globalTasks.Add(new TaskWalkToPosition(100, new Vector2(Random.Range(-5, 5), Random.Range(-5, 5))));
        globalTasks.Add(new TaskWalkToPosition(100, new Vector2(Random.Range(-5, 5), Random.Range(-5, 5))));
        globalTasks.Add(new TaskWalkToPosition(100, new Vector2(Random.Range(-5, 5), Random.Range(-5, 5))));

    }
    public ITask RequestGlobalTask(GameObject owner)
    {
        if (globalTasks.Count > 0)
        {
            ITask task = globalTasks[0];
            globalTasks.RemoveAt(0);
            task.owner = owner;
            return task;
        }
        else
            return null;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskListManager : MonoBehaviour
{
    public TaskManagerScript taskManager;
    public GameObject taskListPanel;
    public GameObject rowPrefab;
    private List<ITask> taskList = new List<ITask>();

    private void Start()
    {
        //Whenever a task is generated, an event should trigger adding that to this list
        //Whenever a task is completed, an event should trigger removing it from this list

    }

    private void Update()
    {
        //Should update the data for all available tasks
    }
}

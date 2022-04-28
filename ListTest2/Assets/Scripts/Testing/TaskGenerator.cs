using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskGenerator : MonoBehaviour
{
    TaskManagerScript taskManagerScript;
    public GameObject taskManager;

    public void Start()
    {
       taskManagerScript = taskManager.GetComponent<TaskManagerScript>();
    }

    public void GenMoveTaskTest(int numberOfTasks)
    {
        for (int i = 0; i < numberOfTasks; i++)
        {
            Vector2 randomVector2 = new Vector2(Random.Range(-5, 5), Random.Range(-5, 5));
            //taskManagerScript.CreateTask_WalkToPosition(100, randomVector2);
        }
    }
}

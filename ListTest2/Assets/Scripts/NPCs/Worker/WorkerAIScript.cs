using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class WorkerAIScript : MonoBehaviour
{
    TaskManagerScript taskManagerScript;
    public AIPath aiPath;
    public GameObject indicatorPrefab;
    public ITask myCurrentTask;    

    public void Start()
    {
        aiPath = GetComponent<AIPath>();
        taskManagerScript = GameObject.Find("TaskManager").GetComponent<TaskManagerScript>();
    }

    public void Update()
    {
        GetComponent<SpriteRenderer>().sortingOrder = Mathf.RoundToInt(transform.position.y * 100f) * -1;

        if (myCurrentTask == null)
        {
            myCurrentTask = taskManagerScript.RequestTask(gameObject);
        }
        else
        {
            Debug.Log("myCurrentTask: " + myCurrentTask.status);

            if (myCurrentTask.status != ITask.Status.Completed)
            {
                myCurrentTask.DoTask();
            }
            else
            {
                myCurrentTask = null;
            }
        }
    }
}

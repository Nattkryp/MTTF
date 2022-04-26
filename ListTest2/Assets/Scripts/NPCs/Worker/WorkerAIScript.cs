using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class WorkerAIScript : MonoBehaviour
{
    TaskManagerScript taskManagerScript;
    GameObject taskManager;
    AIDestinationSetter destinationSetter;
    public AIPath aiPath;
    public GameObject indicatorPrefab;

    public Task currentTask;    //fetched task with Vector 2 target
    //public Vector2 currMoveTarget;  //activated target from fetched vector 2
    public float moveSpeed;
    public GameObject currTargetIndicator;

    public void Start()
    {
        aiPath = GetComponent<AIPath>();
        destinationSetter = GetComponent<AIDestinationSetter>();
        taskManager = GameObject.Find("TaskManager");
        taskManagerScript = taskManager.GetComponent<TaskManagerScript>();
    }

    public void Update()
    {
        GetComponent<SpriteRenderer>().sortingOrder = Mathf.RoundToInt(transform.position.y * 100f) * -1;


        if (currentTask == null)
        {
            RequestTask();
        }
        else {
            if (currTargetIndicator == null)
                SetTarget();
        }

        if (aiPath.reachedEndOfPath) {

            StopMovement();
        };

    }

    public void StopMovement() {

        currentTask.SetCompleted();
        currentTask = null;
        destinationSetter.target = null;
        //aiPath.canMove = false;
    }

    public void StartMovement() {
        aiPath.canMove=true;
    }

    public void SetTarget() {

        GameObject target = currentTask.GetMachine();
        destinationSetter.target = target.transform;

    }

    public void RequestTask()
    {
        currentTask = taskManagerScript.RequestNextTask(gameObject);
    }

}

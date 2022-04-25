using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorkerAIScript : MonoBehaviour
{
    public MoveTaskList moveTaskList;



    
    public MoveTask currentTask;    //fetched task with Vector 2 target
    public Vector2 currMoveTarget;  //activated target from fetched vector 2
    public float moveSpeed;
    
    

    public void Update()
    {
        if (currentTask == null)
        {
            RequestTask();
        }
        else { 
            MoveToTarget();
        }

        
    }

    public void MoveToTarget() {
        currMoveTarget = currentTask.moveToPos;

        if (Vector2.Distance(transform.position, currMoveTarget) > 0.5f)
        {
            transform.position = Vector2.MoveTowards(transform.position, currMoveTarget, moveSpeed*Time.deltaTime);
            Debug.DrawLine(transform.position, currMoveTarget);
            //Debug.Log("I'm moving to: " +currMoveTarget.x + ":" +currMoveTarget.y);
        }
        else {
            currentTask = null;
        }
    }

    public void RequestTask()
    {
        currentTask = moveTaskList.RequestNextTask();
    }

}

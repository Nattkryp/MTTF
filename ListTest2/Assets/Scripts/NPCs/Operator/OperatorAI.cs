using UnityEngine;
using System.Collections;
using Pathfinding;

public class OperatorAI : MonoBehaviour {

    //public OperatorController controller;
    public AIPath aIPath;
    TaskManagerScript taskManagerScript;

    public GameObject[] allMachines;
    public GameObject machineTarget;

    Animator animator;
    //WorkerAudio sounds;


    public bool isMoving;

    //public Transform moveTarget;

    public enum State {
        Idle,
        Move,
        FindDestination,
        StartMachine,
        StopMachine,
        }

    public State state;
    public ITask myCurrentTask;
    public string myCurrTaskDesc;
    

    private void Start()
    {
        taskManagerScript = GameObject.Find("TaskManager").GetComponent<TaskManagerScript>();

        animator = GetComponent<Animator>();
        //sounds = GetComponentInChildren<WorkerAudio>();

        aIPath = GetComponent<AIPath>();
        aIPath.maxSpeed = 3;
        state = State.Idle;

        allMachines = GameObject.FindGameObjectsWithTag("Machine");
    }

    private Transform GetNearestObjectWithTag(string tagname)
    {
        GameObject[] sitPositions = GameObject.FindGameObjectsWithTag(tagname);

        Transform tMin = null;
        float minDist = Mathf.Infinity;
        Vector3 currentPos = transform.position;
        foreach (GameObject t in sitPositions)
        {
            float dist = Vector3.Distance(t.transform.position, currentPos);
            if (dist < minDist)
            {
                tMin = t.transform;
                minDist = dist;
            }
        }
        return tMin;
        
    }

    private void Update()
    {
        

        UpdateGfx();


        if (myCurrentTask == null || myCurrentTask.status == ITask.Status.Completed)
        {
            Debug.Log("I have no task, ill request");
            myCurrentTask = taskManagerScript.OperatorRequestTask(gameObject);
            
            //If no target, go sit somewhere close
            if (myCurrentTask == null)
            {
                Transform nearestSitPosition;
                nearestSitPosition = GetNearestObjectWithTag("WorkIdleSpot");

                if (nearestSitPosition != null)
                {
                    aIPath.destination = nearestSitPosition.transform.position;
                }
            }

            else { Debug.Log("I recieved one!: " + myCurrentTask.desc); }
        }

        if (myCurrentTask != null && myCurrentTask.status != ITask.Status.Completed)
        {
            myCurrentTask.DoTask();
            //Debug.Log("Doing task");

        }
        else if (myCurrentTask != null && myCurrentTask.status == ITask.Status.Completed)
        {
            myCurrentTask = null;
        }


        //switch (state) {

        //    case State.Idle:

        //        state = State.FindDestination;

        //        break;

        //    case State.FindDestination:
        //foreach (var machine in allMachines)
        //{   
        //    if (machine.GetComponent<Machine>().GetState() == Machine.State.Stopped)
        //    {
        //        machineTarget = machine;
        //        state = State.Move;
        //        //Debug.Log("Found machinetarget: " + machineTarget.ToString() + " moving to MoveState");
        //        break;
        //    }
        //}
        //    
        //    if (myCurrentTask != null)
        //    {
        //        state = State.Move;
        //    }


        //    break;
        //case State.Move:

        //    myCurrentTask
        //Vector2 movePosition = new Vector2();
        //movePosition = machineTarget.GetComponent<Machine>().GetOperatorStartPos().position;

        //if (Vector2.Distance(transform.position, movePosition) < 0.5)
        //{
        //    aIPath.canMove = false;
        //    state = State.StartMachine;
        //    //Debug.Log("Near machineTarget, moving to start machine state");
        //}
        //else
        //{
        //    aIPath.canMove = true;
        //    if (machineTarget.transform.position != null)
        //    {
        //        aIPath.destination = movePosition;
        //    }
        //    else
        //    {
        //        state = State.Idle;
        //    }
        //}
        //break;

        //case State.StartMachine:
        //    machineTarget.GetComponent<Machine>().SetState(Machine.State.Running);
        //    machineTarget = null;
        //    //Debug.Log("Ordered machine to start. Dropping target and going idle");
        //    state = State.Idle;
        //    break;
        //case State.StopMachine:
        //    break;
        //}
    }

    public State GetState() {
        return state;
    }

    public void CheckIfMoving()
    {

        if (transform.hasChanged)
        {
            animator.SetBool("IsMoving", true);
            transform.hasChanged = false;
        }
        else
        {
            isMoving = false;
            //Debug.Log("isn't moving");
        }

    }

    public void UpdateGfx()
    {
        CheckIfMoving();

        if (isMoving)
        {
            animator.SetBool("IsMoving", true);
        }
        else
        {
            animator.SetBool("IsMoving", false);
        }
    }

}
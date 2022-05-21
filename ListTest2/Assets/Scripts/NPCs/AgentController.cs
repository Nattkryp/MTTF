using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class AgentController : MonoBehaviour
{

    
    public TaskManagerScript taskManagerScript;
    GameObject taskBoard;
    SpriteRenderer spriteRenderer;
    Animator animator;
    //WorkerAudio sounds;
    AIPath aiPath;
    float maxSpeed = 2;                        //on aiPath

    public ITask myCurrentTask;
    public string myCurrentTaskType;

    float waitToAskForTaskTimeMin = 2;       //The time set to reset the timer
    float waitToAskForTaskTimeMax = 10;            //The actual countdown number of the timer
    public float waitToAskForTaskTimer = 5;

    //Task performance modifier
    int skillRepair = 1;        //character skill
    int toolRepair = 2;         //tool stat
    int sumRepair;


    public bool isMoving;
    public bool isInteracting = false;

    //new stuff
    NeedsManager needsManager;
    public GameObject neededVendor;
    public Need focusedNeed;
    public GameObject machine;
    public GameObject entrance;
    public int machinePrio = 2;
    public BreakRoomStool favoriteStool;


    public bool careToSelf = false;

    public Vector2 moveToNeedTarget;        //location for a vendor to collide with to initate interaction
    public float moveSpeed = 1;
    public float normalMoveSpeed = 1;


    public int calendarActivity = 3;  //0 all agents stop, 1 go to machine 2 break - sort out needs, 3 entrance

    public void OnEnable()
    {
        CalendarManager.onHourChange += ActivityUpdate;
    }

    public void ActivityUpdate(int i) {
        calendarActivity = i;
    }

    private void Start()
    {
        //External
        taskManagerScript = GameObject.Find("TaskManager").GetComponent<TaskManagerScript>();
        taskBoard = GameObject.FindGameObjectWithTag("TaskBoard");

        entrance = GameObject.FindGameObjectWithTag("Entrance");                                //Temporary starting position

        //Internal components
        needsManager = GetComponent<NeedsManager>();
        animator = GetComponent<Animator>();
        //sounds = GetComponentInChildren<WorkerAudio>();
        aiPath = GetComponent<AIPath>();

        //Parameters

        aiPath.maxSpeed = maxSpeed;

        moveSpeed = normalMoveSpeed;

        //Skills and attributes  that effect task performance, should probably update through a function at some point
        sumRepair = skillRepair + toolRepair;
    }

    private void Update()
    {
        UpdateGfx();
        aiPath.maxSpeed = moveSpeed;
        GetComponent<SpriteRenderer>().sortingOrder = Mathf.RoundToInt(transform.position.y * 100f) * -1;
        //activity announced through onHourChange-event.


        switch (calendarActivity)
            {
                case 0:
                    //agents stop in track for some seconds
                    pauseMovement(30);

                    break;
                case 1:

                GoGetTask();                    // If doesn't have task                 Get a task from task manage position 
                if (myCurrentTask != null)
                {
                    DoTask();                   // If have task that is not completed   Perform the current task according to the task specification
                    GoReportWorkComplete();     // If have task that _is_ completed     Go to task manaage position and report task and forget about it
                }
                

                break;

                case 2:
                    //agents figure out what they need and sort that out. Agent's need-decay is haltet.
                    ManageNeeds();
                    MoveToNeedTarget();
                    break;
                case 3:
                    //agents figure out what they need and sort that out. Agent's need-decay is haltet.
                    LeaveWork();
                    MoveToNeedTarget();
                    break;
                default:
                    // code block
                    break;
            }
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
//        Debug.Log("Agent Got triggered by " + other);
        if (other.gameObject.GetComponent<Interactable>() != null)
        {
            Interactable interactable = other.gameObject.GetComponent<Interactable>();
            Debug.Log("Trying to get the vendor to interact");
            interactable.Interact(this);
            StartCoroutine(AnimDrinkCoffee(5f));

        }
        else
        {
            //Debug.Log("Didn't look as if whatever i collided with could be interacted with");
        }
    }

    IEnumerator AnimDrinkCoffee(float t)
    {
        Debug.Log("Pausing movement for animation for: " + t + " secs");
        moveSpeed = 0f;
        Debug.Log("Playing animation");
        animator.Play("interact");

        yield return new WaitForSeconds(t);
        //Debug.Log("Resuming movement");
        moveSpeed = normalMoveSpeed;
    }


    IEnumerator pauseMovement(float t)
    {
        Debug.Log("Pausing movement");
        moveSpeed = 0f;
        yield return new WaitForSeconds(t);
        Debug.Log("Resuming movement");
        moveSpeed = normalMoveSpeed;
    }

    public void ManageNeeds() {
        if (needsManager.GetHighestPriorityNeedPriority() < 10)
        {
            //Debug.Log("My needs are getting higher priority - below 10");
            focusedNeed = needsManager.GetHighestPriorityNeed();
            neededVendor = GameObject.FindGameObjectWithTag(focusedNeed.vendorTag);
            moveToNeedTarget = neededVendor.GetComponent<VendingMachine>().interactposition.transform.position;
        }
        else
        {
            //Debug.Log("My needs are fullfilled, but I'm having a break so lets go sit down for a while");
        IdleInBreakRoom();
        }
    }

    public void IdleInBreakRoom() {

        if (favoriteStool == null)
        {
            GameObject[] seats = GameObject.FindGameObjectsWithTag("BreakIdle");

            foreach (var seat in seats)
            {

                if (seat.GetComponent<BreakRoomStool>().GetAvailability())
                {
                    //Debug.Log("Testing one seat");
                    seat.GetComponent<BreakRoomStool>().Reserve(this.gameObject);
                    favoriteStool = seat.gameObject.GetComponent<BreakRoomStool>();
                    return;
                }

            }

        }
        else if (moveToNeedTarget != null) 
        {

            moveToNeedTarget = favoriteStool.transform.position;
        }
    }

    public void LeaveWork()
    {
        moveToNeedTarget = entrance.transform.position;
    }

    public void GoGetTask()
    {
        
        if (aiPath.destination != taskBoard.transform.position)
        {
            aiPath.destination = taskBoard.transform.position;
        }
        if (myCurrentTask == null)
        {
            myCurrentTaskType = "No current Task";

            //go to task integration spot (taskboard tagged item (only 1 currently)
            aiPath.destination = taskBoard.transform.position;
            Debug.Log("Set destination to taskboard");

            //Wait for anti-spam timer if needed before requesting again
            waitToAskForTaskTimer -= Time.deltaTime;
            if (myCurrentTask == null && waitToAskForTaskTimer <= 0f)
            {
                waitToAskForTaskTimer = 3f;
                myCurrentTask = taskManagerScript.RequestTask(gameObject);
            }
        }
    }
    public void DoTask()
    {
        if (myCurrentTask.status != ITask.Status.Completed)
        {
            myCurrentTask.DoTask();
        }
    }

    public void GoReportWorkComplete()
    {
        if (myCurrentTask.status == ITask.Status.Completed)
        {
            if (aiPath.destination != taskBoard.transform.position)
            {
                //go to task integration spot (taskboard tagged item (only 1 currently)
                aiPath.destination = taskBoard.transform.position;
                Debug.Log("Set destination to taskboard");
            }

            // if close enough
            if (aiPath.reachedDestination)
            {
                // when interaction complete, clear my current task
                myCurrentTask = null;
                Debug.Log("Play Report task animation");
            }
        }
    }

    public int GetSumRepair()
    {
        return sumRepair;
    }

    public void MoveToNeedTarget()
    {

        if (moveToNeedTarget != null && Vector2.Distance(transform.position, moveToNeedTarget) > 0.5)
        {
            aiPath.destination = moveToNeedTarget;
        }
        else
        {

        }
    }

    public void CheckIfMoving()
    {

        

    }

    
    public void SetIsInteracting(bool interacting)
    {
        isInteracting = interacting;
    }

    public void UpdateGfx()
    {
        if (transform.hasChanged)
        {
            transform.hasChanged = false;
            animator.SetBool("IsMoving", true);
        }
        else
        {
            animator.SetBool("IsMoving", false);
        }

        if (isInteracting)
        {
            animator.SetBool("IsInteracting", true);
        }
        else
        {
            animator.SetBool("IsInteracting", false);
        }

    }

    public void AnimRepair()
    {
        animator.Play("interact");
    }
}

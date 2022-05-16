using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class AgentController : MonoBehaviour
{
    //from old worker
    
    
    TaskManagerScript taskManagerScript;
    SpriteRenderer spriteRenderer;
    Animator animator;
    WorkerAudio sounds;
    AIPath aiPath;
    float maxSpeed = 2;                        //on aiPath

    public ITask myCurrentTask;
    public string myCurrentTaskType;

    float waitToAskForTaskTimeMin = 2;       //The time set to reset the timer
    float waitToAskForTaskTimeMax = 10;            //The actual countdown number of the timer
    float waitToAskForTaskTimer = 5;

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
        DailyCalendarScript.onHourChange += ActivityUpdate;
    }

    public void ActivityUpdate(int i) {
        calendarActivity = i;
    }

    private void Start()
    {
        //External
        taskManagerScript = GameObject.Find("TaskManager").GetComponent<TaskManagerScript>();
        entrance = GameObject.FindGameObjectWithTag("Entrance");                                //Temporary starting position

        //Internal components
        needsManager = GetComponent<NeedsManager>();
        animator = GetComponent<Animator>();
        sounds = GetComponentInChildren<WorkerAudio>();
        aiPath = GetComponent<AIPath>();

        //Parameters

        aiPath.maxSpeed = maxSpeed;

        moveSpeed = normalMoveSpeed;


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
                    //agents go to the machine. Agent's needs are consumed
                    Work();
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
        Debug.Log("Resuming movement");
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
    public void Work()
    {
        if (myCurrentTask == null) //add needs-check
        {
            myCurrentTaskType = "No current Task";

            waitToAskForTaskTimer -= Time.deltaTime;
            if (myCurrentTask == null && waitToAskForTaskTimer <= 0f)
            {
                myCurrentTask = taskManagerScript.RequestTask(gameObject);
            }
        }


        else
        {
            myCurrentTaskType = myCurrentTask.desc;
            //Debug.Log("myCurrentTask: " + myCurrentTask.status);

            if (myCurrentTask.status != ITask.Status.Completed)
            {
                myCurrentTask.DoTask();
            }
            else
            {
                myCurrentTask = null;
                waitToAskForTaskTimer = Random.Range(waitToAskForTaskTimeMin, waitToAskForTaskTimeMax);
                animator.Play("note-OK");
            }
        }
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

        if (transform.hasChanged)
        {
            //Debug.Log("is moving");
            isMoving = true;
            transform.hasChanged = false;
        }
        else
        {
            isMoving = false;
            //Debug.Log("isn't moving");
        }

    }

    
    public void SetIsInteracting(bool interacting)
    {
        isInteracting = interacting;
    }

    public void UpdateGfx()
    {



        if (isMoving)
        {
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
}

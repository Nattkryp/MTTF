using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class WorkerAIScript : MonoBehaviour
{

    TaskManagerScript taskManagerScript;
    SpriteRenderer spriteRenderer;
    Animator animator;
    WorkerAudio sounds;
    public GameObject tool;
    

    //Task
    public string myCurrentTaskType;
    public ITask myCurrentTask;
    float waitToAskForTaskTimeMin = 2;       //The time set to reset the timer
    float waitToAskForTaskTimeMax = 10;            //The actual countdown number of the timer
    float waitToAskForTaskTimer = 5;


    //Movement
    public bool isMoving;
    public Vector2 lastPos;
    public bool isInteracting = false;
    float maxSpeed = 2;                        //on aiPath
    AIPath aiPath;

    //Needs
    GameObject needTarget;
    Transform needTargetTransform;
    float energyMax = 200f;
    public float energyCurr;
    float energyDrainRate = 1f; //1 per sec
    public float energyLowLimit = 20f;

    //Timing
    public float TimerWaitWithMove;
    public bool isBusy;
    public float interactRequestTimer = 0;
    public bool canInteract;

    public void SetTimerWaitWithMove(float t) {
    TimerWaitWithMove = t;
    }

    public void SetIsInteracting(bool interacting) {
    isInteracting = interacting;
    }

    public void Start()
    {
        needTarget = new GameObject();
        taskManagerScript = GameObject.Find("TaskManager").GetComponent<TaskManagerScript>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        aiPath = GetComponent<AIPath>();
        aiPath.maxSpeed = maxSpeed;

        //needs
        energyCurr = energyMax;
    }

    public void Update()
    {
        Debug.DrawLine(transform.position, aiPath.destination);
        energyCurr -= energyDrainRate * Time.deltaTime;
        GetComponent<SpriteRenderer>().sortingOrder = Mathf.RoundToInt(transform.position.y * 100f) * -1;

        TimerWaitWithMove -= Time.deltaTime;
        if (!isBusy) {
            OkToDoThings();
        }
        interactRequestTimer -= Time.deltaTime;
        if (interactRequestTimer < 0f)
        {
            canInteract = true;
        }
        else
        {
            canInteract = false;
        }

    }

    public void OkToDoThings() {
        CheckIfMoving();
        DoWhileMoving();
        TestStuff();
        CheckNeeds();
        ManageTask();
    }

    public void SetMaxEnergy() {
        energyCurr = energyMax;
        needTarget = null;
        Debug.Log("Energy is now set to max!");
    }

    void CheckNeeds() {
        energyCurr -= energyDrainRate * Time.deltaTime;

        if (NoNeeds() == false) {
            isInteracting = false;

            if (needTarget == null)
            {
                needTarget = FindItemToReplenishNeed("energy");
                needTargetTransform = needTarget.GetComponent<SimpleMachine>().interactPosition;
            }
            else if (canInteract)
            {
                aiPath.destination = needTargetTransform.position;
                
                if (aiPath.reachedDestination && needTarget.GetComponent<SimpleMachine>().isAvailable && canInteract)
                {
                    interactRequestTimer = 1;
                    needTarget.GetComponent<SimpleMachine>().Interact(gameObject);

                }
                else if (!needTarget.GetComponent<SimpleMachine>().isAvailable)
                {
                    interactRequestTimer = 1;
                    Debug.Log("SimpleMachine is busy, waiting some time");
                    return;

                }
                else if (!aiPath.reachedDestination)
                {
                    Debug.Log("Probably not close enough to destination to allow interact");
                }
                else if (!canInteract) {
                    Debug.Log("Not allowed to interact yet");
                }
            }
        }
        else
        {
            needTarget = null;
        }
    }

    public GameObject FindItemToReplenishNeed(string source)
    {


        if (source == "energy")
        {
            GameObject RefillEnergyObject = GameObject.FindGameObjectWithTag("EnergyRefill");
             needTarget = RefillEnergyObject;

        }
        if (source == "food")
        {
            GameObject RefillEnergyObject = GameObject.FindGameObjectWithTag("FoodRefill");
            needTarget = RefillEnergyObject;
        }
        return needTarget;
    }

    public bool NoNeeds()
    {
        if (energyCurr > energyLowLimit)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void SetMoveSpeed(float speed) {
    maxSpeed = speed;
    }

    public void TestStuff() {
        //PlayerAITestInteract();
    }

    public bool GetIsMoving(bool x) {
        x = isMoving;
        
        return x;
    }

    public void DoWhileMoving()
    {
        UpdateGfx();
        //UpdateSound();
    }

    //public void UpdateSound()
    //{
    //    if (isMoving)
    //    {
            
    //        if (!audioSource.isPlaying)

    //        {
    //            audioSource.pitch = Time.timeScale;
    //            audioSource.Play();

    //        }
    //    }
    //}

    public void CheckIfMoving() {

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

    //public void PlayerAITestInteract()
    //{
    //    if (Input.GetKey(KeyCode.Space))
    //    {
    //        isInteracting = true;
    //    }

    //    else
    //    {
    //        isInteracting = false;
    //    }
    //}

    public void ManageTask() {

        if (NoNeeds() == true)
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
        else
        {
            return; // Instead go fullfill needs
        }
    }

    public void UpdateGfx() {
        spriteRenderer.sortingOrder = Mathf.RoundToInt(transform.position.y * 100f) * -1;


        if (isMoving)
        {
            animator.SetBool("IsMoving", true);
        }
        else {
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

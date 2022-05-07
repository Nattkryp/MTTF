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
    float maxSpeed = 4;                        //on aiPath
    AIPath aiPath;

    //Needs
    float energyMax = 100f;
    public float energyCurr;
    float energyDrainRate = 1f; //1 per sec
    float energyLowLimit = 30f;




    public void SetIsInteracting(bool interacting) {
    isInteracting = interacting;
    }

    public void Start()
    {

        taskManagerScript = GameObject.Find("TaskManager").GetComponent<TaskManagerScript>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        sounds = GetComponentInChildren<WorkerAudio>();
        aiPath = GetComponent<AIPath>();
         aiPath.maxSpeed = maxSpeed;

        //needs
        energyCurr = energyMax;
    }

    public void Update()
    {
        energyCurr -= 1f * Time.deltaTime;
        GetComponent<SpriteRenderer>().sortingOrder = Mathf.RoundToInt(transform.position.y * 100f) * -1;


        CheckIfMoving();
        DoWhileMoving();
        TestStuff();
        CheckNeeds();
        ManageTask();
    }

    void CheckNeeds() {
        energyCurr -= energyDrainRate;

        if (NoNeeds() == false) {
            //ownerAIPath.destination = coffe machine
        }
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
            Debug.Log("is moving");
            isMoving = true;
            transform.hasChanged = false;
        }
        else
        {
            isMoving = false;
            Debug.Log("isn't moving");
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

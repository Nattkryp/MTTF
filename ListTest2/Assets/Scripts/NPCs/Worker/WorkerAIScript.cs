using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class WorkerAIScript : MonoBehaviour
{
    TaskManagerScript taskManagerScript;
    SpriteRenderer spriteRenderer;
    public Animator animator;
    public AudioSource audioSource;

    public AIPath aiPath;
    public GameObject indicatorPrefab;
    public ITask myCurrentTask;
    float waitToAskForTaskTime = 10f;       //The time set to reset the timer
    float waitToAskForTaskTimer;            //The actual countdown number of the timer



    public float testMoveSpeed = 5;
    public float movedDistance;
    
    //animation testing (temporary worker livecontrol - comment/uncomment //Manage task as needed)
    public bool isMoving;
    public Vector2 lastPos;
    public bool isInteracting;

    

    public void Start()
    {
        aiPath = GetComponent<AIPath>();
        taskManagerScript = GameObject.Find("TaskManager").GetComponent<TaskManagerScript>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void Update()
    {



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

        if (isMoving)
        {
            if (!audioSource.isPlaying)

            {
                audioSource.pitch = Time.timeScale;
                audioSource.Play();
            }
        }
        else {
        audioSource.Stop();
        }


        TestStuff();
        Gfx();
        ManageTask();
        
    }

    public void TestStuff() {
        PlayerAITestMove();
        PlayerAITestInteract();
    }

    public void PlayerAITestMove()
    {
        //Camera Movement
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.position = new Vector3(transform.position.x + -testMoveSpeed * Time.deltaTime, transform.position.y, transform.position.z);
           // isMoving = true;
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.position = new Vector3(transform.position.x + testMoveSpeed * Time.deltaTime, transform.position.y, transform.position.z);
          //  isMoving = true;
        }
        else if (Input.GetKey(KeyCode.UpArrow))
        {
            transform.position = new Vector3(transform.position.x, transform.position.y + testMoveSpeed * Time.deltaTime, transform.position.z);
          //  isMoving = true;
        }
        else if (Input.GetKey(KeyCode.DownArrow))
        {
            transform.position = new Vector3(transform.position.x, transform.position.y + -testMoveSpeed * Time.deltaTime, transform.position.z);
          //  isMoving = true;
        }
        else {
          //  isMoving = false;
        }
    }
    public void PlayerAITestInteract()
    {
        if (Input.GetKey(KeyCode.Space)) {
            isInteracting = true;
        }

        else
        {
            isInteracting = false;
        }
    }

    public void ManageTask() {

        if (myCurrentTask == null)
        {
            waitToAskForTaskTimer -= Time.deltaTime;
            if (myCurrentTask == null && waitToAskForTaskTimer <= 0f)
            {
                myCurrentTask = taskManagerScript.RequestTask(gameObject);
            }
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
                waitToAskForTaskTimer = waitToAskForTaskTime;
            }
        }
    }

    public void Gfx() {
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class WorkerAIScript : MonoBehaviour
{
    TaskManagerScript taskManagerScript;
    SpriteRenderer spriteRenderer;
    public Animator animator;

    public AIPath aiPath;
    public GameObject indicatorPrefab;
    public ITask myCurrentTask;
    public float testMoveSpeed = 5;
    
    //animation testing (temporary worker livecontrol - comment/uncomment //Manage task as needed)
    public bool isMoving;
    public bool isInteracting;

    public void Start()
    {
        aiPath = GetComponent<AIPath>();
        taskManagerScript = GameObject.Find("TaskManager").GetComponent<TaskManagerScript>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void Update()
    {
        TestStuff();
        Gfx();
        //ManageTask();
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
            isMoving = true;
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.position = new Vector3(transform.position.x + testMoveSpeed * Time.deltaTime, transform.position.y, transform.position.z);
            isMoving = true;
        }
        else if (Input.GetKey(KeyCode.UpArrow))
        {
            transform.position = new Vector3(transform.position.x, transform.position.y + testMoveSpeed * Time.deltaTime, transform.position.z);
            isMoving = true;
        }
        else if (Input.GetKey(KeyCode.DownArrow))
        {
            transform.position = new Vector3(transform.position.x, transform.position.y + -testMoveSpeed * Time.deltaTime, transform.position.z);
            isMoving = true;
        }
        else {
            isMoving = false;
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

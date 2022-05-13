using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleMachine : MonoBehaviour
{
    Animator animator;
    public AudioSource audioGrind;
    public AudioSource audioFill;
    public Transform interactPosition;

    public bool makeSound = false;
    public bool isRunning = false;
    public bool isStopped = true;
    public bool isBroken = false;
    public bool isRepared = false;

    public GameObject interactObject;
    public bool isAvailable = true;
    public float restartTimer = 0;

    




    public void Start()
    {
        animator = GetComponent<Animator>();
        GetComponent<SpriteRenderer>().sortingOrder = Mathf.RoundToInt(transform.position.y * 100f) * -1;
    }

    public void Update()
    {
        restartTimer -= 1 * Time.deltaTime;

        if (restartTimer <= 0)
        {
            isAvailable = true;
        }
        else 
        {
        isAvailable = false;
        }
    }

    public void Interact(GameObject worker) {
        isAvailable = false;
        interactObject = worker; 
        FillCup();
    }
    public void FillCup()
    {

        if (isBroken)
        {
            //play broken sound?
            return;
        }
        else
        {
            animator.Play("fill");
        }
    }
    public void ReEnergize()
    {
        Debug.Log("Animation tries to set max energy on worker");
        interactObject.GetComponent<WorkerAIScript>().SetMaxEnergy();
        interactObject = null;
        restartTimer = 10f; //waiting time after completed animations before allowing next in queue to interact (they check available)
    }

    public void PlayFill()
    {

        audioFill.Play();
    }




    public void PlayGrind()
    {
        audioGrind.Play();
    }




    public void StopMachine() {
        if (isRunning)
        {
            isRunning = false;
            isStopped = true;
        }
        else
        {
            return;
        }
    }

    public void BreakMachine() {

        if (isBroken)
        {
        return ;
        }
        else
        {
        isRunning = false;
        isStopped = true;
        }
    }
    public void RepairMachine() {
        if (isBroken)
        {
            isBroken = false;
        }
        else
        {
            isStopped = true;
        }
    }
}

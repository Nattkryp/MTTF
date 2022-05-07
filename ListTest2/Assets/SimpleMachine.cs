using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleMachine : MonoBehaviour
{
    Animator animator;
    AudioSource audioSource;
    public AudioSource audioGrind;
    public AudioSource audioFill;

    public bool makeSound = false;
    public bool isRunning = false;
    public bool isStopped = true;
    public bool isBroken = false;
    public bool isRepared = false;

    public void Start()
    {
        audioSource = GetComponent<AudioSource>();
        animator = GetComponent<Animator>();
    }

    public void PlayGrind() {
        audioGrind.Play();
        
    }

    public void PlayFill()
    {
        audioFill.Play();
    }

    public void FillCup() {

        if (isBroken)
        {
            //play broken sound?
            return;
        }
        else {

            animator.Play("fill");
        }
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VendingMachine : Interactable
{
    //VendorClass vendor;
    public VendorClass vendor;
    public AgentController agent;
    public Need suppliedNeed;
    float supplyAmount = 55f;
    string fillAnimName = "fill";
    public Transform interactposition;
    public Animator animator;
    public AudioSource soundGrind;
    public AudioSource soundFill;



    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        vendor = new VendorClass(supplyAmount, suppliedNeed, fillAnimName, this);

    }

    private void Update()
    {
        GetComponent<SpriteRenderer>().sortingOrder = Mathf.RoundToInt(transform.position.y * 100f) * -1;
    }

    public override void Interact(AgentController agent)
    {
        vendor.SupplyNeed(agent, agent.focusedNeed, supplyAmount);
        PlayVendingAnimation();
    }

    public void PlayVendingAnimation()
    {
        animator.Play(fillAnimName);
        Debug.Log("Playing coffee machine filling");

    }

    public void PlayGrind()
    {
        soundGrind.Play();
    }
    public void PlayFill()
    {
        soundFill.Play();
    }

}



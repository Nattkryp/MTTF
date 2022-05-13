using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VendingMachine : MonoBehaviour
{
    //VendorClass vendor;
    public VendorClass vendor;
    public AgentController agent;
    public Need suppliedNeed;
    float supplyAmount = 55f;

    // Start is called before the first frame update
    void Start()
    {
      vendor = new VendorClass(supplyAmount, suppliedNeed);
        
    }

    private void Update()
    {
        GetComponent<SpriteRenderer>().sortingOrder = Mathf.RoundToInt(transform.position.y * 100f) * -1;
    }
}

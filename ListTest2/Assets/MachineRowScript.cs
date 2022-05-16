using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MachineRowScript : MonoBehaviour
{
    //Data
    public GameObject theMachineOnThisRow;
    //public Machine machineScript;
    public string machineID;
    public Machine.State machineState;

    //public float machineUptime7d; //not used yet


    //Row objects
    public Image machineStateImage;
    public Text txtMachineID;
    public Text txtMachineCond;
    
    //public Text txtMachineUptime7d;  //not used yet


    public void SetMachine(GameObject newMachine) {
        theMachineOnThisRow = newMachine;
    }

    private void Update()
    {
        txtMachineID.text = theMachineOnThisRow.name;
        txtMachineCond.text = theMachineOnThisRow.GetComponent<Machine>().GetCondition().ToString("000");



        if (theMachineOnThisRow.GetComponent<Machine>().GetState() == Machine.State.Running)
        {
            machineStateImage.GetComponent<Image>().color = Color.green;

        }
        else if (theMachineOnThisRow.GetComponent<Machine>().GetState() == Machine.State.Stopped)
        {
            machineStateImage.GetComponent<Image>().color = Color.yellow;
        }
        else if (theMachineOnThisRow.GetComponent<Machine>().GetState() == Machine.State.Broken)
        {
            machineStateImage.GetComponent<Image>().color = Color.red;
        }
        else if (theMachineOnThisRow.GetComponent<Machine>().GetState() == Machine.State.Repairing)
        {
            machineStateImage.GetComponent<Image>().color = Color.blue;
        }
        else
        {
            machineStateImage.GetComponent<Image>().color = Color.white;
        }

    }
}

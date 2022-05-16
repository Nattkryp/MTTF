using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MachineManager : MonoBehaviour
{
    public GameObject machineListPanel;
    public GameObject rowPrefab;
    private List<GameObject> machineList = new List<GameObject>();

    private void Start()
    {
        GameObject[] allMachines;
        allMachines = GameObject.FindGameObjectsWithTag("Machine");
        foreach (GameObject machine in allMachines)
        {
            GameObject go = Instantiate(rowPrefab, machineListPanel.transform);
            go.GetComponent<MachineRowScript>().SetMachine(machine);
        }
    }

    private void Update()
    {
        ////Each row
        //for (int i = 0; i < machineListPanel.transform.childCount; i++)
        //{
        //    machineListPanel.transform.GetChild(i).GetComponent<MachineRowScript>().theMachineOnThisRow = machineList[i].gameObject;
        //}


        //(Transform child in machineListPanel.transform)
        //{
        //    child.GetComponent<Machine>().GetState();
        //}
        ////For each machine in permanent list, get its stats and update the UI

        //foreach (var machine in machineList)
        //{
        //    Instantiate(rowPrefab, machineListPanel.transform);
        //}
    }

    public void AddMachineToList(GameObject machine) 
    {
        machineList.Add(machine);
    }
}

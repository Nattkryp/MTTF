using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorkerListManager : MonoBehaviour
{
    public GameObject workerListPanel;
    public GameObject rowPrefab;
    private List<AgentController> workerList = new List<AgentController>();
    public float updateTime;
    private float updateTimer = 0.2f;

    private void Start()
    {

        gameObject.SetActive(false);


    }

    private void Update()
    {
        updateTimer -= Time.deltaTime;
        if (updateTimer <= 0)
        {
            updateTimer = updateTime;
            ClearData();
            UpdateListRows();
            Debug.Log("Worker list Updated");
            UpdateListRowData();
        }
    }

    private void OnEnable()
    {
        Debug.Log("WorkerListManager Enabled");

        ClearData();
        UpdateListRows();
        UpdateListRowData();
    }

    private void UpdateListRows()
    {
        GameObject[] allWorkers = GameObject.FindGameObjectsWithTag("Worker");
        foreach (GameObject worker in allWorkers)
        {
            AgentController agent = worker.GetComponent<AgentController>();
            workerList.Add(agent);
        }
    }

    private void UpdateListRowData()
    {
        Debug.Log("Trying to fill list");
        //Create rows, for each earlier identified worker
        foreach (AgentController worker in workerList)
        {
            Debug.Log("one attempt");
            GameObject newRowPrefab = Instantiate(rowPrefab, transform);
            string activity;

            if (worker.GetComponent<AgentController>().myCurrentTask == null)
            {
                activity = "No task assigned";
            }
            else
            {
                activity = worker.GetComponent<AgentController>().myCurrentTask.ToString();
            }
            newRowPrefab.GetComponent<WorkerListRowData>().UpdateValues(worker.characterName, activity, 66);
        }
    }

    private void OnDisable()
    {
        Debug.Log("Worker list disabled");
    }

    public void ClearData()
    {
        //Check if rows exist and if so destroy, else nothing
        workerList.Clear();
        if (transform.childCount > 0)
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                Destroy(transform.GetChild(i).gameObject);
            }
        }
    }
}

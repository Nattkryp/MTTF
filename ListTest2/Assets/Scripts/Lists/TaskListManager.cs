using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskListManager : MonoBehaviour
{
    public TaskManagerScript taskManager;
    public GameObject taskListPanel;
    public GameObject rowPrefab;
    private List<ITask> masterTaskList = new List<ITask>();
    public float updateTime;
    private float updateTimer = 0.2f;

    private void Start()
    {
        gameObject.SetActive(false);
        //taskManager = GameObject.Find("TaskManager").GetComponent<TaskManagerScript>();
    }

    private void Update()
    {
        updateTimer -= Time.deltaTime;
        if (updateTimer <= 0)
        {
            updateTimer = updateTime;
            ClearData();
            Debug.Log("Tasklist Updated");
            FillList();
        }

        //Should update the data for all available tasks
    }

    private void OnEnable()
    {
        ClearData();
        Debug.Log("Tasklistmanager Enabled");
        FillList();
    }

    private void FillList()
    {
        for (int i = 0; i < taskManager.operatorTasks.Count; i++)
        {
            masterTaskList.Add(taskManager.operatorTasks[i]);
            //Debug.Log("Added: " + taskManager.operatorTasks[i].desc);
        }
        for (int i = 0; i < taskManager.workerTasks.Count; i++)
        {
            masterTaskList.Add(taskManager.workerTasks[i]);
            //Debug.Log("Added: " + taskManager.workerTasks[i].desc);
        }

        foreach (ITask itask in masterTaskList)
        {
            GameObject newRowPrefab = Instantiate(rowPrefab, transform);
            Debug.Log(newRowPrefab.ToString());
            Debug.Log(newRowPrefab.GetComponent<TasklistRowData>().ToString());
            string ownerText = "Unassigned";
            if (itask.owner != null)
            {
                ownerText = itask.owner.ToString();
            }
            newRowPrefab.GetComponent<TasklistRowData>().UpdateValues(itask.priority, itask.title, ownerText);
        }
    }

    private void OnDisable()
    {
        Debug.Log("Tasklistmanager disabled");
    }

    public void ClearData()
    {
        //Check if rows exist and if so destroy, else nothing
        masterTaskList.Clear();
        if (transform.childCount > 0)
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                Destroy(transform.GetChild(i).gameObject);
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskPanel : MonoBehaviour
{
    public TaskManagerScript taskManagerScript;
    
    
    
    public GameObject rowPrefab;
        
    public float updateTimer;

    public void Start()
    {
        taskManagerScript = GameObject.Find("TaskManager").GetComponent<TaskManagerScript>();
        TasklistRowData row = rowPrefab.GetComponent<TasklistRowData>();
    }

    public void Update()
    {
        updateTimer -= Time.deltaTime;
        if (updateTimer < 0) {
            UpdatePanel();
        }
    }
    public void UpdatePanel()
    {
        updateTimer = 1f;
        ClearData();
        UpdateData();
    }
    public void ClearData()
    {
        //Check if rows exist and if so destroy, else nothing
        if (transform.childCount > 0)
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                Destroy(transform.GetChild(i).gameObject);
            }
        }
    }

    public void UpdateData()
    {
        //Check if there are tasks to populate the panel with
        if (taskManagerScript.workerTasks.Count > 0)
        {
            foreach (ITask task in taskManagerScript.workerTasks)
            {
                //create the rowobject which will instantiate without data
                GameObject newRowPrefab = Instantiate(rowPrefab, transform);
                //Debug.Log("Created a row");


                //update the created row's script with data from list
                TasklistRowData newRowData = newRowPrefab.GetComponent<TasklistRowData>();
                newRowData.description = task.desc;
                if(task is Task_WalkToPosition)
                {
                    newRowData.posX = (task as Task_WalkToPosition).destination.x;
                    newRowData.posY = (task as Task_WalkToPosition).destination.y;
                }
                else
                {
                    newRowData.posX = 0;
                    newRowData.posY = 0;
                }
                newRowData.priority = task.priority;

                //Run the update method in the row
                //newRowData.UpdateValues();
                //Debug.Log("populated a row");
            }
        }
    }

}

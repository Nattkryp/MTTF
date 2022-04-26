using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveTaskPanel : MonoBehaviour
{
    TaskManagerScript taskManagerScript;
    public GameObject taskManager;
    
    
    public GameObject rowPrefab;
        
    public float updateTimer;

    public void Start()
    {
        taskManagerScript = taskManager.GetComponent<TaskManagerScript>();
        MoveTasklistRowData row = rowPrefab.GetComponent<MoveTasklistRowData>();
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
        if (taskManagerScript.tasks.Count > 0)
        {
            foreach (Task task in taskManagerScript.tasks)
            {
                //create the rowobject which will instantiate without data
                GameObject newRowPrefab = Instantiate(rowPrefab, transform);
                Debug.Log("Created a row");


                //update the created row's script with data from list
                MoveTasklistRowData newRowData = newRowPrefab.GetComponent<MoveTasklistRowData>();
                newRowData.description = task.GetDescription();
                newRowData.posX = task.GetTargetPosition().x;
                newRowData.posY = task.GetTargetPosition().y;
                newRowData.priority = task.GetPrio();

                //Run the update method in the row
                newRowData.UpdateValues();
                Debug.Log("populated a row");
            }
        }
    }

}

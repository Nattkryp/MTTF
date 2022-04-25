using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveTaskPanel : MonoBehaviour
{
    public MoveTaskList moveTaskList;
    public GameObject rowPrefab;
    public float updateTimer;

    public void Start()
    {
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
        if (moveTaskList.moveTaskList.Count > 0)
        {
            foreach (MoveTask movetask in moveTaskList.moveTaskList)
            {
                //create the rowobject which will instantiate without data
                GameObject newRowPrefab = Instantiate(rowPrefab, transform);
                Debug.Log("Created a row");

                //update the created row's script with data from list
                MoveTasklistRowData newRowData = newRowPrefab.GetComponent<MoveTasklistRowData>();
                newRowData.description = movetask.description;
                newRowData.posX = movetask.moveToPos.x;
                newRowData.posY = movetask.moveToPos.y;
                newRowData.priority = movetask.priority;

                //Run the update method in the row
                newRowData.UpdateValues();
                Debug.Log("populated a row");
            }
        }
    }

}

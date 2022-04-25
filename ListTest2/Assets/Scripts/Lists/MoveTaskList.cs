using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveTaskList : MonoBehaviour
{
    public float updateTimer;


    public List<MoveTask> moveTaskList;


    private MoveTaskList()
    {
        moveTaskList = new List<MoveTask>();
    }

    public MoveTask RequestNextTask()
    {
        // Worker requesting a task
        if (moveTaskList.Count > 0)
        {
            //Give worker the first task in list
            MoveTask task = moveTaskList[0];
            moveTaskList.RemoveAt(0);
            return task;

        }
        else
        {
            //No tasks arer available
            return null;
        }
    }

    public void AddTask(MoveTask newtask) { 
    moveTaskList.Add(newtask);
    //Sort list here by priority, How?
    }

    public void RemoveTaskAt(int x)
    {
        moveTaskList.RemoveAt(x);
    }

    public void ClearList()
    {
        moveTaskList.Clear();
    }

    public void PrintList() {

        if (moveTaskList.Count > 0)
        {
            for (int i = 0; i < moveTaskList.Count; i++)
            {
                Debug.Log(moveTaskList[i].description + "Pos: " + moveTaskList[i].moveToPos.ToString());
            }
        }
        else {
            Debug.Log("Nothing in list");
        }
    }


}

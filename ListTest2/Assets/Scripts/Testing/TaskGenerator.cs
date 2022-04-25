using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskGenerator : MonoBehaviour
{
    public MoveTaskList moveTaskList;

    public void GenMoveTaskTest(int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            Vector2 randomVector2 = new Vector2(Random.Range(-5, 5), Random.Range(-5, 5));
            MoveTask task = new MoveTask("A new task" + Random.Range(1, 2000), randomVector2, Random.Range(1, 10));
            moveTaskList.AddTask(task);
        }
        //moveTaskList.PrintList();
    }
}

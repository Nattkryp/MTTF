using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveTask:Task
{
    public Vector2 moveToPos;

    public MoveTask(string a, Vector2 b, int p) { 
        description = a;
        moveToPos = b;
        priority = p;
    }
}

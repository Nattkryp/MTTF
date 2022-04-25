using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class WorkerMovement : MonoBehaviour
{
    AIPath aiPath;

    private void Start()
    {
        aiPath = gameObject.GetComponent<AIPath>();
    }

    public void SetMoveTarget(Vector2 target) { 
    aiPath.destination = target;
    }

    public void ResetMoveTarget() {
        //aiPath.destination = new Vector2(;
    }
}

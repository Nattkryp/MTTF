using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Task
{
    int taskId, prio;
    string title, desc, notes;
    GameObject owner, machine;
    float progress, time;
    bool received, started, onHold, completed;
    Vector2 targetPos;

    public Task CreateTask(int taskId, int prio, string title, string desc, float time, Vector2 targetPos)
    {
        this.taskId = taskId;
        this.prio = prio;
        this.title = title;
        this.desc = desc;
        this.time = time;
        this.targetPos = targetPos;
        return this;
    }

    public int GetTaskID()
    {
        return this.taskId;
    }

    public string GetTitle()
    {
        return this.title;
    }

    public string GetDescription()
    {
        return this.desc;
    }

    public float GetTime()
    {
        return this.time;
    }

    public Vector2 GetTargetPosition()
    {
        return this.targetPos;
    }

    public GameObject GetOwner()
    {
        return this.owner;
    }

    public void SetOwner(GameObject worker)
    {
        this.owner = worker;
    }

    public GameObject GetMachine()
    {
        return this.machine;
    }

    public void SetMachine(GameObject machine)
    {
        this.machine = machine;
    }
}

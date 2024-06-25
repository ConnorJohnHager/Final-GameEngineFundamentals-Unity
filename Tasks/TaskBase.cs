using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class TaskBase : MonoBehaviour
{
    public bool complete = false;

    public string GetDescription()
    {
        return description;
    }

    public bool CheckTask()
    {
        return complete;
    }

    /* Unique to each task */ 
    public abstract string description { get; }

    public abstract void UpdateTask(GameObject interaction);
}

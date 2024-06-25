using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskManager : MonoBehaviour
{
    private UI_TaskMessage taskUI;
    private SMSystem smSystem;

    public List<TaskBase> taskOrder = new List<TaskBase>();
    public TaskBase activeTask;
    private int index = 0;

    private void Start()
    {
        smSystem = FindObjectOfType<SMSystem>();
        taskUI = FindObjectOfType<UI_TaskMessage>();
        activeTask = taskOrder[index];
        taskUI.Observe(activeTask.GetDescription());
    }

    #region // Called by InteractSystem

    public void CheckActiveTask()
    {
        if (activeTask.CheckTask())
        {
            index++;

            if (index < taskOrder.Count)
            {
                activeTask = taskOrder[index];
                taskUI.Observe(activeTask.GetDescription());
                smSystem.NotifyObservers(index);
            }
            else
            {
                taskUI.taskDescription.text = "No active task";
            }
        }
        else
        {
            taskUI.Observe(activeTask.GetDescription());
        }
    }

    public int CheckIndex()
    {
        return index;
    }

    #endregion
}

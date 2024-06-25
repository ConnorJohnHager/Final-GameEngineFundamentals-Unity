using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InteractSystem : MonoBehaviour
{
    public InputActionReference interact;
    public bool controlsEnabled;

    private ConversingManager conversingManager;
    private CollectingManager collectingManager;
    private DepositManager depositManager;
    private DialogueManager dialogueManager;
    private TaskManager taskManager;

    private void Start()
    {
        conversingManager = FindObjectOfType<ConversingManager>();
        collectingManager = FindObjectOfType<CollectingManager>();
        depositManager = FindObjectOfType<DepositManager>();
        dialogueManager = FindObjectOfType<DialogueManager>();
        taskManager = FindObjectOfType<TaskManager>();
    }

    private void Update()
    {
        if (controlsEnabled)
        {
            if (depositManager.isInteractable)
            {
                if (interact.action.triggered)
                {
                    depositManager.StartDepositProcess();
                    return;
                }
            }

            if (conversingManager.isConversable)
            {
                if (interact.action.triggered)
                {
                    taskManager.activeTask.UpdateTask(dialogueManager.StartConversation(taskManager.CheckIndex()));
                    taskManager.CheckActiveTask();
                    return;
                }
            }

            if (collectingManager.isCollectable)
            {
                if (interact.action.triggered)
                {
                    taskManager.activeTask.UpdateTask(collectingManager.GatherCollectable());
                    taskManager.CheckActiveTask();
                    return;
                }
            }
        }
    }

    #region // Called by ControlsManager

    public void TurningOn()
    {
        StartCoroutine("ProcessTime");
    }

    IEnumerator ProcessTime()
    {
        yield return new WaitForSeconds(0.5F);
        controlsEnabled = true;
    }

    #endregion
}

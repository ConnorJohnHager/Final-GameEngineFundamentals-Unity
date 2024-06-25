using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class DialogueManager : MonoBehaviour
{
    private ControlsManager controlsManager;
    private ConversingManager conversingManager;
    private UI_Dialogue dialogueUI;

    public InputActionReference interact;
    public bool controlsEnabled;

    private List<string> conversationParts = new List<string>();
    private string speaker;
    private int index = 0;

    private void Start()
    {
        controlsManager = FindObjectOfType<ControlsManager>();
        conversingManager = FindObjectOfType<ConversingManager>();
        dialogueUI = FindObjectOfType<UI_Dialogue>();
        dialogueUI.ToggleOff();
    }

    private void Update()
    {
        if (controlsEnabled)
        {
            if (interact.action.triggered)
            {
                index++;

                if (index < conversationParts.Count)
                {
                    dialogueUI.Observe(speaker, conversationParts[index], CheckForLastMessage());
                }
                else
                {
                    index = 0;
                    conversationParts.Clear();
                    controlsManager.DialogueEnded();
                }
            }
        }
    }

    #region // Called by ControlsManager

    public void TurningOn()
    {
        dialogueUI.ToggleOn();
        StartCoroutine("ProcessTime");
    }

    IEnumerator ProcessTime()
    {
        yield return new WaitForSeconds(0.5F);
        controlsEnabled = true;
    }

    public void TurningOff()
    {
        dialogueUI.ToggleOff();
        controlsEnabled = false;
    }

    #endregion

    #region // Called by InteractSystem

    public GameObject StartConversation(int taskIndex)
    {
        controlsManager.DialogueStarted();
        ConversableBase conversable = conversingManager.GatherConversable();
        speaker = conversable.conversableType;
        conversationParts.AddRange(conversable.GatherConversation(taskIndex));
        dialogueUI.Observe(speaker, conversationParts[index], CheckForLastMessage());
        return conversable.gameObject;
    }

    #endregion

    #region // Internal Methods

    private bool CheckForLastMessage()
    {
        if (index < conversationParts.Count - 1)
        {
            return false;
        }

        return true;
    }

    #endregion
}

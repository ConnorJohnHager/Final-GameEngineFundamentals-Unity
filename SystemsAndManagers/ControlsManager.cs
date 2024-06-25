using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlsManager : MonoBehaviour
{
    private PlayerController playerControls;
    private InteractSystem interactControls;
    private DepositManager depositControls; // Also accesses will used to toggle UI
    private DialogueManager dialogueControls; // Also accesses will used to toggle UI
    private UI_InteractMessage interactUI;
    private UI_TaskMessage taskUI;

    private void Start()
    {
        playerControls = FindObjectOfType<PlayerController>();
        interactControls = FindObjectOfType<InteractSystem>();
        depositControls = FindObjectOfType<DepositManager>();
        dialogueControls = FindObjectOfType<DialogueManager>();
        interactUI = FindObjectOfType<UI_InteractMessage>();
        taskUI = FindObjectOfType<UI_TaskMessage>();

        playerControls.controlsEnabled = true;
        interactControls.controlsEnabled = true;
        depositControls.controlsEnabled = false;
        dialogueControls.controlsEnabled = false;
    }

    #region // Called by DepositManager

    public void DepositStarted()
    {
        playerControls.controlsEnabled = false;
        interactControls.controlsEnabled = false;
        interactUI.ToggleOff();
        taskUI.ToggleOff();
        depositControls.TurningOn();
    }

    public void DepositEnded()
    {
        depositControls.TurningOff();
        taskUI.ToggleOn();
        interactUI.ToggleOn();
        interactControls.TurningOn();
        playerControls.controlsEnabled = true;
    }

    #endregion

    #region // Called by DialogueManager

    public void DialogueStarted()
    {
        playerControls.controlsEnabled = false;
        interactControls.controlsEnabled = false;
        interactUI.ToggleOff();
        taskUI.ToggleOff();
        dialogueControls.TurningOn();
    }

    public void DialogueEnded()
    {
        dialogueControls.TurningOff();
        taskUI.ToggleOn();
        interactUI.ToggleOn();
        interactControls.TurningOn();
        playerControls.controlsEnabled = true;
    }

    #endregion
}

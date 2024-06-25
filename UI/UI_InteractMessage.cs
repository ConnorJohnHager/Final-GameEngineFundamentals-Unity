using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_InteractMessage : UIBase
{
    private ConversingManager conversingManager;
    private CollectingManager collectingManager;
    private DepositManager depositManager;

    public Text interactAlert;

    void Start()
    {
        conversingManager = FindObjectOfType<ConversingManager>();
        collectingManager = FindObjectOfType<CollectingManager>();
        depositManager = FindObjectOfType<DepositManager>();

        interactAlert.text = "";
    }

    #region // Called in ConversingManager, CollectingManager, and PlayerController

    public void Observe()
    {
        if (depositManager.isInteractable)
        {
            interactAlert.text = "Prese SPACE to open";
            return;
        }
        else if (conversingManager.isConversable)
        {
            interactAlert.text = "Prese SPACE to chat";
            return;
        }
        else if (collectingManager.isCollectable)
        {
            interactAlert.text = "Press SPACE to collect";
            return;
        }
        else
        {
            interactAlert.text = "";
        }
    }

    #endregion
}

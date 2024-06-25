using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;

public class DepositReedsTask : TaskBase
{
    private DepositManager depositManager;
    private int currentAmount = 0;
    private int requiredAmount = 5;

    private void Start()
    {
        depositManager = FindObjectOfType<DepositManager>();
    }

    public override string description
    {
        get { return "Deposit " + currentAmount.ToString() + "/" + requiredAmount.ToString() + " Reeds"; }
    }

    public override void UpdateTask(GameObject interaction)
    {
        if (depositManager.isInteractable)
        {
            if (interaction.GetComponent<CollectableBase>())
            {
                if (interaction.GetComponent<CollectableBase>().collectableType == "Reed")
                {
                    currentAmount++;

                    if (currentAmount == requiredAmount)
                    {
                        complete = true;
                    }
                }
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DepositCabbagesTask : TaskBase
{
    private DepositManager depositManager;
    private int currentAmount = 0;
    private int requiredAmount = 20;

    private void Start()
    {
        depositManager = FindObjectOfType<DepositManager>();
    }

    public override string description
    {
        get { return "Deposit " + currentAmount.ToString() + "/" + requiredAmount.ToString() + " Cabbages"; }
    }

    public override void UpdateTask(GameObject interaction)
    {
        if (depositManager.isInteractable)
        {
            if (interaction.GetComponent<CollectableBase>())
            {
                if (interaction.GetComponent<CollectableBase>().collectableType == "Cabbage")
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

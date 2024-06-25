using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FarmerPeteTask : TaskBase
{
    public override string description
    {
        get { return "Talk to Farmer Pete"; }
    }

    public override void UpdateTask(GameObject interaction)
    {
        if (interaction.GetComponent<ConversableBase>())
        {
            if (interaction.GetComponent<ConversableBase>().conversableType == "Farmer Pete")
            {
                complete = true;
            }
        }
    }
}

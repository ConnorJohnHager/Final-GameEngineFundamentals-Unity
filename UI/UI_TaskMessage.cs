using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_TaskMessage : UIBase
{
    public Text taskDescription;

    #region // Called by TaskManager

    public void Observe(string description)
    {
        taskDescription.text = description;
    }

    #endregion
}

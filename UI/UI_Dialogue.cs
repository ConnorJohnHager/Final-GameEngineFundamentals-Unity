using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Dialogue : UIBase
{
    public Text speaker;
    public Text dialogue;
    public Text continueMessage;

    #region // Called by DialogueManager

    public void Observe(string _speaker, string _dialogue, bool lastMessage)
    {
        speaker.text = _speaker;
        dialogue.text = _dialogue;

        if (!lastMessage)
        {
            continueMessage.text = "Press SPACE to continue";
        }
        else if (lastMessage)
        {
            continueMessage.text = "Press SPACE to exit";
        }
    }

    #endregion
}

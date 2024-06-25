using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConversableBase : MonoBehaviour
{
    public List<string> conversationParts = new List<string>();
    public string conversableType;

    public void InitializeConversable(string name)
    {
        conversableType = name;
        this.gameObject.name = name;
    }

    public virtual List<string> GatherConversation(int taskIndex)
    {
        return conversationParts;
    }
}

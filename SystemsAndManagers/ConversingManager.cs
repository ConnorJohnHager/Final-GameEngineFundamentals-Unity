using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConversingManager : MonoBehaviour
{
    [SerializeField] List<GameObject> conversables = new List<GameObject>();
    private UI_InteractMessage interactUI;
    public bool isConversable = false;

    private void Start()
    {
        interactUI = FindObjectOfType<UI_InteractMessage>();
    }

    #region // Called by PlayerController

    public void AddConversable(GameObject conversable)
    {
        conversables.Add(conversable);

        if (!isConversable)
        {
            isConversable = true;
        }

        interactUI.Observe();
    }

    public void RemoveConversable(GameObject conversable)
    {
        conversables.Remove(conversable);

        if (conversables.Count < 1)
        {
            isConversable = false;
        }

        interactUI.Observe();
    }

    #endregion

    #region // Called by InteractSystem

    public ConversableBase GatherConversable()
    {
        return conversables[0].GetComponent<ConversableBase>();
    }

    #endregion
}

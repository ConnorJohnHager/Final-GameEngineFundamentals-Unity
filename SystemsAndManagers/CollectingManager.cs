using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectingManager : MonoBehaviour
{
    [SerializeField] List<GameObject> collectables = new List<GameObject>();
    private InventoryManager inventoryManager;
    private UI_InteractMessage interactUI;
    public bool isCollectable = false;


    private void Start()
    {
        inventoryManager = FindObjectOfType<InventoryManager>();
        interactUI = FindObjectOfType<UI_InteractMessage>();
    }

    #region // Called by PlayerController

    public void AddCollectable(GameObject collectable)
    {
        collectables.Add(collectable);

        if (!isCollectable)
        {
            isCollectable = true;
        }

        interactUI.Observe();
    }

    public void RemoveCollectable(GameObject collectable)
    {
        collectables.Remove(collectable);

        if (collectables.Count < 1)
        {
            isCollectable = false;
        }

        interactUI.Observe();
    }

    #endregion

    #region // Called by InteractSystem

    public GameObject GatherCollectable()
    {
        bool collected = inventoryManager.CollectItem(collectables[0]);

        if (collected)
        {
            GameObject collectedItem = collectables[0];
            collectables[0].SetActive(false);
            collectables[0].GetComponent<CollectableBase>().homeSpawn.OnCollect(collectables[0]); // Remove item from Home Spawn's list
            RemoveCollectable(collectables[0]);
            return collectedItem;
        }

        return null;
    }

    #endregion
}

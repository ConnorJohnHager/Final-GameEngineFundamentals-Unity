using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public UI_Inventory inventoryUI;
    private DepositManager depositManager;

    private List<List<GameObject>> inventoryList = new List<List<GameObject>>();
    private int inventoryCap = 15;
    private int itemCap = 99;

    private void Start()
    {
        inventoryUI = FindObjectOfType<UI_Inventory>();
        depositManager = FindObjectOfType<DepositManager>();

        for (int i = 0; i < inventoryCap; i++)
        {
            inventoryList.Add(new List<GameObject>());
        }
    }

    #region // Called by UI_Inventory

    public List<GameObject> CheckSlot(int slotNumber)
    {
        return inventoryList[slotNumber];
    }

    public string GetAmountUI(int slotNumber)
    {
        return inventoryList[slotNumber].Count.ToString();
    }

    #endregion

    #region // Called by CollectableManager & DepositManager

    public bool CollectItem(GameObject item)
    {
        if (item.GetComponent<CollectableBase>())
        {
            CollectableBase itemScript = item.GetComponent<CollectableBase>();

            for (int i = 0; i < inventoryCap - 1; i++)
            {
                if (inventoryList[i].Count == 0)
                {
                    inventoryList[i].Add(item);
                    inventoryUI.Observe();
                    return true;
                }
                else if (inventoryList[i][0].GetComponent<CollectableBase>().collectableType == itemScript.collectableType)
                {
                    if (inventoryList[i].Count < itemCap)
                    {
                        inventoryList[i].Add(item);
                        inventoryUI.Observe();
                        return true;
                    }
                }
            }
        }

        return false;
    }

    #endregion

    #region // Called by DepositManager

    public bool TransferItem(int slotNumber)
    {
        if (inventoryList[slotNumber].Count == 0)
        {
            return false; // No items to transfer
        }

        bool collected = depositManager.CollectItem(inventoryList[slotNumber][0]);

        if (collected)
        {
            inventoryList[slotNumber].Remove(inventoryList[slotNumber][0]);
        }

        return collected;
    }

    #endregion
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Inventory : UIBase
{
    private InventoryManager inventoryManager;

    public Texture inventorySlotImage;
    public GameObject[] inventorySlots = new GameObject[15];
    public Text[] inventoryAmounts = new Text[15];

    private void Start()
    {
        inventoryManager = FindObjectOfType<InventoryManager>();

        for (int i = 0; i < inventorySlots.Length; i++)
        {
            inventorySlots[i].GetComponent<RawImage>().texture = inventorySlotImage;
            inventoryAmounts[i].text = "";
        }
    }

    #region // Called by InventoryManager

    public void Observe()
    {
        for (int i = 0; i < inventorySlots.Length; i++)
        {
            if (inventoryManager.CheckSlot(i).Count > 0)
            {
                List<GameObject> inventoryItem = inventoryManager.CheckSlot(i);
                inventorySlots[i].GetComponent<RawImage>().texture = inventoryItem[0].GetComponent<CollectableBase>().inventoryIcon;
                inventoryAmounts[i].text = inventoryManager.GetAmountUI(i);
            }
            else
            {
                inventorySlots[i].GetComponent<RawImage>().texture = inventorySlotImage;
                inventoryAmounts[i].text = "";
            }
        }
    }

    #endregion
}

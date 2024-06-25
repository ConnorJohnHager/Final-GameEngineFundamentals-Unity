using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Deposit : UIBase
{
    private DepositManager depositManager;
    public Texture depositSlotImage;
    public GameObject[] depositSlots = new GameObject[15];
    public Text[] depositAmounts = new Text[15];

    public GameObject[] depositSelection = new GameObject[15];
    public GameObject[] inventorySelection = new GameObject[15];
    private int previousBox = 0;
    private int previousSlot = 0;

    #region // Called by DepositManager

    public void FindDepositManager(DepositManager script)
    {
        depositManager = script;
    }

    public void Observe()
    {
        for (int i = 0; i < depositSlots.Length; i++)
        {
            if (depositManager.CheckSlot(i).Count > 0)
            {
                List<GameObject> depositList = depositManager.CheckSlot(i);
                depositSlots[i].GetComponent<RawImage>().texture = depositList[0].GetComponent<CollectableBase>().inventoryIcon;
                depositAmounts[i].text = depositManager.GetAmountUI(i);
            }
            else
            {
                depositSlots[i].GetComponent<RawImage>().texture = depositSlotImage;
                depositAmounts[i].text = "";
            }
        }
    }

    public void SelectionStart()
    {
        inventorySelection[0].SetActive(true);
    }

    public void SelectionUpdate(int box, int slot)
    {
        if (previousBox == 0)
        {
            inventorySelection[previousSlot].SetActive(false);
        }
        else if (previousBox == 1)
        {
            depositSelection[previousSlot].SetActive(false);
        }

        if (box == 0)
        {
            inventorySelection[slot].SetActive(true);
        }
        else if (box == 1)
        {
            depositSelection[slot].SetActive(true);
        }

        previousBox = box;
        previousSlot = slot;
    }

    public void SelectionEnd()
    {
        if (previousBox == 0 && inventorySelection[previousSlot].activeSelf)
        {
            inventorySelection[previousSlot].SetActive(false);
        }
        else if (previousBox == 1 && depositSelection[previousSlot].activeSelf)
        {
            depositSelection[previousSlot].SetActive(false);
        }

        previousBox = 0;
        previousSlot = 0;
    }

    #endregion
}

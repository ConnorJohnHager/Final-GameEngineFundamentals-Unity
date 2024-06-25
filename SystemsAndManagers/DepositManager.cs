using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class DepositManager : MonoBehaviour
{
    public InputActionReference interact;

    private ControlsManager controlsManager;
    private TaskManager taskManager;
    private InventoryManager inventoryManager;
    private UI_Deposit depositUI;

    private List<List<GameObject>> depositList = new List<List<GameObject>>();
    private int depositCap = 15;
    private int itemCap = 99;
    public bool controlsEnabled;
    public bool isInteractable = false;

    private int whichBox = 0;
    private int whichSlot = 0;

    private void Start()
    {
        controlsManager = FindObjectOfType<ControlsManager>();
        inventoryManager = FindObjectOfType<InventoryManager>();
        taskManager = FindObjectOfType<TaskManager>();
        depositUI = FindObjectOfType<UI_Deposit>();
        depositUI.FindDepositManager(this);
        depositUI.ToggleOff();

        for (int i = 0; i < depositCap; i++)
        {
            depositList.Add(new List<GameObject>());
        }
    }

    private void Update()
    {
        if (controlsEnabled)
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                EndDepositProcess();
            }

            SelectionProcess();
            ItemTransferProcess();
        }
    }

    #region // Called by ControlsManager

    public void TurningOn()
    {
        depositUI.ToggleOn();
        depositUI.Observe();
        depositUI.SelectionStart();
        StartCoroutine("ProcessTime");
    }

    IEnumerator ProcessTime()
    {
        yield return new WaitForSeconds(0.5F);
        controlsEnabled = true;
    }

    public void TurningOff()
    {
        depositUI.ToggleOff();
        controlsEnabled = false;
    }

    #endregion

    #region // Called by InteractSystem

    public void StartDepositProcess()
    {
        controlsManager.DepositStarted();
        depositUI.Observe();
    }

    #endregion

    #region // Called by InventoryManager

    public bool CollectItem(GameObject item)
    {
        if (item.GetComponent<CollectableBase>())
        {
            CollectableBase itemScript = item.GetComponent<CollectableBase>();

            for (int i = 0; i < depositCap - 1; i++)
            {
                if (depositList[i].Count == 0)
                {
                    depositList[i].Add(item);
                    return true;
                }
                else if (depositList[i][0].GetComponent<CollectableBase>().collectableType == itemScript.collectableType)
                {
                    if (depositList[i].Count < itemCap)
                    {
                        depositList[i].Add(item);
                        return true;
                    }
                }
            }
        }

        return false;
    }

    #endregion

    #region // Called by UI_Deposit

    public List<GameObject> CheckSlot(int slotNumber)
    {
        return depositList[slotNumber];
    }

    public string GetAmountUI(int slotNumber)
    {
        return depositList[slotNumber].Count.ToString();
    }

    #endregion

    #region // Internal Methods

    private void SelectionProcess()
    {
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            whichBox++;
            if (whichBox > 1)
            {
                whichBox = 0;
            }
            depositUI.SelectionUpdate(whichBox, whichSlot);
            return;
        }

        if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
        {
            whichBox--;
            if (whichBox < 0)
            {
                whichBox = 1;
            }
            depositUI.SelectionUpdate(whichBox, whichSlot);
            return;
        }

        if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            whichSlot++;
            if (whichSlot > depositList.Count - 1)
            {
                whichSlot = 0;
            }
            depositUI.SelectionUpdate(whichBox, whichSlot);
            return;
        }

        if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            whichSlot--;
            if (whichSlot < 0)
            {
                whichSlot = depositList.Count - 1;
            }
            depositUI.SelectionUpdate(whichBox, whichSlot);
            return;
        }
    }

    private void ItemTransferProcess()
    {
        if (interact.action.triggered)
        {
            if (whichBox == 0) // Inventory Box gives an item to Deposit Box
            {
                if (inventoryManager.TransferItem(whichSlot))
                {
                    depositUI.Observe();
                    inventoryManager.inventoryUI.Observe();
                }
                return;
            }

            if (whichBox == 1) // Deposit Box gives an item to Inventory Box
            {
                if (TransferItem(whichSlot))
                {
                    depositUI.Observe();
                    inventoryManager.inventoryUI.Observe();
                }
                return;
            }
        }
    }

    private bool TransferItem(int slotNumber)
    {
        bool collected = inventoryManager.CollectItem(depositList[slotNumber][0]);

        if (collected)
        {
            depositList[slotNumber].Remove(depositList[slotNumber][0]);
        }

        return collected;
    }

    private void EndDepositProcess()
    {
        depositUI.SelectionEnd();
        ProcessDepositedItems();
        whichBox = 0;
        whichSlot = 0;
        controlsManager.DepositEnded();
    }


    private void ProcessDepositedItems() // Not working correctly
    {
        foreach (List<GameObject> depositSlot in depositList)
        {
            if (depositSlot.Count > 0)
            {
                for (int i = 0; i < depositSlot.Count; i++)
                {
                    depositSlot[i].SetActive(true);
                    taskManager.activeTask.UpdateTask(depositSlot[i]);
                }
            }
        }

        foreach (List<GameObject> depositSlot in depositList)
        {
            if (depositSlot.Count > 0)
            {
                depositSlot.Clear();
            }
        }

        taskManager.CheckActiveTask();
    }

    #endregion
}

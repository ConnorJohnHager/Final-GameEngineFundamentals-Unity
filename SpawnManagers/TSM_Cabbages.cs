using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TSM_Cabbages : TimedSMBase
{
    public GameObject youngCabbage;
    public GameObject almostCabbage;
    public GameObject readyCabbage;

    void Start()
    {
        InitializeObserver();
        almostCabbage.SetActive(false);
    }

    private void Update()
    {
        if (turnedOn)
        {
            if (spawnedObjects.Count < 1 && spawnTimer < 30)
            {
                spawnTimer += Time.deltaTime;

                if (spawnTimer > 10 && spawnTimer < 20)
                {
                    if (!youngCabbage.activeSelf)
                    {
                        youngCabbage.SetActive(true);
                        return;
                    }
                }
                if (spawnTimer > 20)
                {
                    if (!almostCabbage.activeSelf)
                    {
                        youngCabbage.SetActive(false);
                        almostCabbage.SetActive(true);
                        return;
                    }
                }
            }
            else if (spawnTimer >= 30)
            {
                almostCabbage.SetActive(false);
                SpawnCabbage();
                spawnTimer = 0;
            }
        }
    }

    #region // Called by SMSystem

    public override void OnNotify(int taskNumber)
    {
        switch (taskNumber)
        {
            case 2:
                youngCabbage.SetActive(false);
                almostCabbage.SetActive(true);
                break;

            case 5:
                almostCabbage.SetActive(false);
                SpawnCabbage();
                turnedOn = true;
                break;

            default:
                break;
        }
    }

    #endregion

    #region // Internal Methods

    private void SpawnCabbage()
    {
        GameObject spawn;
        spawn = Instantiate(readyCabbage, gameObject.transform.position, transform.rotation);
        spawn.GetComponent<CollectableBase>().AddHomeSpawn(this);
        spawnedObjects.Add(spawn);
    }

    #endregion
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableBase : MonoBehaviour
{
    public SpawnManagerBase homeSpawn;
    public Texture inventoryIcon;
    public string collectableType;

    public void InitializeCollectable(string name)
    {
        collectableType = name;
        this.gameObject.name = name;
    }

    public void AddHomeSpawn(SpawnManagerBase spawnManager)
    {
        homeSpawn = spawnManager;
    }
}
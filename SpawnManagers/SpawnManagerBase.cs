using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManagerBase : MonoBehaviour
{
    public List<GameObject> spawnedObjects = new List<GameObject>();
    public float spawnTimer = 0;
    public float spawnRotation = 0;
    public int chance = 0;

    #region // Called by CollectingManager

    public void OnCollect(GameObject item)
    {
        spawnedObjects.Remove(item);
    }

    #endregion
}

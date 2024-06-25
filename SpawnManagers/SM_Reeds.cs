using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SM_Reeds : SpawnManagerBase
{
    public GameObject reed_v1;
    public GameObject reed_v2;

    private void Start()
    {
        SpawnReed();
    }

    private void Update()
    {
        if (spawnedObjects.Count < 1 && spawnTimer < 10)
        {
            spawnTimer += Time.deltaTime;

        }
        else if (spawnTimer >= 10)
        {
            SpawnReed();
            spawnTimer = 0;
        }
    }

    #region // Internal Methods

    private void SpawnReed()
    {
        GameObject spawn;
        System.Random random = new System.Random();
        chance = random.Next(0, 2);
        spawnRotation = random.Next(0, 361);


        if (chance == 0)
        {
            spawn = Instantiate(reed_v1, gameObject.transform.position, transform.rotation);
        }
        else
        {
            spawn = Instantiate(reed_v2, gameObject.transform.position, transform.rotation);
        }

        spawn.transform.Rotate(0, spawnRotation, 0);
        spawn.GetComponent<CollectableBase>().AddHomeSpawn(this);
        spawnedObjects.Add(spawn);
    }

    #endregion
}

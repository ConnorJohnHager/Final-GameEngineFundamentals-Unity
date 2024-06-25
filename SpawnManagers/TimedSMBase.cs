using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimedSMBase : SpawnManagerBase
{
    public SMSystem smSystem;
    public bool turnedOn = false;

    public void InitializeObserver()
    {
        smSystem = FindObjectOfType<SMSystem>();
        smSystem.AddObserver(this);
    }

    // Unique to each Timed Spawn Manager
    public virtual void OnNotify(int taskNumber)
    {
        ;
    }
}

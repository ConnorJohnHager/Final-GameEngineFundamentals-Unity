using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SMSystem : MonoBehaviour
{

    [SerializeField] public List<TimedSMBase> observers = new List<TimedSMBase>();

    #region // Called by TimedSMBase

    public void AddObserver(TimedSMBase observer)
    {
        observers.Add(observer);
    }

    #endregion

    #region // Called by TaskManager

    public void NotifyObservers(int taskNumber)
    {
        if (observers.Count > 0)
        {
            foreach (TimedSMBase observer in observers)
            {
                observer.OnNotify(taskNumber);
            }

            // ChatGPT showed that I needed to reference an external removal list to avoid issues
            List<TimedSMBase> observersToRemove = new List<TimedSMBase>();
            foreach (TimedSMBase observer in observers)
            {
                if (observer.turnedOn)
                {
                    observersToRemove.Add(observer);
                }
            }

            foreach (TimedSMBase observer in observersToRemove)
            {
                RemoveObserver(observer);
            }
        }
    }

    #endregion

    #region // Internal Methods

    private void RemoveObserver(TimedSMBase observer)
    {
        observers.Remove(observer);
    }

    #endregion
}

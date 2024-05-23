using System;
using UnityEngine;

public abstract class Spawner : MonoBehaviour
{
    [SerializeField] protected Transform Container;
    [SerializeField] protected int MaxObjectsCount;
    [SerializeField] protected bool AutoExpand = true;

    public event Action ObjectSpawned;
    public event Action ObjectDeactivated;

    protected void SpawnEventInvoke()
    {
        ObjectSpawned?.Invoke();
    }

    protected void DeactivateEventInvoke()
    {
        ObjectDeactivated?.Invoke();
    }
}

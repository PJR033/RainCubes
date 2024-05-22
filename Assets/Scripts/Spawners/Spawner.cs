using UnityEngine;

public abstract class Spawner : MonoBehaviour
{
    [SerializeField] protected Transform _container;
    [SerializeField] protected int _maxObjectsCount;
    [SerializeField] protected bool _autoExpand = true;
}

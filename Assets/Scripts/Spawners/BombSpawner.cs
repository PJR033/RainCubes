using System;
using UnityEngine;

public class BombSpawner : Spawner
{
    [SerializeField] private Bomb _prefab;
    [SerializeField] private CubeSpawner _cubeSpawner;

    private MonoPool<Bomb> _pool;

    public event Action BombSpawned;
    public event Action BombDeactivated;

    private void Awake()
    {
        _pool = new MonoPool<Bomb>(_prefab, _maxObjectsCount, _container, _autoExpand);
    }

    private void OnEnable()
    {
        _cubeSpawner.CubeSpawned += SubscribeOnCube;
    }

    private void OnDisable()
    {
        _cubeSpawner.CubeSpawned -= SubscribeOnCube;
    }

    private void SubscribeOnCube(LifetimeCube cube)
    {
        cube.LifetimeEnd += SpawnBomb;
    }

    private void SpawnBomb(LifetimeCube cube)
    {
        Bomb bomb = _pool.GetFreeElement();
        bomb.ExplosionDelay = cube.Delay;
        bomb.transform.position = cube.transform.position;
        bomb.IsExplosed += DeactivateBomb;
        cube.LifetimeEnd -= SpawnBomb;
        BombSpawned?.Invoke();
    }

    private void DeactivateBomb(Bomb bomb)
    {
        _pool.PutElement(bomb);
        bomb.IsExplosed -= DeactivateBomb;
        BombDeactivated?.Invoke();
    }
}

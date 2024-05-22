using System;
using System.Collections;
using UnityEngine;

public class CubeSpawner : Spawner
{
    [SerializeField] private LifetimeCube _prefab;
    [SerializeField] private float _spawnDelay;

    private MonoPool<LifetimeCube> _pool;
    private Transform[] _spawnPoints;
    private WaitForSeconds _delay;

    public event Action<LifetimeCube> CubeSpawned;
    public event Action CubeDeactivated;

    private void Awake()
    {
        _pool = new MonoPool<LifetimeCube>(_prefab, _maxObjectsCount, _container, _autoExpand);
        _delay = new WaitForSeconds(_spawnDelay);
        _spawnPoints = new Transform[transform.childCount];

        for (int i = 0; i < transform.childCount; i++)
        {
            _spawnPoints[i] = transform.GetChild(i).transform;
        }
    }

    private void OnEnable()
    {
        StartCoroutine(SpawningCubes());
    }

    private IEnumerator SpawningCubes()
    {
        while (_spawnPoints.Length > 0)
        {
            yield return _delay;
            int pointIndex = UnityEngine.Random.Range(0, _spawnPoints.Length);
            float maxRotation = 360;
            Vector3 cubeRotation = new Vector3(UnityEngine.Random.Range(0, maxRotation), UnityEngine.Random.Range(0, maxRotation), UnityEngine.Random.Range(0, maxRotation));

            LifetimeCube cube = _pool.GetFreeElement();
            cube.LifetimeEnd += DeactivateCube;
            cube.transform.position = _spawnPoints[pointIndex].position;
            cube.transform.rotation = Quaternion.Euler(cubeRotation);
            CubeSpawned?.Invoke(cube);
        }
    }

    private void DeactivateCube(LifetimeCube cube)
    {
        _pool.PutElement(cube);
        cube.LifetimeEnd -= DeactivateCube;
        CubeDeactivated?.Invoke();
    }
}

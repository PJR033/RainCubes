using System.Collections;
using UnityEngine;

public class CubeSpawner : MonoBehaviour
{
    [SerializeField] private LifetimeCube _prefab;
    [SerializeField] private float _spawnDelay;
    [SerializeField] private Transform _container;
    [SerializeField] private int _maxCubesCount;
    [SerializeField] private bool _autoExpand = true;

    private MonoPool<LifetimeCube> _pool;
    private Transform[] _spawnPoints;
    private WaitForSeconds _delay;

    private void Awake()
    {
        _pool = new MonoPool<LifetimeCube>(_prefab, _maxCubesCount, _container, _autoExpand);
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
            int pointIndex = Random.Range(0, _spawnPoints.Length);
            float maxRotation = 360;
            Vector3 cubeRotation = new Vector3(Random.Range(0, maxRotation), Random.Range(0, maxRotation), Random.Range(0, maxRotation));

            LifetimeCube cube = _pool.GetFreeElement();
            cube.LifetimeEnd += DeactivateCube;
            cube.transform.position = _spawnPoints[pointIndex].position;
            cube.transform.rotation = Quaternion.Euler(cubeRotation);
        }
    }

    private void DeactivateCube(LifetimeCube cube)
    {
        _pool.PutElement(cube);
        cube.LifetimeEnd -= DeactivateCube;
    }
}

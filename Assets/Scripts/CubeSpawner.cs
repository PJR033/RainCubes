using System.Collections;
using UnityEngine;

public class CubeSpawner : MonoBehaviour
{
    [SerializeField] DisappearingCube _prefab;
    [SerializeField] float _spawnDelay;

    private Transform[] _spawnPoints;
    private WaitForSeconds _delay;

    private void Awake()
    {
        _spawnPoints = new Transform[transform.childCount];

        for (int i = 0; i < transform.childCount; i++)
        {
            _spawnPoints[i] = transform.GetChild(i).transform;
        }

        _delay = new WaitForSeconds(_spawnDelay);
    }

    private void Start()
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

            DisappearingCube cube = Instantiate(_prefab, _spawnPoints[pointIndex].position, Quaternion.Euler(cubeRotation));
        }
    }
}

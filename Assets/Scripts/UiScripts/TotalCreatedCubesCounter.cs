using TMPro;
using UnityEngine;

[RequireComponent(typeof(TextMeshProUGUI))]
public class TotalCreatedCubesCounter : MonoBehaviour
{
    [SerializeField] protected CubeSpawner CubesSpawner;

    protected TextMeshProUGUI TextMesh;
    protected int CubesCount = 0;

    protected void Awake()
    {
        TextMesh = GetComponent<TextMeshProUGUI>();
    }

    protected virtual void OnEnable()
    {
        CubesSpawner.CubeSpawned += IncreaseCubesCount;
    }

    protected virtual void OnDisable()
    {
        CubesSpawner.CubeSpawned -= IncreaseCubesCount;
    }

    protected void IncreaseCubesCount(LifetimeCube cube)
    {
        CubesCount++;
        TextMesh.text = CubesCount.ToString();
    }
}

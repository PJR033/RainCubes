using TMPro;
using UnityEngine;

[RequireComponent(typeof(TextMeshProUGUI))]
public class TotalCreatedBombsCounter : MonoBehaviour
{
    [SerializeField] protected BombSpawner BombsSpawner;

    protected TextMeshProUGUI TextMesh;
    protected int BombsCount = 0;

    protected void Awake()
    {
        TextMesh = GetComponent<TextMeshProUGUI>();
    }

    protected virtual void OnEnable()
    {
        BombsSpawner.BombSpawned += IncreaseBombsCount;
    }

    protected virtual void OnDisable()
    {
        BombsSpawner.BombSpawned -= IncreaseBombsCount;
    }

    protected void IncreaseBombsCount()
    {
        BombsCount++;
        TextMesh.text = BombsCount.ToString();
    }
}

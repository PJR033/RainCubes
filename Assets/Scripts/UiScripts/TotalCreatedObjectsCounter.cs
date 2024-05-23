using TMPro;
using UnityEngine;

[RequireComponent(typeof(TextMeshProUGUI))]
public class TotalCreatedObjectsCounter : MonoBehaviour
{
    [SerializeField] protected Spawner Spawner;

    protected TextMeshProUGUI TextMesh;
    protected int ObjectsCount = 0;

    protected void Awake()
    {
        TextMesh = GetComponent<TextMeshProUGUI>();
    }

    protected virtual void OnEnable()
    {
        Spawner.ObjectSpawned += IncreaseObjectsCount;
    }

    protected virtual void OnDisable()
    {
        Spawner.ObjectSpawned -= IncreaseObjectsCount;
    }

    protected void IncreaseObjectsCount()
    {
        ObjectsCount++;
        TextMesh.text = ObjectsCount.ToString();
    }
}

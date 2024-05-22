public class ActiveBombsCounter : TotalCreatedBombsCounter
{
    protected override void OnEnable()
    {
        base.OnEnable();
        BombsSpawner.BombDeactivated += DecreaseBombsCount;
    }

    protected override void OnDisable()
    {
        base.OnDisable();
        BombsSpawner.BombDeactivated -= DecreaseBombsCount;
    }

    private void DecreaseBombsCount()
    {
        BombsCount--;
        TextMesh.text = BombsCount.ToString();
    }
}
public class ActiveObjectsCounter : TotalCreatedObjectsCounter
{
    protected override void OnEnable()
    {
        base.OnEnable();
        Spawner.ObjectDeactivated += DecreaseObjectsCount;
    }

    protected override void OnDisable()
    {
        base.OnDisable();
        Spawner.ObjectDeactivated -= DecreaseObjectsCount;
    }

    private void DecreaseObjectsCount()
    {
        ObjectsCount--;
        TextMesh.text = ObjectsCount.ToString();
    }
}
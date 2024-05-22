public class ActiveCubesCounter : TotalCreatedCubesCounter
{
    protected override void OnEnable()
    {
        base.OnEnable();
        CubesSpawner.CubeDeactivated += DecreaseCubesCount;
    }

    protected override void OnDisable()
    {
        base.OnDisable();
        CubesSpawner.CubeDeactivated -= DecreaseCubesCount;
    }

    private void DecreaseCubesCount()
    {
        CubesCount--;
        TextMesh.text = CubesCount.ToString();
    }
}

using UnityEngine;

[RequireComponent(typeof(Collider))]
public class ColorChangingPlatform : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.TryGetComponent(out LifeTimeCube cube))
        {
            cube.ChangeColor();
        }
    }
}

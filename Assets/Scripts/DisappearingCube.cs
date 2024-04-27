using System.Collections;
using UnityEngine;

[RequireComponent(typeof(MeshRenderer), typeof(Collider))]
public class DisappearingCube : MonoBehaviour
{
    private WaitForSeconds _delay;
    private MeshRenderer _meshRenderer;
    private bool _isCanChangeColor = true;

    private void Awake()
    {
        _delay = new WaitForSeconds(SetDelay());
        _meshRenderer = GetComponent<MeshRenderer>();
    }

    private void Start()
    {
        StartCoroutine(WaitingForDestroy());
    }

    public void ChangeColor()
    {
        if (_isCanChangeColor)
        {
            string colorName = "_Color";
            float colorMaxValue = 1f;
            Color newColor = new Color(Random.Range(0f, colorMaxValue), Random.Range(0f, colorMaxValue), Random.Range(0f, colorMaxValue));
            _meshRenderer.material.SetColor(colorName, newColor);
            _isCanChangeColor = false;
        }
    }

    private IEnumerator WaitingForDestroy()
    {
        yield return _delay;
        Destroy(gameObject);
    }

    private float SetDelay()
    {
        float minDelay = 2f;
        float maxDelay = 5f;

        return Random.Range(minDelay, maxDelay);
    }
}
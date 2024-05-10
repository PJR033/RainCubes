using System.Collections;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(MeshRenderer), typeof(Collider))]
public class LifeTimeCube : MonoBehaviour
{
    private Color _startColor;
    private WaitForSeconds _delay;
    private MeshRenderer _meshRenderer;
    private bool _isCanChangeColor = true;

    public UnityEvent<LifeTimeCube> IsLifeTimeEnd;

    private void Awake()
    {
        _delay = new WaitForSeconds(SetDelay());
        _meshRenderer = GetComponent<MeshRenderer>();
        _startColor = _meshRenderer.material.color;
    }

    private void OnEnable()
    {
        StartCoroutine(Disappearing());
        _isCanChangeColor = true;
        _meshRenderer.material.color = _startColor;
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

    private IEnumerator Disappearing()
    {
        yield return _delay;
        IsLifeTimeEnd?.Invoke(this);
    }

    private float SetDelay()
    {
        float minDelay = 2f;
        float maxDelay = 5f;

        return Random.Range(minDelay, maxDelay);
    }
}
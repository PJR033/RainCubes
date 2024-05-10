using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(MeshRenderer), typeof(Collider))]
public class LifetimeCube : MonoBehaviour
{
    private Color _startColor;
    private WaitForSeconds _delay;
    private MeshRenderer _meshRenderer;
    private bool _isCanChangeColor = true;

    public event Action<LifetimeCube> LifetimeEnd;

    private void Awake()
    {
        _meshRenderer = GetComponent<MeshRenderer>();
        _startColor = _meshRenderer.material.color;
    }

    private void OnEnable()
    {
        _delay = new WaitForSeconds(SetDelay());
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
            Color newColor = new Color(UnityEngine.Random.Range(0f, colorMaxValue), UnityEngine.Random.Range(0f, colorMaxValue), UnityEngine.Random.Range(0f, colorMaxValue));
            _meshRenderer.material.SetColor(colorName, newColor);
            _isCanChangeColor = false;
        }
    }

    private IEnumerator Disappearing()
    {
        yield return _delay;
        LifetimeEnd?.Invoke(this);
    }

    private float SetDelay()
    {
        float minDelay = 2f;
        float maxDelay = 5f;

        return UnityEngine.Random.Range(minDelay, maxDelay);
    }
}
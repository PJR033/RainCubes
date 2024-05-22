using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(MeshRenderer))]
public class Bomb : MonoBehaviour
{
    [SerializeField] float _explosionForce;
    [SerializeField] float _eplosionRadius;

    public float ExplosionDelay;

    private MeshRenderer _renderer;

    public event Action<Bomb> IsExplosed;

    private void Awake()
    {
        _renderer = GetComponent<MeshRenderer>();
    }

    private void OnEnable()
    {
        StartCoroutine(ChangingTransparency());
    }

    private IEnumerator ChangingTransparency()
    {
        float transparencyChangeDelta = _renderer.material.color.a / ExplosionDelay;
        float totalTIme = 0f;

        while (totalTIme < ExplosionDelay)
        {
            Color tempColor = _renderer.material.color;
            tempColor.a = Mathf.MoveTowards(tempColor.a, 0, transparencyChangeDelta * Time.deltaTime);
            _renderer.material.color = tempColor;
            totalTIme += Time.deltaTime;
            yield return null;
        }

        Explose();
    }

    private void Explose()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, _eplosionRadius);

        foreach (Collider collider in hitColliders)
        {
            Rigidbody currentBody = collider.attachedRigidbody;

            if (currentBody != null)
            {
                currentBody.AddExplosionForce(_explosionForce, transform.position, _eplosionRadius);
            }
        }

        IsExplosed?.Invoke(this);
    }
}

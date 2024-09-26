using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitParticle : MonoBehaviour, IPoolable
{
    public string PoolName => "HitParticle";
    public GameObject ObjectPrefab => gameObject;
    public bool isUI => false;

    private ParticleSystem _particle;

    private void Awake()
    {
        _particle = transform.GetComponent<ParticleSystem>();
    }

    public void ResetItem() { }

    public void ParticlePlay(Vector3 position, Color hitColor)
    {
        transform.position = position;

        _particle.startColor = hitColor;
        _particle.Play();
        StartCoroutine(PushReady());
    }

    private IEnumerator PushReady()
    {
        yield return new WaitForSeconds(_particle.startLifetime + 0.5f);
        PoolManager.instance.Push(this);
    }
}

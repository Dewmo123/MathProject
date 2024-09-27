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

    public void ResetItem()
    {
        _particle.Play();
        StartCoroutine(PushReady());
    }

    public void SetParticle(Vector3 pos)
    {
        _particle.transform.position = pos;
        
    }

    private IEnumerator PushReady()
    {
        yield return new WaitForSeconds(0.5f);
        PoolManager.instance.Push(this);
    }
}

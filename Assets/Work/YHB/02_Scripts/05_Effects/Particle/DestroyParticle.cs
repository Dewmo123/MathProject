using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyParticle : MonoBehaviour, IPoolable
{
    public string PoolName => "DestoryParticle";
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

    public void SetParticle(Vector3 pos, Color color)
    {
        _particle.transform.position = pos;
        _particle.startColor = color;
    }

    private IEnumerator PushReady()
    {
        yield return new WaitForSeconds(1);
        PoolManager.instance.Push(this);
    }
}

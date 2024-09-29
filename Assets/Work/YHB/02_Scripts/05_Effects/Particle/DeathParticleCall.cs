using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathParticleCall : MonoBehaviour
{
    [SerializeField] private Color _deathColor;
    [SerializeField] private PoolItemSO _destoryParticleSo;

    public void DeathParitleCall()
    {
        DestroyParticle hitParticle = PoolManager.instance.Pop(_destoryParticleSo.poolName) as DestroyParticle;
        hitParticle.SetParticle(transform.position, _deathColor);
    }
}

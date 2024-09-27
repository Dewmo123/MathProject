using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageCaster : MonoBehaviour
{
    public ContactFilter2D filter;
    public float damageRadius;
    public int detectCount = 1;

    [SerializeField] private PoolItemSO _hitParticleSo;
    [SerializeField] private Vector2 _randomMinPos;
    [SerializeField] private Vector2 _randomMaxPos;

    private Collider2D[] _colliders;

    private void Awake()
    {
        _colliders = new Collider2D[detectCount];
    }
    public bool CastDamage(int damage)
    {
        int cnt = Physics2D.OverlapCircle(transform.position, damageRadius, filter, _colliders);
        for(int i = 0; i < cnt; i++)
        {
            if(_colliders[i].TryGetComponent(out Health health))
            {
                health.ChangeValue(-damage);
                HitParticle hitParticle = PoolManager.instance.Pop(_hitParticleSo.poolName) as HitParticle;
                hitParticle.SetParticle(transform.position);
            }
        }

        return cnt > 0;
    }
#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, damageRadius);
        Gizmos.color = Color.white;
    }
#endif
}

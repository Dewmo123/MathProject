using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageCaster : MonoBehaviour
{
    public ContactFilter2D filter;
    public float damageRadius;
    public int detectCount = 1;

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

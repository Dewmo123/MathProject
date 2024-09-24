using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticlePoolItem : MonoBehaviour, IPoolable
{
    [SerializeField] private string _poolName;
    [SerializeField] private Vector3 _playerDistance;
    public string PoolName => _poolName;

    public GameObject ObjectPrefab => gameObject;
    private void Update()
    {
        if(GameManager.instance.Player != null)
        {
            transform.position = GameManager.instance.Player.transform.position + _playerDistance;
        }
    }

    public void ResetItem()
    {
    }
}

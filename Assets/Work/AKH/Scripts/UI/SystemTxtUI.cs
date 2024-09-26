using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SystemTxtUI : MonoBehaviour, IPoolable
{
    [SerializeField] private Vector3 _firstPos;
    [SerializeField] private float _secondPos;
    [SerializeField] private float _time;
    public string PoolName => "SystemText";

    public GameObject ObjectPrefab => gameObject;

    public bool isUI => true;

    private void OnEnable()
    {
        transform.position = _firstPos;
        StartCoroutine(Move());
    }

    private IEnumerator Move()
    {
        yield return null;
        transform.DOMoveY(_secondPos, _time).SetEase(Ease.InQuad);
        transform.DOScale(Vector3.one/2, _time).SetEase(Ease.InQuad);
        yield return new WaitForSeconds(_time);
        transform.localScale = Vector3.one;
        PoolManager.instance.Push(this);
        gameObject.SetActive(false);
    }

    public void ResetItem()
    {
    }
}

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
        Vector2 cResolution = new Vector3(Screen.width, Screen.height);
        Vector2 resolution = new Vector3(1920, 1080);
        transform.position = _firstPos * (cResolution / resolution);
        StartCoroutine(Move((cResolution / resolution).y));
    }

    private IEnumerator Move(float y )
    {
        yield return null;
        transform.DOMoveY(_secondPos*y, _time).SetEase(Ease.InQuad);
        transform.DOScale(Vector3.one / 2, _time).SetEase(Ease.InQuad);
        yield return new WaitForSeconds(_time);
        transform.localScale = Vector3.one;
        PoolManager.instance.Push(this);
        gameObject.SetActive(false);
    }

    public void ResetItem()
    {
    }
}

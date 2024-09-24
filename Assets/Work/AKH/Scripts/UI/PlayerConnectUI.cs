using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerConnectUI : MonoBehaviour
{
    protected Player _player;
    protected virtual void OnEnable()
    {
        StartCoroutine(FindPlayerCoroutine());
    }

    private IEnumerator FindPlayerCoroutine()
    {
        yield return new WaitUntil(()=> GameManager.instance!=null);
        yield return new WaitUntil(()=> GameManager.instance.Player != null);
        _player = GameManager.instance.Player;
        AfterFindPlayer();
    }
    public abstract void AfterFindPlayer();
}

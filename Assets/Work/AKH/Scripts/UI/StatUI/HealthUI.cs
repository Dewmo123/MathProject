using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthUI : StatUI
{
    public override void AfterFindPlayer()
    {
        _playerStat = _player.healthCompo;
        base.AfterFindPlayer();
    }
}

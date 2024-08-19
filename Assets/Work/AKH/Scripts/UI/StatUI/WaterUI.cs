using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaterUI : StatUI
{
    public override void AfterFindPlayer()
    {
        _playerStat = _player.waterCompo;
        base.AfterFindPlayer();
    }
}

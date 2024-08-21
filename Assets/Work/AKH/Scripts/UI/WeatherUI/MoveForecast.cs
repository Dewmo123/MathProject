using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveForecast : MoveUI
{
    public void PlusCnt()
    {
        moveCnt.Value++;
    }
    private void Update()
    {

    }
    public override void Move(int pos)
    {
        rTransform.DOMoveY(pos, time);
    }
}

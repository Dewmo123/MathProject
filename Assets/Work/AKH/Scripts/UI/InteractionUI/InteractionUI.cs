using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class InteractionUI : MoveUI
{
    [field:SerializeField]public UIType MyType { get; protected set; }
    public abstract void AddDic();
    protected override void HandleCnt(int prev, int next)
    {
        if (next == 1)
        {
            Move(_firstPos);
            GameManager.instance.SetInteractionUI(true);
        }
        else if (next == 2)
        {
            Move(_secondPos);
            moveCnt.Value = 0;
            GameManager.instance.SetInteractionUI(false);
        }
    }
    public override void Move(int pos)
    {
        rTransform.DOMoveY(pos,time);
    }
}

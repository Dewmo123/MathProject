using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class InteractionUI : MoveUI
{
    [field:SerializeField]public UIType MyType { get; protected set; }
    
    public abstract void AddDic();
    protected override void Awake()
    {
        base.Awake();
        StartCoroutine(WaitInteractionManager());
    }
    private IEnumerator WaitInteractionManager()
    {
        yield return new WaitUntil(()=>InteractionManager.instance != null);
        AddDic();
    }
    protected override void Close()
    {
        SoundPlayer player = PoolManager.instance.Pop("SoundPlayer") as SoundPlayer;
        player.PlaySound(InteractionManager.instance.closeUI);
        Move(_secondPos);
        moveCnt.Value = 0;
        GameManager.instance.SetInteractionUI(false);
    }

    protected override void Open()
    {
        SoundPlayer player = PoolManager.instance.Pop("SoundPlayer") as SoundPlayer;
        player.PlaySound(InteractionManager.instance.openUI);
        Move(_firstPos);
        GameManager.instance.SetInteractionUI(true);
    }

    public override void Move(int pos)
    {
        rTransform.DOMoveY(pos,time);
    }
}

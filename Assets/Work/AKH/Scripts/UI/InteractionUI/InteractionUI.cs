using DG.Tweening;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public abstract class InteractionUI : MoveUI
{
    [field: SerializeField] public UIType MyType { get; protected set; }

    public abstract void AddDic();
    protected override void Awake()
    {
        base.Awake();
        SetButton(false);
        StartCoroutine(WaitInteractionManager());
    }
    private IEnumerator WaitInteractionManager()
    {
        yield return new WaitUntil(() => InteractionManager.instance != null);
        AddDic();
    }
    protected override void Close()
    {
        SoundPlayer player = PoolManager.instance.Pop("SoundPlayer") as SoundPlayer;
        player.PlaySound(InteractionManager.instance.closeUI);
        SetButton(false);
        Move(_secondPos);
        moveCnt.Value = 0;
        GameManager.instance.SetInteractionUI(false);
    }

    private void SetButton(bool val)
    {
        foreach (Transform trm in rTransform)
        {
            Button B;
            if (trm.TryGetComponent(out B))
                B.interactable = val;
        }
    }

    protected override void Open()
    {
        SoundPlayer player = PoolManager.instance.Pop("SoundPlayer") as SoundPlayer;
        player.PlaySound(InteractionManager.instance.openUI);
        SetButton(true);
        Move(_firstPos);
        GameManager.instance.SetInteractionUI(true);
    }

    public override void Move(int pos)
    {
        int cHei = Screen.height;
        rTransform.DOMoveY(pos * ((float)cHei / 1080), time);
    }
}

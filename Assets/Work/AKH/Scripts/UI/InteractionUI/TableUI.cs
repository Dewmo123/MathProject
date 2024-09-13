using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TableUI : InteractionUI
{
    private ClothUI _coreCloth;
    private ItemSO _leather;
    protected override void Awake()
    {
        base.Awake();
        _coreCloth = GetComponentInChildren<ClothUI>();
        _leather = GameManager.instance.GetItemSO("Leather");
    }
    public void ChangeCloth(ClothSO cloth)
    {
        if (_leather.cnt.Value > cloth.leatherCount)
        {
            _leather.cnt.Value -= cloth.leatherCount;
            _coreCloth.SetCurCloth(cloth);
        }
    }
    public override void AddDic()
    {
        InteractionManager.instance.InteractionUIDic.Add(MyType, this);
    }
}

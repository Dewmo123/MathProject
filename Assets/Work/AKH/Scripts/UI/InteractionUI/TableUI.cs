using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TableUI : InteractionUI
{
    public ClothSO CurCloth { get; private set; }
    [SerializeField] private ClothSO _noneCloth;
    [SerializeField] private Image _clothImage;
    protected override void Awake()
    {
        base.Awake();
        CurCloth = _noneCloth;
    }
    public void ChangeCloth(ClothSO cloth)
    {
        CurCloth = cloth;
        _clothImage.sprite = cloth.clothImage;
    }
    public override void AddDic()
    {
        InteractionManager.instance.InteractionUIDic.Add(MyType, this);
    }
}

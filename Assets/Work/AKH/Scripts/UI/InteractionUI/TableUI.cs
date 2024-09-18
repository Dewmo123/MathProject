using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TableUI : InteractionUI
{
    [SerializeField] private SpriteRenderer _houseRenderer;
    private ClothUI _coreCloth;
    private ItemSO _leather;
    private ItemSO _wood;
    private ItemSO _rock;
    protected override void Awake()
    {
        base.Awake();
        _coreCloth = GetComponentInChildren<ClothUI>();
        _leather = GameManager.instance.GetItemSO("Leather");
        _wood = GameManager.instance.GetItemSO("Wood");
        _rock = GameManager.instance.GetItemSO("Rock");
    }
    public void ChangeCloth(ClothSO cloth)
    {
        if (cloth != GameManager.instance.CurCloth.Value && _leather.cnt.Value > cloth.leatherCount)
        {
            _leather.cnt.Value -= cloth.leatherCount;
            _coreCloth.SetCurCloth(cloth);
        }
    }
    public void ChangeHouse(HouseSO house)
    {
        if (house != GameManager.instance.CurHouse.Value && house.woodCount < _wood.cnt.Value && house.rockCount < _rock.cnt.Value)
        {
            _wood.cnt.Value -= house.woodCount;
            _rock.cnt.Value -= house.rockCount;
            GameManager.instance.SetHouseSO(house);
            _houseRenderer.sprite = house.houseImage;
        }
    }
    public override void AddDic()
    {
        InteractionManager.instance.InteractionUIDic.Add(MyType, this);
    }
}

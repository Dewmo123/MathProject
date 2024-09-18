using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HouseInfoUI : GoodsInfoUI
{
    [SerializeField] private HouseSO _goods;
    private void Start()
    {
        _price.text = $"����: {_goods.woodCount}��, ��: {_goods.rockCount}��";
        _goodsInfo.text = _goods.houseName+"\n"+_goods.info;
        _image.sprite = _goods.houseImage;
    }
}

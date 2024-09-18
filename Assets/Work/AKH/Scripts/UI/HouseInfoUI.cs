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
        _price.text = $"나무: {_goods.woodCount}개, 돌: {_goods.rockCount}개";
        _goodsInfo.text = _goods.houseName+"\n"+_goods.info;
        _image.sprite = _goods.houseImage;
    }
}

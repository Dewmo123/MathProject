using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ClothInfoUI : GoodsInfoUI
{
    [SerializeField] private ClothSO cloth;
    private void Start()
    {
        _goodsInfo.text = cloth.clothName + "\n" + cloth.info;
        _price.text = $"Ãµ {cloth.leatherCount}°³";
        _image.sprite = cloth.clothImage;
    }
}

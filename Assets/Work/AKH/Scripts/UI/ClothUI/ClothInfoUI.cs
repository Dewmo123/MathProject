using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ClothInfoUI : MonoBehaviour
{
    [SerializeField] private ClothSO cloth;
    [SerializeField] private TextMeshProUGUI _goodsInfo;
    [SerializeField] private TextMeshProUGUI _price;
    [SerializeField] private Image _image; 
    private void Awake()
    {
        _goodsInfo.text = cloth.clothName + "\n" + cloth.info;
        _price.text = $"Ãµ {cloth.leatherCount}°³";
        _image.sprite = cloth.clothImage;
    }
}

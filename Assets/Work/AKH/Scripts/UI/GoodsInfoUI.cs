using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public abstract class GoodsInfoUI : MonoBehaviour
{
    [SerializeField]protected TextMeshProUGUI _goodsInfo;
    [SerializeField]protected TextMeshProUGUI _price;
    protected Image _image;
    private void Awake()
    {
        _image = GetComponentInChildren<Image>();
    }
}

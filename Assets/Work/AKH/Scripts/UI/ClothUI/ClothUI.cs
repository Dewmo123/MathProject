using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ClothUI : MonoBehaviour
{
    public NotifyValue<ClothSO> CurCloth = new NotifyValue<ClothSO>();
    private NotifyValue<int> usedDay = new NotifyValue<int>(0);
    [SerializeField] private Image _clothImage;
    [SerializeField] private TextMeshProUGUI _durabilityTxt;
    [SerializeField] private ClothSO _noneCloth;
    private void Awake()
    {
        CurCloth.OnvalueChanged += HandleClothChanged;
        CurCloth.Value = _noneCloth;
    }

    private void HandleClothChanged(ClothSO prev, ClothSO next)
    {
        if (usedDay.Value == 0)
            HandleUsedDayChanged(0, 0);
        else
            usedDay.Value = 0;
    }

    private void Start()
    {
        TimeManager.instance.DayCnt.OnvalueChanged += (int prev, int next) => usedDay.Value++;
        usedDay.OnvalueChanged += HandleUsedDayChanged;
        HandleUsedDayChanged(0, 0);
    }
    private void HandleUsedDayChanged(int prev, int next)
    {
        if (next / CurCloth.Value.durability == 1) SetCurCloth(_noneCloth);
        else _durabilityTxt.text = $"{next} / {CurCloth.Value.durability}";
    }

    public void SetCurCloth(ClothSO cloth)
    {
        CurCloth.Value = cloth;
        _clothImage.sprite = cloth.clothImage;
    }
}

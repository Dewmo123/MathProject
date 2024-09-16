using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ClothUI : MonoBehaviour
{
    private NotifyValue<int> usedDay = new NotifyValue<int>(0);
    private NotifyValue<ClothSO> CurCloth;
    [SerializeField] private Image _clothImage;
    [SerializeField] private TextMeshProUGUI _durabilityTxt;
    [SerializeField] private ClothSO _noneCloth;
    private void HandleClothChanged(ClothSO prev, ClothSO next)
    {
        if (usedDay.Value == 0)
            HandleUsedDayChanged(0, 0);
        else
            usedDay.Value = 0;
    }

    private void Start()
    {
        CurCloth = GameManager.instance.CurCloth;

        CurCloth.OnvalueChanged += HandleClothChanged;
        TimeManager.instance.DayCnt.OnvalueChanged += (int prev, int next) => usedDay.Value++;
        usedDay.OnvalueChanged += HandleUsedDayChanged;

        CurCloth.Value = _noneCloth;
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

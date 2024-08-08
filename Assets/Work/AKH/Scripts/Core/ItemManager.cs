using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoSingleton<ItemManager>
{
    public event Action ItemCntChange;
    public List<ItemSO> items;

    private void Awake()
    {
    }
}

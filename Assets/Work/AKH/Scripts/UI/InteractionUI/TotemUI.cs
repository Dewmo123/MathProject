using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TotemUI : InteractionUI
{
    private void Start()
    {
        AddDic();
    }
    public override void AddDic()
    {
        InteractionManager.instance.InteractionUIDic.Add(MyType,this);
    }
}

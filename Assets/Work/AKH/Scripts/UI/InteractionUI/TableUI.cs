using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TableUI : InteractionUI
{
    public override void AddDic()
    {
        InteractionManager.instance.InteractionUIDic.Add(MyType, this);
    }
}

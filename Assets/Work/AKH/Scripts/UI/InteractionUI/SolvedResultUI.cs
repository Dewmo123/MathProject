using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SolvedResultUI : InteractionUI
{
    
    [SerializeField]private TextMeshProUGUI _gotItemTxt;
    [SerializeField]private TextMeshProUGUI _resultTxt;

    public override void AddDic()
    {
        InteractionManager.instance.InteractionUIDic.Add(MyType,this);
    }
    public void SetItemTxt(string message)
    {
        _gotItemTxt.text = message;
    }
    public void SetResultTxt(bool val)
    {
        if (val)
            _resultTxt.text = "정답";
        else
            _resultTxt.text = "오답";
    }

}

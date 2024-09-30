using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MoveForecast : MoveInven
{
    public override void Move(int pos)
    {
        int cHei = Screen.height;
        rTransform.DOMoveY(pos * ((float)cHei / 1080), time);
    }
}

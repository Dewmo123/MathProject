using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MoveForecast : MoveInven
{
    public override void Move(int pos)
    {
        rTransform.DOMoveY(pos, time);
    }
}

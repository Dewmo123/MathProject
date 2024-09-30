using DG.Tweening;
using UnityEngine;

public class MoveForecast : MoveInven
{
    public override void Move(int pos)
    {
        int cHei = Screen.height;
        rTransform.DOMoveY(pos * ((float)cHei / 1080), time);
    }
}

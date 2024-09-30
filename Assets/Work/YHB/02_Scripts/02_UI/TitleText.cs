using DG.Tweening;
using UnityEngine;

public class TitleText : MonoBehaviour
{
    [SerializeField] private float _titleFadeTime;

    private Vector3 _myStartPos;

    private void Awake()
    {
        _myStartPos = transform.position;
        BringTitle();
    }

    private void BringTitle()
    {
        transform.position = new Vector3(_myStartPos.x, Screen.height * 2);
        transform.DOMoveY(_myStartPos.y, _titleFadeTime);
    }
}

using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInteractionUI : MonoBehaviour
{
    [Header("Info")]
    [SerializeField] private Image _fKeyImage;
    [SerializeField] private Transform _playerTrm;

    [Header("FKeySet")]
    [SerializeField] private float _upYPos = 200;

    [HideInInspector] public Dictionary<string, InteractionObjectInfoSo> _interactionObjectInfo;

    private Camera mainCam;

    private void Awake()
    {
        Initialize();
    }

    private void FixedUpdate()
    {
        GetPlayerPos();
    }

    private void Initialize()
    {
        _interactionObjectInfo = new();
        mainCam = Camera.main;
    }

    private void GetPlayerPos()
    {
        Vector2 playerPos = mainCam.WorldToScreenPoint(_playerTrm.position);
        _fKeyImage.rectTransform.position = new Vector2(playerPos.x, playerPos.y + _upYPos);
    }

    public void FadeInteractionUI(string code)
    {
        Debug.Log("OnTo");
        Sequence fKeySequence = DOTween.Sequence();

        if (_interactionObjectInfo[code]._canInteraction)
        {
            Debug.Log("Sequence");
            fKeySequence.Append(_fKeyImage.rectTransform.DOScale(new Vector3(1, 0.2f), 0.5f)); // 가로로 펴짐
            fKeySequence.Append(_fKeyImage.rectTransform.DOScale(new Vector3(1, 1), 0.5f)); // 정상화

            fKeySequence.Append(_fKeyImage.rectTransform.DOScale(new Vector3(1, 1), 1f));
        }
        else if (_interactionObjectInfo[code]._bigTitle)
        {

            return;
        }

        Debug.Log("ForEnd");
    }

    public void OutFadeInteractionUI()
    {
        Sequence fKeySequence = DOTween.Sequence();

        fKeySequence.Append(_fKeyImage.rectTransform.DOScale(new Vector3(1, 0.2f), 0.5f)); // 가로로 접혀짐
        fKeySequence.Append(_fKeyImage.rectTransform.DOScale(new Vector3(0, 0), 0.5f)); // 사라짐

        fKeySequence.OnComplete(() => fKeySequence.Complete());
    }
}

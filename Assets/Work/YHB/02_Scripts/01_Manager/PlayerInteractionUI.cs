using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInteractionUI : MonoBehaviour
{
    [Header("FKeySet")]
    [Tooltip("�߾ӿ��� �󸶳� �ø��� �����Դϴ�.")]
    [SerializeField] private float _upYPos = 200;
    [Tooltip("FŰ�� �㶧 ������ �ִϸ��̼��� �� ������ �ð��Դϴ�. (�׳� / 3�� �� ����)")]
    [SerializeField] private float _OnSecond = 0.5f;

    [Header("Info")]
    [SerializeField] private RectTransform _fKeyImage;
    [SerializeField] private RectTransform _titleImage;
    [SerializeField] private Image _bigTitleImage;

    [SerializeField] private Transform _playerTrm;

    [HideInInspector] public Dictionary<string, InteractionObjectInfoSo> _interactionObjectInfo;

    private Camera mainCam;
    private TextMeshProUGUI _bigTitleText;

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
        if (_playerTrm == null)
        {
            Debug.LogWarning($"{gameObject.name} can't find player. So, You have to put {gameObject.name}'s interactor");
        }

        _interactionObjectInfo = new();
        mainCam = Camera.main;
        _bigTitleText = _bigTitleImage.rectTransform.Find("Text").GetComponent<TextMeshProUGUI>();

        _fKeyImage.gameObject.SetActive(false);
        _titleImage.DOScale(Vector3.zero, 0);
        _bigTitleImage.DOFade(0, 0);
        _bigTitleText.DOFade(0, 0);
    }

    private void GetPlayerPos()
    {
        Vector2 playerPos = mainCam.WorldToScreenPoint(_playerTrm.position);
        _fKeyImage.position = new Vector2(playerPos.x, playerPos.y + _upYPos);
        _titleImage.position = new Vector2(playerPos.x, playerPos.y - _upYPos / 2);
    }

    private IEnumerator ActivationTimeSet(string code)
    {
        _interactionObjectInfo[code]._canActivation = false;

        yield return new WaitForSeconds(_interactionObjectInfo[code]._activationTime);

        _interactionObjectInfo[code]._canActivation = true;
    }

    public void FadeInteractionUI(string code)
    {
        Sequence interactionUISequence = DOTween.Sequence();

        if (_interactionObjectInfo[code]._canInteraction)
        {
            _fKeyImage.gameObject.SetActive(true);
            interactionUISequence.Append(_fKeyImage.DOScale(Vector3.zero, 0));
            interactionUISequence.Append(_fKeyImage.DOScale(new Vector3(0.1f, 0.3f), _OnSecond));
            interactionUISequence.Append(_fKeyImage.DOScale(new Vector3(1, 0.3f), _OnSecond)); // ���η� ����
            interactionUISequence.Append(_fKeyImage.DOScale(new Vector3(1, 1), _OnSecond)); // ����ȭ
        }
        else if (_interactionObjectInfo[code]._bigTitle && _interactionObjectInfo[code]._canActivation)
        {
            StartCoroutine(ActivationTimeSet(code));

            _bigTitleText.text = _interactionObjectInfo[code]._str;

            interactionUISequence.Append(_bigTitleImage.DOFade(0, 0));
            interactionUISequence.Join(_bigTitleText.DOFade(0, 0));
            interactionUISequence.Append(_bigTitleImage.DOFade(0.75f, _OnSecond * 3));
            interactionUISequence.Join(_bigTitleText.DOFade(1, _OnSecond * 3));
            interactionUISequence.Append(_bigTitleImage.DOFade(0.85f, _OnSecond * 9));
            interactionUISequence.Append(_bigTitleImage.DOFade(0, _OnSecond * 6));
            interactionUISequence.Join(_bigTitleText.DOFade(0, _OnSecond * 6));
            return;
        }

        _titleImage.Find("Text").GetComponent<TextMeshProUGUI>().text = _interactionObjectInfo[code]._str;

        interactionUISequence.Join(_titleImage.DOScale(Vector3.zero, 0));
        interactionUISequence.Append(_titleImage.DOScale(new Vector3(0.1f, 1), _OnSecond));
        interactionUISequence.Append(_titleImage.DOScale(new Vector3(4, 1), _OnSecond));
    }

    public void OutFadeInteractionUI(bool canInteraction)
    {
        Sequence interactionUISequence = DOTween.Sequence();

        if (canInteraction)
        {
            interactionUISequence.Append(_fKeyImage.DOScale(new Vector3(1, 1), 0));
            interactionUISequence.Append(_fKeyImage.DOScale(new Vector3(1, 0.3f), _OnSecond)); // ���η� ������
            interactionUISequence.Append(_fKeyImage.DOScale(new Vector3(0.1f, 0.3f), _OnSecond));
            interactionUISequence.Append(_fKeyImage.DOScale(Vector3.zero, _OnSecond)); // �����
            _fKeyImage.gameObject.SetActive(false);
        }

        interactionUISequence.Join(_titleImage.DOScale(new Vector3(4, 1), 0));
        interactionUISequence.Append(_titleImage.DOScale(new Vector3(0.1f, 1), _OnSecond));
        interactionUISequence.Append(_titleImage.DOScale(Vector3.zero, 0));
    }
}

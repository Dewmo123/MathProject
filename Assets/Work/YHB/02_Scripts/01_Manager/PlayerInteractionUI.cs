using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInteractionUI : MonoBehaviour
{
    [Header("FKeySet")]
    [Tooltip("중앙에서 얼마나 올릴지 여부입니다.")]
    [SerializeField] private float _upYPos = 200;
    [Tooltip("F키가 뜰때 까지의 애니메이션의 한 과정의 시간입니다. (그냥 / 3한 값 ㄱㄱ)")]
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
        _interactionObjectInfo = new();
        mainCam = Camera.main;
        _bigTitleText = _bigTitleImage.rectTransform.Find("Text").GetComponent<TextMeshProUGUI>();

        _fKeyImage.gameObject.SetActive(false);
        _titleImage.gameObject.SetActive(false);
        _bigTitleImage.DOFade(0, 0);
    }

    private void GetPlayerPos()
    {
        Vector2 playerPos = mainCam.WorldToScreenPoint(_playerTrm.position);
        _fKeyImage.position = new Vector2(playerPos.x, playerPos.y + _upYPos);
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
            interactionUISequence.Append(_fKeyImage.DOScale(new Vector3(0, 0), 0));
            interactionUISequence.Append(_fKeyImage.DOScale(new Vector3(0.1f, 0.3f), _OnSecond));
            interactionUISequence.Append(_fKeyImage.DOScale(new Vector3(1, 0.3f), _OnSecond)); // 가로로 펴짐
            interactionUISequence.Append(_fKeyImage.DOScale(new Vector3(1, 1), _OnSecond)); // 정상화
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

        _titleImage.gameObject.SetActive(true);
        interactionUISequence.Append(_titleImage.DOScale(new Vector3(0, 0), 0));
        interactionUISequence.Append(_titleImage.DOScale(new Vector3(0.1f, 0.3f), _OnSecond));
        interactionUISequence.Append(_titleImage.DOScale(new Vector3(1, 0.3f), _OnSecond));
        interactionUISequence.Append(_titleImage.DOScale(new Vector3(1, 1), _OnSecond));

        interactionUISequence.OnComplete(() => interactionUISequence.Complete());
    }

    public void OutFadeInteractionUI(bool canInteraction)
    {
        Sequence interactionUISequence = DOTween.Sequence();

        if (canInteraction)
        {
            interactionUISequence.Append(_fKeyImage.DOScale(new Vector3(1, 1), 0));
            interactionUISequence.Append(_fKeyImage.DOScale(new Vector3(1, 0.3f), _OnSecond)); // 가로로 접혀짐
            interactionUISequence.Append(_fKeyImage.DOScale(new Vector3(0.1f, 0.3f), _OnSecond));
            interactionUISequence.Append(_fKeyImage.DOScale(new Vector3(0, 0), _OnSecond)); // 사라짐
            _fKeyImage.gameObject.SetActive(false);
            return;
        }

        interactionUISequence.Append(_titleImage.DOScale(new Vector3(1, 1), 0));
        interactionUISequence.Append(_titleImage.DOScale(new Vector3(1, 0.3f), _OnSecond));
        interactionUISequence.Append(_titleImage.DOScale(new Vector3(0.1f, 0.3f), _OnSecond));
        interactionUISequence.Append(_titleImage.DOScale(new Vector3(0, 0), _OnSecond));
        _fKeyImage.gameObject.SetActive(false);

        interactionUISequence.OnComplete(() => interactionUISequence.Complete());
    }
}

using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public enum UIType
{
    Problem,
    Totem,
    Table,
}
public class InteractionManager : MonoBehaviour
{
    public static InteractionManager instance = null;

    public Dictionary<UIType, InteractionUI> InteractionUIDic;

    [Header("FKeySet")]
    [Tooltip("플레이어에게서 얼마나 올릴지 여부입니다. RectTransform기준으로 하지마세요.")]
    [SerializeField] private float _upYPos = 200;
    [Tooltip("플레이어에게서 얼마나 올릴지 여부입니다. RectTransform기준으로 하지마세요.")]
    [SerializeField] private float _downYPos = 200;
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
    private void Update()
    {
        GetPlayerPos();
    }

    private void Initialize()
    {
        if (instance == null)
        {
            instance = this;
        }

        if (_playerTrm == null)
        {
            Debug.LogWarning($"{gameObject.name} can't find player. So, You have to put {gameObject.name}'s interactor");
        }
        InteractionUIDic = new Dictionary<UIType, InteractionUI>();
        _interactionObjectInfo = new();
        mainCam = Camera.main;
        _bigTitleText = _bigTitleImage.rectTransform.Find("Text").GetComponent<TextMeshProUGUI>();

        _fKeyImage.DOScale(0, 0);
        _fKeyImage.gameObject.SetActive(false);
        _titleImage.DOScale(0, 0);
        _bigTitleImage.DOFade(0, 0);
        _bigTitleText.DOFade(0, 0);
    }

    public void InteractionInfoAdd(InteractionObjectInfoSo interObj)
    {
        foreach (InteractionObjectInfoSo item in _interactionObjectInfo.Values)
        {
            if (item == interObj) return;
        }

        _interactionObjectInfo.Add(interObj._code, interObj);
    }

    private void GetPlayerPos()
    {
        Vector2 playerPos = mainCam.WorldToScreenPoint(_playerTrm.position);

        _fKeyImage.position = new Vector2(playerPos.x, playerPos.y + _upYPos * 100);
        _titleImage.position = new Vector2(playerPos.x, playerPos.y + _downYPos * 100);
    }

    public void FadeInteractionUI(string code)
    {
        Sequence interactionUISequence = DOTween.Sequence();

        if (_interactionObjectInfo[code]._canInteraction && _interactionObjectInfo[code]._title)
        {
            _titleImage.Find("Text").GetComponent<TextMeshProUGUI>().text = _interactionObjectInfo[code]._str;

            _fKeyImage.gameObject.SetActive(true);

            interactionUISequence.Append(_fKeyImage.DOScale(0, 0));
            interactionUISequence.Append(_fKeyImage.DOScale(new Vector3(0.1f, 0.3f), _OnSecond));
            interactionUISequence.Join(_titleImage.DOScale(0, 0));
            interactionUISequence.Append(_fKeyImage.DOScale(new Vector3(1, 0.3f), _OnSecond));
            interactionUISequence.Join(_titleImage.DOScale(1, _OnSecond));
            interactionUISequence.Append(_fKeyImage.DOScale(1, _OnSecond));
            interactionUISequence.Join(_titleImage.DOScale(new Vector3(4, 1), _OnSecond));
        }
        else if (_interactionObjectInfo[code]._canInteraction)
        {
            _fKeyImage.gameObject.SetActive(true);
            interactionUISequence.Append(_fKeyImage.DOScale(0, 0));
            interactionUISequence.Append(_fKeyImage.DOScale(new Vector3(0.1f, 0.3f), _OnSecond));
            interactionUISequence.Append(_fKeyImage.DOScale(new Vector3(1, 0.3f), _OnSecond)); // 가로로 펴짐
            interactionUISequence.Append(_fKeyImage.DOScale(1, _OnSecond)); // 정상화
        }
        else if (_interactionObjectInfo[code]._title)
        {
            _titleImage.Find("Text").GetComponent<TextMeshProUGUI>().text = _interactionObjectInfo[code]._str;

            interactionUISequence.Join(_titleImage.DOScale(0, 0));
            interactionUISequence.Append(_titleImage.DOScale(1, _OnSecond));
            interactionUISequence.Append(_titleImage.DOScale(new Vector3(4, 1), _OnSecond));
        }
        else
        {
            _bigTitleText.text = _interactionObjectInfo[code]._str;

            interactionUISequence.Append(_bigTitleImage.DOFade(0, 0));
            interactionUISequence.Join(_bigTitleText.DOFade(0, 0));
            interactionUISequence.Append(_bigTitleImage.DOFade(0.75f, _OnSecond * 3));
            interactionUISequence.Join(_bigTitleText.DOFade(1, _OnSecond * 3));
            interactionUISequence.Append(_bigTitleImage.DOFade(0.85f, _OnSecond * 6));
            interactionUISequence.Append(_bigTitleImage.DOFade(0, _OnSecond * 6));
            interactionUISequence.Join(_bigTitleText.DOFade(0, _OnSecond * 6));
        }
    }

    public void OutFadeInteractionUI(string code)
    {
        Sequence interactionUISequence = DOTween.Sequence();

        if (_interactionObjectInfo[code]._canInteraction && _interactionObjectInfo[code]._title)
        {
            interactionUISequence.Append(_fKeyImage.DOScale(1, 0));
            interactionUISequence.Append(_fKeyImage.DOScale(new Vector3(1, 0.3f), _OnSecond));
            interactionUISequence.Join(_titleImage.DOScale(new Vector3(4, 1), 0));
            interactionUISequence.Append(_fKeyImage.DOScale(new Vector3(0.1f, 0.3f), _OnSecond));
            interactionUISequence.Join(_titleImage.DOScale(1, _OnSecond));
            interactionUISequence.Append(_fKeyImage.DOScale(0, _OnSecond));
            interactionUISequence.Join(_titleImage.DOScale(0, _OnSecond));
        }
        else if (_interactionObjectInfo[code]._canInteraction)
        {
            interactionUISequence.Append(_fKeyImage.DOScale(1, 0));
            interactionUISequence.Append(_fKeyImage.DOScale(new Vector3(1, 0.3f), _OnSecond)); // 가로로 접혀짐
            interactionUISequence.Append(_fKeyImage.DOScale(new Vector3(0.1f, 0.3f), _OnSecond));
            interactionUISequence.Append(_fKeyImage.DOScale(0, _OnSecond)); // 사라짐
            _fKeyImage.gameObject.SetActive(false);
        }
        else if(_interactionObjectInfo[code]._title)
        {
            interactionUISequence.Join(_titleImage.DOScale(new Vector3(4, 1), 0));
            interactionUISequence.Append(_titleImage.DOScale(1, _OnSecond));
            interactionUISequence.Append(_titleImage.DOScale(0, _OnSecond));
        }
    }
}

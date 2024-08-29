using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;

enum Shape
{
    Square,
    Circle
}

public class InteractionObject : MonoBehaviour
{
    [Header("Target Layer")]
    [Tooltip("상호 작용 주체")]
    [SerializeField] private LayerMask _layer;

    [Header("Over Shape")]
    [Tooltip("상호작용 범위 모양")]
    [SerializeField] private Shape _shape;

    [Header("Value")]
    [Tooltip("위치")]
    [SerializeField] private Vector2 _movePosition;

    [Header("Value")]
    [Tooltip("사각형의 가로(원의 반지름) 세로")]
    [SerializeField] private Vector2 _size;

    [Header("So")]
    [Tooltip("해당 오브젝트의 So")]
    [SerializeField] private InteractionObjectInfoSo _interactionObjectInfo;

    #region SoSynchronization

    [Header("So Setting")]
    [Tooltip("F키를 띄울지 여부입니다.")]
    [SerializeField] private bool _canInteractionSet;
    [Tooltip("제목을 띄울지 여부입니다.")]
    [SerializeField] public bool _titleSet;
    [Tooltip("플레이어에게 표시딜 때 크게 표시 되는 지 여부입니다.")]
    [SerializeField] private bool _bigTitleSet;
    [Tooltip("게임에 표시될 문장입니다.")]
    [SerializeField] private string _strSet;

    #endregion

    private Vector2 _position;

    private bool _canInteraction = false;
    private bool _enterCollision = false;

    private void Update()
    {
        ChackInteraction();
    }

    private void Start()
    {
        Initialize();
    }

    public void Initialize()
    {
        _position.x = transform.position.x + _movePosition.x; // 좌표 초기화
        _position.y = transform.position.y + _movePosition.y;

        PlayerInteractionUI.instance.InteractionInfoAdd(_interactionObjectInfo);
    }

    public void ChackInteraction()
    {
        switch (_shape) // 모양별 충돌 구분
        {
            case Shape.Square:
                _enterCollision = Physics2D.OverlapBox(_position, _size, 0, _layer);
                break;

            case Shape.Circle:
                _enterCollision = Physics2D.OverlapCircle(_position, _size.x, _layer);
                break;
        }

        if (_enterCollision && !_canInteraction) // 인터렉션 가능 (범위에 들어옴)
        {
            PlayerInteractionUI.instance.FadeInteractionUI(_interactionObjectInfo._code);
            _canInteraction = true;
        }
        else if (!_enterCollision && _canInteraction) // 인터렉션 불가 (범위에서 벗어남)
        {
            PlayerInteractionUI.instance.OutFadeInteractionUI(_interactionObjectInfo._code);
            _canInteraction = false;
        }
    }

#if UNITY_EDITOR
    private void OnValidate()
    {
        switch (_shape) // 원 일때 wh값 동기화
        {
            case Shape.Circle:
                _size.y = _size.x;
                break;
        }

        if (_canInteractionSet || _titleSet)
        {
            if (_bigTitleSet)
            {
                _bigTitleSet = false;
            }
        }
        else
        {
            _bigTitleSet = true;
            _titleSet = false;
        }

        _interactionObjectInfo._code = _interactionObjectInfo.name;
        _interactionObjectInfo._canInteraction = _canInteractionSet;
        _interactionObjectInfo._title = _titleSet;
        _interactionObjectInfo._bigTitle = _bigTitleSet;
        _interactionObjectInfo._str = _strSet;
    }

    protected virtual void OnDrawGizmosSelected()
    {
        _position.x = transform.position.x + _movePosition.x; // 좌표 확인
        _position.y = transform.position.y + _movePosition.y;

        Gizmos.color = Color.green;
        switch (_shape)
        {
            case Shape.Square:
                Gizmos.DrawCube(_position, _size);
                break;

            case Shape.Circle:
                Gizmos.DrawWireSphere(_position, _size.x);
                break;
        }
        Gizmos.color = Color.white;
    }
#endif
}

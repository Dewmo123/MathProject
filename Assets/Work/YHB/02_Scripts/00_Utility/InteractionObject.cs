using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum Shape
{
    Square,
    Circle
}

public class InteractionObject : MonoBehaviour
{
    [Header("Player")]
    [Tooltip("플레이어")]
    [SerializeField] private Interactioner _intaractioner;

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

    [Header("UI")]
    [Tooltip("인터렉션 창을 띄어주는 UI")]
    [SerializeField] private PlayerInteractionUI _interactionInfoUI;

    #region SoSynchronization

    [Header("So Setting")]
    [Tooltip("딕셔너리의 키로 들어갈 이름입니다.")]
    [SerializeField] private string _codeSet;
    [Tooltip("F키를 띄울지 여부입니다.")]
    [SerializeField] private bool _canInteractionSet;
    [Tooltip("플레이어에게 표시딜 때 크게 표시 되는 지 여부입니다.")]
    [SerializeField] private bool _bigTitleSet;
    [Tooltip("게임에 표시될 문장입니다.")]
    [SerializeField] private string _strSet;
    [Tooltip("다시 활성화 될때 까지의 시간")]
    [SerializeField] private float _activationTimeSet;

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

        _interactionObjectInfo._canActivation = true;
        _interactionInfoUI._interactionObjectInfo.Add(_interactionObjectInfo._code, _interactionObjectInfo);
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
            _intaractioner.CanInteraction(_interactionObjectInfo._code);
            _canInteraction = true;
        }
        else if (!_enterCollision && _canInteraction) // 인터렉션 불가 (범위에서 벗어남)
        {
            _intaractioner.CanNotInteraction(_interactionObjectInfo._canInteraction);
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

        _interactionObjectInfo._code = _codeSet;
        _interactionObjectInfo._canInteraction = _canInteractionSet;
        _interactionObjectInfo._bigTitle = !_canInteractionSet;
        _interactionObjectInfo._str = _strSet;
        _interactionObjectInfo._activationTime = _activationTimeSet > 0 ? _activationTimeSet : 0;

        _codeSet = _interactionObjectInfo._code;
        _canInteractionSet = _interactionObjectInfo._canInteraction;
        _bigTitleSet = _interactionObjectInfo._bigTitle;
        _strSet = _interactionObjectInfo._str;
        _activationTimeSet = _interactionObjectInfo._activationTime;
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

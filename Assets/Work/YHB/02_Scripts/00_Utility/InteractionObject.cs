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
    [Tooltip("�÷��̾�")]
    [SerializeField] private Interactioner _intaractioner;

    [Header("Target Layer")]
    [Tooltip("��ȣ �ۿ� ��ü")]
    [SerializeField] private LayerMask _layer;

    [Header("Over Shape")]
    [Tooltip("��ȣ�ۿ� ���� ���")]
    [SerializeField] private Shape _shape;

    [Header("Value")]
    [Tooltip("��ġ")]
    [SerializeField] private Vector2 _movePosition;

    [Header("Value")]
    [Tooltip("�簢���� ����(���� ������) ����")]
    [SerializeField] private Vector2 _size;

    [Header("So")]
    [Tooltip("�ش� ������Ʈ�� So")]
    [SerializeField] private InteractionObjectInfoSo _interactionObjectInfo;

    [Header("UI")]
    [Tooltip("���ͷ��� â�� ����ִ� UI")]
    [SerializeField] private PlayerInteractionUI _interactionInfoUI;

    #region SoSynchronization

    [Header("So Setting")]
    [Tooltip("��ųʸ��� Ű�� �� �̸��Դϴ�.")]
    public string _codeSet;
    [Tooltip("FŰ�� ����� �����Դϴ�.")]
    public bool _canInteractionSet;
    [Tooltip("�÷��̾�� ǥ�õ� �� ũ�� ǥ�� �Ǵ� �� �����Դϴ�.")]
    public bool _bigTitleSet;
    [Tooltip("���� ���� ������ ����� ���մϴ�. (�߰� ������� 0.4�� ����)")]
    public float _secondSet = 0.5f;
    [Tooltip("���ӿ� ǥ�õ� �����Դϴ�.")]
    public string _strSet;

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
        _position.x = transform.position.x + _movePosition.x; // ��ǥ �ʱ�ȭ
        _position.y = transform.position.y + _movePosition.y;

        _interactionInfoUI._interactionObjectInfo.Add(_interactionObjectInfo._code, _interactionObjectInfo);
    }

    public void ChackInteraction()
    {
        switch (_shape) // ��纰 �浹 ����
        {
            case Shape.Square:
                _enterCollision = Physics2D.OverlapBox(_position, _size, 0, _layer);
                break;

            case Shape.Circle:
                _enterCollision = Physics2D.OverlapCircle(_position, _size.x, _layer);
                break;
        }

        if (_enterCollision && !_canInteraction) // ���ͷ��� ���� (������ ����)
        {
            _intaractioner.CanInteraction(_interactionObjectInfo._code);
            _canInteraction = true;
        }
        else if (!_enterCollision && _canInteraction) // ���ͷ��� �Ұ� (�������� ���)
        {
            _intaractioner.CanNotInteraction();
            _canInteraction = false;
        }
    }

#if UNITY_EDITOR
    private void OnValidate()
    {
        switch (_shape) // �� �϶� wh�� ����ȭ
        {
            case Shape.Circle:
                _size.y = _size.x;
                break;
        }

        _interactionObjectInfo._code = _codeSet;
        _interactionObjectInfo._canInteraction = _canInteractionSet;
        _interactionObjectInfo._bigTitle = _bigTitleSet;
        _interactionObjectInfo._second = _secondSet;
        _interactionObjectInfo._str = _strSet;

        _codeSet = _interactionObjectInfo._code;
        _canInteractionSet = _interactionObjectInfo._canInteraction;
        _bigTitleSet = _interactionObjectInfo._bigTitle;
        _secondSet = _interactionObjectInfo._second;
        _strSet = _interactionObjectInfo._str;

        if (_bigTitleSet)
        {
            _canInteractionSet = false;
            _interactionObjectInfo._canInteraction = false;
        }
    }

    protected virtual void OnDrawGizmosSelected()
    {
        _position.x = transform.position.x + _movePosition.x; // ��ǥ Ȯ��
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

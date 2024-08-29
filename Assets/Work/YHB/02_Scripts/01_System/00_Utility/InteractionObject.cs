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

    #region SoSynchronization

    [Header("So Setting")]
    [Tooltip("FŰ�� ����� �����Դϴ�.")]
    [SerializeField] private bool _canInteractionSet;
    [Tooltip("������ ����� �����Դϴ�.")]
    [SerializeField] public bool _titleSet;
    [Tooltip("�÷��̾�� ǥ�õ� �� ũ�� ǥ�� �Ǵ� �� �����Դϴ�.")]
    [SerializeField] private bool _bigTitleSet;
    [Tooltip("���ӿ� ǥ�õ� �����Դϴ�.")]
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
        _position.x = transform.position.x + _movePosition.x; // ��ǥ �ʱ�ȭ
        _position.y = transform.position.y + _movePosition.y;

        PlayerInteractionUI.instance.InteractionInfoAdd(_interactionObjectInfo);
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
            PlayerInteractionUI.instance.FadeInteractionUI(_interactionObjectInfo._code);
            _canInteraction = true;
        }
        else if (!_enterCollision && _canInteraction) // ���ͷ��� �Ұ� (�������� ���)
        {
            PlayerInteractionUI.instance.OutFadeInteractionUI(_interactionObjectInfo._code);
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

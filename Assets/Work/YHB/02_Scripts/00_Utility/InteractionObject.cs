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
    [Header("Over Shape")]
    [Tooltip("��ȣ�ۿ� ���� ���")]
    [SerializeField] private Shape _shape;

    [Header("Value")]
    [Tooltip("��ġ")]
    [SerializeField] private Vector2 _movePosition;

    [Header("Value")]
    [Tooltip("�簢���� ����(���� ������) ����")]
    [SerializeField] private Vector2 _size;

    [SerializeField] private LayerMask _layer;

    private Collider2D[] _collider;
    private Vector2 _position;

    private void Awake()
    {
        
    }

    public void Initialize()
    {
        _collider = new Collider2D[1];
    }

    public void ChackInteraction()
    {
        switch (_shape)
        {
            case Shape.Square:
                Physics2D.OverlapBox(_position, _size, 0, _layer);
                break;

            case Shape.Circle:
                Physics2D.OverlapCircle(_position, _size.x, 0, _layer);
                break;
        }
    }

#if UNITY_EDITOR
    private void OnValidate()
    {
        switch (_shape)
        {
            case Shape.Circle:
                _size.y = _size.x;
                break;
        }
    }

    protected virtual void OnDrawGizmosSelected()
    {
        _position.x = transform.position.x + _movePosition.x;
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

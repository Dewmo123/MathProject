using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/InteractionObjectString")]
public class InteractionObjectInfoSo : ScriptableObject
{
    [Header("Setting")]
    [Tooltip("��ųʸ��� Ű�� �� �̸��Դϴ�.")]
    public string _code;
    [Tooltip("FŰ�� ����� �����Դϴ�.")]
    public bool _canInteraction;
    [Tooltip("�÷��̾�� ǥ�õ� �� ũ�� ǥ�� �Ǵ� �� �����Դϴ�.")]
    public bool _bigTitle;
    [Tooltip("���� ���� ������ ����� ���մϴ�. (�߰� ������� 0.4�� ����)")]
    public float _second = 0.5f;

    [Header("string")]
    [Tooltip("���ӿ� ǥ�õ� �����Դϴ�.")]
    public string _str;
}

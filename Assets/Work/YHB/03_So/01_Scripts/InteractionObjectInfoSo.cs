using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/InteractionObjectString")]
public class InteractionObjectInfoSo : ScriptableObject
{
    [Header("Setting")]
    [Tooltip("��ųʸ��� Ű�� �� �̸��Դϴ�.")]
    public string _code;

    [Header("FKeySet")]
    [Tooltip("FŰ�� ����� �����Դϴ�.")]
    public bool _canInteraction;

    [Header("Title")]
    [Tooltip("������ ����� �����Դϴ�.")]
    public bool _title;

    [Header("BigTitleSet")]
    [Tooltip("�÷��̾�� ǥ�õ� �� ũ�� ǥ�� �Ǵ� �� �����Դϴ�.")]
    public bool _bigTitle;
    [Tooltip("�ٽ� Ȱ��ȭ �ɶ� ������ �ð�")]
    public float _activationTime;

    [Header("String")]
    [Tooltip("���ӿ� ǥ�õ� �����Դϴ�.")]
    public string _str;

    public bool _canActivation = true;
}

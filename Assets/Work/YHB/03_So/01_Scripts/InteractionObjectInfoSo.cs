using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/InteractionObjectString")]
public class InteractionObjectInfoSo : ScriptableObject
{
    [Header("Setting")]
    [Tooltip("딕셔너리의 키로 들어갈 이름입니다.")]
    public string _code;

    [Header("FKeySet")]
    [Tooltip("F키를 띄울지 여부입니다.")]
    public bool _canInteraction;

    [Header("Title")]
    [Tooltip("제목을 띄울지 여부입니다.")]
    public bool _title;

    [Header("BigTitleSet")]
    [Tooltip("플레이어에게 표시딜 때 크게 표시 되는 지 여부입니다.")]
    public bool _bigTitle;
    [Tooltip("다시 활성화 될때 까지의 시간")]
    public float _activationTime;

    [Header("String")]
    [Tooltip("게임에 표시될 문장입니다.")]
    public string _str;

    public bool _canActivation = true;
}

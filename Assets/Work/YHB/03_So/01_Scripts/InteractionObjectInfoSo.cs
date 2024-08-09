using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/InteractionObjectString")]
public class InteractionObjectInfoSo : ScriptableObject
{
    [Header("Setting")]
    [Tooltip("딕셔너리의 키로 들어갈 이름입니다.")]
    public string _code;
    [Tooltip("F키를 띄울지 여부입니다.")]
    public bool _canInteraction;
    [Tooltip("플레이어에게 표시딜 때 크게 표시 되는 지 여부입니다.")]
    public bool _bigTitle;
    [Tooltip("몇초 동안 문장을 띄울지 정합니다. (뜨고 사라지는 0.4초 제외)")]
    public float _second = 0.5f;

    [Header("string")]
    [Tooltip("게임에 표시될 문장입니다.")]
    public string _str;
}

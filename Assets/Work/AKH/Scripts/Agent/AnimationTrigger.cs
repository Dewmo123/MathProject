using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationTrigger : MonoBehaviour
{
    [SerializeField] private Agent _char;
    public void EndTriggerCall()
    {
        _char.EndTriggerCall();
    }
}

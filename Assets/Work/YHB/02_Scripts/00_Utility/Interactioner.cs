using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Interactioner : MonoBehaviour
{
    [SerializeField] private PlayerInteractionUI _playerInteractionUI;

    public void CanInteraction(string code)
    {
        _playerInteractionUI.FadeInteractionUI(code);
    }

    public void CanNotInteraction()
    {
        _playerInteractionUI.OutFadeInteractionUI();
    }
}

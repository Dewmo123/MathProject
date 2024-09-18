using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class StageLocker : MonoBehaviour
{
    [SerializeField] private LayerMask _layer;
    private bool _canInteraction;

    private void HandleInteraction(InputAction.CallbackContext context)
    {
        if (context.performed && _canInteraction && InteractionManager.instance.InteractionUIDic[UIType.Problem].MoveCnt == 0 && !GameManager.instance.isInteractionUI)
        {
            InteractionManager.instance.InteractionUIDic[UIType.Problem].IncreaseCnt();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == _layer)
        {
            _canInteraction = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.layer == _layer)
        {
            _canInteraction = false;
        }
    }
}

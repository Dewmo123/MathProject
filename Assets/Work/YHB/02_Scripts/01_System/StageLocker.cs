using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Tilemaps;

public class StageLocker : MonoBehaviour
{
    [SerializeField] private LayerMask _layer;
    private TilemapCollider2D _tilemapCollider;
    private bool _canInteraction = false;
    private bool _canStageLock = false;

    private void Awake()
    {
        _tilemapCollider = GetComponent<TilemapCollider2D>();
    }

    private void Start()
    {
        GameManager.instance.Player.playerInput.Input.Interaction.performed += HandleInteraction;

    }

    private void HandleInteraction(InputAction.CallbackContext context)
    {
        if (context.performed && _canInteraction && InteractionManager.instance.InteractionUIDic[UIType.Problem].MoveCnt == 0 && !GameManager.instance.isInteractionUI)
        {
            ProblemPass(true);
            InteractionManager.instance.InteractionUIDic[UIType.Problem].IncreaseCnt();
        }
    }

    private void ProblemPass(bool pass)
    {
        _tilemapCollider.isTrigger = pass ? pass : _canStageLock == pass;
    }

    private IEnumerator ProblemPass()
    {
        yield return new WaitForSeconds(3);
        _canStageLock = true;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (1 << collision.gameObject.layer == _layer)
        {
            _canInteraction = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (1 << collision.gameObject.layer == _layer)
        {
            _canInteraction = false;
            ProblemPass();
        }
    }
}

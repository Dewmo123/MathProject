using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Tilemaps;

public class StageLocker : MonoBehaviour
{
    [SerializeField] private LayerMask _layer;
    [SerializeField] private InteractionObjectInfoSo _StageLockerSo;
    private TilemapCollider2D _tilemapCollider;
    private bool _canInteraction = false;
    private bool _canStageLock = false;

    private void Awake()
    {
        _tilemapCollider = GetComponent<TilemapCollider2D>();
        InteractionInfoAdd(_StageLockerSo);
    }

    private void Start()
    {
        GameManager.instance.Player.playerInput.Input.Interaction.performed += HandleInteraction;

    }

    public void InteractionInfoAdd(InteractionObjectInfoSo interObj)
    {
        foreach (InteractionObjectInfoSo item in InteractionManager.instance._interactionObjectInfo.Values)
        {
            if (item == interObj) return;
        }

        InteractionManager.instance._interactionObjectInfo.Add(interObj._code, interObj);
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
            InteractionManager.instance.FadeInteractionUI(_StageLockerSo._code);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (1 << collision.gameObject.layer == _layer)
        {
            _canInteraction = false;
            InteractionManager.instance.OutFadeInteractionUI(_StageLockerSo._code);
            ProblemPass();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        
    }
}

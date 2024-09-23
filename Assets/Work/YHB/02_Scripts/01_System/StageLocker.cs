using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Tilemaps;

public class StageLocker : MonoBehaviour
{
    [SerializeField] private InteractionObjectInfoSo _stageLocker;
    [SerializeField] private LayerMask _layer;
    [SerializeField] private bool _horizon;

    private Collider2D _col;
    private QuestionUI _question;
    private bool _canInteraction;
    private Vector3 _playerPos;

    private void Awake()
    {
        _col = GetComponent<TilemapCollider2D>();
    }

    private void Start()
    {
        StartCoroutine(WaitInteractionManager());
    }

    private IEnumerator WaitInteractionManager()
    {
        yield return null;
        GameManager.instance.Player.playerInput.Input.Interaction.performed += HandleInteraction;
        _question = InteractionManager.instance.InteractionUIDic[UIType.Problem] as QuestionUI;
        _question.Solved += HandleProblemResult;

        InteractionManager.instance.InteractionInfoAdd(_stageLocker);
    }

    private void HandleInteraction(InputAction.CallbackContext context)
    {
        if (context.performed && _canInteraction && InteractionManager.instance.InteractionUIDic[UIType.Problem].MoveCnt == 0 && !GameManager.instance.isInteractionUI)
        {
            _question.IncreaseCnt();
            _question.Set((DifficultEnum)Random.Range(0,3));
        }
    }

    private void HandleProblemResult(bool pass)
    {
        _col.isTrigger = pass;
        InteractionManager.instance.OutFadeInteractionUI(_stageLocker._code);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (1 << collision.gameObject.layer == _layer)
        {
            _playerPos = collision.transform.position;
            Debug.Log(_playerPos.x);
            _canInteraction = true;
            InteractionManager.instance.FadeInteractionUI(_stageLocker._code);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (1 << collision.gameObject.layer == _layer)
        {
            _canInteraction = false;
            InteractionManager.instance.OutFadeInteractionUI(_stageLocker._code);
        }
    }


    private void OnTriggerExit2D(Collider2D collision)
    {
        if (1 << collision.gameObject.layer == _layer)
        {
            if (_horizon && _playerPos.y <= collision.transform.position.y)
            {
                _col.isTrigger = false;
            }
        }
    }
}
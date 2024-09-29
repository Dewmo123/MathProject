using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Tilemaps;

public class StageLocker : MonoBehaviour
{
    [SerializeField] private InteractionObjectInfoSo _stageLocker;
    [SerializeField] private LayerMask _layer;
    [Tooltip("upOpen을 키면 아래에서 위로 올라가 통과하면 닫칩니다. 좌우의 경우 왼쪽에서 오른쪽으로 통과 할 때 입니다.")]
    [SerializeField] private bool _upOpen;

    [SerializeField] private Color _onColor;
    [SerializeField] private Color _offColor;

    private Tilemap _tilemap;
    private Collider2D _col;
    private QuestionUI _question;
    private bool _canInteraction;
    private Vector3 _playerPos;

    private void Awake()
    {
        _col = transform.GetComponent<TilemapCollider2D>();
        _tilemap = transform.GetComponent<Tilemap>();
        _tilemap.color = _onColor;
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
        if (pass)
        {
            _tilemap.color = _offColor;
        }
        _col.isTrigger = pass;
        _canInteraction = false;
        InteractionManager.instance.OutFadeInteractionUI(_stageLocker._code);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (1 << collision.gameObject.layer == _layer)
        {
            _question.Solved += HandleProblemResult;

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
            if (_upOpen ? _playerPos.y <= collision.transform.position.y : _playerPos.y >= collision.transform.position.y)
            {
                _tilemap.color = _onColor;
                _col.isTrigger = false;
            }
        }
    }
}
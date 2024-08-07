using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Character : MonoBehaviour
{
    public Animator animatorCompo;
    public AgentMovement movementCompo;

    public bool isDead = false;
    protected virtual void Awake()
    {
        animatorCompo = transform.Find("Visual").GetComponent<Animator>();
        movementCompo = GetComponent <AgentMovement>();
        movementCompo.Initialize(this);
    }
    #region Flip Character
    public bool IsFacingRight()
    {
        return Mathf.Approximately(transform.eulerAngles.y, 0);
    }

    public virtual void HandleSpriteFlip(Vector2 targetPosition)
    {
        bool isRight = IsFacingRight();
        if (targetPosition.x < transform.position.x && isRight)
        {
            transform.eulerAngles = new Vector3(0, -180f, 0);
        }
        else if (targetPosition.x > transform.position.x && !isRight)
        {
            transform.eulerAngles = Vector3.zero;
        }
    }
    #endregion
}

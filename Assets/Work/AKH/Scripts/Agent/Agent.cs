using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Agent : MonoBehaviour
{
    public Animator animatorCompo { get; private set; }
    public AgentMovement movementCompo { get; private set; }
    public Health healthCompo { get; private set; }
    public bool isStop = false;

    public bool isDead { get; protected set; }
    protected virtual void Awake()
    {
        healthCompo = GetComponent<Health>();
        animatorCompo = transform.Find("Visual").GetComponent<Animator>();
        movementCompo = GetComponent <AgentMovement>();
        healthCompo.Initialize(this);
        movementCompo.Initialize(this);
    }
    public virtual void SetDead(bool value)
    {
        isDead = value;
    }
    public virtual void Attack()
    {

    }
    public abstract void EndTriggerCall();
    #region Flip Character
    public bool IsFacingRight()
    {
        return Mathf.Approximately(transform.eulerAngles.y, 0);
    }

    public virtual void HandleSpriteFlip(Vector2 targetPosition)
    {
        if (isStop) return;
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

using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AgentMovement : MonoBehaviour
{
    public Agent character;
    public Rigidbody2D rbCompo;
    public Vector2 vector2 { get; private set; }
    public void Initialize(Agent agent)
    {
        character = agent;
        rbCompo = GetComponent<Rigidbody2D>();
    }
    public void FixedUpdate()
    {
        rbCompo.velocity = vector2;
    }
    public void SetMovement(Vector2 vector2)
    {
        this.vector2 = vector2;
    }
    public void StopImmediately()
    {
        vector2 = Vector2.zero;
        rbCompo.velocity = vector2;
    }
}

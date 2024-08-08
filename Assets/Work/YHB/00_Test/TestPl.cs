using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestPl : MonoBehaviour
{
    private Vector2 _dir;
    private Rigidbody2D _rid;

    private void Awake()
    {
        _rid = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        _dir = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

        _rid.velocity = _dir * 5;
    }
}

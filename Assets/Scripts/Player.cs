﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class Player : MonoBehaviour
{
    [SerializeField] GameObject projectilePrefab;

    public float speed = 0.5f;
    public Rigidbody2D rig_body;
    public BoxCollider2D collider_2d;
    public Vector3 move_delta;
    public RaycastHit2D hit;
    Animator myAnimator;
    public bool facing_right;

    private void Start()
    {
        myAnimator = GetComponent<Animator>();
        collider_2d = GetComponent<BoxCollider2D>();
        rig_body = GetComponent<Rigidbody2D>();
        facing_right = true;
    }

    private void Update()
    {
        move_delta = new Vector3(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"), 0);
    }

    void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        rig_body.MovePosition(transform.position + (move_delta * speed * Time.deltaTime));
        if (move_delta.x > 0 && !facing_right)
        {
            Flip();
        }
        else if (move_delta.x < 0 && facing_right)
        {
            Flip();
        }
    }

    private void Flip()
    {
        transform.Rotate(0.0f, 180.0f, 0.0f);
        facing_right = !facing_right;
    }
}

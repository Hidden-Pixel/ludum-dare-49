﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class Player : MonoBehaviour
{
    [SerializeField] GameObject projectilePrefab;

    public BoxCollider2D collider_2d;
    public Vector3 move_delta;
    public RaycastHit2D hit;

    private void Start()
    {
        collider_2d = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Move();
    }

    private void Update()
    {
        Fire();
    }

    private void Move()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");
        move_delta = new Vector3(x, y, 0);

        if (move_delta.x > 0)
        {
            transform.localScale = Vector3.one;
        }
        else if (move_delta.x < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
        hit = Physics2D.BoxCast(transform.position, collider_2d.size, 0,
                                new Vector2(0, move_delta.y), Mathf.Abs(move_delta.y * Time.deltaTime),
                                LayerMask.GetMask("Actor", "Blocking"));
        if (hit.collider == null)
        {
            transform.Translate(0, move_delta.y * Time.deltaTime, 0);
        }
        else
        {
            Debug.Log("hit collider is not null y axis");
        }
        hit = Physics2D.BoxCast(transform.position, collider_2d.size, 0,
                                new Vector2(move_delta.x, 0), Mathf.Abs(move_delta.x * Time.deltaTime),
                                LayerMask.GetMask("Actor", "Blocking"));
        if (hit.collider == null) 
        {
            transform.Translate(move_delta.x * Time.deltaTime, 0, 0);
        }
        else
        {
            Debug.Log("hit collider is not null x axis");
        }
    }

    private void Fire()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Debug.Log("Fire!");

            // Get the direction that the player is pointed in.
            //Debug.Log("LocalScale:" + transform.localScale);


            // TODO: Figure out how to point the arrow.
            GameObject projectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity) as GameObject;

            var velocity = projectile.GetComponent<Projectile>().GetVelocity();

            var x = transform.localScale.x;
            var y = 0;

            var angle = Mathf.Atan2(y, x) * Mathf.Rad2Deg;

            projectile.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            projectile.GetComponent<Rigidbody2D>().velocity = new Vector2(x * velocity, 0);
        }
    }
}

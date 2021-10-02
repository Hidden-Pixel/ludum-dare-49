using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class Player : MonoBehaviour
{
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
}

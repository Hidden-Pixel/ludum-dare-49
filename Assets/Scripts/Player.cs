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
        transform.Translate(move_delta.x * Time.deltaTime, move_delta.y * Time.deltaTime, 0);
    }
}

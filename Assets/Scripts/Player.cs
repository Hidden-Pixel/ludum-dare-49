using System.Collections;
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

    private void Start()
    {
        myAnimator = GetComponent<Animator>();
        collider_2d = GetComponent<BoxCollider2D>();
        rig_body = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        move_delta = new Vector3(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"), 0);
        Fire();
    }

    void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        rig_body.MovePosition(transform.position + (move_delta * speed * Time.deltaTime));
        if (move_delta.x > 0)
        {
            transform.localScale = Vector3.one;
        }
        else if (move_delta.x < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
    }

    private void Fire()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Vector3 pos = transform.position;
            Quaternion rot = Quaternion.AngleAxis(transform.localScale.x * -90.0f, Vector3.forward);
            Instantiate(projectilePrefab, pos, rot);
            /*
            var velocity = projectile.GetComponent<Projectile>().GetVelocity();
            // Set the orientation of the arrow based on user face
            var x = transform.localScale.x;
            projectile.transform.rotation = ;
            projectile.GetComponent<Rigidbody2D>().velocity = new Vector2(x * velocity, 0);
            */
        }
    }
}

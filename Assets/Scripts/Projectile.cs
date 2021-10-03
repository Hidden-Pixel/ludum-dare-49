using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] int damage = 100;
    [SerializeField] float velocity = 1.0f;
    public Rigidbody2D rig_body;

    public int GetDamage() { return damage; }

    public float GetVelocity() { return velocity; }

    private void Start ()
    {
        LayerMask.NameToLayer("Actor");
        rig_body = GetComponent<Rigidbody2D>();
        rig_body.velocity = new Vector2(transform.localScale.x * velocity, 0);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        int current_layer = collision.gameObject.layer;
        int actor = LayerMask.NameToLayer("Actor");
        int blocking = LayerMask.NameToLayer("Blocking");
        if (current_layer == blocking)
        {
            Hit();
        }
    }

    private void Update()
    {

    }

    private void FixedUpdate()
    {
        
    }

    public void Hit()
    {
        Destroy(gameObject);
    }
}

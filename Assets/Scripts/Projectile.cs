using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] int damage = 100;
    [SerializeField] float speed = 1.0f;
    public Rigidbody2D rig_body;

    public int GetDamage() { return damage; }

    public float GetVelocity() { return speed; }

    private void Start() 
    {
        rig_body = GetComponent<Rigidbody2D>();
    }

    public void SetDirection()
    {
        rig_body.velocity = transform.right * speed;
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

    public void Hit()
    {
        Destroy(gameObject);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] int damage = 100;
    [SerializeField] float velocity = 1f;

    public int GetDamage() { return damage; }

    public float GetVelocity() { return velocity; }

    public void Hit()
    {
        Destroy(gameObject);
    }
}

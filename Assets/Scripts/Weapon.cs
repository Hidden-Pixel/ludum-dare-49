using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public Transform fire_point;
    public GameObject projectile;

    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }
    }

    void Shoot()
    {
        GameObject g = Instantiate(projectile, fire_point.position, fire_point.rotation);
        Projectile p = g.GetComponent<Projectile>();
        p.SetDirection();
    }
}
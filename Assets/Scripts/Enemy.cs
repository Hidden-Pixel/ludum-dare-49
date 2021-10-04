using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    // configuration
    [SerializeField] int health = 500;
    [SerializeField] Transform target;
    [SerializeField] float chaseRange = 0.5f;
    [SerializeField] float attackDistance = 0.1f;
    [SerializeField] float moveSpeed = 0.5f;

    // initialized variables
    Rigidbody2D myRigidBody;
    Animator myAnimator;
    Trigger2DProxy hitCollider;

    bool isProvoked = false;
    bool hit_wall = false;
    float distanceToTarget = Mathf.Infinity;

    // Start is called before the first frame update
    void Start()
    {
        GameObject player = GameObject.Find("Player");
        if (player == null)
        {
            Debug.Log("could not find player game object");
        }
        else
        {
            Debug.Log("found player game object: " + player);
            target = player.transform;
        }
        myAnimator = GetComponent<Animator>();
        myRigidBody = GetComponent<Rigidbody2D>();
    }

    // Handle collisions related to projectiles.
    private void HitCollider_OnTriggerEnter2D(Collider2D collision)
    {

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector2 targetPos = target.position;
        distanceToTarget = Vector2.Distance(targetPos, transform.position);

        if (isProvoked)
        {
            EngageTarget();
        }
        else if (distanceToTarget <= chaseRange)
        {
            Debug.Log("Enemy is provoked!");

            isProvoked = true;


        }
        else
        {
            // set enemy back to starting state
            isProvoked = false;

            // transition back to idle state
            myAnimator.SetBool("Running", false);
        }
    }

    private void ChaseTarget()
    {
        if (hit_wall == false)
        {
            if (isProvoked)
            {
                Vector3 dir = (target.position - transform.position).normalized;
                myRigidBody.MovePosition(transform.position + dir * moveSpeed * Time.deltaTime);
            }
        }
    }

    private void EngageTarget()
    {
        if (distanceToTarget > attackDistance)
        {
            // Start run animation
            myAnimator.SetBool("Running", true);

            ChaseTarget();
        }

        if (distanceToTarget <= attackDistance)
        {
            AttackTarget();
        }
    }

    private void AttackTarget()
    {
        // Debug.Log("ATTACKING TARGET");
    }

    // Draws the lines in Unity editor so we can see detection and attack radius in scene
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, chaseRange);
        Gizmos.DrawWireSphere(transform.position, attackDistance);
    }

    private void CheckBlocking(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Blocking"))
        {
            Debug.Log("hit the wall");
            hit_wall = true;
        }
        else
        {
            Debug.Log("not hitting the wall - type: " + collision.gameObject.name);
            hit_wall = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        CheckBlocking(collision);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Projectile")
        {
            Projectile projectile = collision.gameObject.GetComponent<Projectile>();
            projectile.Hit();

            health -= projectile.GetDamage();
            if (health <= 0)
            {
                Destroy(gameObject);
            }
        }
    }
}

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
    float distanceToTarget = Mathf.Infinity;

    // Start is called before the first frame update
    void Start()
    {
        myAnimator = GetComponent<Animator>();
        myRigidBody = GetComponent<Rigidbody2D>();

        // Subscribe to collision events. (Probably not the best way, but it works...)
        // https://answers.unity.com/questions/188775/having-more-than-one-collider-in-a-gameobject.html
        hitCollider = transform.Find("HitCollider").GetComponent<Trigger2DProxy>();
        hitCollider.OnCollisionTrigger2D_Action += HitCollider_OnTriggerEnter2D;
    }

    // Handle collisions related to projectiles.
    private void HitCollider_OnTriggerEnter2D(Collider2D collision)
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

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector2 targetPos = target.position;
        distanceToTarget = Vector2.Distance(targetPos, transform.position);

        if (isProvoked)
        {
            EngageTarget(targetPos);
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

    private void ChaseTarget(Vector2 targetPos)
    {
        // Move enemy towards player
        myRigidBody.MovePosition(targetPos * moveSpeed * Time.fixedDeltaTime);
    }

    private void EngageTarget(Vector2 targetPos)
    {
        if (distanceToTarget > attackDistance)
        {
            // Start run animation
            myAnimator.SetBool("Running", true);

            ChaseTarget(targetPos);
        }

        if (distanceToTarget <= attackDistance)
        {
            AttackTarget();
        }
    }

    private void AttackTarget()
    {
        Debug.Log("ATTACKING TARGET");
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, chaseRange);
        Gizmos.DrawWireSphere(transform.position, attackDistance);
    }

    /*    private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.tag == "Player")
            {
                // player is in detection radius
                lastDetectedPlayerPos = collision.transform;


            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            // if the player left detection radius and the enemy was chasing the player, stop the enemy
            if (collision.gameObject.tag == "Player")
            {
                lastDetectedPlayerPos = null;
            }
        }

        private void OnTriggerStay2D(Collider2D collision)
        {
            // if player remains in radius, move towards player
            if (collision.gameObject.tag == "Player")
            {
                lastDetectedPlayerPos = collision.transform;
            }
        }*/
}

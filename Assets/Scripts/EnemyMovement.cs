using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] int health = 500;
    [SerializeField] float moveSpeed = 0.5f;
    [SerializeField] GameObject player;

    Rigidbody2D myRigidBody;
    Animator myAnimator;
    CircleCollider2D detectionRadius;
    BoxCollider2D playerCollider;
    Trigger2DProxy hitCollider;

    bool isDetectingPlayer = false;
    Transform lastDetectedPlayerPos;

    // Start is called before the first frame update
    void Start()
    {
        myAnimator = GetComponent<Animator>();
        myRigidBody = GetComponent<Rigidbody2D>();
        detectionRadius = GetComponent<CircleCollider2D>();
        playerCollider = player.GetComponent<BoxCollider2D>();

        // Subscribe to collision events. (Probably not the best way, but it works...)
        // https://answers.unity.com/questions/188775/having-more-than-one-collider-in-a-gameobject.html
        hitCollider = transform.Find("HitCollider").GetComponent<Trigger2DProxy>();
        hitCollider.OnCollisionTrigger2D_Action += HitCollider_OnTriggerEnter2D;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (isDetectingPlayer == true && lastDetectedPlayerPos != null)
        {
            Debug.Log("Player is in radius");

            // Move enemy towards player
            myRigidBody.MovePosition(player.transform.position * moveSpeed * Time.fixedDeltaTime);
        }
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            // player is in detection radius
            isDetectingPlayer = true;
            lastDetectedPlayerPos = collision.transform;

            // Start run animation
            myAnimator.SetBool("Running", true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        // if the player left detection radius and the enemy was chasing the player, stop the enemy
        if (collision.gameObject.tag == "Player" && isDetectingPlayer == true)
        {
            isDetectingPlayer = false;
            lastDetectedPlayerPos = null;

            // transition back to idle state
            myAnimator.SetBool("Running", false);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        // if player remains in radius, move towards player
        if (collision.gameObject.tag == "Player" && isDetectingPlayer == true)
        {
            lastDetectedPlayerPos = collision.transform;
        }
    }
}

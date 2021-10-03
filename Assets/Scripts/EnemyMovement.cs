using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public int health = 500;
    public float moveSpeed = 0.25f;
    [SerializeField] GameObject player;

    public Vector3 movement;
    Rigidbody2D myRigidBody;
    Animator myAnimator;
    CircleCollider2D detectionRadius;
    BoxCollider2D playerCollider;
    Trigger2DProxy hitCollider;

    bool isDetectingPlayer = false;
    bool hit_wall = false;
    Transform lastDetectedPlayerPos;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        if (player == null)
        {
            Debug.Log("could not find player game object");
        }
        else
        {
            Debug.Log("found player game object: " + player);
        }
        myAnimator = GetComponent<Animator>();
        myRigidBody = GetComponent<Rigidbody2D>();
        detectionRadius = GetComponent<CircleCollider2D>();
        playerCollider = player.GetComponent<BoxCollider2D>();

        // Subscribe to collision events. (Probably not the best way, but it works...)
        // https://answers.unity.com/questions/188775/having-more-than-one-collider-in-a-gameobject.html
        //hitCollider = transform.Find("HitCollider").GetComponent<Trigger2DProxy>();
        //hitCollider.OnCollisionTrigger2D_Action += HitCollider_OnTriggerEnter2D;
    }

    void Update() 
    {

    }

    void FixedUpdate()
    {
        if (hit_wall == false)
        {
            if (isDetectingPlayer == true && lastDetectedPlayerPos != null)
            {
                Vector3 dir = (player.transform.position - transform.position).normalized;
                myRigidBody.MovePosition(transform.position + dir * moveSpeed * Time.deltaTime);
            }
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
        CheckBlocking(collision);
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
        CheckBlocking(collision);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        // if player remains in radius, move towards player
        if (collision.gameObject.tag == "Player" && isDetectingPlayer == true)
        {
            lastDetectedPlayerPos = collision.transform;
        }
        CheckBlocking(collision);
    }

    private void CheckBlocking(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Blocking"))
        {
            Debug.Log("hit the wall");
            hit_wall = true;
        }
        else 
        {
            Debug.Log("not hitting the wall - type: " + collision.GetComponent<Collider>().GetType());
            hit_wall = false;
        }
    }
}

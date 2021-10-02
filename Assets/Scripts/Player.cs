using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class Player : MonoBehaviour
{
    // Configuration
    [SerializeField] float runSpeed = 5f;
    [SerializeField] float jumpSpeed = 5f;
    [SerializeField] float climbSpeed = 5f;

    // State
    bool isAlive = true;

    // Cached component references
    Rigidbody2D myRigidBody;
    Animator myAnimator;
    CapsuleCollider2D myBodyCollider;
    BoxCollider2D myFeet;
    float gravityScaleAtStart;

    // Start is called before the first frame update
    void Start()
    {
        myRigidBody = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        myBodyCollider = GetComponent<CapsuleCollider2D>();
        myFeet = GetComponent<BoxCollider2D>();
        gravityScaleAtStart = myRigidBody.gravityScale;
    }

    // Update is called once per frame
    void Update()
    {
        ProcessInput();
    }

    private void ProcessInput()
    {
        float controlThrow = CrossPlatformInputManager.GetAxis("Horizontal"); // value is between -1 to +1
        float verticalThrow = CrossPlatformInputManager.GetAxis("Vertical"); // value is between -1 to +1
        bool playerHasHorizontalSpeed = Mathf.Abs(myRigidBody.velocity.x) > Mathf.Epsilon;
        bool isJumping = CrossPlatformInputManager.GetButtonDown("Jump");
        bool isTouchingGround = myFeet.IsTouchingLayers(LayerMask.GetMask("Ground"));
        bool isTouchingLadder = myFeet.IsTouchingLayers(LayerMask.GetMask("Traversal Objects"));

        ClimbLadder(verticalThrow, isTouchingLadder, isTouchingGround);

        // Make player run
        Run(controlThrow, playerHasHorizontalSpeed);

        // Flip player sprite based on velocity X
        if (playerHasHorizontalSpeed)
        {
            FlipSprite();
        }

        // Jump player if jump is pressed
        if (isJumping && isTouchingGround)
        {
            Jump();
        }


    }

    private void Run(float controlThrow, bool playerHasHorizontalSpeed)
    {
        
        Vector2 playerVelocity = new Vector2(controlThrow * runSpeed, myRigidBody.velocity.y);
        myRigidBody.velocity = playerVelocity;
        myAnimator.SetBool("Running", playerHasHorizontalSpeed);
    }

    private void Jump()
    {
        Vector2 jumpVelocityToAdd = new Vector2(0f, jumpSpeed);
        myRigidBody.velocity += jumpVelocityToAdd;
    }

    private void FlipSprite()
    {
        transform.localScale = new Vector2 (Mathf.Sign(myRigidBody.velocity.x), 1f);
    }

    private void ClimbLadder(float verticalThrow, bool isTouchingLadder, bool isTouchingGround)
    {
        bool isClimbing = myAnimator.GetBool("Climbing");

        if (!isTouchingLadder)
        {
            myRigidBody.gravityScale = gravityScaleAtStart;

            if (isClimbing)
            {
                myAnimator.SetBool("Climbing", false);
            }
            return;
        }

        // if you are touching ground and touching a ladder and holding up, you set Climbing to true

        // if you aren't touching ground and touching a ladder and holding up, you climb up
        // if you aren't touching ground and touching a ladder and holding down, you climb down

        // if you are touching ground and touching a ladder and holding down, you set Climbing to false
        if (isClimbing && isTouchingGround && verticalThrow < Mathf.Epsilon)
        {
            myAnimator.SetBool("Climbing", false);
            return;
        }

        if (!isClimbing && isTouchingGround && verticalThrow > Mathf.Epsilon)
        {
            myAnimator.SetBool("Climbing", true);
            return;
        }

        if (isClimbing)
        {
            Vector2 climbVelocity = new Vector2(myRigidBody.velocity.x, verticalThrow * climbSpeed);
            myRigidBody.velocity = climbVelocity;
            myRigidBody.gravityScale = 0f;
        }
    }
}

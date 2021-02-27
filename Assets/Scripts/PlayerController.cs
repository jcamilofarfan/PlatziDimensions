using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Config 
    [SerializeField] float runSpeed = 5f;

    // State
    bool isAlive = true;

    // Cached component references
    Rigidbody2D myRigidBody;
    Animator myAnimator;
    SpriteRenderer mySprite;

    // String const
    private const string horizontal = "Horizontal";
    private const string vertical = "Vertical";
    private const string lastHorizontal = "LastHorizontal";
    private const string lastVertical = "LastVertical";
    private const string walkingState = "isWalking";
    private const string attackingState = "isAttacking";

    // Start is called before the first frame update
    void Start()
    {
        myRigidBody = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        mySprite = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!isAlive) { return; }
        Move();
    }

    private void Move()
    {
        float xDirection = Input.GetAxis(horizontal);
        float yDirection = Input.GetAxis(vertical);
        Vector2 playerVelocity = new Vector2(xDirection*runSpeed, yDirection*runSpeed);
        myRigidBody.velocity = playerVelocity;

        bool playerIsWalking = Mathf.Abs(myRigidBody.velocity.x) > Mathf.Epsilon || Mathf.Abs(myRigidBody.velocity.y) > Mathf.Epsilon;
        myAnimator.SetBool(walkingState, playerIsWalking);

        if (xDirection < 0)
            mySprite.flipX = true;
        
        if (xDirection > 0)
            mySprite.flipX = false;
    }
}

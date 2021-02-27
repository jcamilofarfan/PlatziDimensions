using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcherController : MonoBehaviour
{
    // Config
    [SerializeField] Camera cameraX;
    [SerializeField] float runSpeed = 5f;
    [SerializeField] Transform aim;
    [SerializeField] GameObject arrow;
    [SerializeField] float attackSpeed = 1;

    // State
    bool isAlive = true;

    // Cached component references
    Rigidbody2D myRigidBody;
    Animator myAnimator;
    Animation attackAnimation;
    SpriteRenderer mySprite;

    // String const
    private const string horizontal = "Horizontal";
    private const string vertical = "Vertical";
    private const string lastHorizontal = "LastHorizontal";
    private const string lastVertical = "LastVertical";
    private const string walkingState = "isWalking";
    private const string attackingState = "isAttacking";

    // Initialize variables
    float xDirection;
    float yDirection;
    Vector2 playerVelocity;
    Vector2 facingDirection;
    bool playerIsWalking;
    bool arrowReady = true;
    GameObject arrowShot;

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
        Aim();
        Move();
    }

    private void Move()
    {
        xDirection = Input.GetAxis(horizontal);
        yDirection = Input.GetAxis(vertical);
        playerVelocity = new Vector2(xDirection * runSpeed, yDirection * runSpeed);
        myRigidBody.velocity = playerVelocity;

        playerIsWalking = Mathf.Abs(myRigidBody.velocity.x) > Mathf.Epsilon || Mathf.Abs(myRigidBody.velocity.y) > Mathf.Epsilon;
        myAnimator.SetBool(walkingState, playerIsWalking);

        if (xDirection < 0)
            mySprite.flipX = true;

        if (xDirection > 0)
            mySprite.flipX = false;

        if (playerIsWalking)
        {
            myAnimator.SetBool(attackingState, false);
        }
        else
            Invoke("PrepareArrow", 2f);
    }

    private void Aim()
    {
        facingDirection = cameraX.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        aim.position = transform.position + (Vector3)facingDirection.normalized * 2.5f;

        if (facingDirection.x < 0)
        {
            mySprite.flipX = true;
        }
        if (facingDirection.x > 0)
        {
            mySprite.flipX = false;
        }

        if (Input.GetMouseButton(0) && arrowReady)
        {
            myAnimator.SetBool(attackingState, true);
        }
    }

    public void Fire()
    {
        arrowReady = false;
        float angle = Mathf.Atan2(facingDirection.y, facingDirection.x) * Mathf.Rad2Deg;
        Quaternion targetRotation = Quaternion.AngleAxis(angle, Vector3.forward);
        arrowShot = Instantiate(arrow, transform.position, targetRotation);
        arrowShot.transform.parent = this.transform;
        myAnimator.SetBool(attackingState, false);
    }

    public void PrepareArrow()
    {
        arrowReady = true;
    }

    public void IncreaseAttackSpeed()
    {

    }
}

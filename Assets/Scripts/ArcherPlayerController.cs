using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcherPlayerController : MonoBehaviour
{
    // Config
    [SerializeField] Camera cameraX;
    [SerializeField] float runSpeed = 5f;
    [SerializeField] Transform aim;
    [SerializeField] GameObject arrow;
    [Range(0f, 1f)] [SerializeField] float attackSpeed;

    // State
    public bool isAlive = true;

    // Cached component references
    Rigidbody2D myRigidBody;
    Animator myAnimator;
    SpriteRenderer mySprite;
    CapsuleCollider2D myCollider;

    // String const
    private const string HORIZONTAL = "Horizontal";
    private const string VERTICAL = "Vertical";
    private const string WALKING_STATE = "isWalking";
    private const string ATTACKING_STATE = "isAttacking";
    private const string ATTACKING_SPEED = "AttackSpeed";
    private const string IS_ALIVE = "isAlive";

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
        myCollider = GetComponent<CapsuleCollider2D>();
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
        xDirection = Input.GetAxis(HORIZONTAL);
        yDirection = Input.GetAxis(VERTICAL);
        playerVelocity = new Vector2(xDirection * runSpeed, yDirection * runSpeed);
        myRigidBody.velocity = playerVelocity;

        playerIsWalking = Mathf.Abs(myRigidBody.velocity.x) > Mathf.Epsilon || Mathf.Abs(myRigidBody.velocity.y) > Mathf.Epsilon;
        myAnimator.SetBool(WALKING_STATE, playerIsWalking);

        if (xDirection < 0)
        {
            mySprite.flipX = true;
            myCollider.offset = new Vector2(0.3f, -0.4f);
        }

        if (xDirection > 0)
        {
            mySprite.flipX = false;
            myCollider.offset = new Vector2(-0.3f, -0.4f);
        }

        if (playerIsWalking)
        {
            myAnimator.SetBool(ATTACKING_STATE, false);
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
            myCollider.offset = new Vector2(0.3f, -0.4f);
        }
        if (facingDirection.x > 0)
        {
            mySprite.flipX = false;
            myCollider.offset = new Vector2(-0.3f, -0.4f);
        }

        if (Input.GetMouseButton(0) && arrowReady)
        {
            myAnimator.SetBool(ATTACKING_STATE, true);
        }
    }

    public void Fire()
    {
        arrowReady = false;
        float angle = Mathf.Atan2(facingDirection.y, facingDirection.x) * Mathf.Rad2Deg;
        Quaternion targetRotation = Quaternion.AngleAxis(angle, Vector3.forward);
        arrowShot = Instantiate(arrow, transform.position, targetRotation);
        arrowShot.transform.parent = this.transform;
        myAnimator.SetBool(ATTACKING_STATE, false);
    }

    public void PrepareArrow()
    {
        arrowReady = true;
    }

    public void IncreaseAttackSpeed()
    {
        if (attackSpeed < 1)
            attackSpeed += 0.1f;
        myAnimator.SetFloat(ATTACKING_SPEED, attackSpeed);
    }

    public void PlayerDied()
    {
        mySprite.enabled = false;
        myCollider.enabled = false;
        Destroy(gameObject);
    }

    public void PlayerFalling()
    {
        isAlive = false;
    }
}
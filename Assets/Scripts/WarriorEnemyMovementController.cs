using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarriorEnemyMovementController : MonoBehaviour
{
    // Config
    [SerializeField] float enemySpeed = 1;
    [SerializeField] float timeBetweenSteps;
    [SerializeField] float timeToMakeStep;

    // State
    private bool isMoving;

    // Cached component references
    Rigidbody2D enemyRigidBody;
    SpriteRenderer enemySprite;
    Animator enemyAnimator;

    // Initialize variables  
    float timeBetweenStepsCounter;
    float timeToMakeStepCounter;
    Vector2 directionToMakeStep;

    // String const
    private const string IS_MOVING = "isMoving";
    private const string IS_ATTACKING = "isAttacking";
    private const string IS_RUNNING = "isRunning";

    // Start is called before the first frame update
    void Start()
    {
        enemyRigidBody = GetComponent<Rigidbody2D>();
        enemyAnimator = GetComponent<Animator>();
        enemySprite = GetComponent<SpriteRenderer>();

        timeBetweenStepsCounter = timeBetweenSteps * Random.Range(0.5f, 1.5f);
        timeToMakeStepCounter = timeToMakeStep * Random.Range(0.5f, 1.5f);
    }

    // Update is called once per frame
    void Update()
    {
        WalkingMovement();         
    }

    private void WalkingMovement()
    {
        if (!enemyAnimator.GetBool(IS_ATTACKING) && !enemyAnimator.GetBool(IS_RUNNING))
        {
            enemyRigidBody.constraints = RigidbodyConstraints2D.None;
            enemyRigidBody.constraints = RigidbodyConstraints2D.FreezeRotation;
            enemyAnimator.SetBool(IS_MOVING, isMoving);
            if (isMoving)
            {
                timeToMakeStepCounter -= Time.deltaTime;

                if (directionToMakeStep.x < 0)
                    enemySprite.flipX = true;
                else
                    enemySprite.flipX = false;

                enemyRigidBody.velocity = directionToMakeStep;

                if (timeToMakeStepCounter < 0)
                {
                    isMoving = false;
                    timeBetweenStepsCounter = timeBetweenSteps;
                    enemyRigidBody.velocity = Vector2.zero;
                }
            }
            else
            {
                timeBetweenStepsCounter -= Time.deltaTime;
                if (timeBetweenStepsCounter < 0)
                {
                    isMoving = true;
                    timeToMakeStepCounter = timeBetweenSteps;

                    do
                    {
                        directionToMakeStep = new Vector2(Random.Range(-1, 2), Random.Range(-1, 2)) * enemySpeed;
                    }
                    while (directionToMakeStep == Vector2.zero);
                }
            }
        }
        else
        {
            enemyRigidBody.constraints = RigidbodyConstraints2D.FreezeAll;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarriorEnemyMovementController : MonoBehaviour
{
    // Config
    [Range(2f, 3f)] [SerializeField] float enemyWalkingSpeed = 2;
    [Range(4f, 5f)] [SerializeField] float enemyRunningSpeed = 4;
    [SerializeField] float timeBetweenSteps;
    [SerializeField] float timeToMakeStep;

    // State
    bool isMoving;
    bool isRunning;
    bool isAggroed;

    // Cached component references
    Rigidbody2D enemyRigidBody;
    SpriteRenderer enemySprite;
    Animator enemyAnimator;
    GameObject player;

    // Initialize variables  
    float timeBetweenStepsCounter;
    float timeToMakeStepCounter;
    Vector2 directionToMakeStep;
    Vector2 directionToRun;

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
        player = FindObjectOfType<ArcherPlayerController>().gameObject;

        timeBetweenStepsCounter = timeBetweenSteps * Random.Range(0.5f, 1.5f);
        timeToMakeStepCounter = timeToMakeStep * Random.Range(0.5f, 1.5f);
    }

    // Update is called once per frame
    void Update()
    {
        WalkingMovement();
        RunningMovement();
        AnimationChanges();
    }

    private void AnimationChanges()
    {
        enemyAnimator.SetBool(IS_RUNNING, isRunning);
        enemyAnimator.SetBool(IS_MOVING, isMoving);
    }

    private void RunningMovement()
    {
        try
        {
            if (isAggroed && !enemyAnimator.GetBool(IS_ATTACKING))
            {
                isRunning = true;
                directionToRun = (player.transform.position - transform.position).normalized;
                enemyRigidBody.velocity = directionToRun * Time.deltaTime * enemyRunningSpeed * 25;
                if (directionToRun.x < 0)
                    enemySprite.flipX = true;
                else
                    enemySprite.flipX = false;
            }
            else if (!isAggroed)
            {
                isRunning = false;
            }
        }
        catch { isRunning = false; return; }    
    }

    private void WalkingMovement()
    {
        if (!enemyAnimator.GetBool(IS_ATTACKING) && !enemyAnimator.GetBool(IS_RUNNING))
        {
            enemyRigidBody.constraints = RigidbodyConstraints2D.None;
            enemyRigidBody.constraints = RigidbodyConstraints2D.FreezeRotation;
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
                }
            }
            else
            {
                enemyRigidBody.velocity = Vector2.zero;
                timeBetweenStepsCounter -= Time.deltaTime;
                if (timeBetweenStepsCounter < 0)
                {
                    isMoving = true;
                    timeToMakeStepCounter = timeBetweenSteps;

                    do
                    {
                        directionToMakeStep = new Vector2(Random.Range(-1, 2), Random.Range(-1, 2)) * enemyWalkingSpeed;
                    }
                    while (directionToMakeStep == Vector2.zero);
                }
            }
        }
        else if(!enemyAnimator.GetBool(IS_RUNNING))
        {
            enemyRigidBody.constraints = RigidbodyConstraints2D.FreezeAll;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isAggroed = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isAggroed = false;
        }
    }
}

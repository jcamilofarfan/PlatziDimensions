using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarriorEnemyMovementController : MonoBehaviour
{
    public Vector2 velocity;
    public float velocityMagnitude;
    // Config
    [Range(2f, 3f)] [SerializeField] float enemyWalkingSpeed;
    [Range(3f, 4f)] [SerializeField] float enemyRunningSpeed;
    [SerializeField] float timeBetweenSteps;
    [SerializeField] float timeToMakeStep;
    [SerializeField] GameObject enemyHealthBar;

    // State
    bool isMoving;
    bool isRunning;
    public bool isAggroed;

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
    public bool healthBarCreated;

    // String const
    private const string IS_MOVING = "isMoving";
    private const string IS_ATTACKING = "isAttacking";
    private const string IS_RUNNING = "isRunning";
    private const string WILL_DIE = "willDie";

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
        EnemyHealthBar();
        velocity = enemyRigidBody.velocity;
        velocityMagnitude = velocity.magnitude;
    }

    private void EnemyHealthBar()
    {
        if (isAggroed && !healthBarCreated)
        {
            healthBarCreated = true;
            var healthBar = (GameObject)Instantiate(enemyHealthBar, transform.position + new Vector3(0, 1.3f, 0), Quaternion.identity);
            healthBar.transform.SetParent(this.transform, true);
        }
        else if (!isAggroed && healthBarCreated)
        {
            Destroy(GetComponentInChildren<EnemyHealthBar>().gameObject);
            healthBarCreated = false;
        }
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
            if (isAggroed && !enemyAnimator.GetBool(IS_ATTACKING) && !enemyAnimator.GetBool(WILL_DIE))
            {
                isRunning = true;
                directionToRun = player.transform.position - transform.position;
                enemyRigidBody.velocity = directionToRun.normalized * enemyRunningSpeed;
                if (directionToRun.x < 0)
                    enemySprite.flipX = true;
                else
                    enemySprite.flipX = false;
            }
            else if (!isAggroed || enemyAnimator.GetBool(WILL_DIE))
            {
                isRunning = false;
            }
        }
        catch { isRunning = false; return; }    
    }

    private void WalkingMovement()
    {
        if (!enemyAnimator.GetBool(IS_ATTACKING) && !enemyAnimator.GetBool(IS_RUNNING) && !enemyAnimator.GetBool(WILL_DIE))
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

                enemyRigidBody.velocity = directionToMakeStep.normalized * enemyWalkingSpeed;

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
                        directionToMakeStep = new Vector2(Random.Range(-1, 2), Random.Range(-1, 2));
                    }
                    while (directionToMakeStep == Vector2.zero);
                }
            }
        }
        else if(!enemyAnimator.GetBool(IS_RUNNING) || enemyAnimator.GetBool(WILL_DIE))
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

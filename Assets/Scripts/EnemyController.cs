using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float visionRadius = 4f;
    public float distancePlayer;

    Transform player;
    Vector2 target;
    public CharacterStats stats;

    public float enemySpeed = 1f;
    private bool walking = false;
    private Rigidbody2D enemyRigidbody;
    private bool isMoving;
    private bool attack = false;
    public float timeBetweenSteps;
    private float timeBetweenStepsCounter;
    public float timeToMakeStep;
    private float timeToMakeStepCounter;
    public Vector2 directionToMakeStep;
    public Vector2 lastMovement = Vector2.zero;

    private Animator enemyAnimator;
    private const string horizontal = "Horizontal";
    private const string vertical = "Vertical";
    private const string keyAttack = "Jump";
    private const string attackState = "Attack";
    private const string lastHorizontal = "LastHorizontal";
    private const string lastVertical = "LastVertical";
    private const string walkingState = "Walking";
    private const string idealState = "Idel";
    void Start()
    {
        stats = GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterStats>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        enemyRigidbody = GetComponent<Rigidbody2D>();
        enemyAnimator = GetComponent<Animator>();
        timeBetweenStepsCounter = timeBetweenSteps * Random.Range(0.5f, 1.5f);
        timeToMakeStepCounter = timeToMakeStep * Random.Range(0.5f, 1.5f);
    }

    // Update is called once per frame
    void Update()
    {
        float totalSpeed = enemySpeed;
        if(stats!= null)
        {
            float speedMultiplier= stats.speedLevelsEnemy[stats.currentLevel]; 
            totalSpeed =stats.speedLevelsEnemy[stats.currentLevel];
        }
        else
        {
            stats = GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterStats>();
            totalSpeed *=stats.speedLevelsEnemy[stats.currentLevel];
        }
        distancePlayer = Vector2.Distance(player.position, transform.position);
        attack = false;
        if (distancePlayer < visionRadius)
        {
            walking = true;
            attack = true;
            Vector2 directionToMakeStep = player.position - transform.position;
            transform.position += (Vector3)directionToMakeStep * Time.deltaTime * totalSpeed;
            lastMovement = directionToMakeStep;
        }
        else
        {
            attack = false;
            if (isMoving)
            {
                timeToMakeStepCounter -= Time.deltaTime;
                enemyRigidbody.velocity = directionToMakeStep;
                if (timeToMakeStepCounter < 0)
                {
                    isMoving = false;
                    timeBetweenStepsCounter = timeToMakeStep;
                    enemyRigidbody.velocity = Vector2.zero;
                    walking = false;
                }
            }
            else
            {
                timeBetweenStepsCounter -= Time.deltaTime;
                if (timeBetweenStepsCounter < 0)
                {
                    isMoving = true;
                    timeToMakeStepCounter = timeToMakeStep;
                    directionToMakeStep = new Vector2(Random.Range(-1, 2), Random.Range(-1, 2)) * totalSpeed;
                    walking = true;
                    lastMovement = directionToMakeStep;
                }
            }
        }
        //Debug.DrawLine(transform.position, target, Color.green);
        enemyAnimator.SetFloat(lastHorizontal, lastMovement.x);
        enemyAnimator.SetFloat(lastVertical, lastMovement.y);
        enemyAnimator.SetBool(walkingState, walking);
        enemyAnimator.SetBool(attackState, attack);
        enemyAnimator.SetFloat(horizontal, directionToMakeStep.x);
        enemyAnimator.SetFloat(vertical, directionToMakeStep.y);
    }
    void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, visionRadius);
    }
}
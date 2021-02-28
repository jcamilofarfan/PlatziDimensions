using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamagePlayer : MonoBehaviour
{
    // Config
    [SerializeField] GameObject damageNumber;
    public int damage = 10;

    // Cached component references
    GameObject player;
    Animator playerAnimator, enemyAnimator;
    HealthManager playerHealth;
    SpriteRenderer enemySprite;

    // Initialize variables
    float attackingDistance = 0;
    float currentDistance;
    Vector2 enemyPosition;
    Vector2 playerPosition;

    // String const
    private const string PLAYER_HIT = "playerHit";
    private const string IS_ATTACKING = "isAttacking";

    // Start is called before the first frame update
    private void Start()
    {
        try
        {
            player = FindObjectOfType<ArcherPlayerController>().gameObject;
            playerAnimator = player.GetComponent<Animator>();
            playerHealth = player.GetComponent<HealthManager>();
            enemySprite = GetComponent<SpriteRenderer>();

            if (gameObject.CompareTag("Enemy")) { enemyAnimator = GetComponent<Animator>(); }
        }
        catch{ return; }
    }

    private void Update()
    {      
        StopAttacking();
    }

    private void OnTriggerEnter2D(Collider2D otherCollider) //This only applies for the thrown weapons
    {
        if (otherCollider.tag == "Player")
        {
            playerAnimator.SetTrigger(PLAYER_HIT);

            playerHealth.DealDamage(damage);
            var clone = (GameObject)Instantiate(damageNumber, otherCollider.transform.position + new Vector3(2.5f,0.7f,0), Quaternion.identity);
            clone.GetComponent<DamageNumber>().damagePoints = damage;
            
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            enemyAnimator.SetBool(IS_ATTACKING, true);
            enemyPosition = new Vector2(transform.position.x, transform.position.y);
            playerPosition = new Vector2(player.transform.position.x, player.transform.position.y);
            
            if (enemyPosition.x > playerPosition.x)
                enemySprite.flipX = true;
            else
                enemySprite.flipX = false;

            attackingDistance = Vector2.Distance(enemyPosition, playerPosition);
        }
    }

    private void StopAttacking()
    {
        if (gameObject.CompareTag("Enemy"))
        {
            if (enemyAnimator.GetBool(IS_ATTACKING))
            {
                enemyPosition = new Vector2(transform.position.x, transform.position.y);
                playerPosition = new Vector2(player.transform.position.x, player.transform.position.y);
                currentDistance = Vector2.Distance(enemyPosition, playerPosition);

                if (currentDistance > attackingDistance * 2f)
                {
                    enemyAnimator.SetBool(IS_ATTACKING, false);
                }

                if (!player.GetComponent<ArcherPlayerController>().isAlive)
                {
                    enemyAnimator.SetBool(IS_ATTACKING, false);
                }
            }
        }
    }

    public void DealDamageToPlayer()
    {
        playerHealth.DealDamage(damage);
        playerAnimator.SetTrigger(PLAYER_HIT);
        var clone = (GameObject)Instantiate(damageNumber, player.transform.position + new Vector3(2.5f, 0.7f, 0), Quaternion.identity);
        clone.GetComponent<DamageNumber>().damagePoints = damage;
    }
}

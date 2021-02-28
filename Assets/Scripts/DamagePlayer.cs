using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamagePlayer : MonoBehaviour
{
    // Config
    [SerializeField] GameObject damageNumber;
    public int damage = 10;

    // String const
    private const string PLAYER_HIT = "playerHit";
    private const string ATTACK = "isAttacking";

    // Initialize variables
    GameObject player;
    Animator playerAnimator, enemyAnimator;
    HealthManager health;
    float attackingDistance = 0;
    float currentDistance;
    Vector2 enemyPosition;
    Vector2 playerPosition;

    private void Start()
    {
        player = FindObjectOfType<ArcherController>().gameObject;
        playerAnimator = player.GetComponent<Animator>();
        health = player.GetComponent<HealthManager>();
        if (gameObject.CompareTag("Enemy")) { enemyAnimator = GetComponent<Animator>(); }
    }

    private void Update()
    {
        OutOfRange();
    }

    private void OnTriggerEnter2D(Collider2D otherCollider) //This only applies for the thrown weapons
    {
        if (otherCollider.tag == "Player")
        {
            playerAnimator.SetTrigger(PLAYER_HIT);

            health.DealDamage(damage);
            var clone = (GameObject)Instantiate(damageNumber, otherCollider.transform.position + new Vector3(2.5f,0.7f,0), Quaternion.identity);
            clone.GetComponent<DamageNumber>().damagePoints = damage;
            
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            enemyAnimator.SetBool(ATTACK, true);
            enemyPosition = new Vector2(transform.position.x, transform.position.y);
            playerPosition = new Vector2(player.transform.position.x, player.transform.position.y);
            attackingDistance = Vector2.Distance(enemyPosition, playerPosition);
        }
    }

    private void OutOfRange()
    {
        if (gameObject.CompareTag("Enemy"))
        {
            if (enemyAnimator.GetBool(ATTACK))
            {
                enemyPosition = new Vector2(transform.position.x, transform.position.y);
                playerPosition = new Vector2(player.transform.position.x, player.transform.position.y);
                currentDistance = Vector2.Distance(enemyPosition, playerPosition);

                if (currentDistance > attackingDistance * 2f)
                {
                    enemyAnimator.SetBool(ATTACK, false);
                    Debug.Log(currentDistance);
                    Debug.Log(attackingDistance);
                }
            }
        }
    }

    public void DealDamageToPlayer()
    {
        health.DealDamage(damage);
        playerAnimator.SetTrigger(PLAYER_HIT);
        var clone = (GameObject)Instantiate(damageNumber, player.transform.position + new Vector3(2.5f, 0.7f, 0), Quaternion.identity);
        clone.GetComponent<DamageNumber>().damagePoints = damage;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    // Config
    [Range(10f, 15f)] [SerializeField] float currentSpeed = 10f;
    [SerializeField] int damage = 10;
    [SerializeField] bool freezer = false;
    [SerializeField] GameObject damageNumber;

    // Initialize variables
    int damageDone;

    // Start is called before the first frame update
    private void Start()
    {
        Destroy(gameObject, 3f);
    }
    // Update is called once per frame
    void Update()
    {
        transform.position += transform.right * Time.deltaTime * currentSpeed;
    }

    private void OnTriggerEnter2D(Collider2D otherCollider)
    {
        try
        {
            var enemyHealth = otherCollider.GetComponent<HealthManager>();
            //var enemy = otherCollider.GetComponent<Enemy>();

            if (otherCollider.CompareTag("Enemy") && !otherCollider.isTrigger)
            {
                damageDone = Random.Range(damage - 2, damage + 3);
                enemyHealth.DealDamage(damageDone);       
                var clone = (GameObject)Instantiate(damageNumber, otherCollider.transform.position + new Vector3(2.5f, 0.7f, 0), Quaternion.identity);
                clone.GetComponent<DamageNumber>().damagePoints = damageDone;
                otherCollider.GetComponent<WarriorEnemyMovementController>().isAggroed = true;
                Destroy(gameObject);
                //if (freezer)
                //    Freeze(attacker);
            }

            if (otherCollider.CompareTag("ForeGround"))
            {
                Destroy(gameObject);
            }
        }
        catch { return; }
    }
}

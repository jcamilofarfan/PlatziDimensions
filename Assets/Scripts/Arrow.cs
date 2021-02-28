using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    // Config
    [Range(10f, 15f)] [SerializeField] float currentSpeed = 10f;
    [Range(0f, 100f)] [SerializeField] int critChance = 20;
    [SerializeField] int damage = 10;
    [SerializeField] bool freezer = false;
    [SerializeField] GameObject damageNumber;
    [SerializeField] GameObject damageNumberCrit;

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
                damageDone = Random.Range((int)((float)damage*0.8f), (int)((float)damage*1.2f));

                if (Random.Range(1, 101) <= critChance)
                {
                    damageDone *= 2;
                    enemyHealth.DealDamage(damageDone);
                    var cloneCrit = (GameObject)Instantiate(damageNumberCrit, otherCollider.transform.position + new Vector3(2.5f, 0.7f, 0), Quaternion.identity);
                    cloneCrit.GetComponent<DamageNumber>().damagePoints = damageDone;
                    otherCollider.GetComponent<WarriorEnemyMovementController>().isAggroed = true;
                    Destroy(gameObject);
                    return;
                }

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

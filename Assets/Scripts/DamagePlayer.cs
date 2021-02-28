using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamagePlayer : MonoBehaviour
{
    // Config
    [SerializeField] GameObject damageNumber;
    public int damage = 10;

    // String const
    private const string THROWN_WEAPON = "ThrownWeapon";

    private void OnTriggerEnter2D(Collider2D otherCollider)
    {
        var health = otherCollider.GetComponent<HealthManager>();
        if (otherCollider.tag == "Player")
        {
            Animator otherAnim = otherCollider.GetComponent<Animator>();
            otherAnim.SetTrigger("playerHit");

            health.DealDamage(damage);
            var clone = (GameObject)Instantiate(damageNumber, otherCollider.transform.position + new Vector3(2.5f,0.7f,0), Quaternion.identity);
            clone.GetComponent<DamageNumber>().damagePoints = damage;
            
            if (gameObject.CompareTag(THROWN_WEAPON))
            {
                Destroy(gameObject);
            }
        }
    }
}

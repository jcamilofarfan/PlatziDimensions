using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponDamage : MonoBehaviour
{
    public int damage;
    public GameObject hurtAnimation;
    //public GameObject hitPoint;
    public GameObject damageNumber;

    public CharacterStats stats;
    private void Start()
    {
        stats = transform.parent.GetComponent<CharacterStats>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("Enemy"))
        {
            damage = Random.Range(10, 20);
            int totalDamage = damage;
            if (stats != null)
            {
                totalDamage += stats.strengthLevels[stats.currentLevel]- stats.defenseLevelsEnemy[stats.currentLevel];
            }
            Debug.Log(damage);
            Debug.Log(totalDamage);
            collision.gameObject.GetComponent<HealthManager>().DamageCharacter(totalDamage);
            var cloneBlod = (GameObject)Instantiate(hurtAnimation, collision.transform.position, collision.transform.rotation);
            var clone = (GameObject)Instantiate(damageNumber, gameObject.transform.position, Quaternion.Euler(Vector3.zero));
            clone.GetComponent<DamageNumber>().damagePoints = totalDamage;

        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamagePlayer : MonoBehaviour
{
    public int damage;
    public GameObject hurtAnimation;
    public GameObject damageNumber;

    public CharacterStats stats;
    private void Start()
    {
        stats = GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterStats>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("Player"))
        {
            damage = Random.Range(1, 15);
            int totalDamage = damage;
            if (stats != null)
            {
                totalDamage += stats.strengthLevelsEnemy[stats.currentLevel] - stats.defenseLevelsEnemy[stats.currentLevel];
            }
            collision.gameObject.GetComponent<HealthManager>().DamageCharacter(totalDamage);
            var cloneBlod = (GameObject)Instantiate(hurtAnimation, collision.transform.position, collision.transform.rotation);
            var clone = (GameObject)Instantiate(damageNumber, gameObject.transform.position, Quaternion.Euler(Vector3.zero));
            clone.GetComponent<DamageNumber>().damagePoints = totalDamage;
        }
    }
}

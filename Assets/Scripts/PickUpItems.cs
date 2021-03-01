using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PickUpItems : MonoBehaviour
{
    // Config
    [SerializeField] int healedAmountPotion;
    [SerializeField] int value;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (gameObject.CompareTag("Potion"))
            {
                HealthManager health = collision.GetComponent<HealthManager>();
                if (health.GetCurrentHealth() < health.GetMaxHealth())
                {
                    health.HealCharacter(healedAmountPotion);
                    Destroy(gameObject);
                }
                else
                    return;
            }
            if (gameObject.CompareTag("Coin"))
            {
                CoinManager.sharedInstance.AddMoney(value);
                Destroy(gameObject);
            }
        }
    }
}

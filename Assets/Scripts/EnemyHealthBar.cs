using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthBar : MonoBehaviour
{
    // Cached component references
    Slider enemyHealthBar;
    HealthManager enemyHealthManager;

    // Start is called before the first frame update
    void Start()
    {
        enemyHealthBar = GetComponent<Slider>();
        enemyHealthManager = GetComponentInParent<HealthManager>();
    }

    // Update is called once per frame
    void Update()
    {
        try
        {
            enemyHealthBar.maxValue = enemyHealthManager.GetMaxHealth();
            enemyHealthBar.value = enemyHealthManager.GetCurrentHealth();
        }
        catch { return; }
    }
}

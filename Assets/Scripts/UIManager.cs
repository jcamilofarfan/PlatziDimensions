using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] Slider playerHealthBar;
    [SerializeField] Text playerHealthText;
    [SerializeField] HealthManager playerHealthManager;

    // Update is called once per frame
    void Update()
    {
        HealthBar();
    }

    private void HealthBar()
    {
        playerHealthBar.maxValue = playerHealthManager.GetMaxHealth();
        playerHealthBar.value = playerHealthManager.GetCurrentHealth();

        playerHealthText.text = "HP: " + playerHealthBar.value + "/" + playerHealthBar.maxValue;
    }
}

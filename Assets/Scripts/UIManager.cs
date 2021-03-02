using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    // Config
    [SerializeField] Slider playerHealthBar;
    [SerializeField] Text playerHealthText;
    [SerializeField] HealthManager playerHealthManager;
    [SerializeField] Text coins;

    // Update is called once per frame
    void Update()
    {
        PlayerHealthBar();
    }

    private void PlayerHealthBar()
    {
        playerHealthBar.maxValue = playerHealthManager.GetMaxHealth();
        playerHealthBar.value = playerHealthManager.GetCurrentHealth();

        playerHealthText.text = "HP: " + playerHealthBar.value + "/" + playerHealthBar.maxValue;
    }
}

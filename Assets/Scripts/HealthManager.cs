using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthManager : MonoBehaviour
{

    public int maxHealth;
    public int currentHealth;

    public bool flashActive;
    public float flashLenght;
    private float flashCounter;
    public int expWhenDefeated;
    private SpriteRenderer characterRenderer;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        characterRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (currentHealth <= 0)
        {
            if (gameObject.tag.Equals("Enemy"))
            {
                Destroy(gameObject);
                GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterStats>().AddExperience(expWhenDefeated);
            }
            else
            {
                gameObject.SetActive(false);
                GameManager.shareInstance.currentGameState = GameState.playerDeath;
            }

            
        }
        if (flashActive)
        {
            flashCounter -= Time.deltaTime;
            if (flashCounter> flashLenght * 0.66f)
            {
                ToggleColor(false);
            }else if (flashCounter > flashLenght * 0.33f)
            {
                ToggleColor(true);
            }else if (flashCounter > 0)
            {
                ToggleColor(false);
            }
            else
            {
                ToggleColor(true);
                flashActive = false;
            }
        }
    }

    public void DamageCharacter(int damage)
    {
        currentHealth -= damage;
        if (flashLenght > 0)
        {
            flashActive = true;
            flashCounter = flashLenght;
        }
    }

    public void UpdateMaxHealth(int newMaxHelath)
    {
        maxHealth = newMaxHelath;
        currentHealth = maxHealth;
    }

    private void ToggleColor(bool visible)
    {
        characterRenderer.color = new Color(
            characterRenderer.color.r,
            characterRenderer.color.g,
            characterRenderer.color.b,
            (visible ? 1.0f : 0.0f));
    }


}

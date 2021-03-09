using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStats : MonoBehaviour
{
    GameManager gamemanager;
    public int currentLevel;
    public int currentExp;
    public int[] expToLevelUp, hpLevels, strengthLevels, defenseLevels;
    public float[] speedLevels;
    public int[] expToLevelUpEnemy, hpLevelsEnemy, strengthLevelsEnemy, defenseLevelsEnemy;
    public float[] speedLevelsEnemy;

    public HealthManager healthManager;
    // Start is called before the first frame update
    void Start()
    {
        gamemanager = GameObject.Find("Game Manager").GetComponent<GameManager>();
        healthManager = GetComponent<HealthManager>();
        currentLevel = gamemanager.levelPlayer;
    }

    // Update is called once per frame
    void Update()
    {
        if(currentLevel >= expToLevelUp.Length)
        {
            return;
        }
        if (currentExp>= expToLevelUp[currentLevel])
        {
            currentLevel++;
            gamemanager.CurrentLevel(currentLevel);
            healthManager.UpdateMaxHealth(hpLevels[currentLevel]);
        }
    }
    public void AddExperience(int exp)
    {
        currentExp += exp;
        gamemanager.CurrentExp(currentExp);
    }
}

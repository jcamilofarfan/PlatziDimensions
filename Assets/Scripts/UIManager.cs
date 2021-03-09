using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Text;

public class UIManager : MonoBehaviour
{
    public Text textLevelPlayer;
    public Text textExpPlayer;
    public Text textRooms;
    public float levelPlayer;
    public float expPlayer;
    public float roomPlayer;
    public Image healthImage;
    public Sprite HealtMax, HealthMaxMiddle, HealthMiddle, HealthMin;
    private float playerHealtCurrent;
    public HealthManager playerHealthManager;
    void Start()
    {
        playerHealthManager = GameObject.FindGameObjectWithTag("Player").GetComponent<HealthManager>();
    }
    void Update()
    {
        roomPlayer = GameManager.shareInstance.rooms;
        levelPlayer = GameManager.shareInstance.levelPlayer;
        expPlayer = GameManager.shareInstance.expPlayer;
        textLevelPlayer.text= levelPlayer.ToString();
        textExpPlayer.text= expPlayer.ToString();
        textRooms.text = roomPlayer.ToString();
        if (playerHealthManager != null)
        {
            Debug.Log("Bien");
        }
        else
        {
            playerHealthManager = GameObject.FindGameObjectWithTag("Player").GetComponent<HealthManager>();
          //  followTarget = target.target;
        }
        
        if(playerHealthManager.currentHealth >= playerHealthManager.maxHealth)
        {
            healthImage.GetComponent<Image>().sprite = HealtMax;
        }
        else if (playerHealthManager.currentHealth >= playerHealthManager.maxHealth*75/100)
        {
            healthImage.GetComponent<Image>().sprite = HealthMaxMiddle;
        }else if (playerHealthManager.currentHealth >= playerHealthManager.maxHealth * 50 / 100)
        {
            healthImage.GetComponent<Image>().sprite = HealthMiddle;
        }else if (playerHealthManager.currentHealth >= playerHealthManager.maxHealth * 25 / 100)
        {
            healthImage.GetComponent<Image>().sprite = HealthMin;
        }
        //playerHealthManager.currentHealth;
        //playerHealthManager.maxHealth
    }
}

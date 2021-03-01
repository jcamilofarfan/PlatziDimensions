using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoinManager : MonoBehaviour
{
    // Config
    public static CoinManager sharedInstance;
    [SerializeField] Text moneyText;
    [SerializeField] bool restartCoins;

    // Initialize variables
    int currentGold;

    // String const
    private const string goldkey = "CurrentGold";

    private void Awake()
    {
        if (sharedInstance == null)
            sharedInstance = this;
        else
            Destroy(gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        if (PlayerPrefs.HasKey(goldkey))
        {
            currentGold = PlayerPrefs.GetInt(goldkey);
        }
        else
        {
            currentGold = 0;
            PlayerPrefs.SetInt(goldkey, 0);
        }
        moneyText.text = currentGold.ToString();

        if (restartCoins)
            RestartCoins();
    }

    public void AddMoney(int moneyCollected)
    {
        currentGold += moneyCollected;
        PlayerPrefs.SetInt(goldkey, currentGold);
        moneyText.text = currentGold.ToString();
    }

    public void RestartCoins() 
    { 
        PlayerPrefs.SetInt(goldkey, 0);
        currentGold = PlayerPrefs.GetInt(goldkey);
        moneyText.text = currentGold.ToString(); 
    }
}

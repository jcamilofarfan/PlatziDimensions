using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeWall : MonoBehaviour
{
    public Sprite backgroundOne, backgroundTwo, backgroundThree, backgroundFour;
    public GameManager numberBackground;
    void Start()
    {
        numberBackground = GameObject.Find("Game Manager").GetComponent<GameManager>();
        Invoke("changeBack", 0f);
    }

    void Update()
    {
        
    }

    void changeBack()
    {
        if (numberBackground.numberBackGround == 1)
        {
            this.gameObject.GetComponent<SpriteRenderer>().sprite = backgroundOne;
        }
        if (numberBackground.numberBackGround == 2)
        {
            this.gameObject.GetComponent<SpriteRenderer>().sprite = backgroundTwo;
        }
        if (numberBackground.numberBackGround == 3)
        {
            this.gameObject.GetComponent<SpriteRenderer>().sprite = backgroundThree;
        }
        if (numberBackground.numberBackGround == 4)
        {
            this.gameObject.GetComponent<SpriteRenderer>().sprite = backgroundFour;
        }

    }
}

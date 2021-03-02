using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeWall : MonoBehaviour
{
    public Image wall;
    public int number;
    // Start is called before the first frame update
    void Start()
    {
        wall = GameObject.Find("wall").GetComponent<Image>();

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void changeBack()
    {
        if (number == 1)
        {
            wall.sprite = Resources.Load<Sprite>("Sprites/Fondo 1");
        }
        if (number == 2)
        {
            wall.sprite = Resources.Load<Sprite>("Sprites/Fondo 2");
        }
        if (number == 3)
        {
            wall.sprite = Resources.Load<Sprite>("Sprites/Fondo 3");
        }
        if (number == 4)
        {
            wall.sprite = Resources.Load<Sprite>("Sprites/Fondo 5");
        }
        if (number == 6)
        {
            wall.sprite = Resources.Load<Sprite>("Sprites/Fondo 6");
        }
    }
}

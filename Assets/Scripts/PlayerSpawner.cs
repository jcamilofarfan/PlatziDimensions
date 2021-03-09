using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawner : MonoBehaviour
{
    public GameObject cameraPlayer;
    public GameObject[] Human;
    public GameObject[] Golem;
    Vector3 startPosition;
    public GameManager numberPlayer;
    private int randHuman, randGolem;
    void Start()
    {
        numberPlayer = GameObject.Find("Game Manager").GetComponent<GameManager>();
        startPosition = this.transform.position;
        Invoke("SpawnPlayer", 0.1f);
    }
    void SpawnPlayer()
    {
        if (numberPlayer.numberplayer== 1)
        {
            randHuman = Random.Range(0, Human.Length);
            GameObject newChildOne = (GameObject)Instantiate(Human[randHuman], startPosition, Quaternion.identity);
            //GameManager.shareInstance.players.Add(newChildOne);
        }
        else if (numberPlayer.numberplayer == 2)
        {
            randGolem = Random.Range(0, Golem.Length);
            GameObject newChildOne = (GameObject)Instantiate(Golem[randGolem], startPosition, Quaternion.identity);
            //GameManager.shareInstance.players.Add(newChildOne);
        }

    }
}

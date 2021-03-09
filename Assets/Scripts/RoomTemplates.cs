using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomTemplates : MonoBehaviour {
	public GameManager numberPlayer;
	public GameObject[] bottomRooms;
	public GameObject[] topRooms;
	public GameObject[] leftRooms;
	public GameObject[] rightRooms;
	RoomsCreate setParentChild;

	public GameObject RoomEntry;

	public List<GameObject> rooms;
	public List<GameObject> enemys;

	public float waitTime;
	private bool spawnedBoss;
	public GameObject portal;
	public GameObject coin;
	public GameObject[] enemyGolem;
	public GameObject[] enemyHuman;
	public int numbercoin;
	public int numberenemy;
	private int enemy;
	Vector3 startPosition;
	public Transform enemyPosition;


	void Start()
    {
		setParentChild = GameObject.FindGameObjectWithTag("RoomsCreate").GetComponent<RoomsCreate>();
		numberPlayer = GameObject.Find("Game Manager").GetComponent<GameManager>();
		startPosition = this.transform.position;
		Instantiate(RoomEntry, startPosition, Quaternion.identity);
	}

	void Update(){
		if (GameManager.shareInstance.currentGameState == GameState.pause || GameManager.shareInstance.currentGameState == GameState.change|| GameManager.shareInstance.currentGameState == GameState.playerDeath)
		{
			for (int i = 0; i < enemys.Count; i++)
            {
				enemys[i].SetActive(false);
			}
		}
		else
		{
			for (int i = 0; i < enemys.Count; i++)
			{
				enemys[i].SetActive(true);
			}
		}


		if (waitTime <= 0 && spawnedBoss == false){
			for (int i = 0; i < rooms.Count; i++) {
				setParentChild.SetParentChild(rooms[i]);
				numberenemy = Random.Range(0, 4);
				if (numberPlayer.numberplayer == 1)
				{
					for (int b = 0; b < numberenemy; b++)
					{
						enemy = Random.Range(0, enemyGolem.Length);
						GameObject newChild = (GameObject) Instantiate(enemyGolem[enemy], rooms[i].transform.position, Quaternion.identity);
						setParentChild.SetParentChild(newChild);
						enemys.Add(newChild);
					}

				}
				else if (numberPlayer.numberplayer == 2)
				{
					for (int b = 0; b < numberenemy; b++)
					{
						enemy = Random.Range(0, enemyHuman.Length);
						GameObject newChild = (GameObject) Instantiate(enemyHuman[enemy], rooms[i].transform.position, Quaternion.identity);
						setParentChild.SetParentChild(newChild);
						enemys.Add(newChild);
					}
				}
				if (i == rooms.Count-1){
					GameObject newChild = (GameObject) Instantiate(portal, rooms[i].transform.position, Quaternion.identity);
					setParentChild.SetParentChild(newChild);
					numbercoin = Random.Range(rooms.Count - 3, rooms.Count-1);
					for (int e = 1; e< numbercoin; e++)
                    {
						//Instantiate(coin, rooms[e].transform.position, Quaternion.identity);
						e++;
                    }
					spawnedBoss = true;
				}
			}
		} else {
			waitTime -= Time.deltaTime;
		}

		
	}
}

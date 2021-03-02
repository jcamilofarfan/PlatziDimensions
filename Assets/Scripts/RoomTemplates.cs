using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomTemplates : MonoBehaviour {

	public GameObject[] bottomRooms;
	public GameObject[] topRooms;
	public GameObject[] leftRooms;
	public GameObject[] rightRooms;

	public GameObject closedRoom;

	public List<GameObject> rooms;

	public float waitTime;
	private bool spawnedBoss;
	public GameObject boss;
	public GameObject coin;

	void Update(){

		if(waitTime <= 0 && spawnedBoss == false){
			for (int i = 0; i < rooms.Count; i++) {
				if(i == rooms.Count-1){
					int rand = Random.Range(rooms.Count - 3, rooms.Count-1);
					for (int e = 1; e< rand; e++)
                    {
						Instantiate(coin, rooms[e].transform.position, Quaternion.identity);
						e++;
                    }
					Instantiate(boss, rooms[i].transform.position, Quaternion.identity);
					Instantiate(boss, rooms[i/2].transform.position, Quaternion.identity);
					spawnedBoss = true;
				}
			}
		} else {
			waitTime -= Time.deltaTime;
		}
	}
}

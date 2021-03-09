using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomSpawner : MonoBehaviour {
	public int openingDirection;
	private RoomTemplates templates;
	private int rand;
	public bool spawned = false;
	public float waitTime = 4f;
    void Awake()
    {
		templates = GameObject.FindGameObjectWithTag("Rooms").GetComponent<RoomTemplates>();
	}
    void Start()
	{
		Destroy(gameObject, waitTime);
		Invoke("Spawn", 0.1f);
	}
	void Spawn(){
		if(spawned == false){
			spawned = true;
			if (openingDirection == 1){
				// Need to spawn a room with a BOTTOM door.
				rand = Random.Range(0, templates.bottomRooms.Length);
				Instantiate(templates.bottomRooms[rand], transform.position, Quaternion.identity);
			} else if(openingDirection == 2){
				// Need to spawn a room with a TOP door.
				rand = Random.Range(0, templates.topRooms.Length);
				Instantiate(templates.topRooms[rand], transform.position, Quaternion.identity);
			} else if(openingDirection == 3){
				// Need to spawn a room with a LEFT door.
				rand = Random.Range(0, templates.leftRooms.Length);
				Instantiate(templates.leftRooms[rand], transform.position, Quaternion.identity);
			} else if(openingDirection == 4){
				// Need to spawn a room with a RIGHT door.
				rand = Random.Range(0, templates.rightRooms.Length);
				Instantiate(templates.rightRooms[rand], transform.position, Quaternion.identity);
			}
			spawned = true;
		}
	}
	void OnTriggerEnter2D(Collider2D other)	
	{
		if(other.CompareTag("SpawnPoint")){
			if (other.GetComponent<RoomSpawner>().spawned == false && spawned == false)
			{
				Instantiate(templates.RoomEntry, transform.position, Quaternion.identity);
				Destroy(gameObject);
			} 
			spawned = true;
		}
	}
}

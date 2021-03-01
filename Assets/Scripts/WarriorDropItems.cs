using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarriorDropItems : MonoBehaviour
{
    //Config
    [SerializeField] GameObject[] DropableItems;

    // Initialize Variables
    int numberOfItemsToDrop;
    int[] values;

    private void Start()
    {
        values = new int[]{0,0,1,1,1,1,2,2,2,3};
    }

    public void DropItems()                   
    {
        numberOfItemsToDrop = values[Random.Range(0, values.Length - 1)];
        for (int i = 0; i < numberOfItemsToDrop; i++)
        {
            Vector3 SpawnLocation = transform.position + new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), 0);
            var clone = (GameObject)Instantiate(DropableItems[Random.Range(0, DropableItems.Length - 1)], SpawnLocation, Quaternion.identity);
        }
    }
}

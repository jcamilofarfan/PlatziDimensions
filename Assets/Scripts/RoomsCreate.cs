using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomsCreate : MonoBehaviour
{
    public Transform parent;
    public GameObject child;
    void Start()
    {
        parent = this.transform;
    }


    public void SetParentChild(GameObject newChild)
    {
        newChild.transform.SetParent(parent, false);
    }

}

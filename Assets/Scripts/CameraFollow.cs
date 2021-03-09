using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField]
    private GameObject followTarget;
    [SerializeField]
    private Vector3 targetPosition;
    [SerializeField]
    private float cameraSpeed = 4.0f;

    public PlayerController target;

    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    }
    
    void Update()
    {
        if(target!= null)
        {
            followTarget = target.target;
        }
        else
        {
            target = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
            followTarget = target.target;
        }
        

        targetPosition = new Vector3(followTarget.transform.position.x, 
                                    followTarget.transform.position.y,
                                    this.transform.position.z);
        this.transform.position = Vector3.Lerp(this.transform.position,
                                                targetPosition,
                                                cameraSpeed * Time.deltaTime);
            
    }
}

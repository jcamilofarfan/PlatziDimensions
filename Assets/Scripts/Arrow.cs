using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    [Range(10f, 15f)] [SerializeField] public float currentSpeed = 10f;
    [SerializeField] float damage = 50f;
    [SerializeField] bool freezer = false;

    private void Start()
    {
        Destroy(gameObject, 3f);
    }
    // Update is called once per frame
    void Update()
    {
        transform.position += transform.right * Time.deltaTime * currentSpeed;
    }

    //private void OnTriggerEnter2D(Collider2D otherCollider)
    //{
    //    var health = otherCollider.GetComponent<Health>();
    //    var enemy = otherCollider.GetComponent<Enemy>();

    //    if (otherCollider.tag == "Enemy")
    //    {
    //        health.DealDamage(damage);
    //        Destroy(gameObject);
    //        if (freezer)
    //            Freeze(attacker);
    //    }
    //}
}

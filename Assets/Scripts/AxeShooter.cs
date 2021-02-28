using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AxeShooter : MonoBehaviour
{
    [SerializeField] GameObject axe;
    bool arrowReady = true;
    GameObject arrowShot;

    // Update is called once per frame
    void Update()
    {
        if (arrowReady)
            Fire();
    }

    public void Fire()
    {
        arrowReady = false;
        arrowShot = Instantiate(axe, this.transform.position, Quaternion.identity);
        arrowShot.transform.parent = this.transform;

        StartCoroutine(WaitForNextShot());
    }

    IEnumerator WaitForNextShot()
    {
        yield return new WaitForSeconds(2f);
        arrowReady = true;
    }
}

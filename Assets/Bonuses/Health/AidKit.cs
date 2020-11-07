using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AidKit : MonoBehaviour
{
    public int HealthCount;
    private PlayerManager[] pm;

    public void OnCollisionEnter(Collision collision)
    { 
        pm = collision.transform.GetComponentsInChildren<PlayerManager>();

        foreach (var a in pm)
        {
            if (a == null)
                continue;
           

            a.AddHealth(HealthCount);
            Destroy(this.gameObject);
        }
    }
}

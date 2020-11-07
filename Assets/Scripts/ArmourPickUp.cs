using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmourPickUp : MonoBehaviour
{
    public int ArmourCount;
    private PlayerManager[] pm;
   
    public void OnCollisionEnter(Collision collision)
    {       
        pm = collision.transform.GetComponentsInChildren<PlayerManager>();

        foreach (var a in pm)
        {
            if (a == null)
                continue;
                      
            a.ChangeArmour(ArmourCount);
            Destroy(this.gameObject);
        }
    }
}

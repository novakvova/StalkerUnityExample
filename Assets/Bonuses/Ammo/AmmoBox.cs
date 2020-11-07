using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoBox : MonoBehaviour
{
    public int AmmoCount;
    private AbstractObject[] ab;

    public void OnCollisionEnter(Collision collision)
    {       
        ab = collision.transform.GetComponentsInChildren<AbstractObject>();


        foreach (var a in ab)
        {          
            if (a == null)
                continue;

           
            a.AllBulletsCount += AmmoCount;
            Destroy(this.gameObject);
        }
    } 
}
